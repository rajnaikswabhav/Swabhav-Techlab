using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Service.SMS;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/eventTickets")]
    public class EventTicketController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();
        private IRepository<EventTicketType> _eventTicketTypeRepository = new EntityFrameworkRepository<EventTicketType>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private TokenGenerationService _tokenService = new TokenGenerationService();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<EventExhibitionMap> _eventExhibitionMapRepository = new EntityFrameworkRepository<EventExhibitionMap>();
        private VisitorEventTicketRepository<EventTicket> _visitorEventTicketIRepository = new VisitorEventTicketRepository<EventTicket>();
        private readonly IRepository<DiscountCoupon> _discountCouponRepository = new EntityFrameworkRepository<DiscountCoupon>();
        private readonly IRepository<VisitorDiscountCouponMap> _visitorDiscountCouponMapRepository = new EntityFrameworkRepository<VisitorDiscountCouponMap>();

        /// <summary>
        /// Get Validated Mobile number
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        [Route("validateMobile/{mobileNumber}")]
        public IHttpActionResult Get(Guid organizerId, string mobileNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var mobileSeriesList = new List<int>(new int[] { 7011, 8700, 7982, 8076, 8178, 8851, 8368, 8920, 8383, 7678, 9773, 7042, 9594, 7977 });
                var firstFourDigits = mobileNumber.Substring(0, 4);

                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Invalid Number")),
                    StatusCode = HttpStatusCode.NotFound
                };

                string responseValidated = "Invalid";
                foreach (int mobileSeries in mobileSeriesList)
                {
                    if (mobileSeries == Convert.ToInt32(firstFourDigits))
                    {
                        responseValidated = "Validated";
                        break;
                    }
                }

                if (responseValidated.ToUpper() == "Invalid".ToUpper())
                    throw new HttpResponseException(response);

                return Ok(responseValidated);
            }
        }

        /// <summary>
        /// Get Single Event Ticket Details 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(TicketDTO))]
        async public Task<IHttpActionResult> Get(Guid organizerId, Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventTicket = _eventTicketRepository.GetById(id);
                if (eventTicket == null)
                    return NotFound();

                EventTicketType eventTicketType = eventTicket.EventTicketType;
                Venue venue = eventTicketType.Event.Venue;

                Event eventDetails = _eventRepository.GetById(eventTicketType.Event.Id);

                string Bgcolor;
                string paymentType = "";

                if (eventDetails != null)
                {
                    TimeSpan dayCount = eventTicket.TicketDate.Date - eventDetails.StartDate.Date;
                    if (dayCount.Days < 0)
                    {
                        return NotFound();
                    }

                    TicketColors colors = new TicketColors();
                    if (eventTicket.IsPayOnLocation == true)
                    {
                        Bgcolor = "#FFFFFF";
                    }
                    else
                    {
                        Bgcolor = colors[dayCount.Days];
                    }
                    if (eventTicket.IsPayOnLocation == true)
                    {
                        if (eventTicket.TotalPriceOfTicket == 0)
                        {
                            paymentType = "FREE Ticket";
                        }
                        else
                        {
                            paymentType = "PAY AT LOCATION";
                        }
                    }
                    else if (eventTicket.IsPayOnLocation != true)
                    {
                        paymentType = "PAID ONLINE";
                    }

                    TicketDTO ticketDTO = new TicketDTO
                    {
                        Id = eventTicket.Id,
                        TicketName = eventTicket.EventTicketType.Name,
                        VenueName = eventDetails.Venue.City,
                        TokenNumber = eventTicket.TokenNumber,
                        TicketDate = eventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                        ExhibitionTime = eventDetails.Time,
                        NumberOfTicket = eventTicket.NumberOfTicket,
                        TotalPriceOfTicket = eventTicket.TotalPriceOfTicket,
                        TicketTypeId = eventTicket.EventTicketType.Id.ToString(),
                        IsPayOnLocation = eventTicket.IsPayOnLocation,
                        BgColor = Bgcolor,
                        Device = eventTicket.Device,
                        EventAddress = eventDetails.Address,
                        PaymentType = paymentType

                    };

                    var eventExhibitionMapcriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                    var eventExhibitionMapsepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitionMapcriteria);
                    var eventExhibitionMap = _eventExhibitionMapRepository.Find(eventExhibitionMapsepcification);

                    if (eventExhibitionMap.Count() == 0)
                        return NotFound();

                    foreach (EventExhibitionMap singleEventExhibitionMap in eventExhibitionMap)
                    {
                        Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibitionMap.Exhibition.Id);

                        if (eventDetails.isActive && eventDetails.StartDate.Date <= eventTicket.TicketDate.Date && eventDetails.EndDate.Date >= eventTicket.TicketDate.Date)
                        {
                            ticketDTO.DisplayExhibitions.Add(new DisplayExhibitionDTO
                            {
                                Name = exhibition.Name,
                                BgImage = exhibition.BgImage,
                                Logo = exhibition.Logo
                            });
                        }
                    }
                    return Ok(ticketDTO);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// Get Event Ticket History 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        //Change Ticket History DTO to TicketDTO both are same 
        [Route("visitor/{visitorId}")]
        [ResponseType(typeof(TicketHistoryDTO))]
        public IHttpActionResult GetTicketHistory(Guid organizerId, Guid visitorId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(visitorId);
                if (visitor == null)
                    return NotFound();

                var criteria = new EventTicketSearchCriteria { VisitorId = visitorId };
                var sepcification = new EventTicketSpecificationForSearch(criteria);
                var eventTicketsCount = _eventTicketRepository.Count(sepcification);
                var tickets = _eventTicketRepository.Find(sepcification).OrderByDescending(x => x.CreatedOn);

                if (tickets == null)
                    return NotFound();

                List<TicketHistoryDTO> ticketDTOs = new List<TicketHistoryDTO>();

                foreach (EventTicket eventTicket in tickets)
                {
                    EventTicketType eventTicketType = eventTicket.EventTicketType;
                    Venue venue = eventTicketType.Event.Venue;

                    Event eventDetails = _eventRepository.GetById(eventTicketType.Event.Id);

                    string Bgcolor;
                    if (eventDetails != null)
                    {
                        if ((eventTicket.IsPayOnLocation == false && eventTicket.PaymentCompleted == true) || (eventTicket.IsPayOnLocation == true && eventTicket.PaymentCompleted == true))
                        {
                            TimeSpan dayCount = eventTicket.TicketDate.Date - eventDetails.StartDate.Date;
                            if (dayCount.Days >= 0)
                            {
                                TicketColors colors = new TicketColors();
                                if (eventTicket.IsPayOnLocation == true)
                                {
                                    Bgcolor = "#FFFFFF";
                                }
                                else
                                {
                                    Bgcolor = colors[dayCount.Days];
                                }

                                TicketHistoryDTO ticketDTO = new TicketHistoryDTO
                                {
                                    Id = eventTicket.Id,
                                    TicketName = eventTicket.EventTicketType.Name,
                                    VenueName = eventDetails.Venue.City,
                                    TokenNumber = eventTicket.TokenNumber,
                                    TicketDate = eventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                                    ExhibitionTime = eventDetails.Time,
                                    NumberOfTicket = eventTicket.NumberOfTicket,
                                    TotalPriceOfTicket = eventTicket.TotalPriceOfTicket,
                                    TicketTypeId = eventTicket.EventTicketType.Id.ToString(),
                                    IsPayOnLocation = eventTicket.IsPayOnLocation,
                                    BgColor = Bgcolor,
                                    EventAddress = eventDetails.Address,

                                };

                                var eventExhibitionMapcriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                                var eventExhibitionMapsepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitionMapcriteria);
                                var eventExhibitionMap = _eventExhibitionMapRepository.Find(eventExhibitionMapsepcification);

                                if (eventExhibitionMap.Count() == 0)
                                    return NotFound();

                                foreach (EventExhibitionMap singleEventExhibitionMap in eventExhibitionMap)
                                {
                                    Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibitionMap.Exhibition.Id);

                                    if (eventDetails.StartDate.Date <= eventTicket.TicketDate.Date && eventDetails.EndDate.Date >= eventTicket.TicketDate.Date)
                                    {
                                        ticketDTO.DisplayExhibitionTicket.Add(new DisplayExhitonTicketDTO
                                        {
                                            Name = exhibition.Name,
                                            BgImage = exhibition.BgImage,
                                            Logo = exhibition.Logo
                                        });
                                    }
                                }
                                ticketDTOs.Add(ticketDTO);
                            }
                        }
                    }
                }
                return Ok(ticketDTOs);
            }
        }

        /// <summary>
        /// Get All TicketsHistory of Visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("visitor/{visitorId}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetTicketHistory(Guid organizerId, Guid visitorId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(visitorId);
                if (visitor == null)
                    return NotFound();

                var criteria = new EventTicketSearchCriteria { VisitorId = visitorId };
                var sepcification = new EventTicketSpecificationForSearch(criteria);
                var eventTicketsCount = _eventTicketRepository.Count(sepcification);
                var tickets = _eventTicketRepository.Find(sepcification).OrderByDescending(x => x.CreatedOn)
                    .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

                var totalCount = eventTicketsCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (tickets == null)
                    return NotFound();

                List<TicketHistoryDTO> ticketDTOs = new List<TicketHistoryDTO>();

                foreach (EventTicket eventTicket in tickets)
                {
                    EventTicketType eventTicketType = eventTicket.EventTicketType;
                    Venue venue = eventTicketType.Event.Venue;

                    Event eventDetails = _eventRepository.GetById(eventTicketType.Event.Id);

                    string Bgcolor;
                    if (eventDetails != null)
                    {
                        if ((eventTicket.IsPayOnLocation == false && eventTicket.PaymentCompleted == true) || (eventTicket.IsPayOnLocation == true && eventTicket.PaymentCompleted == true))
                        {
                            TimeSpan dayCount = eventTicket.TicketDate.Date - eventDetails.StartDate.Date;
                            if (dayCount.Days >= 0)
                            {
                                TicketColors colors = new TicketColors();
                                if (eventTicket.IsPayOnLocation == true)
                                {
                                    Bgcolor = "#FFFFFF";
                                }
                                else
                                {
                                    Bgcolor = colors[dayCount.Days];
                                }

                                TicketHistoryDTO ticketDTO = new TicketHistoryDTO
                                {
                                    Id = eventTicket.Id,
                                    TicketName = eventTicket.EventTicketType.Name,
                                    VenueName = eventDetails.Venue.City,
                                    TokenNumber = eventTicket.TokenNumber,
                                    TicketDate = eventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                                    ExhibitionTime = eventDetails.Time,
                                    NumberOfTicket = eventTicket.NumberOfTicket,
                                    TotalPriceOfTicket = eventTicket.TotalPriceOfTicket,
                                    TicketTypeId = eventTicket.EventTicketType.Id.ToString(),
                                    IsPayOnLocation = eventTicket.IsPayOnLocation,
                                    BgColor = Bgcolor,
                                    EventAddress = eventDetails.Address
                                };

                                var eventExhibitionMapcriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                                var eventExhibitionMapsepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitionMapcriteria);
                                var eventExhibitionMap = _eventExhibitionMapRepository.Find(eventExhibitionMapsepcification);

                                if (eventExhibitionMap.Count() == 0)
                                    return NotFound();

                                foreach (EventExhibitionMap singleEventExhibitionMap in eventExhibitionMap)
                                {
                                    Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibitionMap.Exhibition.Id);

                                    if (eventDetails.StartDate.Date <= eventTicket.TicketDate.Date && eventDetails.EndDate.Date >= eventTicket.TicketDate.Date)
                                    {
                                        ticketDTO.DisplayExhibitionTicket.Add(new DisplayExhitonTicketDTO
                                        {
                                            Name = exhibition.Name,
                                            BgImage = exhibition.BgImage,
                                            Logo = exhibition.Logo
                                        });
                                    }
                                }
                                ticketDTOs.Add(ticketDTO);
                            }
                        }
                    }
                }
                var result =
                new
                {
                    totalCount = eventTicketsCount,
                    totalPages = totalPages,
                    ticketDTOs = ticketDTOs
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get ticket By TokenNo
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="tokenNo"></param>
        /// <returns></returns>
        [Route("token/{tokenNo}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetValidTicket(Guid organizerId, string tokenNo)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new EventTicketSearchCriteria { TokenNo = tokenNo };
                var sepcification = new EventTicketSpecificationForSearch(criteria);
                var eventTicketList = _eventTicketRepository.Find(sepcification);
                if (eventTicketList.Count() == 0)
                    return NotFound();

                EventTicketType eventTicketType = eventTicketList.FirstOrDefault().EventTicketType;
                Venue venue = eventTicketType.Event.Venue;

                Event eventDetails = _eventRepository.GetById(eventTicketType.Event.Id);
                List<EventTicketSearchDTO> searchTicketListDTO = new List<EventTicketSearchDTO>();
                foreach (EventTicket eventTicket in eventTicketList)
                {
                    EventTicketSearchDTO ticketDTO = new EventTicketSearchDTO
                    {
                        Id = eventTicket.Id,
                        TicketName = eventTicket.EventTicketType.Name,
                        VenueName = venue.City,
                        TokenNumber = eventTicket.TokenNumber,
                        TicketDate = eventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                        ExhibitionTime = eventDetails.Time,
                        NumberOfTicket = eventTicket.NumberOfTicket,
                        TotalPriceOfTicket = eventTicket.TotalPriceOfTicket,
                        TicketTypeId = eventTicket.EventTicketType.Id.ToString(),
                        IsPayOnLocation = eventTicket.IsPayOnLocation,
                        EmailId=eventTicket.Visitor.EmailId,
                        MobileNo=eventTicket.Visitor.MobileNo
                    };
                    if (eventTicket.TotalPriceOfTicket == 0)
                    {
                        ticketDTO.PaymentType = "FREE Ticket";
                    }
                    else if (eventTicket.IsPayOnLocation)
                    {
                        ticketDTO.PaymentType = "Pay At Location";
                    }
                    else if (eventTicket.IsPayOnLocation == false && eventTicket.PaymentCompleted)
                    {
                        ticketDTO.PaymentType = "Paid Online";
                    }
                    if (eventTicket.ValidityDayCount == 0)
                    {
                        ticketDTO.Status = "Expired or Used";
                    }
                    else
                    {
                        ticketDTO.Status = "Availabe";
                    }
                    var eventExhibitionMapcriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                    var eventExhibitionMapsepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitionMapcriteria);
                    var eventExhibitionMap = _eventExhibitionMapRepository.Find(eventExhibitionMapsepcification);
                    searchTicketListDTO.Add(ticketDTO);
                }
                return Ok(searchTicketListDTO);
            }
        }

        /// <summary>
        /// Edit Issue Ticket 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventTicketId"></param>
        /// <returns></returns>
        [Route("token/{eventTicketId:guid}/issue")]
        public IHttpActionResult PutIssueTicket(Guid organizerId, Guid eventTicketId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var ticket = _eventTicketRepository.GetById(eventTicketId);
                if (ticket == null)
                    return NotFound();

                var isIssued = ticket.IssueTicket();
                unitOfWork.SaveChanges();

                string status = "";

                if (isIssued)
                    status = "Ticket Issued";
                else
                    status = "Ticket is expired or used";

                return Ok(new
                {
                    Message = status
                });
            }
        }

        /// <summary>
        /// Add Event Ticket
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="ticketDTO"></param>
        /// <returns></returns>
        [Route("visitor/{visitorId}")]
        [ModelValidator]
        [ResponseType(typeof(TicketDTO))]
        async public Task<IHttpActionResult> PostBookNow(Guid organizerId, Guid visitorId, TicketDTO ticketDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventTicketType = _eventTicketTypeRepository.GetById(new Guid(ticketDTO.TicketTypeId));
                if (eventTicketType == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(visitorId);
                if (visitor == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventTicketType.Event.Id);
                if (eventDetails == null)
                    return NotFound();

                string tokenNumber = _tokenService.Generate();
                string txnIdForPaymentGateWay = GetPaymentGateWayTxnId();
                if (ticketDTO.Device == 0 || ticketDTO.Device == null)
                    return NotFound();

                var eventTicket = EventTicket.Create(tokenNumber, Convert.ToDateTime(ticketDTO.TicketDate), ticketDTO.NumberOfTicket, ticketDTO.TotalPriceOfTicket, txnIdForPaymentGateWay, null, ticketDTO.Device);

                eventTicket.IsPayOnLocation = ticketDTO.IsPayOnLocation;
                eventTicket.EventTicketType = eventTicketType;
                eventTicket.Visitor = visitor;
                VisitorDiscountCouponMap visitorDiscountCouponMap = new VisitorDiscountCouponMap();
                DiscountCoupon discountCouponDetails = new DiscountCoupon();
                if (ticketDTO.DiscountCouponId != Guid.Empty)
                {
                    discountCouponDetails = _discountCouponRepository.GetById(ticketDTO.DiscountCouponId);
                    if (discountCouponDetails == null)
                        return NotFound();

                    var ticketTotalAmount = ticketDTO.NumberOfTicket * eventTicketType.NonBusinessHrs;
                    var totalDiscountedPrice = (int)Math.Round((double)(Convert.ToDouble(ticketTotalAmount) - (Convert.ToDouble(discountCouponDetails.Discount) / 100.00) * Convert.ToDouble(ticketTotalAmount)));
                    if (totalDiscountedPrice != ticketDTO.TotalPriceOfTicket)
                        return NotFound();

                    visitorDiscountCouponMap.DiscountCoupon = discountCouponDetails;
                    visitorDiscountCouponMap.Visitor = visitor;
                    visitorDiscountCouponMap.EventTicket = eventTicket;
                    _visitorDiscountCouponMapRepository.Add(visitorDiscountCouponMap);
                }

                if (ticketDTO.IsPayOnLocation == true)
                {
                    eventTicket.PaymentCompleted = true;
                }
                else
                {
                    eventTicket.PaymentCompleted = false;
                }

                if (eventTicketType.Name == TypeOfTicket.AllDays.ToString())
                {
                    eventTicket.ValidityDayCount = (int)TypeOfTicket.AllDays;
                }
                else if (eventTicketType.Name == TypeOfTicket.Weekend.ToString())
                {
                    eventTicket.ValidityDayCount = (int)TypeOfTicket.Weekend;
                }
                else if (eventTicketType.Name == TypeOfTicket.Single.ToString())
                {
                    eventTicket.ValidityDayCount = (int)TypeOfTicket.Single;
                }

                _eventTicketRepository.Add(eventTicket);

                unitOfWork.SaveChanges();

                if (ticketDTO.IsPayOnLocation)
                {
                    var criteria = new ExhibitionSearchCriteria { TicketDate = eventTicket.TicketDate };
                    var sepcification = new ExhibitionSpecificationForSearch(criteria);
                    var exhibitions = _exhibitionRepository.Find(sepcification);

                    var visitorName = visitor.FirstName;
                    var tokenNumbertoSms = eventTicket.TokenNumber;
                    var mobileNo = visitor.MobileNo;
                    var tickeTypeName = eventTicketType.Name;
                    var venue = eventDetails.Venue.City;
                    var ticketDate = eventTicket.TicketDate.ToString("dd-MMM-yyyy");
                    var noOfTickets = eventTicket.NumberOfTicket;
                    string ExhibitionTime = eventDetails.Time;
                    //var paymentComplete = "Pay at Location";
                    string bgColor = "#EAEAE7";
                    var totalAmount = eventTicketType.NonBusinessHrs * eventTicket.NumberOfTicket;
                    var discountedAmount = 0;
                    var finalAmount = 0;
                    var discountEmail = "";
                    var validityText = "";
                    string paymentMode;
                    string paymentType;
                    string sponsorName = "";

                    if (visitor.isMobileNoVerified == false)
                    {
                        paymentMode = "Web Ticket";
                        paymentType = "PAY AT LOCATION";
                    }
                    else
                    {
                        paymentMode = "Mobile Ticket";
                        paymentType = "PAY AT LOCATION";
                    }

                    if (ticketDTO.DiscountCouponId != Guid.Empty)
                    {
                        discountedAmount = (int)Math.Round((double)((Convert.ToDouble(discountCouponDetails.Discount) / 100.00) * Convert.ToDouble(totalAmount)));
                        finalAmount = (int)Math.Round((double)(Convert.ToDouble(totalAmount) - (Convert.ToDouble(discountCouponDetails.Discount) / 100.00) * Convert.ToDouble(totalAmount)));
                        if (discountCouponDetails.DiscountType.Type.ToUpper() == "Ticket".ToUpper())
                        {
                            if (discountCouponDetails.Discount == 100)
                            {
                                discountEmail = string.Format("<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='550' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' style='border:3px dotted #000;/* For browsers that do not support gradients */ background: -webkit-linear-gradient(-135deg, #febb13, yellow); /* For Safari 5.1 to 6.0 */ background: -o-linear-gradient(-135deg, #febb13, yellow); /* For Opera 11.1 to 12.0 */ background: -moz-linear-gradient(-135deg, #febb13, yellow); /* For Firefox 3.6 to 15 */ background: linear-gradient(-135deg, #febb13, yellow); /* Standard syntax */'> <tbody> <tr> <td width='100%'> <div> <center> <h1 style='margin: 0px;padding-top: 10px;'>Congratulations</h1> <h2 style='line-height:30px;'>This is a <span style='font-weight:bold;font-size: 1.5em;color:red'>Free</span> ticket</h2> </center> </div></td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table>");
                                validityText = "<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr width='100%'> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='20' style='background:red;color:#fff;padding:10px;'> <h3 style='margin:2px;'>This ticket is valid from 11 AM To 4 PM</h3> </td></tr></tbody> </table> </td></tr></tbody> </table>";
                                paymentType = "Free Ticket";
                            }
                            else
                            {
                                discountEmail = string.Format("<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='550' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' style='border:3px dotted #000;/* For browsers that do not support gradients */ background: -webkit-linear-gradient(-135deg, #febb13, yellow); /* For Safari 5.1 to 6.0 */ background: -o-linear-gradient(-135deg, #febb13, yellow); /* For Opera 11.1 to 12.0 */ background: -moz-linear-gradient(-135deg, #febb13, yellow); /* For Firefox 3.6 to 15 */ background: linear-gradient(-135deg, #febb13, yellow); /* Standard syntax */'> <tbody> <tr> <td width='100%'> <div> <center> <h1 style='margin: 0px;padding-top: 10px;'>Congratulations</h1> <h2 style='line-height:30px;'>You get <span style='font-weight:bold;font-size: 1.5em;color:red'>{0}%</span>Discount</h2> </center> </div></td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table><!-- End of discount -->", visitorDiscountCouponMap.DiscountCoupon.Discount);
                            }

                            if (discountCouponDetails.CouponCode.Substring(0, 4).ToUpper() == "UBER".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/mobile_images/uber.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-UBER";
                            }
                            else if (discountCouponDetails.CouponCode.Substring(0, 3).ToUpper() == "JIO".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/mobile_images/jio.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-JIO";
                            }
                            else if (discountCouponDetails.CouponCode.Substring(0, 4).ToUpper() == "CONX".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/mobile_images/conexiaLogo.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-CONEXIA";
                            }
                            else if (discountCouponDetails.CouponCode.Substring(0, 4).ToUpper() == "HDFC".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/mobile_images/hdfc.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-HDFC";
                            }
                            else if (discountCouponDetails.CouponCode.Substring(0, 3).ToUpper() == "OYO".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/mobile_images/oyo.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-OYO";
                            }
                            else if (discountCouponDetails.CouponCode.Substring(0, 3).ToUpper() == "FED".ToUpper())
                            {
                                discountEmail = "<table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td align=center height=10 style=font-size:1px;line-height:1px> </table></table><table border=0 cellpadding=0 cellspacing=0 id=backgroundTable width=100% st-sortable=seperator><tr><td><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=600 align=center class=myBgColor><tr width=100%><td width=100%><table border=0 cellpadding=0 cellspacing=0 id=devicewidth width=550 align=center style='border:3px dotted #000;background:-webkit-linear-gradient(-135deg,#febb13,#ff0);background:-o-linear-gradient(-135deg,#febb13,#ff0);background:-moz-linear-gradient(-135deg,#febb13,#ff0);background:linear-gradient(-135deg,#febb13,#ff0)'><tr width=100%><td width=40%><img class=logo src='http://gsmktg.com/India-International-Mega-Trade-Fair/images/logos/federal.png'><td width=60%><div><center><b class=congratulations style='margin:0;padding-top:5px;font-size: 27px;'>Congratulations</b><h2 style='font-size: 30px;margin-top: 15px; line-height:34px;'>This is a <span style=font-weight:700;font-size:1.5em;color:red>Free</span> ticket</h2></center></div></table></table></table></table>";
                                sponsorName = "Free Ticket-Federal Bank";
                            }
                        }
                        else if (discountCouponDetails.DiscountType.Type.ToUpper() == "Voucher".ToUpper())
                        {
                            if (discountCouponDetails.Discount == 100)
                            {
                                discountEmail = "<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='550' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' style='border:3px dotted #000;/* For browsers that do not support gradients */ background: -webkit-linear-gradient(-135deg, #febb13, yellow); /* For Safari 5.1 to 6.0 */ background: -o-linear-gradient(-135deg, #febb13, yellow); /* For Opera 11.1 to 12.0 */ background: -moz-linear-gradient(-135deg, #febb13, yellow); /* For Firefox 3.6 to 15 */ background: linear-gradient(-135deg, #febb13, yellow); /* Standard syntax */'> <tbody> <tr> <td width='100%'> <div> <center> <h1>Congratulations</h1> <h2 style='margin-bottom:15px; line-height:34px;'> You get <span style='font-weight:bold;font-size: 1.5em;color:red'>Rs 1000</span><sup>*</sup> Discount Voucher</h2><h2>On <span style='font-weight:bold;font-size: 1.5em;color:red'>International </span>Products</h2><br>(Kindly Show this ticket at ticket counter and collect Discount voucher)<br>(* Condition Apply) </center> </div></td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table>";
                            }
                            else
                            {
                                discountEmail = "<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='550' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' style='border:3px dotted #000;/* For browsers that do not support gradients */ background: -webkit-linear-gradient(-135deg, #febb13, yellow); /* For Safari 5.1 to 6.0 */ background: -o-linear-gradient(-135deg, #febb13, yellow); /* For Opera 11.1 to 12.0 */ background: -moz-linear-gradient(-135deg, #febb13, yellow); /* For Firefox 3.6 to 15 */ background: linear-gradient(-135deg, #febb13, yellow); /* Standard syntax */'> <tbody> <tr> <td width='100%'> <div> <center> <h1>Congratulations</h1> <h2 style='margin-bottom:15px; line-height:34px;'> You get <span style='font-weight:bold;font-size: 1.5em;color:red'>Rs 1000</span><sup>*</sup> Discount Voucher <br>On <span style='font-weight:bold;font-size: 1.5em;color:red'>International </span>Products</h2><br>(Kindly Show this ticket at ticket counter and collect Discount voucher)<br>(* Condition Apply) </center> </div></td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table>";
                            }
                        }
                    }
                    else
                    {
                        discountedAmount = (int)Math.Round((double)((20.00 / 100.00) * Convert.ToDouble(totalAmount)));
                        finalAmount = (int)Math.Round((double)(Convert.ToDouble(totalAmount) - (20.00 / 100.00) * Convert.ToDouble(totalAmount)));
                        discountEmail = string.Format("<table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='550' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' style='border:3px dotted #000;/* For browsers that do not support gradients */ background: -webkit-linear-gradient(-135deg, #febb13, yellow); /* For Safari 5.1 to 6.0 */ background: -o-linear-gradient(-135deg, #febb13, yellow); /* For Opera 11.1 to 12.0 */ background: -moz-linear-gradient(-135deg, #febb13, yellow); /* For Firefox 3.6 to 15 */ background: linear-gradient(-135deg, #febb13, yellow); /* Standard syntax */'> <tbody> <tr> <td width='100%'> <div> <center> <h1 style='margin: 0px;padding-top: 5px;'>Congratulations</h1> <h2 style='margin-bottom:15px; line-height:34px;>You get <span style='font-weight:bold;font-size: 1.5em;color:red'>{0}%</span>Discount</h2> </center> </div></td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table><!-- End of discount -->", 20);
                    }

                    SendSMS(paymentMode, paymentType, tokenNumbertoSms, mobileNo, tickeTypeName, venue, ticketDate, noOfTickets, finalAmount, sponsorName);
                    string logoImage = "";

                    var eventExhibitionMapcriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                    var eventExhibitionMapsepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitionMapcriteria);
                    var eventExhibitionMap = _eventExhibitionMapRepository.Find(eventExhibitionMapsepcification);

                    if (eventExhibitionMap.Count() == 0)
                        return NotFound();

                    var eventDate = eventDetails.StartDate.ToString("dd-MMM-yyyy") + " To " + eventDetails.EndDate.ToString("dd-MMM-yyyy");

                    foreach (EventExhibitionMap singleEventExhibitionMap in eventExhibitionMap)
                    {
                        Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibitionMap.Exhibition.Id);
                        if (exhibition.isActive)
                        {
                            var exhibitionName = exhibition.Name;
                            var BgImage = exhibition.BgImage;
                            var Logo = exhibition.Logo;

                            logoImage += string.Format("<h3 style='margin-top:5px;margin-bottom:5px;color:#febb13'>{2}</h3>",
                                BgImage, Logo, exhibitionName, exhibitionName);
                        }
                    }

                    var data = string.Format("<h5 style='margin-top:5px;margin-bottom:5px;'>{0}</h5> <b style='margin-top:5px;margin-bottom:5px;'>{1}</b>&nbsp;&nbsp; | &nbsp;&nbsp;<u>{2}</u> </td> <td width='30%' align='center' style='padding: 10px; border-left: dotted 1px;'> <h4 style='font-weight:bolder;color:#febb13; font-size:16px;'>{3}</h4> <img src='http://gsmktg.com/mobile_images/ticket_icon.png' style='width:40%;height:auto;'><br><b style='font-size:16px;'> Ticket Quantity:</b><b style='font-size:22px;'>{4}</b> </td> </tr> <tr width='100%' style='paddimg:10px;'> <td width='70%' align='left' style='padding: 10px;'> Ticket Date </td> <td width='30%' align='right' style='padding: 10px;border-left: dotted 1px #D0CEC7;'> {5} </td> </tr> <tr width='100%' style='paddimg:10px;'> <td width='70%' align='left' style='padding: 10px;'> Ticket Amount </td> <td width='30%' align='right' style='padding: 10px;border-left: dotted 1px #D0CEC7;'> {6} </td> </tr> <tr width='100%' style='paddimg:10px;'> <td width='70%' align='left' style='padding: 10px;'> Ticket Quantity </td> <td width='30%' align='right' style='padding: 10px;border-left: dotted 1px #D0CEC7;'> {7} </td> </tr> <tr width='100%' style='paddimg:10px;'> <td width='70%' align='left' style='padding: 10px;'> Discount </td> <td width='30%' align='right' style='padding: 10px;border-left: dotted 1px #D0CEC7;'> {8} </td> </tr> <tr width='100%' style='paddimg:10px;border-top: black solid 2px;'> <td width='70%' align='left' style='padding: 10px;'> <b>Total Amount</b> </td> <td width='30%' align='right' style='padding: 10px;border-left: dotted 1px #D0CEC7;'> <b>{9} </b> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td> </td> </tr> </tbody></table><!--ticket-->", eventDetails.Address, eventDate, ExhibitionTime, paymentType, noOfTickets, ticketDate, totalAmount, noOfTickets, discountedAmount, finalAmount);
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'> <head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1.0'/> <title>Ticket</title> <style type='text/css'> /* Client-specific Styles */ #outlook a {padding:0;} /* Force Outlook to provide a 'view in browser' menu link. */ body{width:100% !important; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; margin:0; padding:0;font-family: Helvetica, arial, sans-serif;} /* Prevent Webkit and Windows Mobile platforms from changing default font sizes, while not breaking desktop design. */ .ExternalClass {width:100%;} /* Force Hotmail to display emails at full width */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmail to display normal line spacing.*/ #backgroundTable {margin:0; padding:0; width:100% !important; line-height: 100% !important;} img {outline:none; text-decoration:none;border:none; -ms-interpolation-mode: bicubic;} a img {border:none;} .image_fix {display:block;} p {margin: 0px 0px !important;} table td {border-collapse: collapse;} table { border-collapse:collapse; mso-table-lspace:0pt; mso-table-rspace:0pt; } a {color: #0a8cce;text-decoration: none;text-decoration:none!important;} /*STYLES*/ table[class=full] { width: 100%; clear: both; } /*IPAD STYLES*/ @media only screen and (max-width: 640px) { a[href^='tel'], a[href^='sms'] { text-decoration: none; color: #0a8cce; /* or whatever your want */ pointer-events: none; cursor: default; } .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] { text-decoration: default; color: #0a8cce !important; pointer-events: auto; cursor: default; } table[id=devicewidth] {width: 440px!important;text-align:center!important;} table[class=devicewidthinner] {width: 420px!important;text-align:center!important;} img[class=banner] {width: 440px!important;height:220px!important;} img[class=colimg2] {width: 440px!important;height:220px!important;} } /*IPHONE STYLES*/ @media only screen and (max-width: 480px) { a[href^='tel'], a[href^='sms'] { text-decoration: none; color: #0a8cce; /* or whatever your want */ pointer-events: none; cursor: default; } .mobile_link a[href^='tel'], .mobile_link a[href^='sms'] { text-decoration: default; color: #0a8cce !important; pointer-events: auto; cursor: default; } table[id=devicewidth] {width: 280px!important;text-align:center!important;} table[class=devicewidthinner] {width: 260px!important;text-align:center!important;} img[class=banner] {width: 280px!important;height:140px!important;} img[class=colimg2] {width: 280px!important;height:140px!important;} td[class=mobile-hide]{display:none!important;} td[class='padding-bottom25']{padding-bottom:25px!important;} } u { border-bottom: 1px dotted #febb13; text-decoration: none; } .myBgColor{ background:" + bgColor + "; } </style> </head> <body> <!-- Start of seperator --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth'> <tbody> <tr> <td align='center' height='20' style='font-size:1px; line-height:1px;'>&nbsp;</td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- End of seperator --> <table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='banner'> <tbody> <tr> <td> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' style='border:1px solid;background:#fff;'> <tbody> <!-- Spacing --> <tr width='100%' style='background:#1f2533;color:#fff;paddimg:10px;'> <td width='30%' align='left' style='padding: 10px;'> <img src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='' border='0' width='100%' style='display:block; border:none; outline:none; text-decoration:none;'> </td> <td width='70%' align='center' style='padding: 10px;border-left: dotted 1px #000;'> <h3 style='padding: 0px; color:#febb13'>Ticket Booking ID :<b style='font-size: 1.57em; color: white;'> " + tokenNumbertoSms + "</b> </h3></td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- Start of seperator -->" + validityText + "<table width='100 %'cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor' > <tbody> <tr> <td align='center' height='20' style='font-size:1px; line-height:1px;'>&nbsp;</td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- End of seperator --> <!--Start of ticket --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='preheader' > <tbody> <tr> <td> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' class='myBgColor' > <tbody> <tr width='100%' > <td width='100%' > <table width='550' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' style='border:1px solid #A9A9A9;background:#FAF9F4'> <tbody> <!-- Spacing --> <tr width='100%' style='background:#1f2533;color:#fff;paddimg:10px;'> <td width='70%' align='left' style='padding: 10px;'>" + logoImage + " " + data + "" + discountEmail + "<!-- Start of seperator --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator' > <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth' class='myBgColor'> <tbody> <tr> <td align='center' height='10' style='font-size:1px; line-height:1px;'>&nbsp;</td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- End of seperator --><!-- Start Full Text --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='full-text'> <tbody> <tr> <td> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' class='myBgColor' > <tbody> <tr> <td width='100%'> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' class='myBgColor'> <tbody> <tr> <td> <table width='550' align='center' cellpadding='0' cellspacing='0' border='0' class='devicewidthinner' style='border: 1px solid #A9A9A9;background:#1f2533'> <tbody> <!-- Title --> <tr> <td style='font-family: Helvetica, arial, sans-serif; font-size: 30px; color: white; text-align:center; line-height: 50px;' st-title='fulltext-heading'> Your Ticket Confirmation </td> </tr> <!-- End of Title --> <!-- spacing --> <tr> <td width='100%' height='20' style='font-size:1px; line-height:1px; mso-line-height-rule: exactly;'>&nbsp;</td> </tr> <!-- End of spacing --> <!-- content --> <tr> <td style='font-family: Helvetica, arial, sans-serif; font-size: 16px; color: white; text-align:center; line-height: 30px;' st-content='fulltext-content'> <span>You have received this email from <a href='http://www.gsmktg.com/'>www.gsmktg.com</a> ,<br> please add <a href='mailto:info@gsmktg.com' target='_top'>info@gsmktg.com</a> to your address book to ensure email delivery in your inbox</span> <hr style='margin:10px;background-color:#febb13; height: 1px; border: 0;'> <span> <b>Important Note,</b> Please carry this email confirmation along with you. </span> </td> </tr> <!-- End of content --> </tbody> </table> </td> </tr> <!-- Spacing --> <tr> <td height='20' style='font-size:1px; line-height:1px; mso-line-height-rule: exactly;'>&nbsp;</td> </tr> <!-- Spacing --> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- end of full text --><!-- Start of Postfooter --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='postfooter' > <tbody> <tr> <td> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth' class='myBgColor'> <tbody> <tr> <td width='100%'> <table width='600' cellpadding='0' cellspacing='0' border='0' align='center' id='devicewidth'> <tbody> <tr> <td align='center' valign='middle' style='font-family: Helvetica, arial, sans-serif; font-size: 14px;color: #666666' st-content='postfooter'> Best regards, GS Marketing Team © 2017 GS Marketing - All Rights Reserved </td> </tr> <!-- Spacing --> <tr> <td width='100%' height='20'></td> </tr> <!-- Spacing --> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- End of postfooter --><!-- Start of seperator --><table width='100%' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' st-sortable='seperator'> <tbody> <tr> <td> <table width='600' align='center' cellspacing='0' cellpadding='0' border='0' id='devicewidth'> <tbody> <tr> <td align='center' height='20' style='font-size:1px; line-height:1px;'>&nbsp;</td> </tr> </tbody> </table> </td> </tr> </tbody></table><!-- End of seperator --> </body></html>";

                    string fileName = "test";
                    string subject = "E-Ticket," + eventDetails.Name + " " + venue;
                    Task.Run(() => SendeMail(htmlPage, fileName, visitor, subject));
                }
                return await Get(organizerId, eventTicket.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetTicketReports(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var criteria = new EventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, IsMobile = false, PaymentCompleted = true };
                var sepcification = new EventTicketReportSpecificationForSearch(criteria);
                //var eventTicketsCount = _visitorEventTicketIRepository.SumOfTickets(sepcification,(v)=>v.NumberOfTicket);
                var eventTickets = _eventTicketRepository.Find(sepcification);

                if (eventTickets == null)
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
                return Ok(ticketReportDTOs);
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

        private void SendSMS(string paymentMode, string paymentType, string tokenNumbertoSms, string mobileNo, string ticketType, string venue, string ticketDate, int noOfTicktets, int finalPrice, string sponsorName)
        {
            var messageText = "";
            if (finalPrice == 0)
            {
                messageText = "Thank you for Booking Ticket, {0}, Type: {1}, Venue: {2}, TicketNo : {3}, Quantity: {4}, Date: {5}, Price: FREE Ticket, Entry Valid:11am to 4pm, {6}";
                messageText = string.Format(messageText, paymentMode, paymentType, venue, tokenNumbertoSms, noOfTicktets, ticketDate, sponsorName);
            }
            else
            {
                messageText = "Thank you for Booking Ticket, {0}, Type: {1}, Venue: {2}, TicketNo : {3}, Quantity: {4}, Date: {5}, Ticket Price:{6}";
                messageText = string.Format(messageText, paymentMode, paymentType, venue, tokenNumbertoSms, noOfTicktets, ticketDate, finalPrice);
            }
            SolutionInfoTechSMSService smsService = new SolutionInfoTechSMSService();
            NotificationService notificationService = new NotificationService(smsService, mobileNo, messageText);
            notificationService.Send();
        }

        private void SendeMail(string htmlPage, string fileName, Visitor visitor, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.Send(visitor.EmailId, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }
        }
    }
}
