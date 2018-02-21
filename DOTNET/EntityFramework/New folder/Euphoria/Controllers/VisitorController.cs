using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.SMS;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/visitors")]
    public class VisitorController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();
        private IRepository<Ticket> _ticketRepository = new EntityFrameworkRepository<Ticket>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<VisitorFeedback> _visitorFeedbackRepository = new EntityFrameworkRepository<VisitorFeedback>();
        private IRepository<VisitorCardPayment> _visitorCardPaymentRepository = new EntityFrameworkRepository<VisitorCardPayment>();
        private IRepository<GeneralMaster> _generalMasterRepository = new EntityFrameworkRepository<GeneralMaster>();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private IRepository<VisitorBookingExhibitorDiscount> _visitorBookingExhibitorDiscountTypeRepository = new EntityFrameworkRepository<VisitorBookingExhibitorDiscount>();
        private readonly IRepository<VisitorDiscountCouponMap> _visitorDiscountCouponMapRepository = new EntityFrameworkRepository<VisitorDiscountCouponMap>();
        private VisitorEventTicketRepository<VisitorCardPayment> _visitorEventTicketRepository = new VisitorEventTicketRepository<VisitorCardPayment>();

        /// <summary>
        /// Get all Visitors
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorDTO[]))]
        public IHttpActionResult Get(Guid organizerId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var orgnizer = _organizerRepository.GetById(organizerId);
                if (orgnizer == null)
                    return NotFound();

                var visitorCount = _visitorRepository.Count(new GetAllSpecification<Visitor>());
                var visitor = _visitorRepository.Find(new GetAllSpecification<Visitor>())
                              .OrderByDescending(x => x.CreatedOn)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize);

                var totalCount = visitorCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (visitor.Count() == 0)
                    return NotFound();
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = visitor.Select(x => GetDTO(x))
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Single Visitor 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(VisitorDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(id);
                if (visitor == null || visitor.Organizer.Id != organizerId)
                    return NotFound();

                VisitorDTO visitorDTO = GetDTO(visitor);

                return Ok(visitorDTO);
            }
        }

        /// <summary>
        /// Get All Visitor for Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorDTO[]))]
        public IHttpActionResult GetByEvent(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                var criteria = new EventTicketReportSearchCriteria
                {
                    StartDate = eventDetail.StartDate,
                    EndDate = eventDetail.EndDate,
                    IsPayatLocation = false,
                    IsPayOnline = false,
                    IsWeb = false,
                    IsMobile = false,
                    PaymentCompleted = true
                };
                var sepcification = new EventTicketReportSpecificationForSearch(criteria);
                var eventTicketsCount = _eventTicketRepository.Count(sepcification);
                var eventTickets = _eventTicketRepository.Find(sepcification)
                                .OrderByDescending(x => x.CreatedOn)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize);

                var totalCount = eventTicketsCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<VisitorDTO> listVisitor = new List<VisitorDTO>();

                foreach (EventTicket singleEventTicket in eventTickets)
                {
                    VisitorDTO visitorDTO = new VisitorDTO();
                    visitorDTO = listVisitor.Find(x => x.EmailId == singleEventTicket.Visitor.EmailId);
                    if (visitorDTO == null)
                    {
                        VisitorDTO singleVisitorDTO = GetDTO(singleEventTicket.Visitor);
                        listVisitor.Add(singleVisitorDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listVisitor
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Event Feedback of Visitor For Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{visitorId:guid}/eventFeedback/{eventId:guid}")]
        [ResponseType(typeof(VisitorFeedbackDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid visitorId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var visitordetails = _visitorRepository.GetById(visitorId);
                if (visitordetails == null)
                    return NotFound();

                var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, VisitorId = visitordetails.Id };
                var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                var visitorFeedbackList = _visitorFeedbackRepository.Find(visitorFeedbackSepcification);

                if (visitorFeedbackList.Count() == 0)
                    return NotFound();

                VisitorFeedbackDTO visitorFeedbackDTO = new VisitorFeedbackDTO
                {
                    SpendRange = visitorFeedbackList.FirstOrDefault().SpendRange,
                    EventRating = visitorFeedbackList.FirstOrDefault().EventRating,
                    RecommendToOther = visitorFeedbackList.FirstOrDefault().RecommendToOther,
                    ReasonForVisiting = visitorFeedbackList.FirstOrDefault().ReasonForVisiting,
                    KnowAboutUs = visitorFeedbackList.FirstOrDefault().KnowAboutUs,
                    Comment = visitorFeedbackList.FirstOrDefault().Comment
                };

                foreach (Category singleCategory in visitorFeedbackList.FirstOrDefault().Categories)
                {
                    CategoryDTO categoryDTO = new CategoryDTO
                    {
                        Id = singleCategory.Id,
                        Name = singleCategory.Name
                    };
                    visitorFeedbackDTO.Categories.Add(categoryDTO);
                }

                foreach (ExhibitorType singleExhibitorType in visitorFeedbackList.FirstOrDefault().ExhibitorTypes)
                {
                    ExhibitorTypeDTO exhibitorTypeDTO = new ExhibitorTypeDTO
                    {
                        Id = singleExhibitorType.Id,
                        Type = singleExhibitorType.Type
                    };
                    visitorFeedbackDTO.ExhibitorTypes.Add(exhibitorTypeDTO);
                }

                foreach (Country singleCountry in visitorFeedbackList.FirstOrDefault().Countries)
                {
                    CountryDTO countryDTO = new CountryDTO
                    {
                        Id = singleCountry.Id,
                        Name = singleCountry.Name
                    };
                    visitorFeedbackDTO.Countries.Add(countryDTO);
                }
                return Ok(visitorFeedbackDTO);
            }
        }

        /// <summary>
        /// Get all Feedback For event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("eventFeedback/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorFeedbackDTO))]
        public IHttpActionResult GetEventFeedback(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId };
                var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSepcification);
                var visitorFeedbackList = _visitorFeedbackRepository.Find(visitorFeedbackSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var totalCount = visitorFeedbackCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (visitorFeedbackList.Count() == 0)
                    return NotFound();

                List<VisitorFeedbackDTO> visitorFeedbackDTOList = new List<VisitorFeedbackDTO>();
                foreach (VisitorFeedback visitorFeedback in visitorFeedbackList)
                {
                    VisitorFeedbackDTO visitorFeedbackDTO = new VisitorFeedbackDTO
                    {
                        VisitorEmailId = visitorFeedback.Visitor.EmailId,
                        PhoneNumber = visitorFeedback.Visitor.MobileNo,
                        Pincode = visitorFeedback.Visitor.Pincode,
                        SpendRange = visitorFeedback.SpendRange,
                        EventRating = visitorFeedback.EventRating,
                        RecommendToOther = visitorFeedback.RecommendToOther,
                        ReasonForVisiting = visitorFeedback.ReasonForVisiting,
                        KnowAboutUs = visitorFeedback.KnowAboutUs,
                        Comment = visitorFeedback.Comment
                    };

                    foreach (Category singleCategory in visitorFeedback.Categories)
                    {
                        CategoryDTO categoryDTO = new CategoryDTO
                        {
                            Id = singleCategory.Id,
                            Name = singleCategory.Name
                        };
                        visitorFeedbackDTO.Categories.Add(categoryDTO);
                    }

                    foreach (ExhibitorType singleExhibitorType in visitorFeedback.ExhibitorTypes)
                    {
                        ExhibitorTypeDTO exhibitorTypeDTO = new ExhibitorTypeDTO
                        {
                            Id = singleExhibitorType.Id,
                            Type = singleExhibitorType.Type
                        };
                        visitorFeedbackDTO.ExhibitorTypes.Add(exhibitorTypeDTO);
                    }

                    foreach (Country singleCountry in visitorFeedback.Countries)
                    {
                        CountryDTO countryDTO = new CountryDTO
                        {
                            Id = singleCountry.Id,
                            Name = singleCountry.Name
                        };
                        visitorFeedbackDTO.Countries.Add(countryDTO);
                    }
                    visitorFeedbackDTOList.Add(visitorFeedbackDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = visitorFeedbackDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Add Visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        [ResponseType(typeof(VisitorDTO))]
        public IHttpActionResult Post(Guid organizerId, VisitorDTO visitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var criteria = new VisitorSearchCriteria { EmailId = visitorDTO.EmailId, PhoneNumber = visitorDTO.MobileNo };
                var sepcification = new VisitorSpecificationForSearch(criteria);
                var visitors = _visitorRepository.Find(sepcification);
                Visitor visitorDetails = new Visitor();
                if (visitors.Count() == 0)
                {
                    visitorDetails = Visitor.Create(visitorDTO.FirstName, visitorDTO.LastName, visitorDTO.Address, visitorDTO.MobileNo, visitorDTO.Pincode, visitorDTO.EmailId, visitorDTO.DateOfBirth, visitorDTO.Gender, visitorDTO.Education, visitorDTO.Income, null);
                    visitorDetails.Organizer = organizer;
                    _visitorRepository.Add(visitorDetails);

                }
                else
                {
                    visitorDetails = visitors.FirstOrDefault();
                    visitorDetails.Pincode = visitorDetails.Pincode;
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, visitorDetails.Id);
            }
        }

        /// <summary>
        /// visitor Login
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorLoginDTO"></param>
        /// <returns></returns>
        [Route("Login")]
        [ModelValidator]
        [ResponseType(typeof(VisitorLoginDTO))]
        public IHttpActionResult Post(Guid organizerId, VisitorLoginDTO visitorLoginDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new VisitorSearchCriteria { EmailId = visitorLoginDTO.EmailId, PhoneNumber = visitorLoginDTO.MobileNo };
                var sepcification = new VisitorSpecificationForSearch(criteria);
                var visitors = _visitorRepository.Find(sepcification);
                if (visitors.Count() == 0)
                    return Content(HttpStatusCode.NotFound, "Invalid EmailId or Password.");
                else
                    return Get(organizerId, visitors.FirstOrDefault().Id);
            }
        }

        /// <summary>
        /// Edit Visitor 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <param name="visitorDTO"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(VisitorDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid id, VisitorDTO visitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                {
                    return NotFound();
                }
                var visitorToUpdate = _visitorRepository.GetById(id);
                if (visitorToUpdate == null)
                {
                    return NotFound();
                }

                if (visitorToUpdate == null || visitorToUpdate.Organizer.Id != organizerId)
                    return NotFound();

                visitorToUpdate.Update(visitorDTO.FirstName, visitorDTO.LastName, visitorDTO.Address,
                    visitorDTO.MobileNo, visitorDTO.Pincode, visitorDTO.EmailId,
                   visitorDTO.DateOfBirth, visitorDTO.Gender, visitorDTO.Education, visitorDTO.Income);

                unitOfWork.SaveChanges();
            }
            return Get(organizerId, id);
        }

        /// <summary>
        /// Activate visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <param name="activationDTO"></param>
        /// <returns></returns>
        [Route("{id}/Activation")]
        public IHttpActionResult Post(Guid organizerId, Guid id, VisitorActivationDTO activationDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Visitor visitor = _visitorRepository.GetById(id);

                if (visitor == null)
                    return NotFound();

                if (activationDTO.Action == ActionType.Send)
                {
                    string code = visitor.GenerateActivationCode();
                    string message = string.Format("Hello {0},Your activation code is:{1} for GSMKTG registration. "
                        + "DO NOT share it with anyone. Thank You", visitor.FirstName, code);

                    SolutionInfoTechSMSService smsService = new SolutionInfoTechSMSService();
                    NotificationService notificationSvc = new NotificationService(smsService, visitor.MobileNo, message);
                    notificationSvc.Send();
                    unitOfWork.SaveChanges();
                    return Ok(code);
                }

                if (activationDTO.Action == ActionType.Verify)
                {
                    visitor.ValidateActivationCode(activationDTO.Code);
                    visitor.isMobileNoVerified = true;
                }
                unitOfWork.SaveChanges();

                return Ok("Validated");
            }
        }

        /// <summary>
        /// Add Visitor Feedback
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="eventId"></param>
        /// <param name="visitorFeedbackDTO"></param>
        /// <returns></returns>
        [Route("{visitorId:guid}/eventFeedback/{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(VisitorFeedbackDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid visitorId, Guid eventId, VisitorFeedbackDTO visitorFeedbackDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var visitorDetails = _visitorRepository.GetById(visitorId);
                if (visitorDetails == null)
                    return NotFound();

                var visitorFeedbackSearchCriteria = new VisitorFeedbackSearchCriteria { EventId = eventId, VisitorId = visitorDetails.Id };
                var visitorFeedbackSepcification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackSearchCriteria);
                var visitorFeedbackSepcificationList = _visitorFeedbackRepository.Find(visitorFeedbackSepcification);

                if (visitorFeedbackSepcificationList.Count() == 0)
                {
                    VisitorFeedback createVisitorFeedback = VisitorFeedback.Create(visitorFeedbackDTO.SpendRange,
                        visitorFeedbackDTO.EventRating, visitorFeedbackDTO.RecommendToOther,
                        visitorFeedbackDTO.ReasonForVisiting, visitorFeedbackDTO.KnowAboutUs, visitorFeedbackDTO.Comment);

                    createVisitorFeedback.Event = eventDetails;
                    createVisitorFeedback.Visitor = visitorDetails;

                    if (visitorFeedbackDTO.Categories.Count() != 0)
                    {
                        foreach (CategoryDTO singleCategory in visitorFeedbackDTO.Categories)
                        {
                            Category categoryAdd = _categoryRepository.GetById(singleCategory.Id);
                            createVisitorFeedback.Categories.Add(categoryAdd);
                        }
                    }

                    if (visitorFeedbackDTO.Countries.Count() != 0)
                    {
                        foreach (CountryDTO singleCountry in visitorFeedbackDTO.Countries)
                        {
                            Country countryAdd = _countryRepository.GetById(singleCountry.Id);
                            createVisitorFeedback.Countries.Add(countryAdd);
                        }
                    }

                    if (visitorFeedbackDTO.ExhibitorTypes.Count() != 0)
                    {
                        foreach (ExhibitorTypeDTO singleExhibitorType in visitorFeedbackDTO.ExhibitorTypes)
                        {
                            ExhibitorType exhibitorTypeAdd = _exhibitorTypeRepository.GetById(singleExhibitorType.Id);
                            createVisitorFeedback.ExhibitorTypes.Add(exhibitorTypeAdd);
                        }
                    }

                    _visitorFeedbackRepository.Add(createVisitorFeedback);
                    unitOfWork.SaveChanges();
                }
                return Get(organizerId, visitorId, eventId);
            }
        }

        /// <summary>
        /// Send Feedback SMS To all Visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/sendFeedbackMessage")]
        [ResponseType(typeof(VisitorDTO[]))]
        public IHttpActionResult SendFeedbackMessage(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                //var criteria = new EventTicketReportSearchCriteria { StartDate = eventDetail.StartDate, EndDate = eventDetail.EndDate, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, IsMobile = false, PaymentCompleted = true };
                //var sepcification = new EventTicketReportSpecificationForSearch(criteria);
                //var totalVisitors = _visitorEventTicketRepository.TotalVisitorsList(sepcification, x => x.Visitor.MobileNo);

                //List<VisitorDTO> listVisitor = new List<VisitorDTO>();
                //var count = totalVisitors.Select(x => x.MobileNo).Distinct().Count();
                //foreach (string singleEventTicket in totalVisitors.Select(x => x.MobileNo).Distinct().Skip((1 - 1) * 10000).Take(10000).ToList())
                //{
                //    if (singleEventTicket.Count() == 10)
                //    {
                //        var visitorCriteria = new VisitorSearchCriteria { PhoneNumber = singleEventTicket };
                //        var visitorSepcification = new VisitorSpecificationForSearch(visitorCriteria);
                //        var visitor = _visitorRepository.Find(visitorSepcification);
                //        var url = string.Format("http://gsmktg.com/f/#/v/{0}", visitor.FirstOrDefault().Id);
                //        //SendFeedbackSMS(url, visitor.FirstOrDefault().MobileNo);
                //    }
                //}

                var visitor = _visitorRepository.GetById(new Guid("d1b20cde-0488-4633-9a55-57e7e4e9bf79"));
                var url = string.Format("http://gsmktg.com/f/#/v/{0}", visitor.Id);

                SendFeedbackSMS(url, visitor.MobileNo);
                return Ok();
            }
        }

        private void SendFeedbackSMS(string url, string mobileNo)
        {
            //    var messageText = "";

            //    messageText = "Thanks for visiting IIMTF’17. Please give feedback to improve Your future shopping experience {0}";
            //    messageText = string.Format(messageText, url);

            SolutionInfoTechSMSService smsService = new SolutionInfoTechSMSService();
            NotificationService notificationService = new NotificationService(smsService, mobileNo, "hi");
            notificationService.Send();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="eventId"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="pageNumber"></param>
        ///// <returns></returns>
        //[Route("event/{eventId:guid}/visitorCardPayment/{pageSize}/{pageNumber}")]
        //[ResponseType(typeof(ExhibitorDTO))]
        //public IHttpActionResult GetVisitorCardPayment(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
        //    {
        //        var organizer = _organizerRepository.GetById(organizerId);
        //        if (organizer == null)
        //            return NotFound();

        //        var eventDetails = _eventRepository.GetById(eventId);
        //        if (eventDetails == null)
        //            return NotFound();

        //        var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId };
        //        var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
        //        var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
        //        var visitorCardPaymentList = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification)
        //                                     .OrderBy(x => x.TransactionDate)
        //                                     .Skip((pageNumber - 1) * pageSize)
        //                                     .Take(pageSize);
        //        var totalCount = visitorCardPaymentCount;
        //        var totalPages = Math.Ceiling((double)totalCount / pageSize);
        //        List<VisitorCardPaymentDTO> listOfCardPaymentDTO = new List<VisitorCardPaymentDTO>();
        //        foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentList)
        //        {
        //            VisitorCardPaymentDTO singleCardPaymentDTO = new VisitorCardPaymentDTO()
        //            {
        //                Name = singleCardPayment.Name,
        //                MobileNo = singleCardPayment.MobileNo,
        //                TransactionDate = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy"),
        //                ExhibitorName = singleCardPayment.ExhibitorName,
        //                TransactionId = singleCardPayment.TransactionId,
        //                Invoice = singleCardPayment.Invoice,
        //                Amount = singleCardPayment.Amount
        //            };
        //            listOfCardPaymentDTO.Add(singleCardPaymentDTO);
        //        }
        //        var result = new
        //        {
        //            totalCount = totalCount,
        //            totalPages = totalPages,
        //            listOfBookingDTO = listOfCardPaymentDTO
        //        };
        //        return Ok(result);
        //    }
        //}

        ///// <summary>
        ///// Delete Visitors all Data 
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="visitorId"></param>
        ///// <param name="eventId"></param>
        ///// <returns></returns>
        //[Route("{visitorId:guid}/delete/event/{eventId}")]
        //public IHttpActionResult Delete(Guid organizerId, Guid visitorId, Guid eventId)
        //{
        //    var organizer = _organizerRepository.GetById(organizerId);
        //    if (organizer == null)
        //        return NotFound();

        //    Visitor visitor = _visitorRepository.GetById(visitorId);

        //    var criteria = new EventTicketSearchCriteria { VisitorId = visitorId };
        //    var sepcification = new EventTicketSpecificationForSearch(criteria);
        //    var tickets = _eventTicketRepository.Find(sepcification);

        //    var discriteria = new VisitorDiscountCouponMapSearchCriteria { VisitorId = visitorId };
        //    var dissepcification = new VisitorDiscountCouponMapSpecificationForSearch(discriteria);
        //    var discounts = _visitorDiscountCouponMapRepository.Find(dissepcification);
        //    var feedcriteria = new VisitorFeedbackSearchCriteria { VisitorId = visitorId, EventId = eventId };
        //    var feedepcification = new VisitorFeedbackSpecificationForSearch(feedcriteria);
        //    var feedbacks = _visitorFeedbackRepository.Find(feedepcification);
        //    var bookcriteria = new VisitorBookingExhibitorDiscountSearchCriteria { VisitorId = visitorId, EventId = eventId };
        //    var bookepcification = new VisitorBookingExhibitorDiscountSpecificationForSearch(bookcriteria);
        //    var bookingvisitor = _visitorBookingExhibitorDiscountTypeRepository.Find(bookepcification);
        //    //var ticketCriteria = new TicketSearchCriteria { VisitorId = visitorId.ToString() };
        //    //var ticketSepcification = new TicketSpecificationForSearch(ticketCriteria);
        //    //var ticketss = _ticketRepository.Find(ticketSepcification);
        //    foreach (VisitorBookingExhibitorDiscount visitorBookingDiscout in bookingvisitor)
        //    {
        //        _visitorBookingExhibitorDiscountTypeRepository.Delete(visitorBookingDiscout.Id);
        //    }

        //    foreach (VisitorDiscountCouponMap visitorDiscountMap in discounts)
        //    {
        //        _visitorDiscountCouponMapRepository.Delete(visitorDiscountMap.Id);
        //    }

        //    foreach (VisitorFeedback visitorFeedBack in feedbacks)
        //    {
        //        _visitorFeedbackRepository.Delete(visitorFeedBack.Id);
        //    }
        //    if (tickets.Count() != 0)
        //    {
        //        foreach (EventTicket ticket in tickets)
        //        {
        //            var transactionCriteria = new TransactionSearchCriteria { TicketId = ticket.Id };
        //            var transactionSepcification = new TransactionSpecificationForSearch(transactionCriteria);
        //            var transaction = _transactionRepository.Find(transactionSepcification);
        //            if (transaction.Count() != 0)
        //            {
        //                _transactionRepository.Delete(transaction.FirstOrDefault().Id);
        //                _eventTicketRepository.Delete(ticket.Id);
        //            }
        //            else
        //            {
        //                _eventTicketRepository.Delete(ticket.Id);

        //            }
        //        }
        //    }

        //    //if (ticketss.Count() != 0)
        //    //{
        //    //    foreach (Ticket ticket in ticketss)
        //    //    {
        //    //        var transactionCriteria = new TransactionSearchCriteria { TicketId = ticket.Id };
        //    //        var transactionSepcification = new TransactionSpecificationForSearch(transactionCriteria);
        //    //        var transaction = _transactionRepository.Find(transactionSepcification);
        //    //        if (transaction.Count() != 0)
        //    //        {
        //    //            _transactionRepository.Delete(transaction.FirstOrDefault().Id);
        //    //            _ticketRepository.Delete(ticket.Id);
        //    //        }
        //    //        else
        //    //        {
        //    //            _ticketRepository.Delete(ticket.Id);

        //    //        }
        //    //    }

        //    //}
        //    _visitorRepository.Delete(visitor.Id);

        //    return Ok("deleted");
        //}



        ///// <summary>
        ///// This API is used for adding excel card payment data to database
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="eventId"></param>
        ///// <param name="visitorFeedbackDTO"></param>
        ///// <returns></returns>
        //[Route("event/{eventId:guid}/visitorCardPayment")]
        //[ModelValidator]
        //[ResponseType(typeof(VisitorCardPaymentDTO))]
        //public IHttpActionResult Post(Guid organizerId, Guid eventId, List<VisitorCardPaymentDTO> visitorFeedbackDTO)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
        //    {
        //        var organizer = _organizerRepository.GetById(organizerId);
        //        if (organizer == null)
        //            return NotFound();

        //        var eventDetails = _eventRepository.GetById(eventId);
        //        if (eventDetails == null)
        //            return NotFound();

        //        foreach (VisitorCardPaymentDTO singleCardPayment in visitorFeedbackDTO)
        //        {
        //            var visitorCardPayment = VisitorCardPayment.Create(singleCardPayment.Name, singleCardPayment.MobileNo, singleCardPayment.ExhibitorName, singleCardPayment.TransactionId, singleCardPayment.Invoice, singleCardPayment.Amount, Convert.ToDateTime(singleCardPayment.TransactionDate));
        //            visitorCardPayment.Event = eventDetails;
        //            _visitorCardPaymentRepository.Add(visitorCardPayment);
        //        }
        //        unitOfWork.SaveChanges();
        //        return GetVisitorCardPayment(organizerId, eventId, 5, 1);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="eventId"></param>
        ///// <param name="visitorFeedbackDTO"></param>
        ///// <returns></returns>
        //[Route("event/{eventId:guid}/visitorCardPayment")]
        //[ModelValidator]
        //[ResponseType(typeof(VisitorCardPaymentDTO))]
        //public IHttpActionResult Post(Guid organizerId, Guid eventId, VisitorCardPaymentDTO visitorFeedbackDTO)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
        //    {
        //        var organizer = _organizerRepository.GetById(organizerId);
        //        if (organizer == null)
        //            return NotFound();

        //        var eventDetails = _eventRepository.GetById(eventId);
        //        if (eventDetails == null)
        //            return NotFound();

        //           var visitorCardPayment = VisitorCardPayment.Create(visitorFeedbackDTO.Name, visitorFeedbackDTO.MobileNo, visitorFeedbackDTO.ExhibitorName, visitorFeedbackDTO.TransactionId, visitorFeedbackDTO.Invoice, visitorFeedbackDTO.Amount, Convert.ToDateTime(visitorFeedbackDTO.TransactionDate));
        //            visitorCardPayment.Event = eventDetails;
        //            _visitorCardPaymentRepository.Add(visitorCardPayment);

        //        unitOfWork.SaveChanges();
        //        return GetVisitorCardPayment(organizerId, eventId, 5, 1);
        //    }
        //}

        private static VisitorDTO GetDTO(Visitor visitor)
        {
            VisitorDTO visitorDTO = new VisitorDTO();

            visitorDTO.Id = visitor.Id;
            visitorDTO.FirstName = visitor.FirstName;
            visitorDTO.LastName = visitor.LastName;
            visitorDTO.Address = visitor.Address;
            visitorDTO.Pincode = visitor.Pincode;
            visitorDTO.MobileNo = visitor.MobileNo;
            visitorDTO.EmailId = visitor.EmailId;
            if (visitor.DateOfBirth != null)
            {
                visitorDTO.DateOfBirth = visitor.DateOfBirth.Value.ToString("dd-MMM-yyyy");
            }
            visitorDTO.DateOfBirth = visitor.DateOfBirth.ToString();
            visitorDTO.Gender = visitor.Gender;
            visitorDTO.Education = visitor.Education;
            visitorDTO.Income = visitor.Income;
            visitorDTO.IsMobileVerified = visitor.isMobileNoVerified;

            return (visitorDTO);
        }
    }
}