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
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/discountCoupon")]
    public class DiscountCouponController : ApiController
    {
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private readonly IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private readonly IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private readonly IRepository<DiscountCoupon> _discountCouponRepository = new EntityFrameworkRepository<DiscountCoupon>();
        private readonly IRepository<DiscountType> _discountTypeRepository = new EntityFrameworkRepository<DiscountType>();
        private readonly IRepository<VisitorDiscountCouponMap> _visitorDiscountCouponMapRepository = new EntityFrameworkRepository<VisitorDiscountCouponMap>();
        private readonly IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private VisitorEventTicketRepository<VisitorDiscountCouponMap> _visitorDiscountCouponEventTicketRepository = new VisitorEventTicketRepository<VisitorDiscountCouponMap>();

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get Single TicketDiscountCoupon Details 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="discountCouponId"></param>
        /// <returns></returns>
        [Route("{discountCouponId:guid}")]
        [ResponseType(typeof(DiscountCouponDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid discountCouponId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var discountCouponDetails = _discountCouponRepository.GetById(discountCouponId);
                if (discountCouponDetails == null)
                    return NotFound();
                DiscountCouponDTO discountCouponDTO = new DiscountCouponDTO()
                {
                    DiscountCouponId = discountCouponDetails.Id,
                    StartDate = discountCouponDetails.StartDate.ToString("dd-MMM-yyyy"),
                    EndDate = discountCouponDetails.EndDate.ToString("dd-MMM-yyyy"),
                    CouponCode = discountCouponDetails.CouponCode,
                    DiscountText = discountCouponDetails.DiscountText,
                    DiscountType = discountCouponDetails.DiscountType.Type,
                    Discount=discountCouponDetails.Discount,
                    IsActive = discountCouponDetails.IsActive
                };
                return Ok(discountCouponDTO);
            }
        }

        /// <summary>
        /// Get All Ticket DiscountTypes
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("discountType")]
        [ResponseType(typeof(ExhibitorDiscountDTO[]))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var discountTypeList = _discountTypeRepository.Find(new GetAllSpecification<DiscountType>());
                List<DiscountTypeDTO> discountTypeListDTO = new List<DiscountTypeDTO>();
                foreach (DiscountType discountType in discountTypeList)
                {
                    DiscountTypeDTO discountTypeDTO = new DiscountTypeDTO()
                    {
                        DiscountTypeID = discountType.Id,
                        Type = discountType.Type
                    };
                    discountTypeListDTO.Add(discountTypeDTO);
                }
                return Ok(discountTypeListDTO);
            }
        }

        /// <summary>
        /// Get All Ticke Discount Coupons 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(DiscountCouponDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var discountCouponCriteria = new DiscountCouponSearchCriteria { EventId = eventId };
                var discountCouponSpecification = new DiscountCouponSpecificationForSearch(discountCouponCriteria);
                var discountCouponCount = _discountCouponRepository.Count(discountCouponSpecification);
                var discountCouponList = _discountCouponRepository.Find(discountCouponSpecification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = discountCouponCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<DiscountCouponDTO> discountCouponListDTO = new List<DiscountCouponDTO>();
                foreach (DiscountCoupon discountCoupon in discountCouponList)
                {
                    var visitorDiscountCouponMapCriteria = new VisitorDiscountCouponMapSearchCriteria { DiscountCouponId = discountCoupon.Id };
                    var visitorDiscountCouponMapSpecification = new VisitorDiscountCouponMapSpecificationForSearch(visitorDiscountCouponMapCriteria);
                    var visitorTicketCount = _visitorDiscountCouponEventTicketRepository.SumOfSingleDiscountCouponAddedTickets(visitorDiscountCouponMapSpecification);
                    var totalVisitors = _visitorDiscountCouponEventTicketRepository.DiscountVisitorsCount(visitorDiscountCouponMapSpecification, x => x.EventTicket.Visitor.EmailId);

                    DiscountCouponDTO discountCouponDTO = new DiscountCouponDTO()
                    {
                        DiscountCouponId = discountCoupon.Id,
                        StartDate = discountCoupon.StartDate.ToString("dd-MMM-yyyy"),
                        EndDate = discountCoupon.EndDate.ToString("dd-MMM-yyyy"),
                        CouponCode = discountCoupon.CouponCode,
                        DiscountText = discountCoupon.DiscountText,
                        DiscountType = discountCoupon.DiscountType.Type,
                        IsActive = discountCoupon.IsActive,
                        Discount = discountCoupon.Discount,
                        TicketCount = visitorTicketCount ?? 0,
                        VisitorCount = totalVisitors
                    };
                    discountCouponListDTO.Add(discountCouponDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfDiscountCouponDTO = discountCouponListDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get all Discount Coupon Tickets List 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="discountCouponId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [Route("discountCoupon/{discountCouponId:guid}/event/{eventId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetTicketReportsByDates(Guid organizerId, Guid discountCouponId, Guid eventId, int pageSize, int pageNumber, string startDate = null, string endDate = null)
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

                var visitorDiscountCouponMapCriteria = new VisitorDiscountCouponMapSearchCriteria { DiscountCouponId = discountCouponDetails.Id, StartDate = Convert.ToDateTime(startDate), EndDate = Convert.ToDateTime(endDate) };
                var visitorDiscountCouponMapSpecification = new VisitorDiscountCouponMapSpecificationForSearch(visitorDiscountCouponMapCriteria);
                var visitorDiscountCouponMapCount = _visitorDiscountCouponMapRepository.Count(visitorDiscountCouponMapSpecification);


                var totalCount = visitorDiscountCouponMapCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                var visitorDiscountCouponMapList = _visitorDiscountCouponMapRepository.Find(visitorDiscountCouponMapSpecification)
                                                    .OrderByDescending(x => x.CreatedOn)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

                if (visitorDiscountCouponMapList.Count() == 0)
                    return NotFound();

                List<TicketReportDTO> ticketReportDTOs = new List<TicketReportDTO>();

                foreach (VisitorDiscountCouponMap visitorDiscountCouponMapTicket in visitorDiscountCouponMapList)
                {
                    string empty = null;
                    var paymentMode = empty;
                    if (visitorDiscountCouponMapTicket.EventTicket.IsPayOnLocation == false)
                    {
                        var transactionCriteria = new TransactionSearchCriteria { TicketId = visitorDiscountCouponMapTicket.EventTicket.Id };
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
                        isMobileVerified = visitorDiscountCouponMapTicket.EventTicket.Visitor.isMobileNoVerified,
                        dateOfBooking = visitorDiscountCouponMapTicket.EventTicket.CreatedOn.ToString("dd-MMM-yyyy"),
                        bookingId = visitorDiscountCouponMapTicket.EventTicket.TokenNumber,
                        emailId = visitorDiscountCouponMapTicket.EventTicket.Visitor.EmailId,
                        phoneNo = visitorDiscountCouponMapTicket.EventTicket.Visitor.MobileNo,
                        ticketDate = visitorDiscountCouponMapTicket.EventTicket.TicketDate.ToString("dd-MMM-yyyy"),
                        numberOfTicket = visitorDiscountCouponMapTicket.EventTicket.NumberOfTicket.ToString(),
                        ticketAmount = visitorDiscountCouponMapTicket.EventTicket.TotalPriceOfTicket.ToString(),
                        paymentMode = paymentMode,
                        isPaymentCompleted = visitorDiscountCouponMapTicket.EventTicket.PaymentCompleted,
                        pincode = visitorDiscountCouponMapTicket.EventTicket.Visitor.Pincode.ToString()
                    };
                    ticketReportDTOs.Add(ticketDTO);
                }

                var result = new
                {
                    totalCount = visitorDiscountCouponMapCount,
                    totalPages = totalPages,
                    ticketReportDTO = ticketReportDTOs
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Check Discount Coupon Applied to Visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="visitorId"></param>
        /// <param name="discountCouponGetDTO"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/applyCoupon/{visitorId:guid}")]
        [ResponseType(typeof(ApplyCouponDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid eventId, Guid visitorId, DiscountCouponGetDTO discountCouponGetDTO)
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

                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Invalid Coupon Code")),
                    StatusCode = HttpStatusCode.NotFound
                };

                if (string.IsNullOrWhiteSpace(discountCouponGetDTO.CouponCode))
                {
                    throw new HttpResponseException(response);
                }

                DateTime todaysDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                var discountCouponCriteria = new DiscountCouponSearchCriteria { EventId = eventId, CouponCode = discountCouponGetDTO.CouponCode, Date = todaysDate };
                var discountCouponSpecification = new DiscountCouponSpecificationForSearch(discountCouponCriteria);
                var discountCoupon = _discountCouponRepository.Find(discountCouponSpecification);
                if (discountCoupon.Count() == 0)
                    throw new HttpResponseException(response);

                var visitorDiscountCouponMapCriteria = new VisitorDiscountCouponMapSearchCriteria { DiscountCouponId = discountCoupon.FirstOrDefault().Id, VisitorId = visitorId };
                var visitorDiscountCouponMapSpecification = new VisitorDiscountCouponMapSpecificationForSearch(visitorDiscountCouponMapCriteria);
                var visitorDiscountCouponMap = _visitorDiscountCouponMapRepository.Find(visitorDiscountCouponMapSpecification);
                if (visitorDiscountCouponMap.Count() != 0)
                    throw new HttpResponseException(response);

                ApplyCouponDTO applyCouponCodeDTO = new ApplyCouponDTO();
                applyCouponCodeDTO.DiscountId = discountCoupon.FirstOrDefault().Id;
                if (discountCoupon.FirstOrDefault().DiscountType.Type.ToUpper() == ("Ticket").ToUpper())
                {
                    var totalDiscountedPrice = (int)Math.Round((double)(Convert.ToDouble(discountCouponGetDTO.TotalAmount) - (Convert.ToDouble(discountCoupon.FirstOrDefault().Discount) / 100.00) * Convert.ToDouble(discountCouponGetDTO.TotalAmount)));
                    applyCouponCodeDTO.DiscountedAmount = totalDiscountedPrice;
                    applyCouponCodeDTO.DiscountText = discountCoupon.FirstOrDefault().DiscountText;
                }
                else
                {
                    //if (discountCouponGetDTO.NumberOfTickets < 4)
                    //{
                    //    response.Content = new StringContent(string.Format("Number Of Tickets should be 4 or More"));
                    //    throw new HttpResponseException(response);
                    //}
                    var totalDiscountedPrice = (int)Math.Round((double)(Convert.ToDouble(discountCouponGetDTO.TotalAmount) - (Convert.ToDouble(discountCoupon.FirstOrDefault().Discount) / 100.00) * Convert.ToDouble(discountCouponGetDTO.TotalAmount)));
                    applyCouponCodeDTO.DiscountedAmount = totalDiscountedPrice;
                    applyCouponCodeDTO.DiscountText = discountCoupon.FirstOrDefault().DiscountText;
                }
                return Ok(applyCouponCodeDTO);
            }
        }

        /// <summary>
        /// Add Ticket Discount Coupon code 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="discountTypeId"></param>
        /// <param name="discountCouponDTO"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/discountType/{discountTypeId:guid}")]
        [ResponseType(typeof(ExhibitorDiscountDTO[]))]
        public IHttpActionResult PostDiscount(Guid organizerId, Guid eventId, Guid discountTypeId, DiscountCouponDTO discountCouponDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var discountType = _discountTypeRepository.GetById(discountTypeId);
                if (discountType == null)
                    return NotFound();

                var discountCoupon = DiscountCoupon.Create(discountCouponDTO.CouponCode, Convert.ToDateTime(discountCouponDTO.StartDate), Convert.ToDateTime(discountCouponDTO.EndDate), discountCouponDTO.DiscountText, discountCouponDTO.Discount, true); // DTO to entity mapping
                discountCoupon.Event = eventDetails;
                discountCoupon.DiscountType = discountType;
                _discountCouponRepository.Add(discountCoupon);

                unitOfWork.SaveChanges();
                return Get(organizerId, eventId, 5, 1);
            }
        }
    }
}
