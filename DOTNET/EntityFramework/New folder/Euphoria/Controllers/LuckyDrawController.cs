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
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/luckyDraw")]
    public class LuckyDrawController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<LuckyDrawEventTicket> _luckyDraweventTicketRepository = new EntityFrameworkRepository<LuckyDrawEventTicket>();
        private IRepository<EventTicketType> _eventTicketTypeRepository = new EntityFrameworkRepository<EventTicketType>();
        private IRepository<LuckyDrawVisitor> _luckyDrawVisitorRepository = new EntityFrameworkRepository<LuckyDrawVisitor>();
        private TokenGenerationService _tokenService = new TokenGenerationService();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<EventExhibitionMap> _eventExhibitionMapRepository = new EntityFrameworkRepository<EventExhibitionMap>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="LuckyDrawVisitorDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        [ResponseType(typeof(LuckyDrawVisitorDTO))]
        public IHttpActionResult Post(Guid organizerId, LuckyDrawVisitorDTO visitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login loginDetails = _loginRepository.GetById(visitorDTO.StaffId);
                if (loginDetails == null)
                    return NotFound();

                LuckyDrawVisitor visitor = LuckyDrawVisitor.Create(visitorDTO.Name, null, null, visitorDTO.MobileNo, 0, visitorDTO.EmailId, null, visitorDTO.Gender, null, null, visitorDTO.FacebookId);
                visitor.Organizer = organizer;
                visitor.CreatedBy = loginDetails.Id;
                _luckyDrawVisitorRepository.Add(visitor);

                string tokenNumber = _tokenService.Generate();
                string txnIdForPaymentGateWay = GetPaymentGateWayTxnId();

                EventTicketType eventTicketType = _eventTicketTypeRepository.GetById(visitorDTO.EventTicketTypeId);
                if (eventTicketType == null)
                    return NotFound();

                var luckyDrawEventTicket = LuckyDrawEventTicket.Create(tokenNumber, Convert.ToDateTime(visitorDTO.TicketDate), visitorDTO.NumberOfTicket, visitorDTO.TotalPriceOfTicket, txnIdForPaymentGateWay, visitorDTO.PhysicalTicketSrNo);
                luckyDrawEventTicket.IsPayOnLocation = true;
                luckyDrawEventTicket.PaymentCompleted = true;
                luckyDrawEventTicket.EventTicketType = eventTicketType;
                luckyDrawEventTicket.CreatedBy = loginDetails.Id;
                luckyDrawEventTicket.LuckyDrawVisitor = visitor;

                _luckyDraweventTicketRepository.Add(luckyDrawEventTicket);
                unitOfWork.SaveChanges();

                return Ok("Register Succsufully");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorDTO"></param>
        /// <returns></returns>
        [Route("edit")]
        [ModelValidator]
        [ResponseType(typeof(LuckyDrawVisitorDTO))]
        public IHttpActionResult put(Guid organizerId, LuckyDrawVisitorDTO visitorDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login loginDetails = _loginRepository.GetById(visitorDTO.StaffId);
                if (loginDetails == null)
                    return NotFound();

                LuckyDrawEventTicket eventTicketDetails = _luckyDraweventTicketRepository.GetById(visitorDTO.ticketId);
                LuckyDrawVisitor visitorDetails = _luckyDrawVisitorRepository.GetById(eventTicketDetails.LuckyDrawVisitor.Id);

                visitorDetails.Update(visitorDTO.Name, visitorDetails.LastName, visitorDetails.Address,
                    visitorDTO.MobileNo, visitorDetails.Pincode, visitorDTO.EmailId, visitorDetails.DateOfBirth.ToString(),
                    visitorDTO.Gender, visitorDetails.Education, visitorDetails.Income);

                eventTicketDetails.Update(eventTicketDetails.TokenNumber, Convert.ToDateTime(visitorDTO.TicketDate), visitorDTO.NumberOfTicket
                    , visitorDTO.TotalPriceOfTicket);

                unitOfWork.SaveChanges();

                return Get(organizerId, eventTicketDetails.EventTicketType.Event.Id, visitorDTO.StaffId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId}")]
        [ResponseType(typeof(LuckyDrawVisitorDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var criteria = new LuckyDrawEventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate };
                var sepcification = new LuckyDrawEventTicketReportSpecificationForSearch(criteria);
                var eventTickets = _luckyDraweventTicketRepository.Find(sepcification);

                if (eventTickets == null)
                    return NotFound();
                List<LuckyDrawVisitorDTO> listOfLuckyDrawVisitorDTO = new List<LuckyDrawVisitorDTO>();

                foreach (LuckyDrawEventTicket singleEventTicket in eventTickets)
                {
                    if (singleEventTicket.PhysicalTicketSerialNo != null)
                    {
                        string userName = "";
                        if (singleEventTicket.CreatedBy != null)
                        {
                            Login login = _loginRepository.GetById(singleEventTicket.CreatedBy.Value);
                            userName = login.UserName;
                        }
                        LuckyDrawVisitorDTO singleLuckyDrawVisitorDTO = new LuckyDrawVisitorDTO
                        {
                            ticketId = singleEventTicket.Id,
                            Name = singleEventTicket.LuckyDrawVisitor.FirstName,
                            EmailId = singleEventTicket.LuckyDrawVisitor.EmailId,
                            MobileNo = singleEventTicket.LuckyDrawVisitor.MobileNo,
                            FacebookId = singleEventTicket.LuckyDrawVisitor.FacebookId,
                            TicketDate = singleEventTicket.TicketDate.ToString("dd/MMM/yyyy"),
                            NumberOfTicket = singleEventTicket.NumberOfTicket,
                            TotalPriceOfTicket = singleEventTicket.TotalPriceOfTicket,
                            Gender = singleEventTicket.LuckyDrawVisitor.Gender,
                            PhysicalTicketSrNo = singleEventTicket.PhysicalTicketSerialNo,
                            StaffName = userName
                        };
                        listOfLuckyDrawVisitorDTO.Add(singleLuckyDrawVisitorDTO);
                    }
                }
                return Ok(listOfLuckyDrawVisitorDTO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="sessionDate"></param>
        /// <returns></returns>
        [Route("event/{eventId}/sessions")]
        [ResponseType(typeof(SessionDTO))]
        public IHttpActionResult GetBySession(Guid organizerId, Guid eventId, string sessionDate)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var loginDetails = _loginRepository.Find(new GetAllSpecification<Login>());
                List<SessionDTO> sessionListDTO = new List<SessionDTO>();
                foreach (Login singleLogin in loginDetails)
                {
                    SessionDTO singleSession = new SessionDTO();
                    singleSession.StaffName = singleLogin.UserName;

                    if (singleLogin.RoleName.ToUpper() == "STAFF")
                    {
                        List<int> timeList = new List<int>()
                        {
                            11,1,3,5,7,9,10
                        };
                        foreach (int time in timeList)
                        {
                            int startTime;
                            int endTime;

                            if (time == 11)
                            {
                                startTime = 01;
                                endTime = 11;
                            }
                            else if (time == 1)
                            {
                                startTime = 11;
                                endTime = 13;
                            }
                            else if (time == 3)
                            {
                                startTime = 13;
                                endTime = 15;
                            }
                            else if (time == 5)
                            {
                                startTime = 15;
                                endTime = 17;
                            }
                            else if (time == 7)
                            {
                                startTime = 17;
                                endTime = 19;
                            }
                            else if (time == 9)
                            {
                                startTime = 19;
                                endTime = 21;
                            }
                            else
                            {
                                startTime = 21;
                                endTime = 24;
                            }

                            var criteria = new LuckyDrawEventTicketReportSearchCriteria { AdminId = singleLogin.Id, StartTime = startTime, EndTime = endTime, DateForSearch = Convert.ToDateTime(sessionDate) };
                            var sepcification = new LuckyDrawEventTicketReportSpecificationForSearch(criteria);
                            var eventTickets = _luckyDraweventTicketRepository.Find(sepcification);
                            SessionTimeCountDTO sessionTime = new SessionTimeCountDTO
                            {
                                Time = startTime.ToString() + "-" + endTime.ToString(),
                                count = eventTickets.Count()
                            };
                            singleSession.SessionTimeCountDTO.Add(sessionTime);
                        }
                        sessionListDTO.Add(singleSession);
                    }
                }
                return Ok(sessionListDTO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId}/allSessions")]
        [ResponseType(typeof(SessionDTO))]
        public IHttpActionResult GetAllSession(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var loginDetails = _loginRepository.Find(new GetAllSpecification<Login>());

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                var AllAvailableListOfDate = getAllAvailableDatesForBooking(eventDetails.StartDate, eventDetails.EndDate);
                List<SessionByDayDTO> listSessionByDay = new List<SessionByDayDTO>();
                foreach (Login singleLogin in loginDetails)
                {
                    if (singleLogin.RoleName.ToUpper() == "STAFF")
                    {
                        SessionByDayDTO singleSessionByDay = new SessionByDayDTO();
                        singleSessionByDay.StaffName = singleLogin.UserName;
                        foreach (DateTime singleDay in AllAvailableListOfDate)
                        {
                            var criteria = new LuckyDrawEventTicketReportSearchCriteria { AdminId = singleLogin.Id, DateForSearch = singleDay };
                            var sepcification = new LuckyDrawEventTicketReportSpecificationForSearch(criteria);
                            var eventTickets = _luckyDraweventTicketRepository.Find(sepcification);
                            DayCountDTO singleDayCount = new DayCountDTO
                            {
                                Date = singleDay.ToString("dd-MMM-yyyy"),
                                Count = eventTickets.Count()
                            };
                            singleSessionByDay.DayCountDTO.Add(singleDayCount);
                        }
                        listSessionByDay.Add(singleSessionByDay);
                    }
                }
                return Ok(listSessionByDay);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/staff/{adminId:guid}")]
        [ResponseType(typeof(LuckyDrawVisitorDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, Guid adminId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var criteria = new LuckyDrawEventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate, AdminId = adminId };
                var sepcification = new LuckyDrawEventTicketReportSpecificationForSearch(criteria);
                var eventTickets = _luckyDraweventTicketRepository.Find(sepcification);

                if (eventTickets == null)
                    return NotFound();

                List<LuckyDrawVisitorDTO> listOfLuckyDrawVisitorDTO = new List<LuckyDrawVisitorDTO>();
                foreach (LuckyDrawEventTicket singleEventTicket in eventTickets)
                {
                    if (singleEventTicket.PhysicalTicketSerialNo != null)
                    {
                        string userName = "";
                        if (singleEventTicket.CreatedBy != null)
                        {
                            Login login = _loginRepository.GetById(singleEventTicket.CreatedBy.Value);
                            userName = login.UserName;
                        }
                        LuckyDrawVisitorDTO singleLuckyDrawVisitorDTO = new LuckyDrawVisitorDTO
                        {
                            ticketId = singleEventTicket.Id,
                            Name = singleEventTicket.LuckyDrawVisitor.FirstName,
                            EmailId = singleEventTicket.LuckyDrawVisitor.EmailId,
                            MobileNo = singleEventTicket.LuckyDrawVisitor.MobileNo,
                            FacebookId = singleEventTicket.LuckyDrawVisitor.FacebookId,
                            TicketDate = singleEventTicket.TicketDate.ToString("dd/MMM/yyyy"),
                            NumberOfTicket = singleEventTicket.NumberOfTicket,
                            TotalPriceOfTicket = singleEventTicket.TotalPriceOfTicket,
                            Gender = singleEventTicket.LuckyDrawVisitor.Gender,
                            PhysicalTicketSrNo = singleEventTicket.PhysicalTicketSerialNo,
                            StaffName = userName
                        };
                        listOfLuckyDrawVisitorDTO.Add(singleLuckyDrawVisitorDTO);
                    }
                }
                return Ok(listOfLuckyDrawVisitorDTO);
            }
        }

        private string GetPaymentGateWayTxnId()
        {
            TokenGenerationService txnIdPaymentGateWaySvc = new TokenGenerationService();
            txnIdPaymentGateWaySvc.Exclusions = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
            txnIdPaymentGateWaySvc.Minimum = 16;
            txnIdPaymentGateWaySvc.Maximum = 20;

            return txnIdPaymentGateWaySvc.Generate();
        }

        private static List<DateTime> getAllAvailableDatesForBooking(DateTime startDateOfExhibition, DateTime endDateOfExhibition)
        {
            List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

            DateTime currentValidStartDate;

            //This is used for sending only live dates comented 
            //if (startDateOfExhibition <= DateTime.UtcNow)
            //    currentValidStartDate = DateTime.UtcNow;
            //else
            currentValidStartDate = startDateOfExhibition.AddDays(1);

            for (DateTime date = currentValidStartDate; date <= endDateOfExhibition; date = date.AddDays(1))
            {
                allAvailableDatesForBooking.Add(date);
            }
            return (allAvailableDatesForBooking);
        }
    }
}
