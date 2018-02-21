using Modules.EventManagement;
using Modules.LayoutManagement;
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
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/bookingExhibitorDiscount")]
    public class BookingExhibitorDiscountController : ApiController
    {
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private readonly IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private readonly IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private readonly IRepository<BookingExhibitorDiscount> _bookingExhibitorDiscountRepository = new EntityFrameworkRepository<BookingExhibitorDiscount>();
        private readonly IRepository<StallBooking> _stallBookingRepository = new EntityFrameworkRepository<StallBooking>();
        private readonly IRepository<Booking> _bookingRepository = new EntityFrameworkRepository<Booking>();
        private readonly IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private readonly IRepository<VisitorBookingExhibitorDiscount> _visitorBookingExhibitorDiscountRepository = new EntityFrameworkRepository<VisitorBookingExhibitorDiscount>();

        /// <summary>
        /// Get all ExhibitorDiscounts 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorDiscountDTO[]))]
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

                var bookingExhibitorDiscountCriteria = new BookingExhibitorDiscountSearchCriteria { EventId = eventId };
                var bookingExhibitorDiscountSpecification = new BookingExhibitorDiscountSpecificationForSearch(bookingExhibitorDiscountCriteria);
                var bookingListCount = _bookingExhibitorDiscountRepository.Count(bookingExhibitorDiscountSpecification);
                var bookingExhibitorDiscountList = _bookingExhibitorDiscountRepository.Find(bookingExhibitorDiscountSpecification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<ExhibitorDiscountDTO> exhibitorDiscountListDTO = new List<ExhibitorDiscountDTO>();
                foreach (BookingExhibitorDiscount bookingExhibitorDiscount in bookingExhibitorDiscountList)
                {
                    ExhibitorDiscountDTO exhibitorDiscountDTO = new ExhibitorDiscountDTO()
                    {
                        BookingExhibitorDiscountId = bookingExhibitorDiscount.Id,
                        CompanyName = bookingExhibitorDiscount.Booking.Exhibitor.CompanyName,
                        Heading = bookingExhibitorDiscount.Heading,
                        Description = bookingExhibitorDiscount.Description,
                        StartDate = bookingExhibitorDiscount.StartDate.ToString("dd-MMM-yyyy"),
                        EndDate = bookingExhibitorDiscount.EndDate.ToString("dd-MMM-yyyy"),
                        BannerUrl = bookingExhibitorDiscount.BgImage,
                        ProductUrl = "",
                        TotalClicks = bookingExhibitorDiscount.ClickCount
                    };
                    var visitorBookingExhibitorDiscountCriteria = new VisitorBookingExhibitorDiscountSearchCriteria { BookingExhibitorDiscountId = bookingExhibitorDiscount.Id };
                    var visitorBookingExhibitorDiscountSpecification = new VisitorBookingExhibitorDiscountSpecificationForSearch(visitorBookingExhibitorDiscountCriteria);
                    var visitorGrabDiscountCount = _visitorBookingExhibitorDiscountRepository.Count(visitorBookingExhibitorDiscountSpecification);

                    exhibitorDiscountDTO.TotalGrabOffer = visitorGrabDiscountCount;

                    if (bookingExhibitorDiscount.Booking.Exhibitor.Country != null)
                        exhibitorDiscountDTO.Country = bookingExhibitorDiscount.Booking.Exhibitor.Country.Name;
                    exhibitorDiscountListDTO.Add(exhibitorDiscountDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorDiscountListDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Exhibitor Discount by Search
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="productId"></param>
        /// <param name="pavilionId"></param>
        /// <returns></returns>
        [Route("search/event/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(ExhibitorBookingDiscountDTO[]))]
        public IHttpActionResult GetDiscountSearch(Guid organizerId, Guid eventId, int pageSize, int pageNumber, string productId = null, string pavilionId = null)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                Guid selectedProductId = new Guid();
                Guid selectedpavilionId = new Guid();
                if (!string.IsNullOrEmpty(productId))
                {
                    selectedProductId = new Guid(productId);
                }
                if (!string.IsNullOrEmpty(pavilionId))
                {
                    selectedpavilionId = new Guid(pavilionId);
                }
                var bookingExhibitorDiscountCriteria = new BookingExhibitorDiscountSearchCriteria { EventId = eventId, ProductId = selectedProductId, PavilionId = selectedpavilionId };

                var bookingExhibitorDiscountSpecification = new BookingExhibitorDiscountSpecificationForSearch(bookingExhibitorDiscountCriteria);
                var bookingListCount = _bookingExhibitorDiscountRepository.Count(bookingExhibitorDiscountSpecification);
                var bookingExhibitorDiscountList = _bookingExhibitorDiscountRepository.Find(bookingExhibitorDiscountSpecification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);


                List<ExhibitorBookingDiscountDTO> exhibitorDiscountListDTO = new List<ExhibitorBookingDiscountDTO>();
                foreach (BookingExhibitorDiscount bookingExhibitorDiscount in bookingExhibitorDiscountList)
                {
                    ExhibitorBookingDiscountDTO exhibitorDiscountDTO = new ExhibitorBookingDiscountDTO();
                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingExhibitorDiscount.Booking.Id, EventId = eventDetails.Id };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                    exhibitorDiscountDTO = GetDTO(bookingExhibitorDiscount, bookingStall);
                    exhibitorDiscountListDTO.Add(exhibitorDiscountDTO);
                }  
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = exhibitorDiscountListDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Single Exhibitor Discount
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingExhibitorDiscountId"></param>
        /// <returns></returns>
        [Route("{bookingExhibitorDiscountId:guid}")]
        [ResponseType(typeof(ExhibitorBookingDiscountDTO))]
        public IHttpActionResult GetSingleDiscount(Guid organizerId, Guid bookingExhibitorDiscountId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingExhibitorDiscount = _bookingExhibitorDiscountRepository.GetById(bookingExhibitorDiscountId);
                ExhibitorBookingDiscountDTO exhibitorDiscountDTO = new ExhibitorBookingDiscountDTO();

                var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingExhibitorDiscount.Booking.Id, EventId = bookingExhibitorDiscount.Booking.Event.Id };
                var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                exhibitorDiscountDTO = GetDTO(bookingExhibitorDiscount, bookingStall);

                bookingExhibitorDiscount.ClickCount = bookingExhibitorDiscount.ClickCount + 1;
                unitOfWork.SaveChanges();

                return Ok(exhibitorDiscountDTO);
            }
        }

        /// <summary>
        /// Get Visitor Discount Data
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorBookingExhibitorDiscountId"></param>
        /// <returns></returns>
        [Route("visitorDiscount/{visitorBookingExhibitorDiscountId:guid}")]
        [ResponseType(typeof(VisitorBookingExhibitorDiscountDTO))]
        public IHttpActionResult GetSingleVisitorDiscount(Guid organizerId, Guid visitorBookingExhibitorDiscountId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var visitorBookingExhibitorDiscount = _visitorBookingExhibitorDiscountRepository.GetById(visitorBookingExhibitorDiscountId);
                VisitorBookingExhibitorDiscountDTO visitorBookingDiscountDTO = new VisitorBookingExhibitorDiscountDTO();
                var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Id, EventId = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Event.Id };
                var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                visitorBookingDiscountDTO = GetVisitorDTO(visitorBookingExhibitorDiscount, bookingStall);

                return Ok(visitorBookingDiscountDTO);
            }
        }

        /// <summary>
        /// Get Applied Discount to single visitor 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="visitorId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/visitor/{visitorId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(VisitorBookingExhibitorDiscountDTO[]))]
        public IHttpActionResult GetVisitorAppliedDiscount(Guid organizerId, Guid eventId, Guid visitorId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (organizer == null)
                    return NotFound();

                var visitorBookingExhibitorDiscountSearchCriteria = new VisitorBookingExhibitorDiscountSearchCriteria { VisitorId = visitorId, EventId = eventId };
                var visitorBookingExhibitorDiscountSepcification = new VisitorBookingExhibitorDiscountSpecificationForSearch(visitorBookingExhibitorDiscountSearchCriteria);
                var visitorDiscountCount = _visitorBookingExhibitorDiscountRepository.Count(visitorBookingExhibitorDiscountSepcification);
                var visitorBookingExhibitorDiscountList = _visitorBookingExhibitorDiscountRepository.Find(visitorBookingExhibitorDiscountSepcification)
                                                         .Skip((pageNumber - 1) * pageSize)
                                                         .Take(pageSize);

                var totalCount = visitorDiscountCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<VisitorBookingExhibitorDiscountDTO> visitorBookingExhibitorDiscountListDTO = new List<VisitorBookingExhibitorDiscountDTO>();

                foreach (VisitorBookingExhibitorDiscount visitorBookingExhibitorDiscount in visitorBookingExhibitorDiscountList)
                {
                    VisitorBookingExhibitorDiscountDTO exhibitorDiscountDTO = new VisitorBookingExhibitorDiscountDTO();
                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Id, EventId = eventId };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                    exhibitorDiscountDTO = GetVisitorDTO(visitorBookingExhibitorDiscount, bookingStall);
                    visitorBookingExhibitorDiscountListDTO.Add(exhibitorDiscountDTO);
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = visitorBookingExhibitorDiscountListDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Add Exhibitor Product Discount
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="bookingId"></param>
        /// <param name="exhibitorDiscountDTO"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/booking/{bookingId:guid}")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, Guid eventId, Guid bookingId, ExhibitorDiscountDTO exhibitorDiscountDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var booking = _bookingRepository.GetById(bookingId);
                if (booking == null)
                    return NotFound();

                TimeSpan startTime = new TimeSpan(00, 00, 00);
                TimeSpan endTime = new TimeSpan(23, 59, 59);
                DateTime startDate = Convert.ToDateTime(exhibitorDiscountDTO.StartDate).Date + startTime;
                DateTime endDate = Convert.ToDateTime(exhibitorDiscountDTO.StartDate).Date + endTime;
                var bookingExhibitorDiscount = BookingExhibitorDiscount.Create(exhibitorDiscountDTO.Heading, exhibitorDiscountDTO.Description, false, false, startDate, endDate, exhibitorDiscountDTO.BannerUrl);
                bookingExhibitorDiscount.Booking = booking;
                _bookingExhibitorDiscountRepository.Add(bookingExhibitorDiscount);
                unitOfWork.SaveChanges();

                return Get(organizerId, eventId, 5, 1);
            }
        }

        /// <summary>
        /// Apply Discount to Visitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingExhibitorDiscountId"></param>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        [Route("applyExhibitorDiscount/{bookingExhibitorDiscountId:guid}/visitor/{visitorId:guid}")]
        [ModelValidator]
        public IHttpActionResult PostDiscountApply(Guid organizerId, Guid bookingExhibitorDiscountId, Guid visitorId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingExhibitorDiscountDetails = _bookingExhibitorDiscountRepository.GetById(bookingExhibitorDiscountId);
                if (bookingExhibitorDiscountDetails == null)
                    return NotFound();

                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Allready Applied Discount, Please check Coupon Listing")),
                    StatusCode = HttpStatusCode.NotFound
                };

                var visitorDetails = _visitorRepository.GetById(visitorId);
                if (visitorDetails == null)
                    return NotFound();

                var visitorBookingExhibitorDiscountSearchCriteria = new VisitorBookingExhibitorDiscountSearchCriteria { VisitorId = visitorId, BookingExhibitorDiscountId = bookingExhibitorDiscountId };
                var visitorBookingExhibitorDiscountSepcification = new VisitorBookingExhibitorDiscountSpecificationForSearch(visitorBookingExhibitorDiscountSearchCriteria);
                var visitorDiscountCount = _visitorBookingExhibitorDiscountRepository.Count(visitorBookingExhibitorDiscountSepcification);
                if (visitorDiscountCount != 0)
                    throw new HttpResponseException(response);

                var discountCount = _visitorBookingExhibitorDiscountRepository.Count(new GetAllSpecification<VisitorBookingExhibitorDiscount>());
                string companyNameLetters = bookingExhibitorDiscountDetails.Booking.Exhibitor.CompanyName.Substring(0, 3);
                string discountCouponCode = GetDiscountCouponCode(companyNameLetters, discountCount.ToString());

                var visitorBookingExhibitorDiscount = VisitorBookingExhibitorDiscount.Create(discountCouponCode);
                visitorBookingExhibitorDiscount.BookingExhibitorDiscount = bookingExhibitorDiscountDetails;
                visitorBookingExhibitorDiscount.Visitor = visitorDetails;
                _visitorBookingExhibitorDiscountRepository.Add(visitorBookingExhibitorDiscount);
                unitOfWork.SaveChanges();

                return GetSingleVisitorDiscount(organizerId, visitorBookingExhibitorDiscount.Id);
            }
        }

        private string GetDiscountCouponCode(string companyNameLetters, string count)
        {
            var BookingRequestId = companyNameLetters.ToUpper() + count.PadLeft(5, '0');
            return BookingRequestId;
        }

        private static ExhibitorBookingDiscountDTO GetDTO(BookingExhibitorDiscount bookingExhibitorDiscount, IList<StallBooking> stallBookingList)
        {
            ExhibitorBookingDiscountDTO exhibitorDTO = new ExhibitorBookingDiscountDTO();
            if (bookingExhibitorDiscount.Booking.Exhibitor != null)
            {
                exhibitorDTO.BookingExhibitorDiscountId = bookingExhibitorDiscount.Id;
                exhibitorDTO.Heading = bookingExhibitorDiscount.Heading;
                exhibitorDTO.Description = bookingExhibitorDiscount.Description;
                exhibitorDTO.EndDate = bookingExhibitorDiscount.EndDate.ToString("dd-MMM-yyyy");
                exhibitorDTO.Name = bookingExhibitorDiscount.Booking.Exhibitor.CompanyName;
                exhibitorDTO.BannerImage = bookingExhibitorDiscount.BgImage;
                exhibitorDTO.Stalls = stallBookingList.Select(x => GetDTO(x.Stall)).ToList();
                exhibitorDTO.Categories = bookingExhibitorDiscount.Booking.Exhibitor.Categories.Select(x => GetDTO(x)).ToList();
                exhibitorDTO.Country = GetDTO(bookingExhibitorDiscount.Booking.Exhibitor.Country);
                exhibitorDTO.State = GetDTO(bookingExhibitorDiscount.Booking.Exhibitor.State);
            }
            return (exhibitorDTO);
        }

        private static VisitorBookingExhibitorDiscountDTO GetVisitorDTO(VisitorBookingExhibitorDiscount visitorBookingExhibitorDiscount, IList<StallBooking> stallBookingList)
        {
            VisitorBookingExhibitorDiscountDTO exhibitorDTO = new VisitorBookingExhibitorDiscountDTO();
            if (visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Exhibitor != null)
            {
                exhibitorDTO.Heading = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Heading;
                exhibitorDTO.Description = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Description;
                exhibitorDTO.Name = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Exhibitor.CompanyName;
                exhibitorDTO.EndDate = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.EndDate.ToString("dd-MMM-yyyy");
                exhibitorDTO.CouponCode = visitorBookingExhibitorDiscount.DiscountcoupanCode;
                exhibitorDTO.BannerImage = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.BgImage;
                exhibitorDTO.Stalls = stallBookingList.Select(x => GetDTO(x.Stall)).ToList();
                exhibitorDTO.Categories = visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Exhibitor.Categories.Select(x => GetDTO(x)).ToList();
                exhibitorDTO.Country = GetDTO(visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Exhibitor.Country);
                exhibitorDTO.State = GetDTO(visitorBookingExhibitorDiscount.BookingExhibitorDiscount.Booking.Exhibitor.State);
            }
            return (exhibitorDTO);
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
    }
}
