using Modules.EventManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/visitorCardPayment")]
    public class VisitorCardPaymentController : ApiController
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
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private VisitorEventTicketRepository<VisitorCardPayment> _visitorEventTicketRepository = new VisitorEventTicketRepository<VisitorCardPayment>();

        /// <summary>
        /// Get All Visitors CardPayments Data for Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorCardPaymentDTO))]
        public IHttpActionResult GetVisitorCardPayment(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId };
                var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                var totalVisitorCardPayments = _visitorEventTicketRepository.TotalCardPaymentOfEvent(eventDetails.Id);
                var visitorCardPaymentList = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification)
                                             .OrderBy(x => x.TransactionDate)
                                             .ThenBy(x => x.Invoice)
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize);

                var totalCount = visitorCardPaymentCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<VisitorCardPaymentDTO> listOfCardPaymentDTO = new List<VisitorCardPaymentDTO>();
                foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentList)
                {
                    VisitorCardPaymentDTO singleCardPaymentDTO = new VisitorCardPaymentDTO()
                    {
                        Name = singleCardPayment.Name,
                        MobileNo = singleCardPayment.MobileNo,
                        TransactionDate = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy"),
                        TransactionId = singleCardPayment.TransactionId,
                        Invoice = singleCardPayment.Invoice,
                        Amount = singleCardPayment.Amount
                    };

                    if (singleCardPayment.Exhibitor != null)
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.Exhibitor.CompanyName;
                    }
                    else
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.ExhibitorName;
                    }

                    listOfCardPaymentDTO.Add(singleCardPaymentDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    totalAmount = totalVisitorCardPayments,
                    listOfBookingDTO = listOfCardPaymentDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get all CardPayment Data by Date 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="paymentDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/paymentDate/{paymentDate}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorCardPaymentDTO))]
        public IHttpActionResult GetVisitorCardPaymentByDate(Guid organizerId, Guid eventId, string paymentDate, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId, CardPaymentDate = Convert.ToDateTime(paymentDate) };
                var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                var totalVisitorCardPayments = _visitorEventTicketRepository.TotalCardPaymentOfAllDay(eventDetails.Id, Convert.ToDateTime(paymentDate));
                var visitorCardPaymentList = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification)
                                             .OrderBy(x => x.TransactionDate)
                                             .ThenBy(x => x.Invoice)
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize);

                var totalCount = visitorCardPaymentCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<VisitorCardPaymentDTO> listOfCardPaymentDTO = new List<VisitorCardPaymentDTO>();
                foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentList)
                {
                    VisitorCardPaymentDTO singleCardPaymentDTO = new VisitorCardPaymentDTO()
                    {
                        Name = singleCardPayment.Name,
                        MobileNo = singleCardPayment.MobileNo,
                        TransactionDate = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy"),
                        TransactionId = singleCardPayment.TransactionId,
                        Invoice = singleCardPayment.Invoice,
                        Amount = singleCardPayment.Amount
                    };

                    if (singleCardPayment.Exhibitor != null)
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.Exhibitor.CompanyName;
                    }
                    else
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.ExhibitorName;
                    }

                    listOfCardPaymentDTO.Add(singleCardPaymentDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    totalAmount = totalVisitorCardPayments,
                    listOfBookingDTO = listOfCardPaymentDTO
                };
                return Ok(result);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="eventId"></param>
        ///// <returns></returns>
        //[Route("event/{eventId:guid}/loginList")]
        //[ResponseType(typeof(VisitorCardPaymentListDTO))]
        //public IHttpActionResult GetVisitorCardPaymentsByLogin(Guid organizerId, Guid eventId)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
        //    {
        //        var organizer = _organizerRepository.GetById(organizerId);
        //        if (organizer == null)
        //            return NotFound();

        //        var eventDetails = _eventRepository.GetById(eventId);
        //        if (eventDetails == null)
        //            return NotFound();

        //        var loginList = _visitorEventTicketRepository.CardPaymentDistinctLogin(eventDetails.Id);
        //        List<VisitorCardPaymentListDTO> visitorCardPaymentListDTO = new List<VisitorCardPaymentListDTO>();

        //        foreach (Login loginDetails in loginList)
        //        {
        //            var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventDetails.Id, LoginId = loginDetails.Id };
        //var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
        //var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
        //VisitorCardPaymentListDTO singleVisitorCardPaymentListDTO = new VisitorCardPaymentListDTO()
        //{
        //    LoginId = loginDetails.Id,
        //    LoignUser = loginDetails.UserName,
        //    totalEntry = visitorCardPaymentCount
        //};
        //visitorCardPaymentListDTO.Add(singleVisitorCardPaymentListDTO);
        //        }
        //        return Ok(visitorCardPaymentListDTO);
        //    }
        //}

        /// <summary>
        /// Get Cardpayments details by Login
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/loginList")]
        [ResponseType(typeof(VisitorCardPaymentListDTO))]
        public IHttpActionResult GetVisitorCardPaymentsByLogin(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var loginList = _visitorEventTicketRepository.CardPaymentDistinctLogin(eventDetails.Id);
                List<VisitorCardPaymentListDTO> visitorCardPaymentListDTO = new List<VisitorCardPaymentListDTO>();

                foreach (Guid loginId in loginList)
                {
                    var loginDetails = _loginRepository.GetById(loginId);

                    var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventDetails.Id, LoginId = loginDetails.Id };
                    var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                    var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                    VisitorCardPaymentListDTO singleVisitorCardPaymentListDTO = new VisitorCardPaymentListDTO()
                    {
                        LoginId = loginDetails.Id,
                        LoignUser = loginDetails.UserName,
                        totalEntry = visitorCardPaymentCount
                    };
                    visitorCardPaymentListDTO.Add(singleVisitorCardPaymentListDTO);
                }
                return Ok(visitorCardPaymentListDTO);
            }
        }

        /// <summary>
        /// Get visitor CardPayment data by login
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="loginId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/login/{loginId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorCardPaymentDTO))]
        public IHttpActionResult GetVisitorCardPaymentListByLogin(Guid organizerId, Guid eventId, Guid loginId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var loginDetails = _loginRepository.GetById(loginId);
                if (loginDetails == null)
                    return NotFound();

                var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventDetails.Id, LoginId = loginDetails.Id };
                var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                var visitorCardPaymentsTotal = _visitorEventTicketRepository.TotalCardPaymentOfEventByLogin(eventDetails.Id, loginDetails.Id);
                var visitorCardPaymentList = _visitorCardPaymentRepository.Find(visitorCardPaymentSepcification)
                                             .OrderBy(x => x.TransactionDate)
                                             .ThenBy(x => x.Invoice)
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize);


                var totalCount = visitorCardPaymentCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<VisitorCardPaymentDTO> listOfCardPaymentDTO = new List<VisitorCardPaymentDTO>();
                foreach (VisitorCardPayment singleCardPayment in visitorCardPaymentList)
                {
                    VisitorCardPaymentDTO singleCardPaymentDTO = new VisitorCardPaymentDTO()
                    {
                        Name = singleCardPayment.Name,
                        MobileNo = singleCardPayment.MobileNo,
                        TransactionDate = singleCardPayment.TransactionDate.ToString("dd/MMM/yyyy"),
                        TransactionId = singleCardPayment.TransactionId,
                        Invoice = singleCardPayment.Invoice,
                        Amount = singleCardPayment.Amount
                    };

                    if (singleCardPayment.Exhibitor != null)
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.Exhibitor.CompanyName;
                    }
                    else
                    {
                        singleCardPaymentDTO.ExhibitorName = singleCardPayment.ExhibitorName;
                    }
                    listOfCardPaymentDTO.Add(singleCardPaymentDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    totalAmount = visitorCardPaymentsTotal,
                    listOfBookingDTO = listOfCardPaymentDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get CardPayments Details By Login for all dates
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/login/{loginId:guid}/cardPaymentbyDate")]
        [ResponseType(typeof(VisitorCardPaymentByDateDTO))]
        public IHttpActionResult GetVisitorCardPaymentListByDate(Guid organizerId, Guid eventId, Guid loginId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var loginDetails = _loginRepository.GetById(loginId);
                if (loginDetails == null)
                    return NotFound();

                List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

                DateTime currentValidStartDate;
                currentValidStartDate = eventDetails.StartDate.AddDays(1);

                for (DateTime date = currentValidStartDate; date <= eventDetails.EndDate; date = date.AddDays(1))
                {
                    allAvailableDatesForBooking.Add(date);
                }
                List<VisitorCardPaymentByDateDTO> visitorCardPaymentByDateListDTO = new List<VisitorCardPaymentByDateDTO>();
                foreach (DateTime singleDate in allAvailableDatesForBooking)
                {
                    var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId, LoginId = loginDetails.Id, CardPaymentDate = singleDate };
                    var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                    var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                    var totalCardPaymentOfDay = _visitorEventTicketRepository.TotalCardPaymentOfDay(eventDetails.Id, singleDate, loginDetails.Id);
                    VisitorCardPaymentByDateDTO visitorCardPaymentByDateDTO = new VisitorCardPaymentByDateDTO()
                    {
                        PaymentDate = singleDate.ToString("dd/MMM/yyyy"),
                        TotalCount = visitorCardPaymentCount,
                        TotalAmount = totalCardPaymentOfDay ?? 0
                    };

                    visitorCardPaymentByDateListDTO.Add(visitorCardPaymentByDateDTO);
                }
                return Ok(visitorCardPaymentByDateListDTO);
            }
        }

        /// <summary>
        /// Get CardPayment Details By Date
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/cardPaymentbyDate")]
        [ResponseType(typeof(VisitorCardPaymentByDateDTO))]
        public IHttpActionResult GetVisitorCardPaymentListAllByDate(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

                DateTime currentValidStartDate;
                currentValidStartDate = eventDetails.StartDate.AddDays(1);

                for (DateTime date = currentValidStartDate; date <= eventDetails.EndDate; date = date.AddDays(1))
                {
                    allAvailableDatesForBooking.Add(date);
                }
                List<VisitorCardPaymentByDateDTO> visitorCardPaymentByDateListDTO = new List<VisitorCardPaymentByDateDTO>();
                foreach (DateTime singleDate in allAvailableDatesForBooking)
                {
                    var visitorCardPaymentSearchCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventId, CardPaymentDate = singleDate };
                    var visitorCardPaymentSepcification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentSearchCriteria);
                    var visitorCardPaymentCount = _visitorCardPaymentRepository.Count(visitorCardPaymentSepcification);
                    var totalCardPaymentOfDay = _visitorEventTicketRepository.TotalCardPaymentOfAllDay(eventDetails.Id, singleDate);
                    VisitorCardPaymentByDateDTO visitorCardPaymentByDateDTO = new VisitorCardPaymentByDateDTO()
                    {
                        PaymentDate = singleDate.ToString("dd/MMM/yyyy"),
                        TotalCount = visitorCardPaymentCount,
                        TotalAmount = totalCardPaymentOfDay ?? 0
                    };

                    visitorCardPaymentByDateListDTO.Add(visitorCardPaymentByDateDTO);
                }
                return Ok(visitorCardPaymentByDateListDTO);
            }
        }

        /// <summary>
        /// Add Card Payment
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="loginId"></param>
        /// <param name="visitorFeedbackDTO"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/exhibitor/{exhibitorId:guid}/login/{loginId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(VisitorCardPaymentDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid eventId, Guid exhibitorId, Guid loginId, VisitorCardPaymentDTO visitorFeedbackDTO)
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

                var loginDetails = _loginRepository.GetById(loginId);
                if (loginDetails == null)
                    return NotFound();

                var visitorCardPayment = VisitorCardPayment.Create(visitorFeedbackDTO.Name, visitorFeedbackDTO.MobileNo, exhibitorDetails.Name,
                    visitorFeedbackDTO.TransactionId, visitorFeedbackDTO.Invoice, visitorFeedbackDTO.Amount,
                    Convert.ToDateTime(visitorFeedbackDTO.TransactionDate));
                visitorCardPayment.Event = eventDetails;
                visitorCardPayment.Login = loginDetails;
                visitorCardPayment.Exhibitor = exhibitorDetails;
                _visitorCardPaymentRepository.Add(visitorCardPayment);

                unitOfWork.SaveChanges();
                return Ok("Added Successfully");
            }
        }
    }
}
