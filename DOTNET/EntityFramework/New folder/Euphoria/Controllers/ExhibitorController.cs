using System;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;
using System.Linq;
using Techlabs.Euphoria.Kernel.Views;
using Techlabs.Euphoria.Kernel.Specification;
using System.Collections.Generic;
using Modules.LayoutManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using System.Threading.Tasks;
using System.Net;
using Modules.EventManagement;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;

namespace Techlabs.Euphoria.API.Controllers
{
    /// <summary>
    /// Manage exhibitors for an Organizer
    /// </summary>
    [RoutePrefix("api/v1/organizers/{organizerId}/exhibitors")]
    public class ExhibitorController : ApiController
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

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get All Exhibitor Status
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitorStatus")]
        [ResponseType(typeof(ExhibitorStatusDTO))]
        public IHttpActionResult GetExhibitorStatus(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorStatusList = _exhibitorStatusRepository.Find(new GetAllSpecification<ExhibitorStatus>());
                if (exhibitorStatusList.Count() == 0)
                    return NotFound();
                List<ExhibitorStatusDTO> exhibitorStatusDTOLIst = new List<ExhibitorStatusDTO>();
                foreach (ExhibitorStatus exhibitorStatus in exhibitorStatusList)
                {
                    ExhibitorStatusDTO exhibitorStatusDTO = new ExhibitorStatusDTO()
                    {
                        Id = exhibitorStatus.Id,
                        Status = exhibitorStatus.Status
                    };
                    exhibitorStatusDTOLIst.Add(exhibitorStatusDTO);
                }
                return Ok(exhibitorStatusDTOLIst);
            }
        }

        /// <summary>
        /// Get Exhibitor Registration Type By TypeName
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="registrationType"></param>
        /// <returns></returns>
        [Route("RegistrationType/{registrationType}")]
        [ResponseType(typeof(RegistrationTypeDTO))]
        public IHttpActionResult GetExhibitorRegistrationType(Guid organizerId, string registrationType)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var registrationTypeCriteria = new RegistrationTypeSearchCriteria { RegistrationType = registrationType };
                var registrationTypeSpecification = new RegistrationTypeSpecificationForSearch(registrationTypeCriteria);
                var exhibitorRegistrationType = _exhibitorRegistrationTypeRepository.Find(registrationTypeSpecification).FirstOrDefault();
                if (exhibitorRegistrationType == null)
                    return NotFound();

                RegistrationTypeDTO registrationTypeDTO = new RegistrationTypeDTO()
                {
                    Id = exhibitorRegistrationType.Id,
                    RegistrationType = exhibitorRegistrationType.RegistrationType
                };

                return Ok(registrationTypeDTO);
            }
        }

        /// <summary>
        /// Get All Registration Types
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("RegistrationType")]
        [ResponseType(typeof(RegistrationTypeDTO))]
        public IHttpActionResult GetListExhibitorRegistrationType(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorRegistrationTypeList = _exhibitorRegistrationTypeRepository.Find(new GetAllSpecification<ExhibitorRegistrationType>()).OrderBy(x => x.RegistrationType);
                if (exhibitorRegistrationTypeList == null)
                    return NotFound();

                List<RegistrationTypeDTO> registrationTypeListDTO = new List<RegistrationTypeDTO>();
                foreach (ExhibitorRegistrationType exhibitorRegistrationType in exhibitorRegistrationTypeList)
                {
                    RegistrationTypeDTO registrationTypeDTO = new RegistrationTypeDTO()
                    {
                        Id = exhibitorRegistrationType.Id,
                        RegistrationType = exhibitorRegistrationType.RegistrationType
                    };
                    registrationTypeListDTO.Add(registrationTypeDTO);
                }
                return Ok(registrationTypeListDTO);
            }
        }

        /// <summary>
        /// Get specific Exhibitor
        /// </summary>
        /// <param name="exhibitiorId"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("{exhibitiorId:guid}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid exhibitiorId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitor = _exhibitorRepository.GetById(exhibitiorId);
                if (exhibitor == null || exhibitor.Organizer.Id != organizerId)
                    return NotFound();

                ExhibitorDTO exhibitorDTO = GetDTO(exhibitor);
                if (exhibitor.ExhibitorIndustryType != null)
                {
                    exhibitorDTO.ExhibitorIndustryType = GetExhibitorIndustryTypeDTO(exhibitor.ExhibitorIndustryType);
                }
                if (exhibitor.ExhibitorType != null)
                {
                    exhibitorDTO.ExhibitorType = GetExhibitorTypeDTO(exhibitor.ExhibitorType);
                }
                if (exhibitor.ExhibitorStatus != null)
                {
                    exhibitorDTO.ExhibitorStatusDTO = GetExhibitorStatusDTO(exhibitor.ExhibitorStatus);
                }
                return Ok(exhibitorDTO);
            }
        }

        /// <summary>
        /// Get Exhibitor Enquiry
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitiorEnquiryId"></param>
        /// <returns></returns>
        [Route("enquiry/{exhibitiorEnquiryId:guid}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorEnquiry(Guid organizerId, Guid exhibitiorEnquiryId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorEnquiry = _exhibitorEnquiryRepository.GetById(exhibitiorEnquiryId);
                if (exhibitorEnquiry == null)
                    return NotFound();

                ExhibitorDTO exhibitorDTO = GetDTO(exhibitorEnquiry);
                if (exhibitorEnquiry.ExhibitorIndustryType != null)
                {
                    exhibitorDTO.ExhibitorIndustryType = GetExhibitorIndustryTypeDTO(exhibitorEnquiry.ExhibitorIndustryType);
                }
                if (exhibitorEnquiry.ExhibitorType != null)
                {
                    exhibitorDTO.ExhibitorType = GetExhibitorTypeDTO(exhibitorEnquiry.ExhibitorType);
                }
                if (exhibitorEnquiry.ExhibitorStatus != null)
                {
                    exhibitorDTO.ExhibitorStatusDTO = GetExhibitorStatusDTO(exhibitorEnquiry.ExhibitorStatus);
                }
                if (exhibitorEnquiry.ExhibitorRegistrationType != null)
                {
                    exhibitorDTO.RegistrationType = exhibitorEnquiry.ExhibitorRegistrationType.RegistrationType;
                }
                exhibitorDTO.Comment = exhibitorEnquiry.Comment;

                return Ok(exhibitorDTO);
            }
        }

        /// <summary>
        /// Get Exhibitor Search By Name 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitiorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("search/{exhibitiorName}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetSearch(Guid organizerId, string exhibitiorName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorCriteria = new ExhibitorSearchCriteria { CompanyName = exhibitiorName };
                var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                var exhibitorList = _exhibitorRepository.Find(exhibitorSpecification).OrderBy(x => x.Name)
                                    .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize);

                var totalCount = exhibitorCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<ExhibitorDTO> exhibitorsList = new List<ExhibitorDTO>();
                foreach (Exhibitor exhibitor in exhibitorList)
                {
                    exhibitorsList.Add(GetExhibitorDTO(exhibitor));
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorsList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Exhibitor Listing By Type 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="typeOfExhibitor"></param>
        /// <returns></returns>
        [Route("listing/{typeOfExhibitor}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorListing(Guid organizerId, string typeOfExhibitor)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var exhibitorList = new List<ExhibitorReportDTO>();
                var exhibitor = new ExhibitorReportDTO();
                if (typeOfExhibitor.ToUpper() == "SalesPerson".ToUpper())
                {
                    //var exhibitorCriteria = new ExhibitorSearchCriteria { CompanyName = exhibitiorName };
                    //var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                    //var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                    //exhibitorList = _exhibitorRepository.Find(exhibitorSpecification).OrderBy(x => x.Name)
                    //                   .Skip((pageNumber - 1) * pageSize)
                    //                .Take(pageSize).ToList();
                    //var totalCount = exhibitorCount;
                    //var totalPages = Math.Ceiling((double)totalCount / pageSize);
                }
                else if (typeOfExhibitor.ToUpper() == "IndustryType".ToUpper())
                {
                    var exhibitorTypeList = _exhibitorTypeListRepository.DistinctIndustryType();
                    foreach (ExhibitorType exhibitorType in exhibitorTypeList)
                    {
                        if (exhibitorType != null)
                        {
                            var exhibitorCriteria = new ExhibitorSearchCriteria { ExhibitorTypeId = exhibitorType.Id };
                            var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                            var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);

                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = exhibitorType.Id,
                                Value = exhibitorType.Type,
                                TotalCount = exhibitorCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                        else
                        {
                            var exhibitorsCount = _exhibitorTypeListRepository.NullExhibitorTypeExhibitors();
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = new Guid(),
                                Value = "Others",
                                TotalCount = exhibitorsCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "ExhibitorIndustryType".ToUpper())
                {
                    var exhibitorTypeList = _exhibitorTypeListRepository.DistinctExhibitorIndustryType();
                    foreach (ExhibitorIndustryType exhibitorType in exhibitorTypeList)
                    {
                        if (exhibitorType != null)
                        {
                            var exhibitorCriteria = new ExhibitorSearchCriteria { ExhibitorIndustryTypeId = exhibitorType.Id };
                            var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                            var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = exhibitorType.Id,
                                Value = exhibitorType.IndustryType,
                                TotalCount = exhibitorCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                        else
                        {
                            var exhibitorsCount = _exhibitorTypeListRepository.NullExhibitorIndustryTypeExhibitors();
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = new Guid(),
                                Value = "Others",
                                TotalCount = exhibitorsCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "location".ToUpper())
                {
                    var exhibitorLocationList = _exhibitorTypeListRepository.DistinctLocation();

                    foreach (Country exhibitorCountry in exhibitorLocationList)
                    {
                        if (exhibitorCountry != null)
                        {
                            var exhibitorCriteria = new ExhibitorSearchCriteria { CountryId = exhibitorCountry.Id };
                            var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                            var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = exhibitorCountry.Id,
                                Value = exhibitorCountry.Name,
                                TotalCount = exhibitorCount
                            };
                            exhibitorList.Add(exhibitor);
                        }

                        else
                        {
                            var exhibitorsCount = _exhibitorTypeListRepository.NullLocationExhibitors();
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = new Guid(),
                                Value = "Others",
                                TotalCount = exhibitorsCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "Product".ToUpper())
                {
                    var exhibitorLocationList = _exhibitorTypeListRepository.DistinctLocation();

                    foreach (Country exhibitorCountry in exhibitorLocationList)
                    {
                        if (exhibitorCountry != null)
                        {
                            var exhibitorCriteria = new ExhibitorSearchCriteria { CountryId = exhibitorCountry.Id };
                            var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                            var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = exhibitorCountry.Id,
                                Value = exhibitorCountry.Name,
                                TotalCount = exhibitorCount
                            };
                            exhibitorList.Add(exhibitor);
                        }

                        else
                        {
                            var exhibitorsCount = _exhibitorTypeListRepository.NullLocationExhibitors();
                            exhibitor = new ExhibitorReportDTO()
                            {
                                Id = new Guid(),
                                Value = "Others",
                                TotalCount = exhibitorsCount
                            };
                            exhibitorList.Add(exhibitor);
                        }
                    }
                }
                return Ok(exhibitorList);
            }
        }

        /// <summary>
        /// Get Exhibitor Data Listing By Type
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="typeOfExhibitor"></param>
        /// <param name="typeId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("listing/{typeOfExhibitor}/{typeId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetDetailExhibitorListing(Guid organizerId, string typeOfExhibitor, Guid typeId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorListDTO = new List<ExhibitorDTO>();
                var exhibitorDTO = new ExhibitorDTO();

                var totalCount = 0;
                var totalPages = 0;

                if (typeOfExhibitor.ToUpper() == "SalesPerson".ToUpper())
                {
                    //var exhibitorCriteria = new ExhibitorSearchCriteria { CompanyName = exhibitiorName };
                    //var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                    //var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                    //exhibitorList = _exhibitorRepository.Find(exhibitorSpecification).OrderBy(x => x.Name)
                    //                   .Skip((pageNumber - 1) * pageSize)
                    //                .Take(pageSize).ToList();
                    //var totalCount = exhibitorCount;
                    //var totalPages = Math.Ceiling((double)totalCount / pageSize);
                }
                else if (typeOfExhibitor.ToUpper() == "IndustryType".ToUpper())
                {
                    var exhibitorType = _exhibitorTypeRepository.GetById(typeId);
                    if (exhibitorType == null)
                        return NotFound();

                    if (exhibitorType != null)
                    {
                        var exhibitorCriteria = new ExhibitorSearchCriteria { ExhibitorTypeId = exhibitorType.Id };
                        var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                        var exhibitorList = _exhibitorRepository.Find(exhibitorSpecification)
                        .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize).ToList();

                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                    else
                    {
                        var exhibitorCount = _exhibitorTypeListRepository.NullExhibitorTypeExhibitors();
                        var exhibitorList = _exhibitorTypeListRepository.NullExhibitorTypeExhibitorDetails(pageSize, pageNumber);
                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "ExhibitorIndustryType".ToUpper())
                {
                    var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(typeId);
                    if (exhibitorIndustryType == null)
                        return NotFound();

                    if (exhibitorIndustryType != null)
                    {
                        var exhibitorCriteria = new ExhibitorSearchCriteria { ExhibitorIndustryTypeId = exhibitorIndustryType.Id };
                        var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                        var exhibitorList = _exhibitorRepository.Find(exhibitorSpecification)
                        .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize).ToList();

                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                    else
                    {
                        var exhibitorCount = _exhibitorTypeListRepository.NullExhibitorIndustryTypeExhibitors();
                        var exhibitorList = _exhibitorTypeListRepository.NullExhibitorIndustryTypeExhibitorDetails(pageSize, pageNumber);
                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "location".ToUpper())
                {
                    var exhibitorLocationList = _exhibitorTypeListRepository.DistinctLocation();
                    var countryDetails = _countryRepository.GetById(typeId);
                    if (countryDetails == null)
                        return NotFound();

                    if (countryDetails != null)
                    {
                        var exhibitorCriteria = new ExhibitorSearchCriteria { CountryId = countryDetails.Id };
                        var exhibitorSpecification = new ExhibitorSpecificationForSearch(exhibitorCriteria);
                        var exhibitorCount = _exhibitorRepository.Count(exhibitorSpecification);
                        var exhibitorList = _exhibitorRepository.Find(exhibitorSpecification)
                        .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize).ToList();

                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                    else
                    {
                        var exhibitorCount = _exhibitorTypeListRepository.NullLocationExhibitors();
                        var exhibitorList = _exhibitorTypeListRepository.NullLocatiocationExhibitorDetails(pageSize, pageNumber);
                        totalCount = exhibitorCount;
                        totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                        foreach (Exhibitor singleExhibitor in exhibitorList)
                        {
                            exhibitorDTO = GetDTO(singleExhibitor);
                            exhibitorListDTO.Add(exhibitorDTO);
                        }
                    }
                }
                else if (typeOfExhibitor.ToUpper() == "Product".ToUpper())
                {

                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorListDTO
                };
                return Ok(result);
            }
        }

       /// <summary>
       /// Get Exhibitor Enquiry Data By RegistrationType
       /// </summary>
       /// <param name="organizerId"></param>
       /// <param name="pageSize"></param>
       /// <param name="pageNumber"></param>
       /// <param name="registrationTypeId"></param>
       /// <returns></returns>
        [Route("registrationType/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult GetExhibitorEnquiryByRegistrationType(Guid organizerId, int pageSize, int pageNumber, string registrationTypeId = null)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorEnquiryCount = 0;
                var totalCount = 0;
                var totalPages = 0;
                var exhibitorEnquiryList = new List<ExhibitorEnquiry>();
                if (!string.IsNullOrEmpty(registrationTypeId))
                {
                    var exhibitorEnquiryCriteria = new ExhibitorEnquirySearchCriteria { ExhibitorRegistrationTypeId = new Guid(registrationTypeId) };
                    var exhibitorEnquirySpecification = new ExhibitorEnquirySpecificationForSearch(exhibitorEnquiryCriteria);
                    exhibitorEnquiryCount = _exhibitorEnquiryRepository.Count(exhibitorEnquirySpecification);
                    exhibitorEnquiryList = _exhibitorEnquiryRepository.Find(exhibitorEnquirySpecification).OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize).ToList();

                    totalCount = exhibitorEnquiryCount;
                    totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                }
                else
                {
                    var exhibitorEnquiryCriteria = new ExhibitorEnquirySearchCriteria { ExhibitorRegistrationType = "true" };
                    var exhibitorEnquirySpecification = new ExhibitorEnquirySpecificationForSearch(exhibitorEnquiryCriteria);
                    exhibitorEnquiryCount = _exhibitorEnquiryRepository.Count(exhibitorEnquirySpecification);
                    exhibitorEnquiryList = _exhibitorEnquiryRepository.Find(exhibitorEnquirySpecification).OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize).ToList();

                    totalCount = exhibitorEnquiryCount;
                    totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
                }
                List<ExhibitorDTO> exhibitorDTOList = new List<ExhibitorDTO>();
                foreach (ExhibitorEnquiry exhibitorEnquiry in exhibitorEnquiryList)
                {
                    ExhibitorDTO exhibitorDTO = GetDTO(exhibitorEnquiry);
                    if (exhibitorEnquiry.ExhibitorRegistrationType != null)
                        exhibitorDTO.RegistrationType = exhibitorEnquiry.ExhibitorRegistrationType.RegistrationType;
                    exhibitorDTOList.Add(exhibitorDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get All Exhibitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Get(Guid organizerId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorCount = _exhibitorRepository.Count(new GetAllSpecification<Exhibitor>());
                var exhibitors = _exhibitorRepository.Find(new GetAllSpecification<Exhibitor>()).OrderByDescending(x => x.CreatedOn)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize);

                var totalCount = exhibitorCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<ExhibitorDTO> exhibitorDTOList = new List<ExhibitorDTO>();
                foreach (Exhibitor exhibitor in exhibitors)
                {
                    ExhibitorDTO exhibitorDTO = GetDTO(exhibitor);
                    exhibitorDTOList.Add(exhibitorDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Search Booking Exhibitor for Event 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="name"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="category"></param>
        /// <param name="pavilionName"></param>
        /// <returns></returns>
        [Route("search/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, int pageSize, int pageNumber, string name = null, string country = null, string state = null, string category = null, string pavilionName = null)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var bookingCriteria = new BookingSearchCriteria
                {
                    Category = category,
                    Country = country,
                    State = state,
                    CompanyName = name,
                    Pavilion = pavilionName,
                    EventId = eventId
                };

                var bookingSpecification = new BookingSpecificationForSearch(bookingCriteria);
                var bookingListCount = _bookingRepository.Count(bookingSpecification);
                var bookingList = _bookingRepository.Find(bookingSpecification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<ExhibitorSearchDTO> exhibitorSearchDTO = new List<ExhibitorSearchDTO>();
                foreach (Booking booking in bookingList)
                {
                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = booking.Id, EventId = eventDetails.Id };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                    exhibitorSearchDTO.Add(GetDTO(booking, bookingStall));
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorSearchDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Add Exhibitor 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Post(Guid organizerId, ExhibitorDTO exhibitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                string tokenNumber = _tokenService.Generate();
                exhibitorDTO.Password = tokenNumber;

                var exhibitorLoginCriteria = new ExhibitorLoginSearchCriteria { EmailId = exhibitorDTO.EmailId, ExhibitorName = exhibitorDTO.Name };
                var exhibitorLoginSepcification = new ExhibitorLoginSpecificationForSearch(exhibitorLoginCriteria);
                var exhibitorLogin = _exhibitorRepository.Find(exhibitorLoginSepcification).FirstOrDefault();
                Exhibitor exhibitor = new Exhibitor();
                if (exhibitorLogin == null)
                {
                    exhibitor = Exhibitor.Create(exhibitorDTO.Name, exhibitorDTO.EmailId, exhibitorDTO.PhoneNo, exhibitorDTO.CompanyName, exhibitorDTO.Designation, exhibitorDTO.CompanyDescription, exhibitorDTO.Address, exhibitorDTO.PinCode, exhibitorDTO.Password, 0);
                    if (exhibitorDTO.State != null)
                    {
                        State state = _stateRepository.GetById(exhibitorDTO.State.Id);
                        exhibitor.State = state;
                    }

                    if (exhibitorDTO.Country != null)
                    {
                        Country country = _countryRepository.GetById(exhibitorDTO.Country.Id);
                        exhibitor.Country = country;
                    }

                    if (exhibitorDTO.Categories.Count() != 0)
                    {
                        foreach (CategoryDTO category in exhibitorDTO.Categories)
                        {
                            Category categoryAdd = _categoryRepository.GetById(category.Id);
                            exhibitor.Categories = new List<Category>();
                            exhibitor.Categories.Add(categoryAdd);
                        }
                    }

                    if (exhibitorDTO.ExhibitorType != null)
                    {
                        ExhibitorType exhibitorType = _exhibitorTypeRepository.GetById(exhibitorDTO.ExhibitorType.Id);
                        exhibitor.ExhibitorType = exhibitorType;
                    }

                    if (exhibitorDTO.ExhibitorIndustryType != null)
                    {
                        ExhibitorIndustryType exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorDTO.ExhibitorIndustryType.Id);
                        exhibitor.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                    exhibitor.Organizer = organizer;
                    _exhibitorRepository.Add(exhibitor);
                    unitOfWork.SaveChanges();
                }
                else
                {
                    exhibitor = exhibitorLogin;
                }
                var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString("dd-MMM-yyyy");
                string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata, West Bengal 700026</span> </td> </tr> <tr> <td class='hide-for-mobile' style='height:180px; width:299px;'> <img width='180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo' /> </td> </tr> </table> </td> <td valign='top' class='col-2'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td> <table cellspacing='0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> Hi, " + exhibitor.Name + "</td> <td class='mobile-padding' style='text-align:right ;'>" + date + "</td> </tr> </table> <table cellspacing='0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! You're just one click away from your personal Account. </td> </tr><tr><td><b> UserName : </b> " + exhibitor.EmailId + "</td></tr><tr><td><b> Password : </b> " + exhibitor.Password + "</td></tr> <!-- </table> --> <table cellspacing='0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width='150' style='width:150px;'> <div style='text-align:center; background-color:#febb13;'><!--[if mso]> <v:roundrect xmlns:v='urn:schemas-microsoft-com:vml' xmlns:w='urn:schemas-microsoft-com:office:word' href='#' style='height:35px;v-text-anchor:middle;width:150px;' arcsize='8%' stroke='f' fillcolor='#48be19'> <w:anchorlock/> <center style='color:#ffffff;font-family:sans-serif;font-size:16px;'>Activate Now!</center> </v:roundrect> <![endif]--> <!--[if !mso]><!-- --><a href='http://gsmktg.com/?page_id=13556'><table cellspacing='0' cellpadding='0' width='100%' style='width:100%'><tr><td style='background-color:#febb13;border-radius:5px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:16px;line-height:45px;text-align:center;text-decoration:none;width:150px;-webkit-text-size-adjust:none;mso-hide:all;'><span style='color:#ffffff'>Login Now!</span></td></tr></table></a> <!--<![endif]--> </div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td style='text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thank-you.png' alt='signature' /> </td> </tr> </table> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br>West Bengal 700026</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                string fileName = "test";
                string subject = "GS Marketing Associate Registration Details";
                Task.Run(() => SendeMail(htmlPage, fileName, exhibitor, subject));

                return Get(organizerId, exhibitor.Id);
            }
        }

        /// <summary>
        /// Add Exhibitor Enquiry
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="registrationTypeId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorDTO"></param>
        /// <returns></returns>
        [Route("exhibitorEnquiry/{registrationTypeId:guid}/event/{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult PostExhibitorForAdds(Guid organizerId, Guid registrationTypeId, Guid eventId, AddExhibitorDTO exhibitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                ExhibitorRegistrationType exhibitorRegistratoinType = new ExhibitorRegistrationType();
                var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString("dd-MMM-yyyy");

                ExhibitorEnquiry exhibitorEnquiry = ExhibitorEnquiry.Create(exhibitorDTO.Name, exhibitorDTO.EmailId, exhibitorDTO.PhoneNo, exhibitorDTO.CompanyName, null, null, null, 0, exhibitorDTO.Comment);

                if (exhibitorDTO.ExhibitorIndustryTypeId != Guid.Empty)
                {
                    ExhibitorIndustryType exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorDTO.ExhibitorIndustryTypeId);
                    exhibitorEnquiry.ExhibitorIndustryType = exhibitorIndustryType;
                }

                exhibitorRegistratoinType = _exhibitorRegistrationTypeRepository.GetById(registrationTypeId);

                if (exhibitorRegistratoinType == null)
                    return NotFound();

                exhibitorEnquiry.ExhibitorRegistrationType = exhibitorRegistratoinType;
                exhibitorEnquiry.Event = eventDetails;
                _exhibitorEnquiryRepository.Add(exhibitorEnquiry);

                if (exhibitorRegistratoinType.RegistrationType.ToUpper() == "gads".ToUpper())
                {

                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Google Ads Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                else if (exhibitorRegistratoinType.RegistrationType.ToUpper() == "web".ToUpper())
                {
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Website Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                else if (exhibitorRegistratoinType.RegistrationType.ToUpper() == "lAds".ToUpper())
                {
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Linkedin Ads Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                else if (exhibitorRegistratoinType.RegistrationType.ToUpper() == "fAds".ToUpper())
                {
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Facebook Ads Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                else if (exhibitorRegistratoinType.RegistrationType.ToUpper() == "email".ToUpper())
                {
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Email Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                else
                {
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span> </td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td><table cellspacing = '0' cellpadding='10' width='100%'><tr><td class='mobile-padding'> Hi, " + exhibitorEnquiry.Name + "</td> <td class='mobile-padding' style='text-align:right;'>" + date + "</td> </tr> </table> <table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td> <b>Welcome to GS Marketing Associates!</b><br> We're really excited for you to join GS Marketing family! <br/>We'll get back to you within 2 Business Days</td> </tr><!-- </table> --> <table cellspacing = '0' cellpadding= '10' width= '100%' > <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;' ></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td style = 'text-align:center;padding-top:30px;'> <img src='http://gsmktg.com/mobile_images/thankyou.png' alt='signature'/> </td> </tr> </table> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span></b><br><span> 108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Registration Details";

                    Task.Run(() => SendeMail(htmlPage, fileName, exhibitorEnquiry, subject));
                    string adminHtmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>GS Marketing Associates Welcome</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br> West Bengal 700026<br/>Contact No: 9836333308<br/>Email: chidrup.shah@gsmktg.com </span></td></tr><tr><td class='hide-for-mobile' style='height:180px; width:299px; text-align:right;'> <img width = '180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo'/> </td> </tr> </table> </td> <td valign = 'top' class='col-2'> <table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad'> <table cellspacing = '0' cellpadding='0' width='100%'><tr><td class='mobile-padding' style='text-align:right ;'>" + date + "</td></tr><tr><td><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='mobile-padding'> <b>Company Name:</b>" + exhibitorEnquiry.CompanyName + "</td></tr><tr> <td class='mobile-padding'> <b>Contact Person:</b>" + exhibitorEnquiry.Name + "</td></tr> <tr> <td class='mobile-padding'> <b>Phone No:</b> " + exhibitorEnquiry.PhoneNo + "</td></tr><tr> <td class='mobile-padding'> <b>Email:</b>" + exhibitorEnquiry.EmailId + "</td></tr><tr> <td class='mobile-padding'> <b>Comment:</b>" + exhibitorEnquiry.Comment + "</td></tr></table><table cellspacing = '0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width = '150' style='width:150px;'> <div style = 'text-align:center; background-color:#febb13;'></div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table><table cellspacing = '0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br/>West Bengal 700026 <br/>Contact No: 9836333308<br/>Email:chidrup.shah@gsmktg.com</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string adminFileName = "test";
                    string adminSubject = "GS Marketing Associate Registration Details";
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "chidrup.shah@gsmktg.com", adminSubject));
                    Task.Run(() => SendeMails(adminHtmlPage, adminFileName, "bhavesh.talsania@gsmktg.com", adminSubject));
                }
                unitOfWork.SaveChanges();
                return GetExhibitorEnquiry(organizerId, exhibitorEnquiry.Id);
            }
        }

        /// <summary>
        /// Exhibitor Login
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitiorloginDTO"></param>
        /// <returns></returns>
        [Route("login")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Post(Guid organizerId, ExhibitorLoginDTO exhibitiorloginDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                if (exhibitiorloginDTO.EmailId == null || exhibitiorloginDTO.EmailId == "" || exhibitiorloginDTO.Password == null || exhibitiorloginDTO.Password == "")
                    return Content(HttpStatusCode.NotFound, "Invalid EmailId or Password.");

                var exhibitorLoginCriteria = new ExhibitorLoginSearchCriteria { EmailId = exhibitiorloginDTO.EmailId, Password = exhibitiorloginDTO.Password };
                var exhibitorLoginSepcification = new ExhibitorLoginSpecificationForSearch(exhibitorLoginCriteria);
                var exhibitorLogin = _exhibitorRepository.Find(exhibitorLoginSepcification).FirstOrDefault();
                if (exhibitorLogin == null)
                    return Content(HttpStatusCode.NotFound, "Invalid Login.");

                ExhibitorDTO exhibitorDTO = GetDTO(exhibitorLogin);

                return Ok(exhibitorDTO);
            }
        }

        /// <summary>
        /// Edit Exhibitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="exhibitorDTO"></param>
        /// <returns></returns>
        [Route("{exhibitorId:guid}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid exhibitorId, ExhibitorDTO exhibitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorToUpdate = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorToUpdate == null)
                    return NotFound();

                if (exhibitorToUpdate.Organizer == null)
                {
                    exhibitorToUpdate.Organizer = organizer;
                }
                exhibitorToUpdate.Update(exhibitorDTO.Name, exhibitorDTO.EmailId, exhibitorDTO.PhoneNo, exhibitorDTO.CompanyName, exhibitorDTO.Designation, exhibitorDTO.CompanyDescription, exhibitorDTO.Address, exhibitorDTO.PinCode, exhibitorToUpdate.Password, exhibitorDTO.Age);

                if (exhibitorDTO.ExhibitorIndustryType != null)
                {
                    var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorDTO.ExhibitorIndustryType.Id);
                    if (exhibitorIndustryType != null)
                    {
                        exhibitorToUpdate.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                }
                if (exhibitorDTO.ExhibitorType != null)
                {
                    var exhibitorType = _exhibitorTypeRepository.GetById(exhibitorDTO.ExhibitorType.Id);
                    if (exhibitorType != null)
                    {
                        exhibitorToUpdate.ExhibitorType = exhibitorType;
                    }
                }
                if (exhibitorDTO.State != null)
                {
                    var state = _stateRepository.GetById(exhibitorDTO.State.Id);
                    if (state != null)
                    {
                        exhibitorToUpdate.State = state;
                    }
                }
                if (exhibitorDTO.Country != null)
                {
                    var country = _countryRepository.GetById(exhibitorDTO.Country.Id);
                    if (country != null)
                    {
                        exhibitorToUpdate.Country = country;
                    }
                }
                if (exhibitorDTO.ExhibitorStatusId != null)
                {
                    var exhibitorStatus = _exhibitorStatusRepository.GetById(exhibitorDTO.ExhibitorStatusId);
                    if (exhibitorStatus != null)
                    {
                        exhibitorToUpdate.ExhibitorStatus = exhibitorStatus;
                    }
                }
                if (exhibitorDTO.Categories.Count() != 0)
                {
                    exhibitorToUpdate.Categories.Clear();
                    foreach (CategoryDTO categoryDTO in exhibitorDTO.Categories)
                    {
                        var categoryToAdd = _categoryRepository.GetById(categoryDTO.Id);
                        if (categoryToAdd != null)
                        {
                            exhibitorToUpdate.Categories.Add(categoryToAdd);
                        }
                    }
                }
                unitOfWork.SaveChanges();
            }
            return Get(organizerId, exhibitorId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid organizerId, Guid id)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            var exhibitor = _exhibitorRepository.GetById(id);
            if (exhibitor.Organizer.Id != organizerId)
                return NotFound();

            _exhibitorRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="allocation"></param>
        /// <returns></returns>
        [Route("{exhibitorId:guid}/exhibitions")]
        public IHttpActionResult PostExhibitorAllocation(Guid organizerId, Guid exhibitorId, ExhibitorAllocationDTO allocation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitor = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitor.Organizer.Id != organizerId)
                    return NotFound();

                var stallAllocation = allocation.Locations.Select(x => new StallAllocation { StallId = x.StallId, PavilionId = x.PavilionId }).ToArray();
                exhibitor.Allocate(allocation.ExhibitionId, stallAllocation);

                unitOfWork.SaveChanges();
            }
            return Ok();
        }

        /// <summary>
        /// Get All Exhibitor Type
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitorType")]
        [ResponseType(typeof(ExhibitorTypeDTO))]
        public IHttpActionResult GetExhibitorType(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorType = _exhibitorTypeRepository.Find(new GetAllSpecification<ExhibitorType>()).OrderBy(x => x.Type);
                if (exhibitorType.Count() == 0)
                    return NotFound();

                return Ok(exhibitorType.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Get All ExhibitorIndustryType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitorIndustryType")]
        [ResponseType(typeof(ExhibitorIndustryTypeDTO))]
        public IHttpActionResult GetExhibitorIndustryType(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorIndustryType = _exhibitorIndustryTypeRepository.Find(new GetAllSpecification<ExhibitorIndustryType>()).OrderBy(x => x.IndustryType);
                if (exhibitorIndustryType.Count() == 0)
                    return NotFound();

                return Ok(exhibitorIndustryType.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Add ExhibitorType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorTypeDTO"></param>
        /// <returns></returns>
        [Route("exhibitorType")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, ExhibitorTypeDTO exhibitorTypeDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorTypeSearchCriteria = new ExhibitorTypeSearchCriteria { ExhibitorType = exhibitorTypeDTO.Type };
                var exhibitorTypeSpcification = new ExhibitorTypeSpecificationForSearch(exhibitorTypeSearchCriteria);
                var exhibitorTypeList = _exhibitorTypeRepository.Find(exhibitorTypeSpcification);
                if (exhibitorTypeList.Count() == 0)
                {
                    var exhibitorType = ExhibitorType.Create(exhibitorTypeDTO.Type); // DTO to entity mapping
                    _exhibitorTypeRepository.Add(exhibitorType);
                    unitOfWork.SaveChanges();
                }
                return GetExhibitorType(organizerId);
            }
        }

        /// <summary>
        /// Add ExhibitorIndustryType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorIndustryTypeDTO"></param>
        /// <returns></returns>
        [Route("exhibitorIndustryType")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, ExhibitorIndustryTypeDTO exhibitorIndustryTypeDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorIndustryTypeSearchCriteria = new ExhibitorIndustryTypeSearchCriteria { IndustryType = exhibitorIndustryTypeDTO.IndustryType };
                var exhibitorIndustryTypeSpcification = new ExhibitorIndustryTypeSpecificationForSearch(exhibitorIndustryTypeSearchCriteria);
                var exhibitorIndustryTypeList = _exhibitorIndustryTypeRepository.Find(exhibitorIndustryTypeSpcification);

                if (exhibitorIndustryTypeList.Count() == 0)
                {
                    var exhibitorIndustryType = ExhibitorIndustryType.Create(exhibitorIndustryTypeDTO.IndustryType); // DTO to entity mapping
                    if (exhibitorIndustryTypeDTO.Color != null)
                        exhibitorIndustryType.Color = exhibitorIndustryTypeDTO.Color;
                    _exhibitorIndustryTypeRepository.Add(exhibitorIndustryType);
                    unitOfWork.SaveChanges();
                }
                return GetExhibitorIndustryType(organizerId);
            }
        }

        /// <summary>
        /// Edit ExhibitorIndustryType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorIndustryTypeId"></param>
        /// <param name="exhibitorIndustryTypeDTO"></param>
        /// <returns></returns>
        [Route("exhibitorIndustryType/{exhibitorIndustryTypeId:guid}")]
        [ModelValidator]
        public IHttpActionResult Put(Guid organizerId, Guid exhibitorIndustryTypeId, ExhibitorIndustryTypeDTO exhibitorIndustryTypeDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorIndustryTypeId);
                if (exhibitorIndustryType == null)
                    return NotFound();

                exhibitorIndustryType.Update(exhibitorIndustryType.IndustryType, exhibitorIndustryTypeDTO.Color); // DTO to entity mapping
                unitOfWork.SaveChanges();
            }
            return GetExhibitorIndustryType(organizerId);
        }

        /// <summary>
        /// Get Exhibitor Feedback For Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("eventFeedback/{eventId:guid}")]
        [ResponseType(typeof(ExhibitorFeedbackDTO))]
        public IHttpActionResult GetEventExhibitorFeedback(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId };
                var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                var exhibitorFeedbackList = _exhibitorFeedbackRepository.Find(exhibitorFeedbackSepcification);

                List<ExhibitorFeedbackDTO> visitorFeedbackDTOList = new List<ExhibitorFeedbackDTO>();

                foreach (ExhibitorFeedback exhibitorFeedback in exhibitorFeedbackList)
                {
                    ExhibitorFeedbackDTO visitorFeedbackDTO = new ExhibitorFeedbackDTO
                    {
                        CompanyName = exhibitorFeedback.Exhibitor.CompanyName,
                        ExhibitorName = exhibitorFeedback.Exhibitor.Name,
                        Satisfaction = exhibitorFeedback.Satisfaction,
                        Objective = exhibitorFeedback.Objective,
                        TargetAudience = exhibitorFeedback.TargetAudience,
                        QualityOfVisitor = exhibitorFeedback.QualityOfVisitor,
                        ExpectedBusiness = exhibitorFeedback.ExpectedBusiness,
                        IIMTFFacility = exhibitorFeedback.IIMTFFacility,
                        IIMTFSatisfaction = exhibitorFeedback.IIMTFSatisfaction,
                        IIMTFTeam = exhibitorFeedback.IIMTFTeam,
                        Comment = exhibitorFeedback.Comment
                    };
                    foreach (Venue singleCountry in exhibitorFeedback.MarketIntrested)
                    {
                        VenueDTO countryDTO = new VenueDTO
                        {
                            Id = singleCountry.Id,
                            City = singleCountry.City
                        };
                        visitorFeedbackDTO.MarketIntrested.Add(countryDTO);
                    }
                    visitorFeedbackDTOList.Add(visitorFeedbackDTO);
                }
                return Ok(visitorFeedbackDTOList);
            }
        }

        /// <summary>
        /// Get Event Feedback for Exhibitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{exhibitorId:guid}/eventFeedback/{eventId:guid}")]
        [ResponseType(typeof(ExhibitorFeedbackDTO))]
        public IHttpActionResult GetExhibitorFeedback(Guid organizerId, Guid exhibitorId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var exhibitorDetails = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorDetails == null)
                    return NotFound();

                var visitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, ExhibitorId = exhibitorDetails.Id };
                var visitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                var visitorFeedbackList = _exhibitorFeedbackRepository.Find(visitorFeedbackSepcification);

                ExhibitorFeedbackDTO visitorFeedbackDTO = new ExhibitorFeedbackDTO
                {
                    CompanyName = visitorFeedbackList.FirstOrDefault().Exhibitor.CompanyName,
                    ExhibitorName = visitorFeedbackList.FirstOrDefault().Exhibitor.Name,
                    Satisfaction = visitorFeedbackList.FirstOrDefault().Satisfaction,
                    Objective = visitorFeedbackList.FirstOrDefault().Objective,
                    TargetAudience = visitorFeedbackList.FirstOrDefault().TargetAudience,
                    QualityOfVisitor = visitorFeedbackList.FirstOrDefault().QualityOfVisitor,
                    ExpectedBusiness = visitorFeedbackList.FirstOrDefault().ExpectedBusiness,
                    IIMTFFacility = visitorFeedbackList.FirstOrDefault().IIMTFFacility,
                    IIMTFSatisfaction = visitorFeedbackList.FirstOrDefault().IIMTFSatisfaction,
                    IIMTFTeam = visitorFeedbackList.FirstOrDefault().IIMTFTeam,
                    Comment = visitorFeedbackList.FirstOrDefault().Comment
                };

                return Ok(visitorFeedbackDTO);
            }
        }

        /// <summary>
        /// Add Event Feedback For Exhibitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorFeedbackDTO"></param>
        /// <returns></returns>
        [Route("{exhibitorId:guid}/eventFeedback/{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(VisitorFeedbackDTO))]
        public IHttpActionResult PostExhibitorFeedback(Guid organizerId, Guid exhibitorId, Guid eventId, ExhibitorFeedbackDTO exhibitorFeedbackDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var exhibitorDetails = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorDetails == null)
                    return NotFound();

                var exhibitorFeedbackSearchCriteria = new ExhibitorFeedbackSearchCriteria { EventId = eventId, ExhibitorId = exhibitorDetails.Id };
                var exhibitorFeedbackSepcification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackSearchCriteria);
                var exhibitorFeedbackSepcificationList = _exhibitorFeedbackRepository.Find(exhibitorFeedbackSepcification);

                if (exhibitorFeedbackSepcificationList.Count() == 0)
                {
                    ExhibitorFeedback createExhibitorFeedback = ExhibitorFeedback.Create(exhibitorFeedbackDTO.Satisfaction,
                        exhibitorFeedbackDTO.Objective, exhibitorFeedbackDTO.TargetAudience,
                        exhibitorFeedbackDTO.QualityOfVisitor, exhibitorFeedbackDTO.ExpectedBusiness, exhibitorFeedbackDTO.IIMTFSatisfaction,
                        exhibitorFeedbackDTO.IIMTFTeam, exhibitorFeedbackDTO.IIMTFFacility, exhibitorFeedbackDTO.Comment);

                    foreach (VenueDTO venueDetailsDTO in exhibitorFeedbackDTO.MarketIntrested)
                    {
                        var venue = _venueRepository.GetById(venueDetailsDTO.Id);
                        createExhibitorFeedback.MarketIntrested.Add(venue);
                    }
                    createExhibitorFeedback.Event = eventDetails;
                    createExhibitorFeedback.Exhibitor = exhibitorDetails;

                    _exhibitorFeedbackRepository.Add(createExhibitorFeedback);
                    unitOfWork.SaveChanges();
                }
                return GetExhibitorFeedback(organizerId, exhibitorId, eventId);
            }
        }

        private static ExhibitorTypeDTO GetDTO(ExhibitorType exhibitorType)
        {
            return new ExhibitorTypeDTO
            {
                Id = exhibitorType.Id,
                Type = exhibitorType.Type
            };
        }

        private static ExhibitorIndustryTypeDTO GetDTO(ExhibitorIndustryType exhibitorIndustryType)
        {
            return new ExhibitorIndustryTypeDTO
            {
                Id = exhibitorIndustryType.Id,
                IndustryType = exhibitorIndustryType.IndustryType
            };
        }

        private static ExhibitorSearchDTO GetDTO(Booking booking, IList<StallBooking> stallBookingList)
        {
            ExhibitorSearchDTO exhibitorDTO = new ExhibitorSearchDTO();
            if (booking.Exhibitor != null)
            {
                exhibitorDTO.Name = booking.Exhibitor.CompanyName;
                exhibitorDTO.Stalls = stallBookingList.Select(x => GetDTO(x.Stall)).ToList();
                exhibitorDTO.Categories = booking.Exhibitor.Categories.Select(x => GetDTO(x)).ToList();
                exhibitorDTO.Country = GetDTO(booking.Exhibitor.Country);
                exhibitorDTO.State = GetDTO(booking.Exhibitor.State);
            }
            return (exhibitorDTO);
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
                Comment = exhibitor.Comment,
                //Stalls = exhibitor..Select(x => GetDTO(x)).ToList(),
                Categories = exhibitor.Categories.Select(x => GetDTO(x)).ToList(),
                Country = GetDTO(exhibitor.Country),
                State = GetDTO(exhibitor.State)
            };
        }
        private static ExhibitorDTO GetDTO(ExhibitorEnquiry exhibitor)
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
                PinCode = exhibitor.PinCode,
                Comment = exhibitor.Comment,
                EnquiryDate = exhibitor.CreatedOn.ToString("dd-MMM-yyyy"),
                //Stalls = exhibitor..Select(x => GetDTO(x)).ToList(),
                //Categories = exhibitor.Categories.Select(x => GetDTO(x)).ToList(),
                Country = GetDTO(exhibitor.Country),
                State = GetDTO(exhibitor.State)
            };
        }
        private static ExhibitorIndustryTypeDTO GetExhibitorIndustryTypeDTO(ExhibitorIndustryType exhibitorIndustryType)
        {
            return new ExhibitorIndustryTypeDTO()
            {
                Id = exhibitorIndustryType.Id,
                IndustryType = exhibitorIndustryType.IndustryType
            };
        }
        private static ExhibitorTypeDTO GetExhibitorTypeDTO(ExhibitorType exhibitorType)
        {
            return new ExhibitorTypeDTO()
            {
                Id = exhibitorType.Id,
                Type = exhibitorType.Type
            };
        }
        private static ExhibitorStatusDTO GetExhibitorStatusDTO(ExhibitorStatus exhibitorStatus)
        {
            return new ExhibitorStatusDTO()
            {
                Id = exhibitorStatus.Id,
                Status = exhibitorStatus.Status
            };
        }

        private static ExhibitorDTO GetExhibitorDTO(Exhibitor exhibitor)
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
                Address = exhibitor.Address
            };
        }

        private static StallDTO GetDTO(Stall stall)
        {
            return new StallDTO
            {
                StallNo = stall.StallNo,
                Pavilion = GetDTO(stall.Section)
            };
        }

        private static PavilionDTO GetDTO(Section pavilion)
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
                    Id = category.Id,
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
                    Id = country.Id,
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
                    Id = state.Id,
                    Name = state.Name
                };
            }
        }
        private void SendeMail(string htmlPage, string fileName, Exhibitor exhibitor, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.Send(exhibitor.EmailId, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }

        }

        private void SendeMail(string htmlPage, string fileName, ExhibitorEnquiry exhibitor, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.Send(exhibitor.EmailId, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }
        }

        private void SendeMails(string htmlPage, string fileName, string emailId, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.Send(emailId, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }

        }
    }
}
