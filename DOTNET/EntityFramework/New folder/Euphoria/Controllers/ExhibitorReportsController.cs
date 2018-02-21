using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    /// <summary>
    /// All Exhibitor Reports
    /// </summary>
    [RoutePrefix("api/v1/organizers/{organizerId}/exhibitorReports")]
    public class ExhibitorReportsController : ApiController
    {
        private readonly IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private TokenGenerationService _tokenService = new TokenGenerationService();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<ExhibitorFeedback> _exhibitorFeedbackRepository = new EntityFrameworkRepository<ExhibitorFeedback>();
        private IRepository<GeneralMaster> _generalMasterRepository = new EntityFrameworkRepository<GeneralMaster>();
        private IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private IRepository<Booking> _bookingRepository = new EntityFrameworkRepository<Booking>();
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<Venue> _venueRepository = new EntityFrameworkRepository<Venue>();
        private IRepository<ExhibitorRegistrationType> _exhibitorRegistrationTypeRepository = new EntityFrameworkRepository<ExhibitorRegistrationType>();
        private readonly IRepository<StallBooking> _stallBookingRepository = new EntityFrameworkRepository<StallBooking>();
        private readonly IRepository<ExhibitorStatus> _exhibitorStatusRepository = new EntityFrameworkRepository<ExhibitorStatus>();
        private readonly IRepository<ExhibitorEnquiry> _exhibitorEnquiryRepository = new EntityFrameworkRepository<ExhibitorEnquiry>();
        private BookingRepository<ExhibitorType> _exhibitorTypeListRepository = new BookingRepository<ExhibitorType>();
        private VisitorEventTicketRepository<VisitorCardPayment> _visitorEventTicketRepository = new VisitorEventTicketRepository<VisitorCardPayment>();
        private IRepository<VisitorCardPayment> _visitorCardPaymentRepository = new EntityFrameworkRepository<VisitorCardPayment>();

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get Event Feedback report by ReportType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        [Route("feedbackByEvent/{eventId:guid}/{reportType}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorFeedbackReport(Guid organizerId, Guid eventId, string reportType)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var generalMasterSearchCriteria = new GeneralMasterSearchCriteria { GeneralTableType = reportType };
                var generalMasterSepcification = new GeneralMasterSpecificationForSearch(generalMasterSearchCriteria);
                var generalMasterTable = _generalMasterRepository.Find(generalMasterSepcification).OrderBy(x => x.Key);
                int[] rating = { 1, 2, 3, 4, 5 };
                List<ExhibitorReportDTO> exhibitorFeedbackReportDTO = new List<ExhibitorReportDTO>();
                if (generalMasterTable.Count() != 0)
                {
                    foreach (GeneralMaster singleReport in generalMasterTable)
                    {
                        if (singleReport.Type.ToUpper() == "Objective".ToUpper())
                        {
                            ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                            var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, Objective = singleReport.Key };
                            var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                            var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                            singleExhibitorFeedbackReport.Value = singleReport.Value;
                            singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                            exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                        }
                    }
                }
                else if (reportType.ToUpper() == "Satisfaction".ToUpper())
                {
                    foreach (int satisfaction in rating)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, Satisfaction = satisfaction };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        singleExhibitorFeedbackReport.Value = satisfaction.ToString();
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "QualityofVisitor".ToUpper())
                {
                    foreach (int qualityofVisitor in rating)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, QualityofVisitor = qualityofVisitor };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        singleExhibitorFeedbackReport.Value = qualityofVisitor.ToString();
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "TargetAudience".ToUpper())
                {
                    bool[] recommendation = { true, false };
                    foreach (bool singleTargetAudience in recommendation)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, TargetAudience = singleTargetAudience };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        if (singleTargetAudience == true)
                        {
                            singleExhibitorFeedbackReport.Value = "Yes";
                        }
                        else
                        {
                            singleExhibitorFeedbackReport.Value = "No";
                        }
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "ExpectedBusiness".ToUpper())
                {
                    bool[] recommendation = { true, false };
                    foreach (bool singleExpectedBusiness in recommendation)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, ExpectedBusiness = singleExpectedBusiness };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        if (singleExpectedBusiness == true)
                        {
                            singleExhibitorFeedbackReport.Value = "Yes";
                        }
                        else
                        {
                            singleExhibitorFeedbackReport.Value = "No";
                        }
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "IIMTFSatisfaction".ToUpper())
                {
                    int[] satisfaction = { 1, 2, 3 };
                    foreach (int singleSatisfaction in satisfaction)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, IIMTFSatisfaction = singleSatisfaction };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        if (singleSatisfaction == 1)
                        {
                            singleExhibitorFeedbackReport.Value = "Satisfactory";
                        }
                        else if (singleSatisfaction == 2)
                        {
                            singleExhibitorFeedbackReport.Value = "Neither";
                        }
                        else
                        {
                            singleExhibitorFeedbackReport.Value = "Not Satisfactory";
                        }
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "IIMTFTeam".ToUpper())
                {
                    int[] iimtfTeam = { 1, 2, 3 };
                    foreach (int singleIIMTFTeam in iimtfTeam)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, IIMTFTeam = singleIIMTFTeam };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        if (singleIIMTFTeam == 1)
                        {
                            singleExhibitorFeedbackReport.Value = "Satisfactory";
                        }
                        else if (singleIIMTFTeam == 2)
                        {
                            singleExhibitorFeedbackReport.Value = "Neither";
                        }
                        else
                        {
                            singleExhibitorFeedbackReport.Value = "Not Satisfactory";
                        }
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "IIMTFFacility".ToUpper())
                {
                    int[] iimtfFacility = { 1, 2, 3 };
                    foreach (int singleIIMTFFacility in iimtfFacility)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();
                        var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, IIMTFFacility = singleIIMTFFacility };
                        var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                        var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSepcification);
                        if (singleIIMTFFacility == 1)
                        {
                            singleExhibitorFeedbackReport.Value = "Satisfactory";
                        }
                        else if (singleIIMTFFacility == 2)
                        {
                            singleExhibitorFeedbackReport.Value = "Neither";
                        }
                        else
                        {
                            singleExhibitorFeedbackReport.Value = "Not Satisfactory";
                        }
                        singleExhibitorFeedbackReport.TotalCount = exhibitorFeedbackCount;
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "MarketIntrested".ToUpper())
                {
                    var exhibitorMarketIntrested = _venueRepository.Find(new GetAllSpecification<Venue>());

                    foreach (Venue singleMarketIntrested in exhibitorMarketIntrested)
                    {
                        ExhibitorReportDTO singleExhibitorFeedbackReport = new ExhibitorReportDTO();

                        singleExhibitorFeedbackReport.Value = singleMarketIntrested.City;
                        singleExhibitorFeedbackReport.TotalCount = singleMarketIntrested.ExhibitorFeedback.Count();
                        exhibitorFeedbackReportDTO.Add(singleExhibitorFeedbackReport);
                    }
                }
                return Ok(exhibitorFeedbackReportDTO);
            }
        }

        /// <summary>
        /// Exhibitor CardPayment Report
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/exhibitorCardPaymentAnalysis/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorCardPaymentAnalysis(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                var distinctExhibitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId };
                var distinctExhibitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(distinctExhibitorCardPaymentSearchCriteria);
                var distinctExhibitorCardPaymentCount = _visitorEventTicketRepository.ExhibitorCardPaymentCount(distinctExhibitorCardPaymentSepcification, e => e.Exhibitor.Id);
                var distinctExhibitorCardPaymentList = _visitorEventTicketRepository.ExhibitorsCardPayment(distinctExhibitorCardPaymentSepcification, e => e.Exhibitor.Id)
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize);

                var totalCount = distinctExhibitorCardPaymentCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<ExhibitorCollectionAnalysisDTO> exhibitorCardPaymentAnalysisDTOList = new List<ExhibitorCollectionAnalysisDTO>();
                foreach (KeyValuePair<Guid, int> singleExhibitor in distinctExhibitorCardPaymentList)
                {
                    var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId, ExhibitorId = singleExhibitor.Key };
                    var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                    var visitorCardPaymentCount = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification).OrderBy(x => x.TransactionDate);

                    if (visitorCardPaymentCount.Count() != 0)
                    {
                        ExhibitorCollectionAnalysisDTO exhibitorCardPaymentAnalysisDTO = new ExhibitorCollectionAnalysisDTO();
                        if (visitorCardPaymentCount.FirstOrDefault().Exhibitor != null)
                        {
                            exhibitorCardPaymentAnalysisDTO.ExhibitorName = visitorCardPaymentCount.FirstOrDefault().Exhibitor.Name;
                            exhibitorCardPaymentAnalysisDTO.CompanyName = visitorCardPaymentCount.FirstOrDefault().Exhibitor.CompanyName;
                        }
                        else
                        {
                            exhibitorCardPaymentAnalysisDTO.ExhibitorName = visitorCardPaymentCount.FirstOrDefault().ExhibitorName;
                            exhibitorCardPaymentAnalysisDTO.CompanyName = visitorCardPaymentCount.FirstOrDefault().ExhibitorName;
                        }

                        exhibitorCardPaymentAnalysisDTO.TotalAmount = singleExhibitor.Value;
                        foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentCount)
                        {
                            VisitorsDTO singleVisitor = new VisitorsDTO()
                            {
                                VisitorName = singleCardPayment.Name,
                                Amount = singleCardPayment.Amount,
                                Date = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy")
                            };
                            exhibitorCardPaymentAnalysisDTO.VisitorsDTO.Add(singleVisitor);
                        }
                        exhibitorCardPaymentAnalysisDTOList.Add(exhibitorCardPaymentAnalysisDTO);
                    }
                }
                var totalCardPaymentAmountForEvent = _visitorEventTicketRepository.TotalCardPaymentOfEvent(eventDetails.Id);
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    totalAmount = totalCardPaymentAmountForEvent,
                    listOfBookingDTO = exhibitorCardPaymentAnalysisDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Exhibitor Loyalty in years
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("loyalty")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                int[] range = { 2, 4, 7, 10, 11 };
                List<ExhibitorLoyaltyDTO> exhibitorLoyaltyDTO = new List<ExhibitorLoyaltyDTO>();
                foreach (int age in range)
                {
                    ExhibitorLoyaltyDTO singleExhibitorDTO = new ExhibitorLoyaltyDTO();
                    if (age == 2)
                    {
                        var bookingSearchCriteria = new ExhibitorSearchCriteria { StartRange = 1, EndRange = age };
                        var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                        singleExhibitorDTO.Years = "< 2Years";
                        singleExhibitorDTO.Count = exhibitorCount;
                    }
                    else if (age == 4)
                    {
                        var bookingSearchCriteria = new ExhibitorSearchCriteria { StartRange = 2, EndRange = age };
                        var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                        singleExhibitorDTO.Years = "2 to 4Years";
                        singleExhibitorDTO.Count = exhibitorCount;

                    }
                    else if (age == 7)
                    {
                        var bookingSearchCriteria = new ExhibitorSearchCriteria { StartRange = 4, EndRange = age };
                        var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                        singleExhibitorDTO.Years = "4 to 7Years";
                        singleExhibitorDTO.Count = exhibitorCount;

                    }
                    else if (age == 10)
                    {
                        var bookingSearchCriteria = new ExhibitorSearchCriteria { StartRange = 7, EndRange = age };
                        var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                        singleExhibitorDTO.Years = "7 to 10years";
                        singleExhibitorDTO.Count = exhibitorCount;

                    }
                    else if (age == 11)
                    {
                        var bookingSearchCriteria = new ExhibitorSearchCriteria { StartRange = 7, EndRange = age };
                        var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                        singleExhibitorDTO.Years = "> 10years";
                        singleExhibitorDTO.Count = exhibitorCount;

                    }
                    exhibitorLoyaltyDTO.Add(singleExhibitorDTO);
                }
                return Ok(exhibitorLoyaltyDTO);
            }
        }

        /// <summary>
        /// Get Exhibitor Data by Years Of Loyalty
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        [Route("{pageSize}/{pageNumber}/{years}")]
        [ResponseType(typeof(ExhibitorDTO[]))]
        public IHttpActionResult Get(Guid organizerId, int pageSize, int pageNumber, int years)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                ExhibitorSearchCriteria criteria = new ExhibitorSearchCriteria();
                if (years == 2)
                {
                    criteria = new ExhibitorSearchCriteria { StartRange = 1, EndRange = 2 };
                }
                else if (years == 4)
                {
                    criteria = new ExhibitorSearchCriteria { StartRange = 2, EndRange = 4 };
                }
                else if (years == 7)
                {
                    criteria = new ExhibitorSearchCriteria { StartRange = 4, EndRange = 7 };
                }
                else if (years == 10)
                {
                    criteria = new ExhibitorSearchCriteria { StartRange = 7, EndRange = 10 };
                }
                else
                {
                    criteria = new ExhibitorSearchCriteria { StartRange = 10, EndRange = 11 };
                }

                var specification = new ExhibitorSpecificationForSearch(criteria);
                var exhibitorCount = _exhibitorRepository.Count(specification);
                var matchingExhibitors = _exhibitorRepository.Find(specification)
                    .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var totalCount = exhibitorCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<ExhibitorDTO> exhibitorDTO = new List<ExhibitorDTO>();

                foreach (Exhibitor x in matchingExhibitors)
                {
                    exhibitorDTO.Add(GetDTO(x));
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = exhibitorDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Exhibitor Report By ExhibitorType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitorTypeReport")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorTypeReport(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var exhibitorTypeList = _exhibitorTypeRepository.Find(new GetAllSpecification<ExhibitorType>());
                List<ExhibitorReportDTO> exhibitorReportDTO = new List<ExhibitorReportDTO>();
                foreach (ExhibitorType singleExhibitorType in exhibitorTypeList)
                {
                    ExhibitorReportDTO singleExhibitorReportDTO = new ExhibitorReportDTO();
                    var bookingSearchCriteria = new ExhibitorSearchCriteria { ExhibitorTypeId = singleExhibitorType.Id };
                    var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                    var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                    singleExhibitorReportDTO.Value = singleExhibitorType.Type;
                    singleExhibitorReportDTO.TotalCount = exhibitorCount;
                    exhibitorReportDTO.Add(singleExhibitorReportDTO);
                }
                return Ok(exhibitorReportDTO);
            }
        }

        /// <summary>
        /// Get Exhibitor Report By ExhibitorIndustryType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitorIndustryTypeReport")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorIndustryTypeReport(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var exhibitorTypeList = _exhibitorIndustryTypeRepository.Find(new GetAllSpecification<ExhibitorIndustryType>());
                List<ExhibitorReportDTO> exhibitorReportDTO = new List<ExhibitorReportDTO>();
                foreach (ExhibitorIndustryType singleExhibitorType in exhibitorTypeList)
                {
                    ExhibitorReportDTO singleExhibitorReportDTO = new ExhibitorReportDTO();
                    var bookingSearchCriteria = new ExhibitorSearchCriteria { ExhibitorIndustryTypeId = singleExhibitorType.Id };
                    var bookingSepcification = new ExhibitorSpecificationForSearch(bookingSearchCriteria);
                    var exhibitorCount = _exhibitorRepository.Count(bookingSepcification);
                    singleExhibitorReportDTO.Value = singleExhibitorType.IndustryType;
                    singleExhibitorReportDTO.TotalCount = exhibitorCount;
                    exhibitorReportDTO.Add(singleExhibitorReportDTO);
                }
                return Ok(exhibitorReportDTO);
            }
        }

        private static ExhibitorDTO GetDTO(Exhibitor exhibitor)
        {
            return new ExhibitorDTO()
            {
                Id = exhibitor.Id,
                Name = exhibitor.Name,
                EmailId = exhibitor.EmailId,
                PhoneNo = exhibitor.PhoneNo,
                CompanyName = exhibitor.CompanyName,
                Designation = exhibitor.Designation,
                CompanyDescription = exhibitor.CompanyDescription,
                Address = exhibitor.Address,
                Age = exhibitor.Age,
                PinCode = exhibitor.PinCode,
                Password = exhibitor.Password,
                //Stalls = exhibitor..Select(x => GetDTO(x)).ToList(),
                Categories = exhibitor.Categories.Select(x => GetDTO(x)).ToList(),
                Country = GetDTO(exhibitor.Country),
                State = GetDTO(exhibitor.State)
            };
        }

        private static PavilionDTO GetDTO(Pavilion pavilion)
        {
            return new PavilionDTO
            {
                Name = pavilion.Name
            };
        }

        private static CategoryDTO GetDTO(Category category)
        {
            if (category == null)
                return null;
            else
            {
                return new CategoryDTO
                {
                    Name = category.Name,
                    Description = category.Description
                };
            }
        }

        private static CountryDTO GetDTO(Country country)
        {
            if (country == null)
                return null;
            else
            {
                return new CountryDTO
                {
                    Name = country.Name
                };
            }
        }

        private static StateDTO GetDTO(State state)
        {
            if (state == null)
                return null;
            else
            {
                return new StateDTO
                {
                    Name = state.Name
                };
            }
        }
    }
}
