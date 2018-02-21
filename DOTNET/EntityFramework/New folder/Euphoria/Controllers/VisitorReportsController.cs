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
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/visitorReports")]
    public class VisitorReportsController : ApiController
    {
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private readonly IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private readonly IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private readonly IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();
        private readonly IRepository<Ticket> _ticketRepository = new EntityFrameworkRepository<Ticket>();
        private readonly IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private readonly IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private readonly IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private readonly IRepository<VisitorFeedback> _visitorFeedbackRepository = new EntityFrameworkRepository<VisitorFeedback>();
        private readonly IRepository<VisitorCardPayment> _visitorCardPaymentRepository = new EntityFrameworkRepository<VisitorCardPayment>();
        private readonly IRepository<GeneralMaster> _generalMasterRepository = new EntityFrameworkRepository<GeneralMaster>();
        private readonly IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private readonly IRepository<DiscountCoupon> _discountCouponRepository = new EntityFrameworkRepository<DiscountCoupon>();
        private readonly IRepository<VisitorBookingExhibitorDiscount> _visitorBookingExhibitorDiscountTypeRepository = new EntityFrameworkRepository<VisitorBookingExhibitorDiscount>();
        private readonly IRepository<VisitorDiscountCouponMap> _visitorDiscountCouponMapRepository = new EntityFrameworkRepository<VisitorDiscountCouponMap>();
        private readonly VisitorEventTicketRepository<VisitorCardPayment> _visitorEventTicketRepository = new VisitorEventTicketRepository<VisitorCardPayment>();

        /// <summary>
        /// Get Event Feedback Report
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        [Route("eventFeedbackReport/{eventId:guid}/{reportType}")]
        [ResponseType(typeof(VisitorFeedbackReportDTO))]
        public IHttpActionResult GetVisitorFeedbackReport(Guid organizerId, Guid eventId, string reportType)
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
                List<VisitorFeedbackReportDTO> visitorFeedbackReportDTO = new List<VisitorFeedbackReportDTO>();

                if (generalMasterTable.Count() != 0)
                {
                    foreach (GeneralMaster singleReport in generalMasterTable)
                    {
                        if (singleReport.Type.ToUpper() == "SpendRange".ToUpper())
                        {
                            VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                            var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, SpendRange = singleReport.Key };
                            var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                            var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                            singleVisitorFeedbackReport.Value = singleReport.Value;
                            singleVisitorFeedbackReport.Count = visitorFeedbackCount;
                            visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                        }
                        else if (singleReport.Type.ToUpper() == "ReasonForVisiting".ToUpper())
                        {
                            VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                            var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, ReasonForVisiting = singleReport.Key };
                            var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                            var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                            singleVisitorFeedbackReport.Value = singleReport.Value;
                            singleVisitorFeedbackReport.Count = visitorFeedbackCount;
                            visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                        }
                        else if (singleReport.Type.ToUpper() == "KnowAboutUs".ToUpper())
                        {
                            VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                            var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, KnowAboutUs = singleReport.Key };
                            var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                            var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                            singleVisitorFeedbackReport.Value = singleReport.Value;
                            singleVisitorFeedbackReport.Count = visitorFeedbackCount;
                            visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                        }
                    }
                }
                else if (reportType.ToUpper() == "eventRating".ToUpper())
                {
                    int[] rating = { 1, 2, 3, 4, 5 };
                    foreach (int singleRating in rating)
                    {
                        VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                        var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, EventRating = singleRating };
                        var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                        var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                        singleVisitorFeedbackReport.Value = singleRating.ToString();
                        singleVisitorFeedbackReport.Count = visitorFeedbackCount;
                        visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "recommendation".ToUpper())
                {
                    bool[] recommendation = { true, false };
                    foreach (bool singleRecommendation in recommendation)
                    {
                        VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                        var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, Recommendation = singleRecommendation };
                        var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                        var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                        if (singleRecommendation == true)
                        {
                            singleVisitorFeedbackReport.Value = "Yes";
                        }
                        else
                        {
                            singleVisitorFeedbackReport.Value = "No";
                        }
                        singleVisitorFeedbackReport.Count = visitorFeedbackCount;
                        visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "Product".ToUpper())
                {

                    var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId };
                    var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                    var visitorFeedbackProducts = _visitorEventTicketRepository.DistinctCategory(visitorFeedbackSepcification).OrderBy(x => x.Name);
                    if (visitorFeedbackProducts.ToList().Count() != 0)
                    {
                        foreach (Category category in visitorFeedbackProducts)
                        {
                            VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();
                            singleVisitorFeedbackReport.Value = category.Name;
                            singleVisitorFeedbackReport.Count = category.VisitorFeedback.Count();
                            visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                        }
                    }
                }
                else if (reportType.ToUpper() == "Country".ToUpper())
                {

                    var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId };
                    var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                    var visitorFeedbackCountry = _visitorEventTicketRepository.DistinctCountry(visitorFeedbackSepcification).OrderBy(x => x.Name);
                    foreach (Country countryDetails in visitorFeedbackCountry)
                    {
                        VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();

                        singleVisitorFeedbackReport.Value = countryDetails.Name;
                        singleVisitorFeedbackReport.Count = countryDetails.VisitorFeedback.Count();
                        visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                    }
                }
                else if (reportType.ToUpper() == "ExhibitorType".ToUpper())
                {

                    var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId };
                    var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                    var visitorFeedbackCountry = _visitorEventTicketRepository.DistinctExhibitorType(visitorFeedbackSepcification).OrderBy(x => x.Type);
                    foreach (ExhibitorType ExhibitorType in visitorFeedbackCountry)
                    {
                        VisitorFeedbackReportDTO singleVisitorFeedbackReport = new VisitorFeedbackReportDTO();

                        singleVisitorFeedbackReport.Value = ExhibitorType.Type;
                        singleVisitorFeedbackReport.Count = ExhibitorType.VisitorFeedback.Count();
                        visitorFeedbackReportDTO.Add(singleVisitorFeedbackReport);
                    }
                }
                return Ok(visitorFeedbackReportDTO);
            }
        }

        /// <summary>
        /// Visitor Card PaymentsList
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/visitorCardPaymentAnalysis/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorCardPaymentAnalysisDTO))]
        public IHttpActionResult GetVisitorCardPaymentAnalysis(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                var distinctVisitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId };
                var distinctvisitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(distinctVisitorCardPaymentSearchCriteria);
                var distinctvisitorCardPaymentCount = _visitorEventTicketRepository.CardPaymentCount(distinctvisitorCardPaymentSepcification, e => e.MobileNo);
                var distinctvisitorCardPaymentList = _visitorEventTicketRepository.VisitorsCardPayment(distinctvisitorCardPaymentSepcification, e => e.MobileNo)
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize);

                var totalCount = distinctvisitorCardPaymentCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<VisitorCardPaymentAnalysisDTO> visitorCardPaymentAnalysisDTOList = new List<VisitorCardPaymentAnalysisDTO>();
                foreach (KeyValuePair<string, int> singleMobileNumber in distinctvisitorCardPaymentList)
                {
                    var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId, MobileNo = singleMobileNumber.Key };
                    var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                    var visitorCardPaymentCount = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification);

                    if (visitorCardPaymentCount.Count() != 0)
                    {
                        VisitorCardPaymentAnalysisDTO visitorCardPaymentAnalysisDTO = new VisitorCardPaymentAnalysisDTO();
                        visitorCardPaymentAnalysisDTO.Name = visitorCardPaymentCount.FirstOrDefault().Name;
                        visitorCardPaymentAnalysisDTO.MobileNo = visitorCardPaymentCount.FirstOrDefault().MobileNo;
                        visitorCardPaymentAnalysisDTO.TotalAmount = singleMobileNumber.Value;
                        foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentCount)
                        {
                            ExhibitorsDTO singleExhibitor = new ExhibitorsDTO()
                            {
                                Amount = singleCardPayment.Amount,
                                Date = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy")
                            };
                            if (singleCardPayment.Exhibitor != null)
                            {
                                singleExhibitor.ExhibitorName = singleCardPayment.Exhibitor.CompanyName;
                            }
                            else
                            {
                                singleExhibitor.ExhibitorName = singleCardPayment.ExhibitorName;
                            }
                            visitorCardPaymentAnalysisDTO.ExhibitorsDTO.Add(singleExhibitor);
                        }
                        visitorCardPaymentAnalysisDTOList.Add(visitorCardPaymentAnalysisDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = visitorCardPaymentAnalysisDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Visitor Event Ticket Report
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="payAtLocation"></param>
        /// <param name="online"></param>
        /// <param name="web"></param>
        /// <param name="mobile"></param>
        /// <param name="paymentCompleted"></param>
        /// <returns></returns>
        [Route("event/{eventId}/{pageSize:int}/{pageNumber:int}/{payAtLocation:bool?}/{online:bool?}/{web:bool?}/{mobile:bool?}/{paymentCompleted:bool?}")]
        [ResponseType(typeof(TicketReportDTO))]
        public IHttpActionResult GetTicketReports(Guid organizerId, Guid eventId, int pageSize, int pageNumber, bool? payAtLocation, bool? online, bool? web, bool? mobile, bool? paymentCompleted)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var eventTicketCriteria = new EventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate, IsPayatLocation = payAtLocation, IsPayOnline = online, IsWeb = web, IsMobile = mobile, PaymentCompleted = paymentCompleted };
                var eventTicketSepcification = new EventTicketReportSpecificationForSearch(eventTicketCriteria);
                var eventTicketsCount = _eventTicketRepository.Count(eventTicketSepcification);

                var totalCount = eventTicketsCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                var eventTickets = _eventTicketRepository.Find(eventTicketSepcification).OrderByDescending(x => x.CreatedOn)
                   .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

                if (eventTickets.Count() == 0)
                    return NotFound();

                List<TicketReportDTO> ticketReportDTOs = new List<TicketReportDTO>();

                foreach (EventTicket singleEventTicket in eventTickets)
                {
                    string empty = null;
                    var paymentMode = empty;
                    if (singleEventTicket.IsPayOnLocation == false)
                    {
                        var transactionCriteria = new TransactionSearchCriteria { TicketId = singleEventTicket.Id };
                        var transactionSepcification = new TransactionSpecificationForSearch(transactionCriteria);
                        var transaction = _transactionRepository.Find(transactionSepcification);
                        if (transaction.Count() != 0 && !String.IsNullOrEmpty(transaction.FirstOrDefault().PaymentMode))
                        {
                            paymentMode = transaction.FirstOrDefault().PaymentMode;
                        }
                        else
                        {
                            paymentMode = "Transaction Pending";
                        }
                    }
                    else
                    {
                        paymentMode = "Pay At Location";
                    }

                    TicketReportDTO ticketDTO = new TicketReportDTO
                    {
                        isMobileVerified = singleEventTicket.Visitor.isMobileNoVerified,
                        dateOfBooking = singleEventTicket.CreatedOn.ToString("dd-MMM-yyyy"),
                        bookingId = singleEventTicket.TokenNumber,
                        emailId = singleEventTicket.Visitor.EmailId,
                        phoneNo = singleEventTicket.Visitor.MobileNo,
                        ticketDate = singleEventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                        numberOfTicket = singleEventTicket.NumberOfTicket.ToString(),
                        ticketAmount = singleEventTicket.TotalPriceOfTicket.ToString(),
                        paymentMode = paymentMode,
                        isPaymentCompleted = singleEventTicket.PaymentCompleted,
                        pincode = singleEventTicket.Visitor.Pincode.ToString()
                    };
                    ticketReportDTOs.Add(ticketDTO);
                }
                var result = new
                {
                    totalCount = eventTicketsCount,
                    totalPages = totalPages,
                    ticketReportDTOs = ticketReportDTOs
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// visitor Ticket Report By Date
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [Route("event/{eventId}/{pageSize:int}/{pageNumber:int}/{startDate:datetime?}/{endDate:datetime?}")]
        [ResponseType(typeof(TicketReportDTO))]
        public IHttpActionResult GetTicketReportsByDates(Guid organizerId, Guid eventId, int pageSize, int pageNumber, DateTime startDate, DateTime endDate)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var eventTicketCriteria = new EventTicketReportSearchCriteria { StartDateForSearch = startDate, EndDateForSearch = endDate, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, IsMobile = false, PaymentCompleted = true };
                var eventTicketSepcification = new EventTicketReportSpecificationForSearch(eventTicketCriteria);
                var eventTicketsCount = _eventTicketRepository.Count(eventTicketSepcification);

                var totalCount = eventTicketsCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                var eventTickets = _eventTicketRepository.Find(eventTicketSepcification).OrderBy(x => x.CreatedOn)
                   .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

                if (eventTickets.Count() == 0)
                    return NotFound();

                List<TicketReportDTO> ticketReportDTOs = new List<TicketReportDTO>();

                foreach (EventTicket singleEventTicket in eventTickets)
                {
                    string empty = null;
                    var paymentMode = empty;
                    if (singleEventTicket.IsPayOnLocation == false)
                    {
                        var transactionCriteria = new TransactionSearchCriteria { TicketId = singleEventTicket.Id };
                        var transactionSepcification = new TransactionSpecificationForSearch(transactionCriteria);
                        var transaction = _transactionRepository.Find(transactionSepcification);
                        if (transaction.Count() != 0 && !String.IsNullOrEmpty(transaction.FirstOrDefault().PaymentMode))
                        {
                            paymentMode = transaction.FirstOrDefault().PaymentMode;
                        }
                        else
                        {
                            paymentMode = "Transaction Pending";
                        }
                    }
                    else
                    {
                        paymentMode = "Pay At Location";
                    }

                    TicketReportDTO ticketDTO = new TicketReportDTO
                    {
                        isMobileVerified = singleEventTicket.Visitor.isMobileNoVerified,
                        dateOfBooking = singleEventTicket.CreatedOn.ToString("dd-MMM-yyyy"),
                        bookingId = singleEventTicket.TokenNumber,
                        emailId = singleEventTicket.Visitor.EmailId,
                        phoneNo = singleEventTicket.Visitor.MobileNo,
                        ticketDate = singleEventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                        numberOfTicket = singleEventTicket.NumberOfTicket.ToString(),
                        ticketAmount = singleEventTicket.TotalPriceOfTicket.ToString(),
                        paymentMode = paymentMode,
                        isPaymentCompleted = singleEventTicket.PaymentCompleted,
                        pincode = singleEventTicket.Visitor.Pincode.ToString()
                    };
                    ticketReportDTOs.Add(ticketDTO);
                }
                var result = new
                {
                    totalCount = eventTicketsCount,
                    totalPages = totalPages,
                    ticketReportDTOs = ticketReportDTOs
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Visitor Ticket Report Dashbord
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId}/ticketDashboard")]
        [ResponseType(typeof(EventTicketDashboardDTO))]
        public IHttpActionResult GetTicketDashboardReportsCount(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                EventTicketDashboardDTO eventTicketDashboardDTO = new EventTicketDashboardDTO();
                var eventTicketReportSearchCriteria = new EventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate, IsMobile = false, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, PaymentCompleted = true };
                var eventTicketReportSepcification = new EventTicketReportSpecificationForSearch(eventTicketReportSearchCriteria);
                var totalVisitors = _visitorEventTicketRepository.CountOfVisitors(eventTicketReportSepcification, x => x.Visitor.EmailId);
                eventTicketDashboardDTO.TotalNumberOfVisitor = totalVisitors;

                var totalTickets = _visitorEventTicketRepository.SumOfTickets(eventTicketReportSepcification);
                var sumOfTotalTicketPrice = _visitorEventTicketRepository.SumOfTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalCountOfTickets = totalTickets ?? 0;
                eventTicketDashboardDTO.TotalTicketPrice = Convert.ToInt32(sumOfTotalTicketPrice);

                var totalMobileTickets = _visitorEventTicketRepository.SumOfMobileTickets(eventTicketReportSepcification);
                var sumOfTotalMobileTicketPrice = _visitorEventTicketRepository.SumOfMobileTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalMobileTickets = totalMobileTickets;
                eventTicketDashboardDTO.TotalMobileTicketPrice = Convert.ToInt32(sumOfTotalMobileTicketPrice);

                var totalWebTickets = _visitorEventTicketRepository.SumOfWebTickets(eventTicketReportSepcification);
                var sumOfTotalWebTicketPrice = _visitorEventTicketRepository.SumOfWebTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalWebTickets = totalWebTickets ?? 0;
                eventTicketDashboardDTO.TotalWebTicketPrice = Convert.ToInt32(sumOfTotalWebTicketPrice);

                var totalOnlineTickets = _visitorEventTicketRepository.SumOfOnlineTickets(eventTicketReportSepcification);
                var sumOfTotalOnlineTicketPrice = _visitorEventTicketRepository.SumOfOnlineTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalOnlineTickets = totalOnlineTickets ?? 0;
                eventTicketDashboardDTO.TotalOnlineTicketPrice = Convert.ToInt32(sumOfTotalOnlineTicketPrice);

                var totalPayAtLocationTickets = _visitorEventTicketRepository.SumOfPayatLocationTickets(eventTicketReportSepcification);
                var sumOfTotalPayAtLocationTicketPrice = _visitorEventTicketRepository.SumOfPayAtLocationTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalPayAtLocationTickets = totalPayAtLocationTickets ?? 0;
                eventTicketDashboardDTO.TotalPayAtLocationPrice = Convert.ToInt32(sumOfTotalPayAtLocationTicketPrice);

                var totalIphoneTickets = _visitorEventTicketRepository.SumOfIphoneTickets(eventTicketReportSepcification);
                var sumOfTotalIphoneTicketPrice = _visitorEventTicketRepository.SumOfIphoneTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalIphoneTickets = totalIphoneTickets ?? 0;
                eventTicketDashboardDTO.TotalIphoneTicketsPrice = Convert.ToInt32(sumOfTotalIphoneTicketPrice);

                var totalAndroidTickets = _visitorEventTicketRepository.SumOfAndroidTickets(eventTicketReportSepcification);
                var sumOfTotalAndroidTicketPrice = _visitorEventTicketRepository.SumOfAndroidTicketsPrice(eventTicketReportSepcification, x => x.TotalPriceOfTicket);
                eventTicketDashboardDTO.TotalAndroidTickets = totalAndroidTickets ?? 0;
                eventTicketDashboardDTO.TotalAndroidTicketsPrice = Convert.ToInt32(sumOfTotalAndroidTicketPrice);

                return Ok(eventTicketDashboardDTO);
            }
        }

        /// <summary>
        /// Visitor Event Ticket Report By Date
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId}/ticketDataByDate")]
        [ResponseType(typeof(EventTicketAnalyticsDTO))]
        public IHttpActionResult GetTicketReportsCount(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

                DateTime currentValidStartDate;
                currentValidStartDate = eventDetails.StartDate.AddDays(1);

                for (DateTime date = currentValidStartDate; date <= eventDetails.EndDate; date = date.AddDays(1))
                {
                    allAvailableDatesForBooking.Add(date);
                }

                List<EventTicketAnalyticsDTO> eventTicketAnalyticsDTO = new List<EventTicketAnalyticsDTO>();
                foreach (DateTime singleDate in allAvailableDatesForBooking)
                {
                    EventTicketAnalyticsDTO singleDayEventTicketAnalyticsDTO = new EventTicketAnalyticsDTO();
                    var eventTicketReportSearchCriteria = new EventTicketReportSearchCriteria { SingleDate = singleDate, IsMobile = true, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, PaymentCompleted = false };
                    var eventTicketReportSepcification = new EventTicketReportSpecificationForSearch(eventTicketReportSearchCriteria);
                    var mobileTickets = _visitorEventTicketRepository.SumOfMobileTickets(eventTicketReportSepcification);
                    var webTickets = _visitorEventTicketRepository.SumOfWebTickets(eventTicketReportSepcification);
                    var onlineTickets = _visitorEventTicketRepository.SumOfOnlineTickets(eventTicketReportSepcification);
                    var payAtLocationTickets = _visitorEventTicketRepository.SumOfPayatLocationTickets(eventTicketReportSepcification);
                    var totalTickets = _visitorEventTicketRepository.SumOfTickets(eventTicketReportSepcification);
                    var totalVisitorsCount = _visitorEventTicketRepository.VisitorsCount(eventTicketReportSepcification, x => x.Visitor.EmailId);

                    singleDayEventTicketAnalyticsDTO.Date = singleDate.ToString("dd/MMM/yyyy");
                    singleDayEventTicketAnalyticsDTO.MobileTickets = mobileTickets ?? 0;
                    singleDayEventTicketAnalyticsDTO.WebTickets = webTickets ?? 0;
                    singleDayEventTicketAnalyticsDTO.OnlineTicktes = onlineTickets ?? 0;
                    singleDayEventTicketAnalyticsDTO.PayAtLocationTickets = payAtLocationTickets ?? 0;
                    singleDayEventTicketAnalyticsDTO.TotalTickets = Convert.ToInt32(totalTickets);
                    singleDayEventTicketAnalyticsDTO.TotalVisitors = totalVisitorsCount;
                    eventTicketAnalyticsDTO.Add(singleDayEventTicketAnalyticsDTO);
                }
                return Ok(eventTicketAnalyticsDTO);
            }
        }

        /// <summary>
        /// visitor validated Event Ticket Report By Pincode 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId}/validatedTicketReportsByPincode/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(ValidatedTicketByPincodeDTO))]
        public IHttpActionResult GetValidatedTicketReportsByPincode(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var pincodeList = _visitorEventTicketRepository.GetPincodeListOfValidatedTicket(eventDetails.Id)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();
                var totalPincodeCount = _visitorEventTicketRepository.GetPincodeListOfValidatedTicket(eventDetails.Id).Count();
                var totalCount = totalPincodeCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                var totalCountOfVisitors = _visitorEventTicketRepository.GetTotalVisitorsOfValidatedPincode(eventDetails.Id);
                var totalCountOfTickets = _visitorEventTicketRepository.GetTotalEventTicketCountOfValidatedPincode(eventDetails.Id);
                List<ValidatedTicketByPincodeDTO> validatedTicketByPincodeDTOList = new List<ValidatedTicketByPincodeDTO>();

                foreach (int pinCode in pincodeList)
                {
                    ValidatedTicketByPincodeDTO validatedTicketByPincodeDTO = new ValidatedTicketByPincodeDTO();
                    var eventTicketCount = _visitorEventTicketRepository.GetTotalEventTicketCountOfValidatedPincode(eventDetails.Id, pinCode);
                    var eventVisitor = _visitorEventTicketRepository.GetTotalVisitorsOfValidatedPincode(eventDetails.Id, pinCode);
                    validatedTicketByPincodeDTO.Pincode = pinCode;
                    validatedTicketByPincodeDTO.TotalVisitorCount = eventVisitor;
                    validatedTicketByPincodeDTO.TotalTicketCount = eventTicketCount ?? 0;
                    validatedTicketByPincodeDTOList.Add(validatedTicketByPincodeDTO);
                }

                var result = new
                {
                    totalCount = totalPincodeCount,
                    totalPages = totalPages,
                    totalCountOfVisitors = totalCountOfVisitors,
                    totalCountOfTickets = totalCountOfTickets,
                    validatedTicketByPincodeDTOs = validatedTicketByPincodeDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Visitor Event Ticket Report By Coupon code
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="discountCouponId"></param>
        /// <returns></returns>
        [Route("event/{eventId}/ticketDataByDate/partner/{discountCouponId:guid}")]
        [ResponseType(typeof(DiscountCouponTicketsCountByDateDTO))]
        public IHttpActionResult GetTicketReportsCountByPartner(Guid organizerId, Guid eventId, Guid discountCouponId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                DiscountCoupon discountCouponDetails = _discountCouponRepository.GetById(discountCouponId);
                if (discountCouponDetails == null)
                    return NotFound();

                List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

                DateTime currentValidStartDate;
                currentValidStartDate = eventDetails.StartDate.AddDays(1);

                for (DateTime date = currentValidStartDate; date <= eventDetails.EndDate; date = date.AddDays(1))
                {
                    allAvailableDatesForBooking.Add(date);
                }

                List<DiscountCouponTicketsCountByDateDTO> discountTicketsCountByDateListDTO = new List<DiscountCouponTicketsCountByDateDTO>();
                foreach (DateTime singleDate in allAvailableDatesForBooking)
                {
                    DiscountCouponTicketsCountByDateDTO discountTicketsCountByDateDTO = new DiscountCouponTicketsCountByDateDTO();
                    TimeSpan startTimeSpan = new TimeSpan(00, 00, 00);
                    TimeSpan endTimeSpan = new TimeSpan(23, 59, 59);
                    DateTime startDate = singleDate.Date + startTimeSpan;
                    DateTime endDate = singleDate.Date + endTimeSpan;

                    var visitorDiscountCouponMapCriteria = new VisitorDiscountCouponMapSearchCriteria { DiscountCouponId = discountCouponDetails.Id, StartDate = startDate, EndDate = endDate };
                    var visitorDiscountCouponMapSpecification = new VisitorDiscountCouponMapSpecificationForSearch(visitorDiscountCouponMapCriteria);
                    var discountCouponTicketsCount = _visitorEventTicketRepository.SumOfDiscountCouponTickets(visitorDiscountCouponMapSpecification);
                    var totalVisitors = _visitorEventTicketRepository.DiscountVisitorsCount(visitorDiscountCouponMapSpecification, x => x.EventTicket.Visitor.EmailId);

                    discountTicketsCountByDateDTO.Date = singleDate.ToString("dd/MMM/yyyy");
                    discountTicketsCountByDateDTO.Day = singleDate.ToString("ddd");
                    discountTicketsCountByDateDTO.VisitorCount = totalVisitors;
                    discountTicketsCountByDateDTO.TotalTicketCount = discountCouponTicketsCount ?? 0;
                    discountTicketsCountByDateListDTO.Add(discountTicketsCountByDateDTO);
                }
                return Ok(discountTicketsCountByDateListDTO);
            }
        }
    }
}
