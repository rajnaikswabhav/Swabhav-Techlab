using Modules.EventManagement;
using Modules.LayoutManagement;
using Modules.SecurityManagement;
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
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/bookingReports")]
    public class BookingReportsController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<BookingRequest> _bookingRequestRepository = new EntityFrameworkRepository<BookingRequest>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Stall> _stallRepository = new EntityFrameworkRepository<Stall>();
        private IRepository<BookingRequestStall> _bookingRequestStallRepository = new EntityFrameworkRepository<BookingRequestStall>();
        private IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private IRepository<Booking> _bookingRepository = new EntityFrameworkRepository<Booking>();
        private IRepository<StallBooking> _stallBookingRepository = new EntityFrameworkRepository<StallBooking>();
        private IRepository<Payment> _paymentRepository = new EntityFrameworkRepository<Payment>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<LayoutPlan> _layoutPlanRepository = new EntityFrameworkRepository<LayoutPlan>();
        private IRepository<Section> _sectionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<PaymentDetails> _paymentDetailsRepository = new EntityFrameworkRepository<PaymentDetails>();
        private IRepository<BookingSalesPersonMap> _bookingSalesPersoneMapRepository = new EntityFrameworkRepository<BookingSalesPersonMap>();
        private BookingRepository<StallBooking> _bookingStallRepository = new BookingRepository<StallBooking>();
        private BookingRepository<BookingSalesPersonMap> _bookingTotalAmountRepository = new BookingRepository<BookingSalesPersonMap>();

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get Booking By Status
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingStatus")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                List<BookingStatusDTO> ListBookingStatusDTO = new List<BookingStatusDTO>();

                string[] bookingStatus = {
                                    "Booked", "Approved","Cancelled", "NotSold", "Reserved", "Pending"
                                 };

                foreach (string status in bookingStatus)
                {
                    BookingStatusDTO singleBookingStatus = new BookingStatusDTO();
                    if (status.ToUpper() == "BOOKED")
                    {
                        var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, Status = status };
                        var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                        var bookingList = _bookingStallRepository.CountOfBookingStalls(bookingSepcification);

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = bookingList;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }
                    if (status == "Approved")
                    {
                        var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, Status = status };
                        var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                        var bookingList = _bookingStallRepository.CountOfBookingStalls(bookingSepcification);

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = bookingList;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }
                    if (status == "Cancelled")
                    {
                        var bookingRequestSearchCriteria = new BookingRequestStallSearchCriteria { EventId = eventId, Status = status };
                        var bookingSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestSearchCriteria);
                        var bookingList = _bookingStallRepository.CountOfBookingRequestStalls(bookingSepcification);

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = bookingList;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }
                    if (status == "Pending")
                    {
                        var bookingRequestSearchCriteria = new BookingRequestStallSearchCriteria { EventId = eventId, Status = status };
                        var bookingSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestSearchCriteria);
                        var bookingList = _bookingStallRepository.CountOfBookingRequestStalls(bookingSepcification);

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = bookingList;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }
                    if (status == "Reserved")
                    {
                        //var bookingRequestSearchCriteria = new BookingRequestStallSearchCriteria { EventId = eventId, Status = status };
                        //var bookingSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestSearchCriteria);
                        var bookingList = _bookingStallRepository.CountOfReservedStalls(null); // TODO:

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = bookingList;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }
                    if (status == "NotSold")
                    {
                        var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                        var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);
                        var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                        if (layoutPlan == null)
                            return NotFound();

                        var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id, Type = "HANGER" };
                        var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                        var sectionList = _sectionRepository.Find(sectionSepcification);
                        int UnbookStalls = 0;
                        foreach (Section section in sectionList)
                        {
                            var stallCriteria = new StallSearchCriteria { sectionId = section.Id, IsBooked = "false" };
                            var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                            var stalls = _stallRepository.Count(stallSepcification);
                            UnbookStalls = UnbookStalls + stalls;
                        }

                        singleBookingStatus.Item = status;
                        singleBookingStatus.TotalStalls = UnbookStalls;
                        singleBookingStatus.TotalAmount = 0;
                        singleBookingStatus.TotalArea = 0;
                        ListBookingStatusDTO.Add(singleBookingStatus);
                    }

                }
                return Ok(ListBookingStatusDTO);
            }
        }

        /// <summary>
        /// Get Booking Status Data
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="statusName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingStatus/{statusName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(List<BookingDTO>))]
        public IHttpActionResult GetBookingStatusData(Guid organizerId, Guid eventId, string statusName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                if (statusName == "Booked")
                {
                    var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId, Status = statusName };
                    var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                    var bookingListCount = _bookingRepository.Count(bookingSepcification);
                    var bookingList = _bookingRepository.Find(bookingSepcification)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                    var totalCount = bookingListCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                    foreach (Booking booking in bookingList)
                    {
                        Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                        if (exhibitor == null)
                            return NotFound();

                        BookingDTO singleBookingDTO = new BookingDTO();
                        foreach (Category singleCategory in exhibitor.Categories)
                        {
                            singleBookingDTO.CategoryName.Add(singleCategory.Name);
                        }

                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                        foreach (StallBooking stall in bookingStallList)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };

                            singleBookingDTO.StallList.Add(bookedSatllListDTO);
                        }

                        var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                        var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                        var payments = _paymentRepository.Find(paymentSepcification);
                        double totalAmountPaid = 0;
                        foreach (Payment payment in payments)
                        {
                            if (payment.IsPaymentDone == true)
                                totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                        }

                        double balance = booking.TotalAmount - totalAmountPaid;
                        singleBookingDTO.Id = booking.Id;
                        singleBookingDTO.BookingRequestId = "";
                        singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                        singleBookingDTO.CompanyName = exhibitor.CompanyName;
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                        singleBookingDTO.FinalAmount = booking.TotalAmount;
                        singleBookingDTO.AmountPaid = totalAmountPaid;
                        singleBookingDTO.Balance = balance;

                        listOfBookingDTO.Add(singleBookingDTO);
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfBookingDTO
                    };
                    return Ok(result);
                }

                if (statusName == "Approved")
                {
                    var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId, Status = statusName };
                    var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                    var bookingListCount = _bookingRepository.Count(bookingSepcification);
                    var bookingList = _bookingRepository.Find(bookingSepcification)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                    var totalCount = bookingListCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                    foreach (Booking booking in bookingList)
                    {
                        Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                        if (exhibitor == null)
                            return NotFound();

                        BookingDTO singleBookingDTO = new BookingDTO();
                        foreach (Category singleCategory in exhibitor.Categories)
                        {
                            singleBookingDTO.CategoryName.Add(singleCategory.Name);
                        }

                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                        foreach (StallBooking stall in bookingStallList)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };

                            singleBookingDTO.StallList.Add(bookedSatllListDTO);
                        }

                        var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                        var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                        var payments = _paymentRepository.Find(paymentSepcification);
                        double totalAmountPaid = 0;
                        foreach (Payment payment in payments)
                        {
                            if (payment.IsPaymentDone == true)
                                totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                        }

                        double balance = booking.TotalAmount - totalAmountPaid;
                        singleBookingDTO.Id = booking.Id;
                        singleBookingDTO.BookingRequestId = "";
                        singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                        singleBookingDTO.CompanyName = exhibitor.CompanyName;
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                        singleBookingDTO.FinalAmount = booking.TotalAmount;
                        singleBookingDTO.AmountPaid = totalAmountPaid;
                        singleBookingDTO.Balance = balance;

                        listOfBookingDTO.Add(singleBookingDTO);
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfBookingDTO
                    };
                    return Ok(result);
                }

                if (statusName == "Cancelled")
                {
                    var bookingSearchCriteria = new BookingRequestSearchCriteria { EventId = eventId, Status = statusName };
                    var bookingSepcification = new BookingRequestSpecificationForSearch(bookingSearchCriteria);
                    var bookingListCount = _bookingRequestRepository.Count(bookingSepcification);
                    var bookingList = _bookingRequestRepository.Find(bookingSepcification)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);
                    var totalCount = bookingListCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                    if (bookingList.Count() == 0)
                    {
                        foreach (BookingRequest booking in bookingList)
                        {
                            Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                            if (exhibitor == null)
                                return NotFound();

                            BookingDTO singleBookingDTO = new BookingDTO();
                            foreach (Category singleCategory in exhibitor.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }

                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                            foreach (StallBooking stall in bookingStallList)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };

                                singleBookingDTO.StallList.Add(bookedSatllListDTO);
                            }

                            var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                            var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                            var payments = _paymentRepository.Find(paymentSepcification);
                            double totalAmountPaid = 0;
                            foreach (Payment payment in payments)
                            {
                                if (payment.IsPaymentDone == true)
                                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                            }

                            double balance = booking.TotalAmount - totalAmountPaid;
                            singleBookingDTO.Id = booking.Id;
                            singleBookingDTO.BookingRequestId = "";
                            singleBookingDTO.DateOfBooking = booking.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.CompanyName = exhibitor.CompanyName;
                            singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                            singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                            singleBookingDTO.FinalAmount = booking.TotalAmount;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.Balance = balance;

                            listOfBookingDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfBookingDTO
                    };
                    return Ok(result);
                }
                if (statusName == "Pending")
                {
                    var bookingSearchCriteria = new BookingRequestSearchCriteria { EventId = eventId, Status = statusName };
                    var bookingSepcification = new BookingRequestSpecificationForSearch(bookingSearchCriteria);
                    var bookingListCount = _bookingRequestRepository.Count(bookingSepcification);
                    var bookingList = _bookingRequestRepository.Find(bookingSepcification)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);
                    var totalCount = bookingListCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                    if (bookingList.Count() == 0)
                    {
                        foreach (BookingRequest booking in bookingList)
                        {
                            Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                            if (exhibitor == null)
                                return NotFound();

                            BookingDTO singleBookingDTO = new BookingDTO();
                            foreach (Category singleCategory in exhibitor.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }

                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                            foreach (StallBooking stall in bookingStallList)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };

                                singleBookingDTO.StallList.Add(bookedSatllListDTO);
                            }

                            var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                            var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                            var payments = _paymentRepository.Find(paymentSepcification);
                            double totalAmountPaid = 0;
                            foreach (Payment payment in payments)
                            {
                                if (payment.IsPaymentDone == true)
                                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                            }

                            double balance = booking.TotalAmount - totalAmountPaid;
                            singleBookingDTO.Id = booking.Id;
                            singleBookingDTO.BookingRequestId = "";
                            singleBookingDTO.DateOfBooking = booking.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.CompanyName = exhibitor.CompanyName;
                            singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                            singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                            singleBookingDTO.FinalAmount = booking.TotalAmount;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.Balance = balance;

                            listOfBookingDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfBookingDTO
                    };
                    return Ok(result);
                }

                if (statusName == "NotSold")
                {
                    var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                    var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);
                    var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                    if (layoutPlan == null)
                        return NotFound();

                    var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id, Type = "HANGER" };
                    var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                    var sectionList = _sectionRepository.Find(sectionSepcification);
                    List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();
                    int unbookedStalls = 0;
                    foreach (Section section in sectionList)
                    {
                        var stallCriteria = new StallSearchCriteria { sectionId = section.Id, IsBooked = "false" };
                        var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                        var stalls = _stallRepository.Find(stallSepcification);
                        unbookedStalls = unbookedStalls + stalls.Count();
                        foreach (Stall singleStall in stalls)
                        {
                            BookingDTO singleBookingDTO = new BookingDTO();
                            BookedStallListDTO unbookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = singleStall.Id,
                                Price = singleStall.Price,
                                StallNo = singleStall.StallNo,
                                StallSize = singleStall.StallSize
                            };
                            singleBookingDTO.StallList.Add(unbookedSatllListDTO);
                            listOfBookingDTO.Add(singleBookingDTO);
                        }
                    }

                    var totalCount = unbookedStalls;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfBookingDTO
                    };
                    return Ok(result);
                }

                if (statusName == "Reserved")
                {

                }
                return NotFound();
            }
        }

        /// <summary>
        /// Get Booking Details by Stall Size
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByStallSize")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingByStallSize(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                List<BookingStatusDTO> ListBookingStatusDTO = new List<BookingStatusDTO>();
                var distinctStallSize = _bookingStallRepository.DistinctStallSize(eventId);
                List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();
                foreach (int? stallSize in distinctStallSize)
                {
                    BookingStatusDTO singleBookingStallSize = new BookingStatusDTO();
                    if (stallSize != null)
                    {
                        var stallCriteria = new StallSearchCriteria { StallSize = stallSize, EventId = eventId };
                        var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                        var stalls = _stallRepository.Count(stallSepcification);
                        singleBookingStallSize.Item = stallSize.ToString();
                        singleBookingStallSize.TotalStalls = stalls;
                        ListBookingStatusDTO.Add(singleBookingStallSize);
                    }
                }
                return Ok(ListBookingStatusDTO);
            }
        }

        /// <summary>
        /// Get Booking Data by Stall Size
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="stallSize"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByStallSize/{stallSize}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(List<BookingDTO>))]
        public IHttpActionResult GetBookingDataByStallSize(Guid organizerId, Guid eventId, int stallSize, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var stallCriteria = new StallSearchCriteria { StallSize = stallSize, EventId = eventId };
                var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                var stallCount = _stallRepository.Count(stallSepcification);
                var stallsList = _stallRepository.Find(stallSepcification)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize);

                var totalCount = stallCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingDTO> ListBookingDTO = new List<BookingDTO>();
                foreach (Stall singleStall in stallsList)
                {
                    if (singleStall.IsBooked == true)
                    {
                        BookingDTO singleBookingDTO = new BookingDTO();
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = singleStall.Id,
                            Price = singleStall.Price,
                            StallNo = singleStall.StallNo,
                            StallSize = singleStall.StallSize
                        };
                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                        var bookingSearchCriteria = new BookingStallSearchCriteria { StallId = singleStall.Id };
                        var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                        var stallBooking = _stallBookingRepository.Find(bookingSepcification);
                        var booking = stallBooking.FirstOrDefault().Booking;
                        if (stallBooking.Count != 0)
                        {
                            var exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                            var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                            var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                            var payments = _paymentRepository.Find(paymentSepcification);
                            double totalAmountPaid = 0;
                            foreach (Payment payment in payments)
                            {
                                if (payment.IsPaymentDone == true)
                                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                            }

                            double balance = booking.TotalAmount - totalAmountPaid;
                            singleBookingDTO.Id = booking.Id;
                            singleBookingDTO.BookingRequestId = "";
                            singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.CompanyName = exhibitor.CompanyName;
                            singleBookingDTO.HangerName = singleStall.Section.Name;
                            singleBookingDTO.HangerId = singleStall.Section.Id;
                            singleBookingDTO.FinalAmount = booking.TotalAmount;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.Balance = balance;
                        }
                        ListBookingDTO.Add(singleBookingDTO);
                    }
                    else
                    {
                        BookingDTO singleBookingDTO = new BookingDTO();
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = singleStall.Id,
                            Price = singleStall.Price,
                            StallNo = singleStall.StallNo,
                            StallSize = singleStall.StallSize
                        };
                        singleBookingDTO.HangerName = singleStall.Section.Name;
                        singleBookingDTO.HangerId = singleStall.Section.Id;
                        singleBookingDTO.FinalAmount = singleStall.Price;
                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                        ListBookingDTO.Add(singleBookingDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = ListBookingDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Details by Sales Person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingBySalesPerson")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingBySalesPerson(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var role = "Sales";
                var criteria = new LoginSearchCriteria { Role = role };
                var sepcification = new LoginSpecificationForSearch(criteria);
                var loginList = _loginRepository.Find(sepcification).OrderBy(x => x.UserName);

                if (loginList.Count() == 0)
                    return NotFound();
                List<BookingStatusDTO> ListBookingStatusDTO = new List<BookingStatusDTO>();

                foreach (Login salesPerson in loginList)
                {
                    var bookingBySalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { SalesPersonId = salesPerson.Id, EventId = eventId };
                    var bookingBySalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingBySalesPersonSearchCriteria);
                    var bookingBySalesPersonList = _bookingSalesPersoneMapRepository.Find(bookingBySalesPersonSepcification);
                    BookingStatusDTO singleBookingStatus = new BookingStatusDTO();
                    int bookingStallsCount = 0;
                    foreach (BookingSalesPersonMap singleMap in bookingBySalesPersonList)
                    {
                        var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = singleMap.Booking.Id };
                        var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                        var bookingStallCount = _stallBookingRepository.Find(bookingSepcification).Count();
                        bookingStallsCount = bookingStallsCount + bookingStallCount;
                    }
                    singleBookingStatus.Item = salesPerson.UserName;
                    singleBookingStatus.TotalStalls = bookingStallsCount;
                    ListBookingStatusDTO.Add(singleBookingStatus);
                }
                return Ok(ListBookingStatusDTO);
            }
        }

        /// <summary>
        /// Get Booking Data by Sales 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPerson"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingBySalesPerson/{salesPerson}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingBySalesPerson(Guid organizerId, Guid eventId, string salesPerson, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new LoginSearchCriteria { UserName = salesPerson };
                var sepcification = new LoginSpecificationForSearch(criteria);
                var salesPersons = _loginRepository.Find(sepcification);

                if (salesPersons.Count() == 0)
                    return NotFound();

                List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                var bookingBySalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { SalesPersonId = salesPersons.FirstOrDefault().Id };
                var bookingBySalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingBySalesPersonSearchCriteria);
                var bookingBySalesPersonCount = _bookingSalesPersoneMapRepository.Count(bookingBySalesPersonSepcification);
                var bookingBySalesPersonList = _bookingSalesPersoneMapRepository.Find(bookingBySalesPersonSepcification)
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize);

                var totalCount = bookingBySalesPersonCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                BookingStatusDTO singleBookingStatus = new BookingStatusDTO();
                foreach (BookingSalesPersonMap singleMap in bookingBySalesPersonList)
                {
                    Exhibitor exhibitor = _exhibitorRepository.GetById(singleMap.Booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    BookingDTO singleBookingDTO = new BookingDTO();
                    foreach (Category singleCategory in exhibitor.Categories)
                    {
                        singleBookingDTO.CategoryName.Add(singleCategory.Name);
                    }

                    var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = singleMap.Booking.Id };
                    var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                    var bookingStallList = _stallBookingRepository.Find(bookingSepcification);

                    foreach (StallBooking stall in bookingStallList)
                    {
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = stall.Id,
                            Price = stall.Stall.Price,
                            StallNo = stall.Stall.StallNo,
                            StallSize = stall.Stall.StallSize
                        };
                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                    }
                    var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = singleMap.Booking.Id };
                    var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                    var payments = _paymentRepository.Find(paymentSepcification);
                    double totalAmountPaid = 0;
                    foreach (Payment payment in payments)
                    {
                        if (payment.IsPaymentDone == true)
                            totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                    }

                    double balance = singleMap.Booking.TotalAmount - totalAmountPaid;
                    singleBookingDTO.Id = singleMap.Booking.Id;
                    singleBookingDTO.BookingRequestId = "";
                    singleBookingDTO.DateOfBooking = singleMap.Booking.BookingDate.ToString("dd-MMM-yyyy");
                    singleBookingDTO.CompanyName = exhibitor.CompanyName;
                    if (bookingStallList.Count() != 0)
                    {
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                    }
                    singleBookingDTO.SalesPerson = salesPersons.FirstOrDefault().UserName;
                    singleBookingDTO.FinalAmount = singleMap.Booking.TotalAmount;
                    singleBookingDTO.AmountPaid = totalAmountPaid;
                    singleBookingDTO.Balance = balance;

                    listOfBookingDTO.Add(singleBookingDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listOfBookingDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Details by Country
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByCountry")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingByCountry(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var distinctCountry = _bookingStallRepository.DistinctCountry();
                List<BookingStatusDTO> listBookingStatusDTO = new List<BookingStatusDTO>();
                foreach (string countryName in distinctCountry)
                {
                    if (countryName != null)
                    {
                        var countrySearchCriteria = new CountrySearchCriteria { CountryName = countryName };
                        var countrySepcification = new CountrySpecificationForSearch(countrySearchCriteria);
                        var country = _countryRepository.Find(countrySepcification);
                        if (country.Count != 0)
                        {
                            BookingStatusDTO singleBookingStatus = new BookingStatusDTO();
                            var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, CountryId = country.FirstOrDefault().Id };
                            var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                            var stallBookingCount = _stallBookingRepository.Count(bookingSepcification);
                            singleBookingStatus.Item = countryName;
                            singleBookingStatus.TotalStalls = stallBookingCount;
                            listBookingStatusDTO.Add(singleBookingStatus);
                        }
                    }
                }
                return Ok(listBookingStatusDTO);
            }
        }

        /// <summary>
        /// get Booking Data by Country 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="countryName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByCountry/{countryName}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingByCountry(Guid organizerId, Guid eventId, string countryName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var countrySearchCriteria = new CountrySearchCriteria { CountryName = countryName };
                var countrySepcification = new CountrySpecificationForSearch(countrySearchCriteria);
                var country = _countryRepository.Find(countrySepcification);

                if (country.Count() == 0)
                    return NotFound();

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId, CountryId = country.FirstOrDefault().Id };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingListCount = _bookingRepository.Count(bookingSepcification);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                foreach (Booking booking in bookingList)
                {
                    Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    BookingDTO singleBookingDTO = new BookingDTO();
                    foreach (Category singleCategory in exhibitor.Categories)
                    {
                        singleBookingDTO.CategoryName.Add(singleCategory.Name);
                    }

                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                    foreach (StallBooking stall in bookingStallList)
                    {
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = stall.Id,
                            Price = stall.Stall.Price,
                            StallNo = stall.Stall.StallNo,
                            StallSize = stall.Stall.StallSize
                        };
                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                    }

                    var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                    var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                    var payments = _paymentRepository.Find(paymentSepcification);
                    double totalAmountPaid = 0;
                    foreach (Payment payment in payments)
                    {
                        if (payment.IsPaymentDone == true)
                            totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                    }

                    var bookingBySalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = booking.Id };
                    var bookingBySalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingBySalesPersonSearchCriteria);
                    var bookingBySalesPerson = _bookingSalesPersoneMapRepository.Find(bookingBySalesPersonSepcification);
                    var salesPerson = _loginRepository.GetById(bookingBySalesPerson.FirstOrDefault().Login.Id);
                    double balance = booking.TotalAmount - totalAmountPaid;
                    singleBookingDTO.Id = booking.Id;
                    singleBookingDTO.BookingRequestId = "";
                    singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                    singleBookingDTO.CompanyName = exhibitor.CompanyName;
                    if (bookingStallList.Count() != 0)
                    {
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                    }
                    if (salesPerson != null)
                        singleBookingDTO.SalesPerson = salesPerson.UserName;
                    singleBookingDTO.FinalAmount = booking.TotalAmount;
                    singleBookingDTO.AmountPaid = totalAmountPaid;
                    singleBookingDTO.Balance = balance;

                    listOfBookingDTO.Add(singleBookingDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listOfBookingDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Details By Date
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByDate")]
        [ResponseType(typeof(List<BookingStatusDTO>))]
        public IHttpActionResult GetBookingByDate(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                var lastSixMonths = Enumerable.Range(0, 6).Select(i => eventDetails.StartDate.AddMonths(-i).AddDays(-eventDetails.StartDate.Day + 1).ToString("MM/dd/yyyy"));
                List<BookingStatusDTO> listBookingStatus = new List<BookingStatusDTO>();
                foreach (string Month in lastSixMonths)
                {
                    DateTime startDate = Convert.ToDateTime(Month);
                    DateTime endDate = new DateTime(startDate.Year, startDate.Month,
                                    DateTime.DaysInMonth(startDate.Year, startDate.Month));

                    BookingStatusDTO singleBookingStatus = new BookingStatusDTO();
                    var bookingSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, StartDate = startDate, EndDate = endDate };
                    var bookingSepcification = new BookingStallSpecificationForSearch(bookingSearchCriteria);
                    var stallBookingCount = _stallBookingRepository.Count(bookingSepcification);
                    singleBookingStatus.Item = startDate.ToString("MMM-yyyy");
                    singleBookingStatus.TotalStalls = stallBookingCount;
                    listBookingStatus.Add(singleBookingStatus);
                }
                return Ok(listBookingStatus);
            }
        }

        /// <summary>
        /// Get Booking Data By Date
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/bookingByDate/{date}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(List<BookingDTO>))]
        public IHttpActionResult GetBookingByDate(Guid organizerId, Guid eventId, string date, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                DateTime startDate = Convert.ToDateTime(date);

                DateTime endDate = new DateTime(startDate.Year, startDate.Month,
                                    DateTime.DaysInMonth(startDate.Year, startDate.Month));

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId, StartDate = startDate, EndDate = endDate };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingListCount = _bookingRepository.Count(bookingSepcification);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                foreach (Booking booking in bookingList)
                {
                    Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    BookingDTO singleBookingDTO = new BookingDTO();
                    foreach (Category singleCategory in exhibitor.Categories)
                    {
                        singleBookingDTO.CategoryName.Add(singleCategory.Name);
                    }

                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { EventId = eventId, BookingId = booking.Id };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                    foreach (StallBooking stall in bookingStallList)
                    {
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = stall.Id,
                            Price = stall.Stall.Price,
                            StallNo = stall.Stall.StallNo,
                            StallSize = stall.Stall.StallSize
                        };
                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                    }

                    var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = booking.Id };
                    var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                    var payments = _paymentRepository.Find(paymentSepcification);
                    double totalAmountPaid = 0;
                    foreach (Payment payment in payments)
                    {
                        if (payment.IsPaymentDone == true)
                            totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                    }

                    var bookingBySalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = booking.Id };
                    var bookingBySalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingBySalesPersonSearchCriteria);
                    var bookingBySalesPerson = _bookingSalesPersoneMapRepository.Find(bookingBySalesPersonSepcification);
                    var salesPerson = _loginRepository.GetById(bookingBySalesPerson.FirstOrDefault().Login.Id);
                    double balance = booking.TotalAmount - totalAmountPaid;
                    singleBookingDTO.Id = booking.Id;
                    singleBookingDTO.BookingRequestId = "";
                    singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                    singleBookingDTO.CompanyName = exhibitor.CompanyName;
                    if (bookingStallList.Count() != 0)
                    {
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                    }
                    if (salesPerson != null)
                        singleBookingDTO.SalesPerson = salesPerson.UserName;
                    singleBookingDTO.FinalAmount = booking.TotalAmount;
                    singleBookingDTO.AmountPaid = totalAmountPaid;
                    singleBookingDTO.Balance = balance;

                    listOfBookingDTO.Add(singleBookingDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listOfBookingDTO
                };
                return Ok(result);
            }
        }
    }
}
