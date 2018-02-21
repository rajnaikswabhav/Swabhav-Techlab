using Modules.EventManagement;
using Modules.LayoutManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    /// <summary>
    /// Get All Booking Details
    /// </summary>
    [RoutePrefix("api/v1/organizers/{organizerId:guid}")]
    public class BookingController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<BookingRequest> _bookingRequestRepository = new EntityFrameworkRepository<BookingRequest>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Section> _pavilionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<Stall> _stallRepository = new EntityFrameworkRepository<Stall>();
        private IRepository<BookingRequestStall> _bookingRequestStallRepository = new EntityFrameworkRepository<BookingRequestStall>();
        private IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private IRepository<Booking> _bookingRepository = new EntityFrameworkRepository<Booking>();
        private IRepository<StallBooking> _stallBookingRepository = new EntityFrameworkRepository<StallBooking>();
        private IRepository<Payment> _paymentRepository = new EntityFrameworkRepository<Payment>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<BookingSalesPersonMap> _bookingSalesPersonMapRepository = new EntityFrameworkRepository<BookingSalesPersonMap>();
        private IRepository<BookingRequestSalesPersonMap> _bookingRequestSalesPersonMapRepository = new EntityFrameworkRepository<BookingRequestSalesPersonMap>();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private IRepository<EventLeadExhibitorMap> _eventLeadExhibitorMapRepository = new EntityFrameworkRepository<EventLeadExhibitorMap>();
        private IRepository<PaymentDetails> _paymentDetailsRepository = new EntityFrameworkRepository<PaymentDetails>();
        private IRepository<BookingRequestInstallment> _bookingRequestInstallmentRepository = new EntityFrameworkRepository<BookingRequestInstallment>();
        private IRepository<BookingInstallment> _bookingInstallmentRepository = new EntityFrameworkRepository<BookingInstallment>();
        private IRepository<ExhibitorStatus> _exhibitorStatusRepository = new EntityFrameworkRepository<ExhibitorStatus>();
        private BookingRepository<BookingRequestSalesPersonMap> _bookingRequestSalesPersonMapBookingRepository = new BookingRepository<BookingRequestSalesPersonMap>();
        private BookingRepository<BookingSalesPersonMap> _bookingSalesPersonMapBookingRepository = new BookingRepository<BookingSalesPersonMap>();
        private BookingRepository<EventLeadExhibitorMap> _eventLeadExhibitorMapBookingRepository = new BookingRepository<EventLeadExhibitorMap>();
        private BookingRepository<BookingRequest> _bookingRequestSearchRepository = new BookingRepository<BookingRequest>();
        private BookingRepository<PaymentDetails> _bookingPaymentRepository = new BookingRepository<PaymentDetails>();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get Booking Listing of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="pavilionId"></param>
        /// <param name="exhibitorIndustryId"></param>
        /// <returns></returns>
        [Route("BookingListing/{eventId:guid}/{pageSize}/{pageNumber}")]
        [ResponseType(typeof(BookingListingDTO[]))]
        public IHttpActionResult GetBookingListing(Guid organizerId, Guid eventId, int pageSize, int pageNumber, string startDate = null, string endDate = null, Guid? salesPersonId = null, Guid? pavilionId = null, Guid? exhibitorIndustryId = null, Guid? partnerId = null)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var bookingCriteria = new BookingSalesPersonMapSearchCriteria();
                if (!string.IsNullOrEmpty(startDate))
                {
                    bookingCriteria.StartDate = Convert.ToDateTime(startDate);
                }
                if (!string.IsNullOrEmpty(startDate))
                {
                    bookingCriteria.EndDate = Convert.ToDateTime(endDate);
                }
                if (salesPersonId != Guid.Empty && salesPersonId != null)
                {
                    bookingCriteria.SalesPersonId = salesPersonId.Value;
                }
                if (pavilionId != Guid.Empty && pavilionId != null)
                {
                    bookingCriteria.PavilionId = pavilionId.Value;
                }
                if (exhibitorIndustryId != Guid.Empty && exhibitorIndustryId != null)
                {
                    bookingCriteria.ExhibitorIndustryId = exhibitorIndustryId.Value;
                }
                if (partnerId != Guid.Empty && partnerId != null)
                {
                    bookingCriteria.PartnerId = partnerId.Value;
                }
                bookingCriteria.EventId = eventId;

                var bookingSpecification = new BookingSalesPersonMapSpecificationForSearch(bookingCriteria);
                var bookingListCount = _bookingSalesPersonMapRepository.Count(bookingSpecification);
                var bookingSalesPersonMapList = _bookingSalesPersonMapRepository.Find(bookingSpecification)
                                  .OrderByDescending(x => x.Booking.BookingDate)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize);

                var totalCount = bookingListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingListingDTO> listOfBookingListingDTO = new List<BookingListingDTO>();
                foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPersonMapList)
                {
                    if (bookingSalesPersonMap.Booking.BookingAccepted == true)
                    {
                        BookingListingDTO singleBookingListingDTO = new BookingListingDTO();

                        var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id };
                        var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                        var payments = _paymentDetailsRepository.Find(paymentSepcification);
                        double totalAmountPaid = 0;
                        foreach (PaymentDetails payment in payments)
                        {
                            if (payment.IsPaymentApprove == true)
                                totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                        }
                        singleBookingListingDTO.ReceivedAmount = totalAmountPaid;
                        singleBookingListingDTO.PendingAmount = bookingSalesPersonMap.Booking.FinalAmountTax - totalAmountPaid;

                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id, EventId = eventDetails.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                        if (bookingStall.Count() == 0)
                            return NotFound();

                        foreach (StallBooking stall in bookingStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallNo = stall.Stall.StallNo
                            };
                            singleBookingListingDTO.StallList.Add(bookedSatllListDTO);
                        }
                        singleBookingListingDTO.BookingId = bookingSalesPersonMap.Booking.Id;
                        singleBookingListingDTO.BookingReferenceId = bookingSalesPersonMap.Booking.BookingId;
                        singleBookingListingDTO.CompanyName = bookingSalesPersonMap.Booking.Exhibitor.CompanyName;
                        singleBookingListingDTO.BookingDate = bookingSalesPersonMap.Booking.BookingDate.ToString("dd-MM-yyyy");
                        singleBookingListingDTO.StallCount = bookingStall.Count();
                        singleBookingListingDTO.TotalAmount = bookingSalesPersonMap.Booking.FinalAmountTax;
                        singleBookingListingDTO.Comment = bookingSalesPersonMap.Booking.Comment;

                        listOfBookingListingDTO.Add(singleBookingListingDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfExhibitorDTO = listOfBookingListingDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Request List of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingRequestList/{eventId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(List<BookingRequestDTO>))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingEvent = _eventRepository.GetById(eventId);
                if (bookingEvent == null)
                    return NotFound();

                var bookingRequeSearchCriteria = new BookingRequestSearchCriteria { EventId = eventId, Status = "Pending" };
                var bookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequeSearchCriteria);
                var bookingRequestListCount = _bookingRequestRepository.Count(bookingRequestSepcification);
                var bookingRequestList = _bookingRequestRepository.Find(bookingRequestSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var totalCount = bookingRequestListCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                if (bookingRequestList.Count() == 0)
                    return NotFound();

                foreach (BookingRequest bookingRequest in bookingRequestList)
                {
                    Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequest.Exhibitor.Id);

                    if (bookingRequest.Booking == null)
                    {
                        BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                        foreach (Category singleCategory in exhibitorDeatils.Categories)
                        {
                            singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                        }
                        var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                        var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                        var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                        if (bookingRequestStall.Count() == 0)
                            return NotFound();

                        Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                        double totalPrice = 0;
                        foreach (BookingRequestStall stall in bookingRequestStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };
                            totalPrice = totalPrice + stall.Stall.Price;
                            singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                        }

                        var bookingRequestSalesPersonMapCriteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequest.Id };
                        var bookingRequestSalesPersonSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequestSalesPersonMapCriteria);
                        var bookingSalesPersonMap = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonSepcification);

                        singleBookingRequestDTO.Id = bookingRequest.Id;
                        singleBookingRequestDTO.BookingRequestId = bookingRequest.BookingRequestId;
                        singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                        singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                        singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                        singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                        singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                        singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                        singleBookingRequestDTO.DateOfBooking = bookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                        singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                        singleBookingRequestDTO.TotalAmount = bookingRequest.TotalAmount;
                        singleBookingRequestDTO.Status = bookingRequest.Status;
                        singleBookingRequestDTO.Comment = bookingRequest.Comment;
                        singleBookingRequestDTO.DiscountInPercent = bookingRequest.DiscountPercent;
                        singleBookingRequestDTO.DiscountInAmount = bookingRequest.DiscountAmount;
                        if (bookingSalesPersonMap.Count() != 0)
                        {
                            singleBookingRequestDTO.SalesPerson = bookingSalesPersonMap.FirstOrDefault().Login.UserName;
                        }
                        singleBookingRequestDTO.FinalAmount = bookingRequest.FinalAmountTax;
                        listBookingRequestDTO.Add(singleBookingRequestDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listBookingRequestDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Request List of Sales person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingRequestList/{eventId:guid}/salesPerson/{salesPersonId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(List<BookingRequestDTO>))]
        public IHttpActionResult GetBookingRequestBySalesPerson(Guid organizerId, Guid eventId, Guid salesPersonId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var bookingRequestSalesPersonMapCount = _bookingRequestSalesPersonMapBookingRepository.FindBookingRequestOfReportingSalespersonsCount(salesPerson.Partner, eventDetails);
                    var bookingRequestSalesPersonMapList = _bookingRequestSalesPersonMapBookingRepository.FindBookingRequestOfReportingSalespersons(salesPerson.Partner, eventDetails, pageNumber, pageSize);
                    var bookingCount = bookingRequestSalesPersonMapCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingRequestSalesPersonMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingRequestSalesPersonMap bookingRequestSalesPersonMap in bookingRequestSalesPersonMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequestSalesPersonMap.BookingRequest.Exhibitor.Id);

                        if (bookingRequestSalesPersonMap.BookingRequest.Booking == null)
                        {
                            BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.Id };
                            var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                            var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                            if (bookingRequestStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (BookingRequestStall stall in bookingRequestStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingRequestDTO.Id = bookingRequestSalesPersonMap.BookingRequest.Id;
                            singleBookingRequestDTO.BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.BookingRequestId;
                            singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                            singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                            singleBookingRequestDTO.DateOfBooking = bookingRequestSalesPersonMap.BookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                            singleBookingRequestDTO.TotalAmount = bookingRequestSalesPersonMap.BookingRequest.TotalAmount;
                            singleBookingRequestDTO.Status = bookingRequestSalesPersonMap.BookingRequest.Status;
                            singleBookingRequestDTO.Comment = bookingRequestSalesPersonMap.BookingRequest.Comment;
                            singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingRequestDTO.DiscountInPercent = bookingRequestSalesPersonMap.BookingRequest.DiscountPercent;
                            singleBookingRequestDTO.DiscountInAmount = bookingRequestSalesPersonMap.BookingRequest.DiscountAmount;
                            if (bookingRequestSalesPersonMap != null)
                            {
                                singleBookingRequestDTO.SalesPerson = bookingRequestSalesPersonMap.Login.UserName;
                            }
                            singleBookingRequestDTO.FinalAmount = bookingRequestSalesPersonMap.BookingRequest.FinalAmountTax;
                            listBookingRequestDTO.Add(singleBookingRequestDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
                else
                {
                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { SalesPersonId = salesPersonId, EventId = eventDetails.Id };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    var bookingRequestSalesPeronMapListCount = _bookingRequestSalesPersonMapRepository.Count(bookingRequestSalesPersonMapSepcification);
                    var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification).OrderByDescending(x => x.CreatedOn)
                                                          .Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                    var bookingCount = bookingRequestSalesPeronMapListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);
                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingRequestSalesPeronMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingRequestSalesPersonMap bookingRequestSalesPersonMap in bookingRequestSalesPeronMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequestSalesPersonMap.BookingRequest.Exhibitor.Id);

                        if (bookingRequestSalesPersonMap.BookingRequest.Booking == null)
                        {
                            BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.Id };
                            var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                            var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                            if (bookingRequestStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (BookingRequestStall stall in bookingRequestStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingRequestDTO.Id = bookingRequestSalesPersonMap.BookingRequest.Id;
                            singleBookingRequestDTO.BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.BookingRequestId;
                            singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                            singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                            singleBookingRequestDTO.DateOfBooking = bookingRequestSalesPersonMap.BookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                            singleBookingRequestDTO.TotalAmount = bookingRequestSalesPersonMap.BookingRequest.TotalAmount;
                            singleBookingRequestDTO.Status = bookingRequestSalesPersonMap.BookingRequest.Status;
                            singleBookingRequestDTO.Comment = bookingRequestSalesPersonMap.BookingRequest.Comment;
                            singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingRequestDTO.DiscountInPercent = bookingRequestSalesPersonMap.BookingRequest.DiscountPercent;
                            singleBookingRequestDTO.DiscountInAmount = bookingRequestSalesPersonMap.BookingRequest.DiscountAmount;

                            if (bookingRequestSalesPersonMap != null)
                            {
                                singleBookingRequestDTO.SalesPerson = bookingRequestSalesPersonMap.Login.UserName;
                            }
                            singleBookingRequestDTO.FinalAmount = bookingRequestSalesPersonMap.BookingRequest.FinalAmountTax;
                            listBookingRequestDTO.Add(singleBookingRequestDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Booking Request Search for Sales Person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="exhibitorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingRequestList/{eventId:guid}/salesPerson/{salesPersonId:guid}/exhibitorSearch/{exhibitorName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(List<BookingRequestDTO>))]
        public IHttpActionResult GetBookingRequestBySalesPersonExhibitorName(Guid organizerId, Guid eventId, Guid salesPersonId, string exhibitorName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var bookingRequestSalesPersonMapCount = _bookingRequestSalesPersonMapBookingRepository.FindBookingRequestOfReportingSalespersonsCount(salesPerson.Partner, eventDetails, exhibitorName);
                    var bookingRequestSalesPersonMapList = _bookingRequestSalesPersonMapBookingRepository.FindBookingRequestOfReportingSalespersons(salesPerson.Partner, eventDetails, exhibitorName, pageNumber, pageSize);
                    var bookingCount = bookingRequestSalesPersonMapCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingRequestSalesPersonMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingRequestSalesPersonMap bookingRequestSalesPersonMap in bookingRequestSalesPersonMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequestSalesPersonMap.BookingRequest.Exhibitor.Id);

                        if (bookingRequestSalesPersonMap.BookingRequest.Booking == null)
                        {
                            BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.Id };
                            var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                            var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                            if (bookingRequestStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (BookingRequestStall stall in bookingRequestStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingRequestDTO.Id = bookingRequestSalesPersonMap.BookingRequest.Id;
                            singleBookingRequestDTO.BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.BookingRequestId;
                            singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                            singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                            singleBookingRequestDTO.DateOfBooking = bookingRequestSalesPersonMap.BookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                            singleBookingRequestDTO.TotalAmount = bookingRequestSalesPersonMap.BookingRequest.TotalAmount;
                            singleBookingRequestDTO.Status = bookingRequestSalesPersonMap.BookingRequest.Status;
                            singleBookingRequestDTO.Comment = bookingRequestSalesPersonMap.BookingRequest.Comment;
                            singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingRequestDTO.DiscountInPercent = bookingRequestSalesPersonMap.BookingRequest.DiscountPercent;
                            singleBookingRequestDTO.DiscountInAmount = bookingRequestSalesPersonMap.BookingRequest.DiscountAmount;
                            if (bookingRequestSalesPersonMap != null)
                            {
                                singleBookingRequestDTO.SalesPerson = bookingRequestSalesPersonMap.Login.UserName;
                            }
                            singleBookingRequestDTO.FinalAmount = bookingRequestSalesPersonMap.BookingRequest.FinalAmountTax;
                            listBookingRequestDTO.Add(singleBookingRequestDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
                else
                {
                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { SalesPersonId = salesPersonId, EventId = eventDetails.Id, ExhibitorCompanyName = exhibitorName };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    var bookingRequestSalesPeronMapListCount = _bookingRequestSalesPersonMapRepository.Count(bookingRequestSalesPersonMapSepcification);
                    var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification)
                                                          .OrderByDescending(x => x.CreatedOn)
                                                          .Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                    var bookingCount = bookingRequestSalesPeronMapListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);
                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingRequestSalesPeronMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingRequestSalesPersonMap bookingRequestSalesPersonMap in bookingRequestSalesPeronMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequestSalesPersonMap.BookingRequest.Exhibitor.Id);

                        if (bookingRequestSalesPersonMap.BookingRequest.Booking == null)
                        {
                            BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.Id };
                            var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                            var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                            if (bookingRequestStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (BookingRequestStall stall in bookingRequestStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingRequestDTO.Id = bookingRequestSalesPersonMap.BookingRequest.Id;
                            singleBookingRequestDTO.BookingRequestId = bookingRequestSalesPersonMap.BookingRequest.BookingRequestId;
                            singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                            singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                            singleBookingRequestDTO.DateOfBooking = bookingRequestSalesPersonMap.BookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                            singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                            singleBookingRequestDTO.TotalAmount = bookingRequestSalesPersonMap.BookingRequest.TotalAmount;
                            singleBookingRequestDTO.Status = bookingRequestSalesPersonMap.BookingRequest.Status;
                            singleBookingRequestDTO.Comment = bookingRequestSalesPersonMap.BookingRequest.Comment;
                            singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingRequestDTO.DiscountInPercent = bookingRequestSalesPersonMap.BookingRequest.DiscountPercent;
                            singleBookingRequestDTO.DiscountInAmount = bookingRequestSalesPersonMap.BookingRequest.DiscountAmount;

                            if (bookingRequestSalesPersonMap != null)
                            {
                                singleBookingRequestDTO.SalesPerson = bookingRequestSalesPersonMap.Login.UserName;
                            }
                            singleBookingRequestDTO.FinalAmount = bookingRequestSalesPersonMap.BookingRequest.FinalAmountTax;
                            listBookingRequestDTO.Add(singleBookingRequestDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Get Single Booking Request Details
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        [Route("bookingRequest/{bookingId:guid}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetByBookingRequestId(Guid organizerId, Guid bookingId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingRequest = _bookingRequestRepository.GetById(bookingId);
                if (bookingRequest == null)
                    return NotFound();

                BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();
                var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                if (bookingRequestStall.Count() == 0)
                    return NotFound();

                var bookingRequestInstallmentSearchCriteria = new BookingRequestInstallmentSearchCriteria { BookingRequestId = bookingRequest.Id };
                var bookingRequestInstallmentSepcification = new BookingRequestInstallmentSpecificationForSearch(bookingRequestInstallmentSearchCriteria);
                var bookingRequestInstallments = _bookingRequestInstallmentRepository.Find(bookingRequestInstallmentSepcification)
                                                .OrderBy(x => x.Order);

                foreach (BookingRequestInstallment bookingRequestInstallment in bookingRequestInstallments)
                {
                    BookingInstallmentDTO bookingRequestInstallmentDTO = new BookingInstallmentDTO
                    {
                        BookingInstallmentId = bookingRequestInstallment.Id,
                        Percent = bookingRequestInstallment.Percent,
                        Amount = bookingRequestInstallment.Amount,
                        PaymentDate = bookingRequestInstallment.PaymentDate,
                        IsPaid = bookingRequestInstallment.IsPaid,
                        Order = bookingRequestInstallment.Order
                    };
                    singleBookingRequestDTO.BookingInstallmentDTO.Add(bookingRequestInstallmentDTO);
                }

                Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);

                Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingRequest.Exhibitor.Id);
                foreach (Category singleCategory in exhibitorDeatils.Categories)
                {
                    singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                }

                double totalPrice = 0;
                foreach (BookingRequestStall stall in bookingRequestStall)
                {
                    BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                    {
                        StallId = stall.Stall.Id,
                        Price = stall.Stall.Price,
                        StallNo = stall.Stall.StallNo,
                        StallSize = stall.Stall.StallSize
                    };
                    totalPrice = totalPrice + stall.Stall.Price;
                    singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                }

                var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequest.Id };
                var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification);

                singleBookingRequestDTO.Id = bookingRequest.Id;
                singleBookingRequestDTO.BookingRequestId = bookingRequest.BookingRequestId;
                singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                singleBookingRequestDTO.DateOfBooking = bookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                singleBookingRequestDTO.TotalAmount = bookingRequest.TotalAmount;
                singleBookingRequestDTO.Status = bookingRequest.Status;
                singleBookingRequestDTO.Comment = bookingRequest.Comment;
                if (bookingRequestSalesPeronMapList.Count() != 0)
                {
                    singleBookingRequestDTO.SalesPerson = bookingRequestSalesPeronMapList.FirstOrDefault().Login.UserName;
                }
                singleBookingRequestDTO.HangerHeight = stallDetails.Section.Height;
                singleBookingRequestDTO.HangerWidth = stallDetails.Section.Width;
                singleBookingRequestDTO.FinalAmount = bookingRequest.FinalAmountTax;
                return Ok(singleBookingRequestDTO);
            }
        }

        /// <summary>
        /// Get Single Booking Details
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        [Route("booking/{bookingId:guid}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetByBookingId(Guid organizerId, Guid bookingId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var booking = _bookingRepository.GetById(bookingId);

                if (booking == null)
                    return NotFound();

                BookingRequestDTO singleBookingDTO = new BookingRequestDTO();
                var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = booking.Id, EventId = booking.Event.Id };
                var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                if (bookingStall.Count() == 0)
                    return NotFound();

                var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = booking.Id };
                var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                var payments = _paymentDetailsRepository.Find(paymentSepcification);
                double totalAmountPaid = 0;
                foreach (PaymentDetails payment in payments)
                {
                    if (payment.IsPaymentApprove == true)
                        totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                }
                singleBookingDTO.AmountPaid = totalAmountPaid;

                var bookingRequestInstallmentSearchCriteria = new BookingInstallmentSearchCriteria { BookingId = booking.Id };
                var bookingRequestInstallmentSepcification = new BookingInstallmentSpecificationForSearch(bookingRequestInstallmentSearchCriteria);
                var bookingInstallments = _bookingInstallmentRepository.Find(bookingRequestInstallmentSepcification).OrderBy(x => x.Order);

                foreach (BookingInstallment bookingInstallment in bookingInstallments)
                {
                    BookingInstallmentDTO bookingInstallmentDTO = new BookingInstallmentDTO
                    {
                        BookingInstallmentId = bookingInstallment.Id,
                        Percent = bookingInstallment.Percent,
                        Amount = bookingInstallment.Amount,
                        PaymentDate = bookingInstallment.PaymentDate,
                        IsPaid = bookingInstallment.IsPaid,
                        Order = bookingInstallment.Order
                    };
                    singleBookingDTO.BookingInstallmentDTO.Add(bookingInstallmentDTO);
                }

                Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);

                Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                foreach (Category singleCategory in exhibitorDeatils.Categories)
                {
                    singleBookingDTO.CategoryName.Add(singleCategory.Name);
                }

                double totalPrice = 0;
                foreach (StallBooking stall in bookingStall)
                {
                    BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                    {
                        StallId = stall.Stall.Id,
                        Price = stall.Stall.Price,
                        StallNo = stall.Stall.StallNo,
                        StallSize = stall.Stall.StallSize
                    };
                    totalPrice = totalPrice + stall.Stall.Price;
                    singleBookingDTO.stallList.Add(bookedSatllListDTO);
                }

                var bookingSalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = booking.Id };
                var bookingSalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPersonSearchCriteria);
                var bookingSalesPersonList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonSepcification);

                if (bookingSalesPersonList.Count() != 0)
                {
                    singleBookingDTO.SalesPerson = bookingSalesPersonList.FirstOrDefault().Login.UserName;
                }

                singleBookingDTO.Id = booking.Id;
                singleBookingDTO.BookingRequestId = booking.BookingId;
                singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                singleBookingDTO.HangerName = stallDetails.Section.Name;
                singleBookingDTO.HangerId = stallDetails.Section.Id;
                singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                singleBookingDTO.Quantity = bookingStall.Count();
                singleBookingDTO.TotalAmount = booking.TotalAmount;
                singleBookingDTO.Status = booking.Status;
                singleBookingDTO.Comment = booking.Comment;
                singleBookingDTO.FinalAmount = booking.FinalAmountTax;
                singleBookingDTO.DiscountInAmount = booking.DiscountAmount;
                singleBookingDTO.DiscountInPercent = booking.DiscountPercent;
                singleBookingDTO.AmountPaid = totalAmountPaid;
                singleBookingDTO.RemainingAmount = booking.FinalAmountTax - totalAmountPaid;

                return Ok(singleBookingDTO);
            }
        }

        /// <summary>
        /// Get Booking Data By Sales Person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/{eventId:guid}/salesPerson/{salesPersonId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBookingDetailsBySalesPerson(Guid organizerId, Guid eventId, Guid salesPersonId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var bookingSalesPersonMapCount = _bookingSalesPersonMapBookingRepository.FindBookingOfReportingSalespersonsCount(salesPerson.Partner, eventDetails);
                    var bookingSalesPersonMapList = _bookingSalesPersonMapBookingRepository.FindBookingOfReportingSalespersons(salesPerson.Partner, eventDetails, pageNumber, pageSize);

                    var bookingCount = bookingSalesPersonMapCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingSalesPersonMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPersonMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingSalesPersonMap.Booking.Exhibitor.Id);

                        if (bookingSalesPersonMap.Booking.BookingAccepted == true)
                        {
                            BookingRequestDTO singleBookingDTO = new BookingRequestDTO();

                            var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id };
                            var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                            var payments = _paymentDetailsRepository.Find(paymentSepcification);
                            double totalAmountPaid = 0;
                            foreach (PaymentDetails payment in payments)
                            {
                                if (payment.IsPaymentApprove == true)
                                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                            }
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id, EventId = bookingSalesPersonMap.Booking.Event.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                            if (bookingStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (StallBooking stall in bookingStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingDTO.Id = bookingSalesPersonMap.Booking.Id;
                            singleBookingDTO.BookingRequestId = bookingSalesPersonMap.Booking.BookingId;
                            singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingDTO.HangerName = stallDetails.Section.Name;
                            singleBookingDTO.HangerId = stallDetails.Section.Id;
                            singleBookingDTO.DateOfBooking = bookingSalesPersonMap.Booking.BookingDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.Quantity = bookingStall.Count();
                            singleBookingDTO.TotalAmount = bookingSalesPersonMap.Booking.TotalAmount;
                            singleBookingDTO.FinalAmount = bookingSalesPersonMap.Booking.FinalAmountTax;
                            singleBookingDTO.Status = bookingSalesPersonMap.Booking.Status;
                            singleBookingDTO.Comment = bookingSalesPersonMap.Booking.Comment;
                            singleBookingDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.RemainingAmount = bookingSalesPersonMap.Booking.FinalAmountTax - totalAmountPaid;

                            if (bookingSalesPersonMap != null)
                            {
                                singleBookingDTO.SalesPerson = bookingSalesPersonMap.Login.UserName;
                            }
                            listBookingRequestDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
                else
                {
                    var bookingSalesPeronMapSearchCriteria = new BookingSalesPersonMapSearchCriteria { SalesPersonId = salesPersonId, EventId = eventDetails.Id };
                    var bookingSalesPersonMapSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPeronMapSearchCriteria);
                    var bookingSalesPeronMapListCount = _bookingSalesPersonMapRepository.Count(bookingSalesPersonMapSepcification);
                    var bookingSalesPeronMapList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonMapSepcification)
                                                    .OrderByDescending(x => x.CreatedOn)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                    var bookingCount = bookingSalesPeronMapListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);
                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();

                    if (bookingSalesPeronMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPeronMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingSalesPersonMap.Booking.Exhibitor.Id);

                        if (bookingSalesPersonMap.Booking.BookingAccepted == true)
                        {
                            var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(bookingSalesPersonMap.Booking, eventDetails);

                            BookingRequestDTO singleBookingDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id, EventId = bookingSalesPersonMap.Booking.Event.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                            if (bookingStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);

                            foreach (StallBooking stall in bookingStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };

                                singleBookingDTO.stallList.Add(bookedSatllListDTO);
                            }
                            singleBookingDTO.Id = bookingSalesPersonMap.Booking.Id;
                            singleBookingDTO.BookingRequestId = bookingSalesPersonMap.Booking.BookingId;
                            singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingDTO.HangerName = stallDetails.Section.Name;
                            singleBookingDTO.HangerId = stallDetails.Section.Id;
                            singleBookingDTO.DateOfBooking = bookingSalesPersonMap.Booking.BookingDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.Quantity = bookingStall.Count();
                            singleBookingDTO.TotalAmount = bookingSalesPersonMap.Booking.TotalAmount;
                            singleBookingDTO.Status = bookingSalesPersonMap.Booking.Status;
                            singleBookingDTO.Comment = bookingSalesPersonMap.Booking.Comment;
                            singleBookingDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingDTO.HangerHeight = stallDetails.Section.Height;
                            if (bookingSalesPersonMap != null)
                            {
                                singleBookingDTO.SalesPerson = bookingSalesPersonMap.Login.UserName;
                            }
                            singleBookingDTO.FinalAmount = bookingSalesPersonMap.Booking.FinalAmountTax;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.RemainingAmount = bookingSalesPersonMap.Booking.FinalAmountTax - totalAmountPaid;
                            listBookingRequestDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Get Booking Data By Hanger
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="hangerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/{eventId:guid}/hanger/{hangerId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBookingDetailsByHanger(Guid organizerId, Guid eventId, Guid hangerId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var pavilionDetails = _pavilionRepository.GetById(hangerId);
                if (eventDetails == null)
                    return NotFound();

                var bookingCriteria = new BookingSearchCriteria { PavilionId = hangerId, EventId = eventDetails.Id };
                var bookingSepcification = new BookingSpecificationForSearch(bookingCriteria);
                var bookingListCount = _bookingRepository.Count(bookingSepcification);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .Skip((pageNumber - 1) * pageSize)
                                                      .Take(pageSize);

                var bookingCount = bookingListCount;
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);
                List<BookingDTO> bookingDTOList = new List<BookingDTO>();

                if (bookingList.Count() == 0)
                    return NotFound();

                foreach (Booking singleBooking in bookingList)
                {
                    Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(singleBooking.Exhibitor.Id);

                    if (singleBooking.BookingAccepted == true)
                    {
                        BookingDTO singleBookingDTO = new BookingDTO();

                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = singleBooking.Id, EventId = singleBooking.Event.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                        if (bookingStall.Count() == 0)
                            return NotFound();

                        Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);

                        foreach (StallBooking stall in bookingStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };

                            singleBookingDTO.StallList.Add(bookedSatllListDTO);
                        }
                        singleBookingDTO.Id = singleBooking.Id;
                        singleBookingDTO.BookingRequestId = singleBooking.BookingId;
                        singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                        singleBookingDTO.HangerName = stallDetails.Section.Name;
                        singleBookingDTO.HangerId = stallDetails.Section.Id;
                        singleBookingDTO.DateOfBooking = singleBooking.BookingDate.ToString("dd-MMM-yyyy");
                        singleBookingDTO.Quantity = bookingStall.Count();
                        if (singleBooking.Login != null)
                        {
                            singleBookingDTO.SalesPerson = singleBooking.Login.UserName;
                        }
                        bookingDTOList.Add(singleBookingDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = bookingDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Search Booking List By Exhibitor Name 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="exhibitorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/{eventId:guid}/salesPerson/{salesPersonId:guid}/exhibitorSearch/{exhibitorName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult GetBookingDetailsBySalesPersonExhibitor(Guid organizerId, Guid eventId, Guid salesPersonId, string exhibitorName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var bookingSalesPersonMapCount = _bookingSalesPersonMapBookingRepository.FindBookingOfReportingSalespersonsCount(salesPerson.Partner, eventDetails, exhibitorName);
                    var bookingSalesPersonMapList = _bookingSalesPersonMapBookingRepository.FindBookingOfReportingSalespersons(salesPerson.Partner, eventDetails, exhibitorName, pageNumber, pageSize);

                    var bookingCount = bookingSalesPersonMapCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingSalesPersonMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPersonMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingSalesPersonMap.Booking.Exhibitor.Id);

                        if (bookingSalesPersonMap.Booking.BookingAccepted == true)
                        {
                            BookingRequestDTO singleBookingDTO = new BookingRequestDTO();

                            var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id };
                            var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                            var payments = _paymentDetailsRepository.Find(paymentSepcification);
                            double totalAmountPaid = 0;
                            foreach (PaymentDetails payment in payments)
                            {
                                if (payment.IsPaymentApprove == true)
                                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                            }
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id, EventId = bookingSalesPersonMap.Booking.Event.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                            if (bookingStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);
                            double totalPrice = 0;
                            foreach (StallBooking stall in bookingStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };
                                totalPrice = totalPrice + stall.Stall.Price;
                                singleBookingDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingDTO.Id = bookingSalesPersonMap.Booking.Id;
                            singleBookingDTO.BookingRequestId = bookingSalesPersonMap.Booking.BookingId;
                            singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingDTO.HangerName = stallDetails.Section.Name;
                            singleBookingDTO.HangerId = stallDetails.Section.Id;
                            singleBookingDTO.DateOfBooking = bookingSalesPersonMap.Booking.BookingDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.Quantity = bookingStall.Count();
                            singleBookingDTO.TotalAmount = bookingSalesPersonMap.Booking.TotalAmount;
                            singleBookingDTO.FinalAmount = bookingSalesPersonMap.Booking.FinalAmountTax;
                            singleBookingDTO.Status = bookingSalesPersonMap.Booking.Status;
                            singleBookingDTO.Comment = bookingSalesPersonMap.Booking.Comment;
                            singleBookingDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.RemainingAmount = bookingSalesPersonMap.Booking.FinalAmountTax - totalAmountPaid;

                            if (bookingSalesPersonMap != null)
                            {
                                singleBookingDTO.SalesPerson = bookingSalesPersonMap.Login.UserName;
                            }
                            listBookingRequestDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
                else
                {
                    var bookingSalesPeronMapSearchCriteria = new BookingSalesPersonMapSearchCriteria { SalesPersonId = salesPersonId, EventId = eventDetails.Id, ExhibitorName = exhibitorName };
                    var bookingSalesPersonMapSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPeronMapSearchCriteria);
                    var bookingSalesPeronMapListCount = _bookingSalesPersonMapRepository.Count(bookingSalesPersonMapSepcification);
                    var bookingSalesPeronMapList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonMapSepcification)
                                                    .OrderByDescending(x => x.CreatedOn)
                                                          .Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                    var bookingCount = bookingSalesPeronMapListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);
                    List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                    if (bookingSalesPeronMapList.Count() == 0)
                        return NotFound();

                    foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPeronMapList)
                    {
                        Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(bookingSalesPersonMap.Booking.Exhibitor.Id);
                        var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(bookingSalesPersonMap.Booking, eventDetails);

                        if (bookingSalesPersonMap.Booking.BookingAccepted == true)
                        {
                            BookingRequestDTO singleBookingDTO = new BookingRequestDTO();

                            foreach (Category singleCategory in exhibitorDeatils.Categories)
                            {
                                singleBookingDTO.CategoryName.Add(singleCategory.Name);
                            }
                            var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingSalesPersonMap.Booking.Id, EventId = bookingSalesPersonMap.Booking.Event.Id };
                            var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                            var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                            if (bookingStall.Count() == 0)
                                return NotFound();

                            Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);

                            foreach (StallBooking stall in bookingStall)
                            {
                                BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                                {
                                    StallId = stall.Stall.Id,
                                    Price = stall.Stall.Price,
                                    StallNo = stall.Stall.StallNo,
                                    StallSize = stall.Stall.StallSize
                                };

                                singleBookingDTO.stallList.Add(bookedSatllListDTO);
                            }

                            singleBookingDTO.Id = bookingSalesPersonMap.Booking.Id;
                            singleBookingDTO.BookingRequestId = bookingSalesPersonMap.Booking.BookingId;
                            singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                            singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                            singleBookingDTO.HangerName = stallDetails.Section.Name;
                            singleBookingDTO.HangerId = stallDetails.Section.Id;
                            singleBookingDTO.DateOfBooking = bookingSalesPersonMap.Booking.BookingDate.ToString("dd-MMM-yyyy");
                            singleBookingDTO.Quantity = bookingStall.Count();
                            singleBookingDTO.TotalAmount = bookingSalesPersonMap.Booking.TotalAmount;
                            singleBookingDTO.Status = bookingSalesPersonMap.Booking.Status;
                            singleBookingDTO.Comment = bookingSalesPersonMap.Booking.Comment;
                            singleBookingDTO.HangerWidth = stallDetails.Section.Width;
                            singleBookingDTO.HangerHeight = stallDetails.Section.Height;
                            singleBookingDTO.AmountPaid = totalAmountPaid;
                            singleBookingDTO.RemainingAmount = bookingSalesPersonMap.Booking.FinalAmountTax - totalAmountPaid;
                            if (bookingSalesPersonMap != null)
                            {
                                singleBookingDTO.SalesPerson = bookingSalesPersonMap.Login.UserName;
                            }
                            singleBookingDTO.FinalAmount = bookingSalesPersonMap.Booking.FinalAmountTax;
                            listBookingRequestDTO.Add(singleBookingDTO);
                        }
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listBookingRequestDTO
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Get Booking List by Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/{eventId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBookingDetails(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                .OrderByDescending(x => x.CreatedOn)
                                .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var bookingCount = _bookingRepository.Count(bookingSepcification);
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingRequestDTO> listOfBookingDTO = new List<BookingRequestDTO>();

                foreach (Booking booking in bookingList)
                {
                    var exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    BookingRequestDTO singleBookingDTO = new BookingRequestDTO();
                    foreach (Category singleCategory in exhibitor.Categories)
                    {
                        singleBookingDTO.CategoryName.Add(singleCategory.Name);
                    }

                    var bookingSalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = booking.Id };
                    var bookingSalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPersonSearchCriteria);
                    var bookingSalesPersonList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonSepcification);

                    if (bookingSalesPersonList.Count() != 0)
                    {
                        var salesPerson = _loginRepository.GetById(bookingSalesPersonList.FirstOrDefault().Login.Id);
                        singleBookingDTO.SalesPerson = salesPerson.UserName;
                    }

                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = booking.Id, EventId = eventId };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                    foreach (StallBooking stall in bookingStallList)
                    {
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = stall.Stall.Id,
                            Price = stall.Stall.Price,
                            StallNo = stall.Stall.StallNo,
                            StallSize = stall.Stall.StallSize
                        };

                        singleBookingDTO.stallList.Add(bookedSatllListDTO);
                    }

                    var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = booking.Id };
                    var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                    var payments = _paymentDetailsRepository.Find(paymentSepcification);
                    double totalAmountPaid = 0;
                    foreach (PaymentDetails payment in payments)
                    {
                        if (payment.IsPaymentApprove == true)
                            totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                    }

                    double balance = booking.FinalAmountTax - totalAmountPaid;
                    singleBookingDTO.Id = booking.Id;
                    singleBookingDTO.BookingRequestId = booking.BookingId;
                    singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                    singleBookingDTO.CompanyName = exhibitor.CompanyName;
                    if (bookingStallList.Count() != 0)
                    {
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                        singleBookingDTO.HangerHeight = bookingStallList.FirstOrDefault().Stall.Section.Height;
                        singleBookingDTO.HangerWidth = bookingStallList.FirstOrDefault().Stall.Section.Width;
                    }
                    singleBookingDTO.Quantity = bookingStallList.Count();
                    singleBookingDTO.Status = booking.Status;
                    singleBookingDTO.Comment = booking.Comment;
                    singleBookingDTO.TotalAmount = booking.TotalAmount;
                    singleBookingDTO.AmountPaid = totalAmountPaid;
                    singleBookingDTO.RemainingAmount = balance;
                    singleBookingDTO.FinalAmount = booking.FinalAmountTax;
                    singleBookingDTO.DiscountInAmount = booking.DiscountAmount;
                    singleBookingDTO.DiscountInPercent = booking.DiscountPercent;
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
        /// Get Exhibitor List By Booking
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/{eventId:guid}/exhibitorList/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult GetExhibitorListByBooking(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                .OrderBy(x => x.Exhibitor.CompanyName)
                                .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var bookingCount = _bookingRepository.Count(bookingSepcification);
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<ExhibitorDTO> listOfExhibitorDTO = new List<ExhibitorDTO>();

                foreach (Booking booking in bookingList)
                {
                    var exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    ExhibitorDTO singleBookingDTO = new ExhibitorDTO()
                    {
                        Id = exhibitor.Id,
                        CompanyName = exhibitor.CompanyName,
                        Name = exhibitor.Name
                    };

                    listOfExhibitorDTO.Add(singleBookingDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listOfExhibitorDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Request by Exhibitor 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <returns></returns>
        [Route("bookingRequests/{exhibitorId:guid}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBookingRequestByExhibitor(Guid organizerId, Guid exhibitorId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Exhibitor exhibitorDeatils = _exhibitorRepository.GetById(exhibitorId);

                var bookingRequestSearchCriteria = new BookingRequestSearchCriteria { ExhibitorId = exhibitorId };
                var bookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequestSearchCriteria);
                var bookingRequestList = _bookingRequestRepository.Find(bookingRequestSepcification)
                                        .OrderByDescending(x => x.CreatedOn);

                if (bookingRequestList.Count() == 0)
                    return NotFound();

                List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                var bookingRequestLists = bookingRequestList.Where(x => !bookingRequestList.Any(y => y.PreviousBookingrequest.Id == x.Id));

                foreach (BookingRequest bookingRequest in bookingRequestList)
                {
                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification);

                    if (bookingRequest.Booking == null)
                    {
                        var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                        var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                        var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                        if (bookingRequestStall.Count() == 0)
                            return NotFound();

                        BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();
                        foreach (Category singleCategory in exhibitorDeatils.Categories)
                        {
                            singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                        }
                        Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);
                        double totalPrice = 0;
                        foreach (BookingRequestStall stall in bookingRequestStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };
                            totalPrice = totalPrice + stall.Stall.Price;
                            singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                        }

                        singleBookingRequestDTO.Id = bookingRequest.Id;
                        singleBookingRequestDTO.BookingRequestId = bookingRequest.BookingRequestId;
                        singleBookingRequestDTO.CompanyName = exhibitorDeatils.CompanyName;
                        singleBookingRequestDTO.EmailId = exhibitorDeatils.EmailId;
                        singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                        singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                        singleBookingRequestDTO.DateOfBooking = bookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                        singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                        singleBookingRequestDTO.TotalAmount = bookingRequest.TotalAmount;
                        singleBookingRequestDTO.Status = bookingRequest.Status;
                        singleBookingRequestDTO.Comment = bookingRequest.Comment;
                        singleBookingRequestDTO.HangerHeight = stallDetails.Height;
                        singleBookingRequestDTO.HangerWidth = stallDetails.Width;
                        if (bookingRequestSalesPeronMapList.Count() != 0)
                        {
                            singleBookingRequestDTO.SalesPerson = bookingRequestSalesPeronMapList.FirstOrDefault().Login.UserName;
                        }
                        singleBookingRequestDTO.FinalAmount = bookingRequest.FinalAmountTax;
                        listBookingRequestDTO.Add(singleBookingRequestDTO);
                    }
                    else
                    {
                        var booking = _bookingRepository.GetById(bookingRequest.Booking.Id);

                        if (booking == null)
                            return NotFound();

                        BookingRequestDTO singleBookingDTO = new BookingRequestDTO();
                        foreach (Category singleCategory in exhibitorDeatils.Categories)
                        {
                            singleBookingDTO.CategoryName.Add(singleCategory.Name);
                        }
                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = booking.Id, EventId = booking.Event.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);
                        if (bookingStall.Count() == 0)
                            return NotFound();

                        Stall stallDetails = _stallRepository.GetById(bookingStall.FirstOrDefault().Stall.Id);

                        foreach (StallBooking stall in bookingStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };

                            singleBookingDTO.stallList.Add(bookedSatllListDTO);
                        }

                        var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = booking.Id };
                        var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                        var payments = _paymentDetailsRepository.Find(paymentSepcification);
                        double amountpaid = 0;
                        if (payments.Count() != 0)
                        {
                            foreach (PaymentDetails payment in payments)
                            {
                                if (payment.IsPaymentApprove == true)
                                    amountpaid = amountpaid + payment.AmountPaid;
                            }
                        }
                        singleBookingDTO.Id = booking.Id;
                        singleBookingDTO.BookingRequestId = booking.BookingId;
                        singleBookingDTO.CompanyName = exhibitorDeatils.CompanyName;
                        singleBookingDTO.EmailId = exhibitorDeatils.EmailId;
                        singleBookingDTO.HangerName = stallDetails.Section.Name;
                        singleBookingDTO.HangerId = stallDetails.Section.Id;
                        singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                        singleBookingDTO.Quantity = bookingStall.Count();
                        singleBookingDTO.TotalAmount = booking.TotalAmount;
                        singleBookingDTO.AmountPaid = amountpaid;
                        singleBookingDTO.Status = booking.Status;
                        singleBookingDTO.Comment = booking.Comment;
                        singleBookingDTO.HangerHeight = stallDetails.Height;
                        singleBookingDTO.HangerWidth = stallDetails.Width;
                        singleBookingDTO.RemainingAmount = booking.FinalAmountTax - amountpaid;
                        if (bookingRequestSalesPeronMapList.Count() != 0)
                        {
                            singleBookingDTO.SalesPerson = bookingRequestSalesPeronMapList.FirstOrDefault().Login.UserName;
                        }
                        singleBookingDTO.FinalAmount = booking.FinalAmountTax;
                        listBookingRequestDTO.Add(singleBookingDTO);
                    }
                }
                return Ok(listBookingRequestDTO);
            }
        }

        /// <summary>
        /// Get Booking Exhibitor Search by Name 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingList/event/{eventId:guid}/searchExhibitor/{exhibitorName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult GetBookingByExhibitorName(Guid organizerId, Guid eventId, string exhibitorName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId, CompanyName = exhibitorName };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingList = _bookingRepository.Find(bookingSepcification)
                                .OrderByDescending(x => x.CreatedOn)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize);

                var bookingCount = _bookingRepository.Count(bookingSepcification);
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                List<BookingDTO> listOfBookingDTO = new List<BookingDTO>();

                foreach (Booking booking in bookingList)
                {
                    var exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                    if (exhibitor == null)
                        return NotFound();

                    BookingDTO singleBookingDTO = new BookingDTO();
                    foreach (Category singleCategory in exhibitor.Categories)
                    {
                        singleBookingDTO.CategoryName.Add(singleCategory.Name);
                    }

                    var bookingSalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = booking.Id };
                    var bookingSalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPersonSearchCriteria);
                    var bookingSalesPersonList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonSepcification);

                    if (bookingSalesPersonList.Count() != 0)
                    {
                        var salesPerson = _loginRepository.GetById(bookingSalesPersonList.FirstOrDefault().Login.Id);
                        singleBookingDTO.SalesPerson = salesPerson.UserName;
                    }

                    var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = booking.Id, EventId = eventId };
                    var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                    var bookingStallList = _stallBookingRepository.Find(bookingStallSepcification);

                    foreach (StallBooking stall in bookingStallList)
                    {
                        BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                        {
                            StallId = stall.Stall.Id,
                            Price = stall.Stall.Price,
                            StallNo = stall.Stall.StallNo,
                            StallSize = stall.Stall.StallSize
                        };

                        singleBookingDTO.StallList.Add(bookedSatllListDTO);
                    }

                    var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = booking.Id };
                    var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                    var payments = _paymentDetailsRepository.Find(paymentSepcification);
                    double totalAmountPaid = 0;
                    foreach (PaymentDetails payment in payments)
                    {
                        if (payment.IsPaymentApprove == true)
                            totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                    }

                    double balance = booking.FinalAmountTax - totalAmountPaid;
                    singleBookingDTO.Id = booking.Id;
                    singleBookingDTO.BookingRequestId = booking.BookingId;
                    singleBookingDTO.DateOfBooking = booking.BookingDate.ToString("dd-MMM-yyyy");
                    singleBookingDTO.CompanyName = exhibitor.CompanyName;
                    if (bookingStallList.Count() != 0)
                    {
                        singleBookingDTO.HangerName = bookingStallList.FirstOrDefault().Stall.Section.Name;
                        singleBookingDTO.HangerId = bookingStallList.FirstOrDefault().Stall.Section.Id;
                        singleBookingDTO.HangerHeight = bookingStallList.FirstOrDefault().Stall.Section.Height;
                        singleBookingDTO.HangerWidth = bookingStallList.FirstOrDefault().Stall.Section.Width;
                    }
                    singleBookingDTO.Comment = booking.Comment;
                    singleBookingDTO.FinalAmount = booking.FinalAmountTax;
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
        /// Booking Request Search By Exhibitor Name 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("bookingRequests/event/{eventId:guid}/searchExhibitor/{exhibitorName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBookingRequestByExhibitorName(Guid organizerId, Guid eventId, string exhibitorName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingRequestSearchCriteria = new BookingRequestSearchCriteria { ExhibitorName = exhibitorName, Status = "Pending", EventId = eventId };
                var bookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequestSearchCriteria);
                var bookingRequestList = _bookingRequestRepository.Find(bookingRequestSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var totalCount = _bookingRequestRepository.Count(bookingRequestSepcification);
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (bookingRequestList.Count() == 0)
                    return NotFound();

                List<BookingRequestDTO> listBookingRequestDTO = new List<BookingRequestDTO>();
                var bookingRequestLists = bookingRequestList.Where(x => !bookingRequestList.Any(y => y.PreviousBookingrequest.Id == x.Id));

                foreach (BookingRequest bookingRequest in bookingRequestList)
                {
                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification);

                    if (bookingRequest.Booking == null)
                    {
                        var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                        var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                        var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                        if (bookingRequestStall.Count() == 0)
                            return NotFound();

                        BookingRequestDTO singleBookingRequestDTO = new BookingRequestDTO();

                        foreach (Category singleCategory in bookingRequest.Exhibitor.Categories)
                        {
                            singleBookingRequestDTO.CategoryName.Add(singleCategory.Name);
                        }
                        Stall stallDetails = _stallRepository.GetById(bookingRequestStall.FirstOrDefault().Stall.Id);

                        foreach (BookingRequestStall stall in bookingRequestStall)
                        {
                            BookedStallListDTO bookedSatllListDTO = new BookedStallListDTO()
                            {
                                StallId = stall.Stall.Id,
                                Price = stall.Stall.Price,
                                StallNo = stall.Stall.StallNo,
                                StallSize = stall.Stall.StallSize
                            };
                            singleBookingRequestDTO.stallList.Add(bookedSatllListDTO);
                        }
                        singleBookingRequestDTO.Id = bookingRequest.Id;
                        singleBookingRequestDTO.BookingRequestId = bookingRequest.BookingRequestId;
                        singleBookingRequestDTO.CompanyName = bookingRequest.Exhibitor.CompanyName;
                        singleBookingRequestDTO.EmailId = bookingRequest.Exhibitor.EmailId;
                        singleBookingRequestDTO.HangerName = stallDetails.Section.Name;
                        singleBookingRequestDTO.HangerId = stallDetails.Section.Id;
                        singleBookingRequestDTO.DateOfBooking = bookingRequest.BookingRequestDate.ToString("dd-MMM-yyyy");
                        singleBookingRequestDTO.Quantity = bookingRequestStall.Count();
                        singleBookingRequestDTO.TotalAmount = bookingRequest.TotalAmount;
                        singleBookingRequestDTO.Status = bookingRequest.Status;
                        singleBookingRequestDTO.Comment = bookingRequest.Comment;
                        singleBookingRequestDTO.HangerHeight = stallDetails.Height;
                        singleBookingRequestDTO.HangerWidth = stallDetails.Width;
                        if (bookingRequestSalesPeronMapList.Count() != 0)
                        {
                            singleBookingRequestDTO.SalesPerson = bookingRequestSalesPeronMapList.FirstOrDefault().Login.UserName;
                        }
                        singleBookingRequestDTO.FinalAmount = bookingRequest.FinalAmountTax;
                        listBookingRequestDTO.Add(singleBookingRequestDTO);
                    }
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = listBookingRequestDTO
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Stall List By SectionId
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [Route("booking/{sectionId:guid}/stalls")]
        [ResponseType(typeof(BookingStallListDTO))]
        public IHttpActionResult GetBookingByStalls(Guid organizerId, Guid sectionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var stallCriteria = new StallSearchCriteria { sectionId = sectionId };
                var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                var stallsList = _stallRepository.Find(stallSepcification).OrderBy(x => x.StallNo);
                List<BookingStallListDTO> bookingStallListDTO = new List<BookingStallListDTO>();
                foreach (Stall singleStall in stallsList)
                {
                    var stallBookingCriteria = new BookingStallSearchCriteria { StallId = singleStall.Id };
                    var stallBookingSepcification = new BookingStallSpecificationForSearch(stallBookingCriteria);
                    var stallBookingList = _stallBookingRepository.Find(stallBookingSepcification);

                    if (singleStall.IsBooked == true && stallBookingList.Count() != 0)
                    {
                        Booking bookingDetails = _bookingRepository.GetById(stallBookingList.FirstOrDefault().Booking.Id);

                        var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingDetails.Id, EventId = bookingDetails.Event.Id };
                        var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                        var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);

                        var paymentSearchCriteria = new BookingPaymentSearchCriteria { BookingId = bookingDetails.Id };
                        var paymentSepcification = new BookingPaymentSpecificationForSearch(paymentSearchCriteria);
                        var payments = _paymentDetailsRepository.Find(paymentSepcification);

                        double amountpaid = 0;
                        if (payments.Count() != 0)
                        {
                            foreach (PaymentDetails payment in payments)
                            {
                                if (payment.IsPaymentApprove == true)
                                    amountpaid = amountpaid + payment.AmountPaid;
                            }
                        }
                        BookingStallListDTO singleBookingStallListDTO = new BookingStallListDTO();

                        if (bookingDetails.Exhibitor.Categories.Count != 0)
                        {
                            foreach (Category category in bookingDetails.Exhibitor.Categories)
                            {
                                singleBookingStallListDTO.Product.Add(category.Name);
                            }
                        }

                        string stateName = "";
                        if (bookingDetails.Exhibitor.State != null)
                        {
                            stateName = bookingDetails.Exhibitor.State.Name;
                        }

                        singleBookingStallListDTO.Id = singleStall.Id;
                        singleBookingStallListDTO.StallNo = singleStall.StallNo;
                        singleBookingStallListDTO.Price = singleStall.Price;
                        singleBookingStallListDTO.IsBooked = singleStall.IsBooked;
                        singleBookingStallListDTO.Height = singleStall.Height;
                        singleBookingStallListDTO.Width = singleStall.Width;
                        singleBookingStallListDTO.X_Coordinate = singleStall.X_Coordinate;
                        singleBookingStallListDTO.Y_Coordinate = singleStall.Y_Coordinate;
                        singleBookingStallListDTO.IsRequested = singleStall.IsRequested;
                        if (singleStall.Partner != null)
                        {
                            singleBookingStallListDTO.PartnerId = singleStall.Partner.Id;
                            singleBookingStallListDTO.PartnerColor = singleStall.Partner.Color;
                        }
                        else if (singleStall.ExhibitorIndustryType != null)
                        {
                            singleBookingStallListDTO.PartnerColor = singleStall.ExhibitorIndustryType.Color;
                        }
                        else if (singleStall.Country != null)
                        {
                            singleBookingStallListDTO.PartnerColor = singleStall.Country.Color;
                        }
                        else if (singleStall.State != null)
                        {
                            singleBookingStallListDTO.PartnerColor = singleStall.State.Color;
                        }
                        singleBookingStallListDTO.CompanyName = bookingDetails.Exhibitor.CompanyName;
                        singleBookingStallListDTO.Location = stateName;
                        singleBookingStallListDTO.SizeOfStall = singleStall.StallSize;
                        singleBookingStallListDTO.TotalAmount = bookingDetails.TotalAmount / bookingStall.Count();
                        singleBookingStallListDTO.RecivedAmount = amountpaid / bookingStall.Count();
                        singleBookingStallListDTO.Balance = ((bookingDetails.FinalAmountTax / bookingStall.Count()) - (amountpaid / bookingStall.Count()));

                        bookingStallListDTO.Add(singleBookingStallListDTO);
                    }
                    else
                    {
                        BookingStallListDTO singleBookingStallListDTO = new BookingStallListDTO
                        {
                            Id = singleStall.Id,
                            StallNo = singleStall.StallNo,
                            Price = singleStall.Price,
                            IsBooked = singleStall.IsBooked,
                            Height = singleStall.Height,
                            Width = singleStall.Width,
                            X_Coordinate = singleStall.X_Coordinate,
                            Y_Coordinate = singleStall.Y_Coordinate,
                            IsRequested = singleStall.IsRequested,

                            CompanyName = "",
                            Location = "",
                            SizeOfStall = singleStall.StallSize,
                            TotalAmount = singleStall.Price,
                            RecivedAmount = 0,
                            Balance = 0
                        };
                        if (singleStall.Partner != null)
                        {
                            singleBookingStallListDTO.PartnerId = singleStall.Partner.Id;
                            singleBookingStallListDTO.PartnerColor = singleStall.Partner.Color;
                        }
                        else if (singleStall.ExhibitorIndustryType != null)
                        {
                            singleBookingStallListDTO.PartnerColor = singleStall.ExhibitorIndustryType.Color;
                        }
                        bookingStallListDTO.Add(singleBookingStallListDTO);
                    }
                }
                return Ok(bookingStallListDTO);
            }
        }

        /// <summary>
        /// Get Booking Request By Exhibitor
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="bookingRequestDTO"></param>
        /// <returns></returns>
        [Route("bookingRequest/{exhibitorId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid exhibitorId, BookingRequestPostDTO bookingRequestDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Exhibitor exhibitor = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorId == null)
                    return NotFound();

                Event bookingEvent = _eventRepository.GetById(bookingRequestDTO.Liststall.FirstOrDefault().EventId);
                if (bookingEvent == null)
                    return NotFound();

                var bookingRequestCount = _bookingRequestRepository.Find(new GetAllSpecification<BookingRequest>()).Where(x => x.Event.Id.Equals(bookingEvent.Id)).Count();
                var bookingRequestId = GetBookingRequestId(bookingEvent, bookingRequestCount.ToString());
                bookingRequestDTO.BookingDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                BookingRequest bookingRequest = BookingRequest.Create(bookingRequestId, bookingRequestDTO.Action, bookingRequestDTO.Confirmed, bookingRequestDTO.Status, bookingRequestDTO.BookingDate, bookingRequestDTO.TotalAmount, bookingRequestDTO.FinalAmountTax, null, null, bookingRequestDTO.Comment);

                var stallDetail = _stallRepository.GetById(bookingRequestDTO.Liststall.FirstOrDefault().StallId);
                bookingRequest.Section = stallDetail.Section;
                bookingRequest.Exhibitor = exhibitor;
                bookingRequest.Event = bookingEvent;
                _bookingRequestRepository.Add(bookingRequest);

                foreach (StallsListDTO stall in bookingRequestDTO.Liststall)
                {
                    Event exhibitionEvent = _eventRepository.GetById(stall.EventId);
                    Exhibition exhibition = _exhibitionRepository.GetById(stall.ExhibitionId);
                    Stall stallAdded = _stallRepository.GetById(stall.StallId);
                    BookingRequestStall bookingRequestStall = new BookingRequestStall();
                    bookingRequestStall.Event = exhibitionEvent;
                    bookingRequestStall.Exhibition = exhibition;
                    bookingRequestStall.Stall = stallAdded;
                    bookingRequestStall.BookingRequest = bookingRequest;
                    _bookingRequestStallRepository.Add(bookingRequestStall);
                }
                unitOfWork.SaveChanges();
                return GetByBookingRequestId(organizerId, bookingRequest.Id);
            }
        }

        /// <summary>
        /// Edit Booking Request
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="previousBookingRequestId"></param>
        /// <param name="bookingRequestDTO"></param>
        /// <returns></returns>
        [Route("bookingRequest/{previousBookingRequestId:guid}/edit")]
        [ModelValidator]
        [ResponseType(typeof(BookingRequestPostDTO))]
        public IHttpActionResult PostForEditBookingRequest(Guid organizerId, Guid previousBookingRequestId, BookingRequestPostDTO bookingRequestDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                BookingRequest previousBookingRequest = _bookingRequestRepository.GetById(previousBookingRequestId);
                if (previousBookingRequest == null)
                    return NotFound();

                Exhibitor exhibitor = _exhibitorRepository.GetById(previousBookingRequest.Exhibitor.Id);
                if (exhibitor == null)
                    return NotFound();

                previousBookingRequest.Action = "Modify";
                previousBookingRequest.Status = "Modified";

                bookingRequestDTO.BookingDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                bookingRequestDTO.Action = "Modify";
                bookingRequestDTO.Status = "Modified";

                Booking booking = Booking.Create(previousBookingRequest.BookingRequestId, bookingRequestDTO.Action, bookingRequestDTO.Status, bookingRequestDTO.BookingDate, false, false, bookingRequestDTO.TotalAmount, bookingRequestDTO.FinalAmountTax, bookingRequestDTO.DiscountPercent, bookingRequestDTO.DiscountAmount, bookingRequestDTO.Comment);
                Event bookingEvent = _eventRepository.GetById(previousBookingRequest.Event.Id);
                if (bookingEvent == null)
                    return NotFound();

                if (previousBookingRequest.Section != null)
                {
                    booking.Section = previousBookingRequest.Section;
                }
                booking.Exhibitor = exhibitor;
                booking.Event = bookingEvent;
                _bookingRepository.Add(booking);

                previousBookingRequest.Booking = booking;

                foreach (BookingInstallmentDTO singleBookingInstallmentDTO in bookingRequestDTO.BookingInstallmentDTO)
                {
                    var todaysDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    DateTime paymentDate = DateTime.MinValue;
                    if (singleBookingInstallmentDTO.Order == 1)
                    {
                        paymentDate = todaysDate.AddDays(3);
                    }
                    else if (singleBookingInstallmentDTO.Order == 2)
                    {
                        paymentDate = bookingEvent.StartDate.AddDays(-30);
                    }
                    else
                    {
                        paymentDate = bookingEvent.StartDate.AddDays(-1);
                    }

                    BookingInstallment bookingRequestInstallment = BookingInstallment.Create(singleBookingInstallmentDTO.Percent, singleBookingInstallmentDTO.Amount, false, singleBookingInstallmentDTO.Order, paymentDate);
                    bookingRequestInstallment.Booking = booking;
                    _bookingInstallmentRepository.Add(bookingRequestInstallment);
                }

                foreach (StallsListDTO stall in bookingRequestDTO.Liststall)
                {
                    Exhibition exhibition = _exhibitionRepository.GetById(stall.ExhibitionId);
                    if (exhibition == null)
                        return NotFound();

                    Stall stallAdded = _stallRepository.GetById(stall.StallId);
                    if (stallAdded == null)
                        return NotFound();

                    StallBooking bookingRequestStall = new StallBooking();
                    bookingRequestStall.Event = bookingEvent;
                    bookingRequestStall.Exhibition = exhibition;
                    bookingRequestStall.Stall = stallAdded;
                    bookingRequestStall.Booking = booking;
                    stallAdded.IsBooked = true;
                    _stallRepository.Update(stallAdded);
                    _stallBookingRepository.Add(bookingRequestStall);
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, previousBookingRequest.Event.Id, 5, 1);
            }
        }

        /// <summary>
        /// Add Sales Person To Booking
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingSalesPersoneDTO"></param>
        /// <returns></returns>
        [Route("booking/addSalesPerson")]
        [ModelValidator]
        [ResponseType(typeof(BookingSalesPersoneDTO))]
        public IHttpActionResult PostBookingSalesPerson(Guid organizerId, BookingSalesPersoneDTO bookingSalesPersoneDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                foreach (Guid bookingId in bookingSalesPersoneDTO.BookingIdList)
                {
                    BookingSalesPersonMap bookingSalesPersoneMap = new BookingSalesPersonMap();
                    var bookingDetails = _bookingRepository.GetById(bookingId);

                    var salesPerson = _loginRepository.GetById(bookingSalesPersoneDTO.SalesPersonId);

                    var bookingSalesPersonMapCriteria = new BookingSalesPersonMapSearchCriteria { EventId = bookingDetails.Event.Id, BookingId = bookingDetails.Id, };
                    var bookingSalesPersonMapSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPersonMapCriteria);
                    var bookingSalesPersonMapList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonMapSepcification);
                    if (bookingSalesPersonMapList.Count == 0)
                    {
                        bookingSalesPersoneMap.Booking = bookingDetails;
                        bookingSalesPersoneMap.Login = salesPerson;
                        _bookingSalesPersonMapRepository.Add(bookingSalesPersoneMap);
                    }
                    else
                    {
                        foreach (BookingSalesPersonMap bookingSalesPersonMap in bookingSalesPersonMapList)
                        {
                            bookingSalesPersonMap.Login = salesPerson;
                        }
                    }
                }
                unitOfWork.SaveChanges();
                return Ok("Data Saved");
            }
        }

        /// <summary>
        /// Edit Booking Exhibitor Status 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingId"></param>
        /// <param name="bookingRequestStatusDTO"></param>
        /// <returns></returns>
        [Route("bookingRequest/{bookingId:guid}/exhibitorStatus")]
        [ModelValidator]
        [ResponseType(typeof(BookingRequestPostDTO))]
        public IHttpActionResult PutStatus(Guid organizerId, Guid bookingId, BookingRequestStatusDTO bookingRequestStatusDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingRequest = _bookingRequestRepository.GetById(bookingId);
                if (bookingRequest == null)
                    return NotFound();

                bookingRequest.Action = bookingRequestStatusDTO.Action;
                bookingRequest.Status = bookingRequestStatusDTO.Status;
                bookingRequest.Update(bookingRequest.BookingRequestId, bookingRequestStatusDTO.Action, bookingRequest.Confirmed, bookingRequestStatusDTO.Status, bookingRequest.BookingRequestDate, bookingRequest.TotalAmount, bookingRequest.FinalAmountTax, bookingRequestStatusDTO.DiscountPercent, bookingRequestStatusDTO.DiscountAmount, bookingRequestStatusDTO.Comment);

                unitOfWork.SaveChanges();
                return GetBookingRequestByExhibitor(organizerId, bookingRequest.Exhibitor.Id);
            }
        }

        /// <summary>
        /// Edit Approve Booking Request 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingRequestId"></param>
        /// <param name="bookingRequestStatusDTO"></param>
        /// <returns></returns>
        [Route("bookingRequest/{bookingRequestId:guid}/status")]
        [ModelValidator]
        [ResponseType(typeof(BookingRequestPostDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid bookingRequestId, BookingRequestStatusDTO bookingRequestStatusDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var bookingRequest = _bookingRequestRepository.GetById(bookingRequestId);
                if (bookingRequest == null)
                    return NotFound();

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = bookingRequest.Event.Id, BookingReqestId = bookingRequest.BookingRequestId };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);
                var bookingCount = _bookingRepository.Count(bookingSepcification);
                if (bookingCount != 0)
                    return Ok("Allready Approved");

                if (bookingRequestStatusDTO.Status == "Approved")
                {
                    Booking finalBooking = Booking.Create(bookingRequest.BookingRequestId, bookingRequestStatusDTO.Action, bookingRequestStatusDTO.Status, bookingRequest.BookingRequestDate, true, false, bookingRequest.TotalAmount, bookingRequestStatusDTO.FinalAmount, bookingRequestStatusDTO.DiscountPercent, bookingRequestStatusDTO.DiscountAmount, bookingRequestStatusDTO.Comment);
                    Exhibitor exhibitor = _exhibitorRepository.GetById(bookingRequest.Exhibitor.Id);
                    Event bookingEvent = _eventRepository.GetById(bookingRequest.Event.Id);
                    if (bookingEvent == null)
                        return NotFound();

                    var bookingRequestInstallmentSearchCriteria = new BookingRequestInstallmentSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestInstallmentSepcification = new BookingRequestInstallmentSpecificationForSearch(bookingRequestInstallmentSearchCriteria);
                    var bookingRequestInstallments = _bookingRequestInstallmentRepository.Find(bookingRequestInstallmentSepcification).OrderBy(x => x.Order);

                    foreach (BookingRequestInstallment bookingRequestInstallment in bookingRequestInstallments)
                    {
                        BookingInstallment bookingInstallment = BookingInstallment.Create(bookingRequestInstallment.Percent, bookingRequestInstallment.Amount, false, bookingRequestInstallment.Order, bookingRequestInstallment.PaymentDate);
                        bookingInstallment.Booking = finalBooking;
                        _bookingInstallmentRepository.Add(bookingInstallment);
                    }
                    if (bookingRequest.Section != null)
                    {
                        finalBooking.Section = bookingRequest.Section;
                    }
                    finalBooking.Exhibitor = exhibitor;
                    finalBooking.Event = bookingEvent;
                    _bookingRepository.Add(finalBooking);

                    var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                    var bookingRequestStall = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                    if (bookingRequestStall.Count() == 0)
                        return NotFound();

                    double totalPayment = 0;
                    foreach (BookingRequestStall stall in bookingRequestStall)
                    {
                        totalPayment = totalPayment + stall.Stall.Price;
                        Exhibition exhibition = _exhibitionRepository.GetById(stall.Exhibition.Id);
                        Stall stallAdded = _stallRepository.GetById(stall.Stall.Id);
                        stallAdded.Update(stallAdded.StallNo, stallAdded.Price, true, stallAdded.Height, stallAdded.Width, stallAdded.X_Coordinate, stallAdded.Y_Coordinate, stallAdded.StallSize, false);
                        StallBooking stallBooking = new StallBooking();
                        stallBooking.Event = bookingEvent;
                        stallBooking.Exhibition = exhibition;
                        stallBooking.Stall = stallAdded;
                        stallBooking.Booking = finalBooking;
                        _stallBookingRepository.Add(stallBooking);
                    }
                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    var bookingRequestSalesPeronMapList = _bookingRequestSalesPersonMapRepository.Find(bookingRequestSalesPersonMapSepcification);

                    bookingRequest.Action = bookingRequestStatusDTO.Action;
                    bookingRequest.Status = bookingRequestStatusDTO.Status;
                    bookingRequest.Booking = finalBooking;
                    bookingRequest.Update(bookingRequest.BookingRequestId, bookingRequestStatusDTO.Action, bookingRequest.Confirmed, bookingRequestStatusDTO.Status, bookingRequest.BookingRequestDate, bookingRequest.TotalAmount, bookingRequestStatusDTO.FinalAmount, bookingRequestStatusDTO.DiscountPercent, bookingRequestStatusDTO.DiscountAmount, bookingRequestStatusDTO.Comment);

                    if (bookingRequestSalesPeronMapList.Count() != 0)
                    {
                        BookingSalesPersonMap bookingSalesPersonMap = new BookingSalesPersonMap();
                        bookingSalesPersonMap.Booking = finalBooking;
                        bookingSalesPersonMap.Login = bookingRequestSalesPeronMapList.FirstOrDefault().Login;
                        _bookingSalesPersonMapRepository.Add(bookingSalesPersonMap);
                    }

                    List<string> emailIds = new List<string>();
                    emailIds.Add(exhibitor.EmailId);
                    emailIds.Add(bookingRequestSalesPeronMapList.FirstOrDefault().Login.EmailId);
                    string link = "http://gsmktg.com/Payment/#/payment/" + finalBooking.Id;
                    var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString("dd-MMM-yyyy");
                    string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>Cleave Confirm</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600' height='723'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata, West Bengal 700026</span> </td> </tr> <tr> <td class='hide-for-mobile' style='height:180px; width:299px;'> <img width='180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo' /> </td> </tr> </table> </td> <td valign='top' class='col-2'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad' width='355' height='661' valign='top'> <table cellpadding='0' cellspacing='0'> <tr> <td width='355'> <h1> Approved: <span> 2NV6E1xiRXOrPr </span> </h1> <h2> <span>" + date + " </span> </h2> </td> </tr> <tr> <td> Hello " + exhibitor.Name + ",<br> <br> We would like you to know that your Booking Request for Stall has been Approved!</td> </tr></table><table cellspacing='0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td><td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing='0' cellpadding='10' width='100%'> <tr> <td width='355' class='footer'> <center> <img width='213' height='48' src='http://gsmktg.com/mobile_images/thankyou.png'> </center> </td> </tr> </table> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br>West Bengal 700026</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                    string fileName = "test";
                    string subject = "GS Marketing Associate Booking Request Approved";
                    //Task.Run(() => SendMail(htmlPage, fileName, exhibitor, subject));
                    //Task.Run(() => SendMultipleMail(htmlPage, fileName, emailIds, subject));
                }
                else if (bookingRequestStatusDTO.Status == "Cancelled")
                {
                    bookingRequest.Action = bookingRequestStatusDTO.Action;
                    bookingRequest.Status = bookingRequestStatusDTO.Status;
                    bookingRequest.Update(bookingRequest.BookingRequestId, bookingRequestStatusDTO.Action, bookingRequest.Confirmed, bookingRequestStatusDTO.Status, bookingRequest.BookingRequestDate, bookingRequest.TotalAmount, bookingRequestStatusDTO.FinalAmount, bookingRequestStatusDTO.DiscountPercent, bookingRequestStatusDTO.DiscountAmount, bookingRequestStatusDTO.Comment);

                    var bookingRequestStallSearchCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequest.Id };
                    var bookingRequestStallSepcification = new BookingRequestStallSpecificationForSearch(bookingRequestStallSearchCriteria);
                    var bookingRequestStallList = _bookingRequestStallRepository.Find(bookingRequestStallSepcification);
                    if (bookingRequestStallList.Count() == 0)
                        return NotFound();
                    foreach (BookingRequestStall bookingRequestStall in bookingRequestStallList)
                    {
                        var stallDetails = _stallRepository.GetById(bookingRequestStall.Stall.Id);
                        stallDetails.Update(stallDetails.StallNo, stallDetails.Price, false, stallDetails.Height, stallDetails.Width, stallDetails.X_Coordinate, stallDetails.Y_Coordinate, stallDetails.StallSize, false);
                    }
                }
                else if (bookingRequestStatusDTO.Status == "Pending")
                {
                    bookingRequest.Update(bookingRequest.BookingRequestId, bookingRequestStatusDTO.Action, bookingRequest.Confirmed, bookingRequestStatusDTO.Status, bookingRequest.BookingRequestDate, bookingRequest.TotalAmount, bookingRequestStatusDTO.FinalAmount, bookingRequestStatusDTO.DiscountPercent, bookingRequestStatusDTO.DiscountAmount, bookingRequestStatusDTO.Comment);
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, bookingRequest.Event.Id, 5, 1);
            }
        }

        /// <summary>
        /// Add Payment for Booking
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingId"></param>
        /// <param name="paymentDTO"></param>
        /// <returns></returns>
        [Route("booking/{bookingId:guid}/payment")]
        [ModelValidator]
        [ResponseType(typeof(PaymentDTO))]
        public IHttpActionResult PostPayment(Guid organizerId, Guid bookingId, PaymentDTO paymentDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var booking = _bookingRepository.GetById(bookingId);
                if (booking == null)
                    return NotFound();

                Exhibitor exhibitor = _exhibitorRepository.GetById(booking.Exhibitor.Id);
                if (exhibitor == null)
                    return NotFound();

                string txnIdForPaymentGateWay = GetPaymentGateWayTxnId();

                var PaymentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                var paymentInvoice = _paymentRepository.Count(new GetAllSpecification<Payment>());
                paymentDTO.InvoiceNo = "Invoice-000" + paymentInvoice;
                Payment paymentDetails = Payment.Create(paymentDTO.AmountPaid, PaymentDate, paymentDTO.InvoiceNo, paymentDTO.IsPayOnLocation, txnIdForPaymentGateWay);
                paymentDetails.Booking = booking;


                if (paymentDTO.IsPayOnLocation == true)
                    paymentDetails.IsPaymentDone = true;
                else
                    paymentDetails.IsPaymentDone = false;

                _paymentRepository.Add(paymentDetails);

                var bookingStallSearchCriteria = new BookingStallSearchCriteria { BookingId = bookingId, EventId = booking.Event.Id };
                var bookingStallSepcification = new BookingStallSpecificationForSearch(bookingStallSearchCriteria);
                var bookingStall = _stallBookingRepository.Find(bookingStallSepcification);

                foreach (StallBooking stall in bookingStall)
                {
                    Stall stallDetails = _stallRepository.GetById(stall.Stall.Id);
                    stallDetails.Update(stallDetails.StallNo, stallDetails.Price, true, stallDetails.Height, stallDetails.Width, stallDetails.X_Coordinate, stallDetails.Y_Coordinate, stallDetails.StallSize, stallDetails.IsRequested);
                }

                unitOfWork.SaveChanges();

                var paymentSearchCriteria = new PaymentSearchCriteria { BookingId = bookingId };
                var paymentSepcification = new PaymentSpecificationForSearch(paymentSearchCriteria);
                var payments = _paymentRepository.Find(paymentSepcification);
                double totalAmountPaid = 0;
                foreach (Payment payment in payments)
                {
                    totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                }
                double AlreadyPaid = totalAmountPaid - paymentDetails.AmountPaid;
                double remainingAmount = booking.TotalAmount - totalAmountPaid;

                PaymentDTO displayPaymentDTO = new PaymentDTO()
                {
                    Id = paymentDetails.Id,
                    InvoiceNo = paymentDetails.InvoiceNo,
                    AmountPaid = paymentDetails.AmountPaid,
                    AmountRemaining = remainingAmount,
                    PaymentDate = paymentDetails.PaymentDate.ToString("dd-MMM-yyyy"),
                    Quantity = bookingStall.Count(),
                    TotalAmount = booking.TotalAmount,
                    ExhibitorName = exhibitor.Name,
                    Address = exhibitor.Address,
                    CompanyName = exhibitor.CompanyName,
                    IsPayOnLocation = paymentDetails.IsPayOnLocation,
                    AlreadyPaid = AlreadyPaid
                };

                return Ok(displayPaymentDTO);
            }
        }

        /// <summary>
        /// Add Stall Allocation for Sales Person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="SalesPersonId"></param>
        /// <param name="bookingRequestStallListDTO"></param>
        /// <returns></returns>
        [Route("bookingRequest/event/{eventId:guid}/exhibitor/{exhibitorId:guid}/stallAllocation/{SalesPersonId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(BookingRequestStallDTO))]
        public IHttpActionResult PostStallAllocation(Guid organizerId, Guid eventId, Guid exhibitorId, Guid SalesPersonId, BookingRequestStallDTO bookingRequestStallListDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login salesPerson = _loginRepository.GetById(SalesPersonId);
                if (salesPerson == null)
                    return NotFound();

                Event bookingEvent = _eventRepository.GetById(eventId);
                if (bookingEvent == null)
                    return NotFound();

                Exhibitor exhibitorDetails = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorDetails == null)
                    return NotFound();

                if (bookingRequestStallListDTO.BookingStallsListDTO.Count() == 0)
                    return NotFound();

                var stallDetail = _stallRepository.GetById(bookingRequestStallListDTO.BookingStallsListDTO.FirstOrDefault().StallId);

                var bookingDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                var bookingRequestCount = _bookingRequestRepository.Find(new GetAllSpecification<BookingRequest>()).Where(x => x.Event.Id.Equals(bookingEvent.Id)).Count();
                var bookingRequestId = GetBookingRequestId(bookingEvent, bookingRequestCount.ToString());

                var bookingRequeSearchCriteria = new BookingRequestSearchCriteria { EventId = eventId, BookingRequestId = bookingRequestId };
                var bookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequeSearchCriteria);
                var bookingRequestListCount = _bookingRequestRepository.Count(bookingRequestSepcification);

                if (bookingRequestListCount != 0)
                    return Ok("Allready Added");

                BookingRequest bookingRequest = BookingRequest.Create(bookingRequestId, "Pending", false, "Pending", bookingDate, bookingRequestStallListDTO.TotalAmount, bookingRequestStallListDTO.FinalAmount, null, null, bookingRequestStallListDTO.Comment);
                bookingRequest.Section = stallDetail.Section;
                bookingRequest.Login = salesPerson;
                bookingRequest.Exhibitor = exhibitorDetails;
                bookingRequest.Event = bookingEvent;
                bookingRequest.CreatedBy = salesPerson.Id;

                foreach (BookingInstallmentDTO singleBookingInstallmentDTO in bookingRequestStallListDTO.BookingInstallmentDTO)
                {
                    var todaysDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    DateTime paymentDate = DateTime.MinValue;
                    if (singleBookingInstallmentDTO.Order == 1)
                    {
                        paymentDate = todaysDate.AddDays(3);
                    }
                    else if (singleBookingInstallmentDTO.Order == 2)
                    {
                        paymentDate = bookingEvent.StartDate.AddDays(-30);
                    }
                    else
                    {
                        paymentDate = bookingEvent.StartDate.AddDays(-1);
                    }

                    BookingRequestInstallment bookingRequestInstallment = BookingRequestInstallment.Create(singleBookingInstallmentDTO.Percent, singleBookingInstallmentDTO.Amount, false, singleBookingInstallmentDTO.Order, paymentDate);
                    bookingRequestInstallment.BookingRequest = bookingRequest;
                    _bookingRequestInstallmentRepository.Add(bookingRequestInstallment);
                }

                BookingRequestSalesPersonMap bookingSalesPersoneMap = new BookingRequestSalesPersonMap();
                bookingSalesPersoneMap.BookingRequest = bookingRequest;
                bookingSalesPersoneMap.Login = salesPerson;
                _bookingRequestSalesPersonMapRepository.Add(bookingSalesPersoneMap);

                foreach (BookingStallsListDTO bookingstallDTO in bookingRequestStallListDTO.BookingStallsListDTO)
                {
                    BookingRequestStall bookingRequestStall = new BookingRequestStall();
                    Exhibition exhibition = _exhibitionRepository.GetById(bookingstallDTO.ExhibitionId);
                    if (exhibition == null)
                        return NotFound();

                    Stall stallAdded = _stallRepository.GetById(bookingstallDTO.StallId);
                    if (stallAdded == null)
                        return NotFound();

                    if (stallAdded.Partner != null)
                    {
                        if (stallAdded.Partner.Id == salesPerson.Partner.Id)
                        {
                            bookingRequestStall.Event = bookingEvent;
                            bookingRequestStall.Exhibition = exhibition;
                            bookingRequestStall.Stall = stallAdded;
                            bookingRequestStall.BookingRequest = bookingRequest;
                            bookingRequestStall.CreatedBy = salesPerson.Id;
                            stallAdded.IsRequested = true;
                            _stallRepository.Update(stallAdded);
                            _bookingRequestStallRepository.Add(bookingRequestStall);
                        }
                        else
                        {
                            HttpResponseMessage response =
                            this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Partner can not book stalls other than allocated");
                            throw new HttpResponseException(response);
                        }
                    }
                    else
                    {
                        bookingRequestStall.Event = bookingEvent;
                        bookingRequestStall.Exhibition = exhibition;
                        bookingRequestStall.Stall = stallAdded;
                        bookingRequestStall.BookingRequest = bookingRequest;
                        bookingRequestStall.CreatedBy = salesPerson.Id;
                        stallAdded.IsRequested = true;
                        _stallRepository.Update(stallAdded);
                        _bookingRequestStallRepository.Add(bookingRequestStall);
                    }
                }
                List<string> emailIds = new List<string>();
                emailIds.Add(exhibitorDetails.EmailId);
                emailIds.Add(salesPerson.EmailId);
                string link = "http://gsmktg.com/Payment/#/payment/";
                var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString("dd-MMM-yyyy");
                string htmlPage = "<html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=320, initial-scale=1' /> <title>Cleave Confirm</title> <style type='text/css' media='screen'> /* ----- Client Fixes ----- */ /* Force Outlook to provide a 'view in browser' message */ #outlook a { padding: 0; } /* Force Hotmail to display emails at full width */ .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } /* Force Hotmail to display normal line spacing */ .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } /* Prevent WebKit and Windows mobile changing default text sizes */ body, table, td, p, a, li, blockquote { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } /* Remove spacing between tables in Outlook 2007 and up */ table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } /* Allow smoother rendering of resized image in Internet Explorer */ img { -ms-interpolation-mode: bicubic; } /* ----- Reset ----- */ html, body, .body-wrap, .body-wrap-cell { margin: 0; padding: 0; background: #ffffff; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; text-align: left; } img { border: 0; line-height: 100%; outline: none; text-decoration: none; } table { border-collapse: collapse !important; } td, th { text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #89898D; line-height:1.5em; } /* ----- General ----- */ h1, h2 { line-height: 1.1; text-align: right; } h1 { margin-top: 0; margin-bottom: 10px; font-size: 24px; } h2 { margin-top: 0; margin-bottom: 60px; font-weight: normal; font-size: 17px; } .outer-padding { padding: 50px 0; } .col-1 { border-right: 1px solid #D9DADA; width: 180px; } td.hide-for-desktop-text { font-size: 0; height: 0; display: none; color: #ffffff; } img.hide-for-desktop-image { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } .body-cell { background-color: #ffffff; padding-top: 60px; vertical-align: top; } .body-cell-left-pad { padding-left: 30px; padding-right: 14px; } /* ----- Modules ----- */ .brand td { padding-top: 25px; } .brand a { font-size: 16px; line-height: 59px; font-weight: bold; } .data-table th, .data-table td { width: 350px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; } .data-table th { background-color: #f9f9f9; color: #f8931e; } .data-table td { padding-bottom: 30px; } .data-table .data-table-amount { font-weight: bold; font-size: 20px; } </style> <style type='text/css' media='only screen and (max-width: 650px)'> @media only screen and (max-width: 650px) { table[class*='w320'] { width: 320px !important; } td[class*='col-1'] { border: none; } td[class*='hide-for-mobile'] { font-size: 0 !important; line-height: 0 !important; width: 0 !important; height: 0 !important; display: none !important; } img[class*='hide-for-desktop-image']{ width: 176px !important; height: 135px !important; display:block !important; padding-left: 60px; } td[class*='hide-for-desktop-image'] { width: 100% !important; display: block !important; text-align: right !important; } td[class*='hide-for-desktop-text'] { display: block !important; text-align: center !important; font-size: 16px !important; height: 61px !important; padding-top: 30px !important; padding-bottom: 20px !important; color: #89898D !important; } td[class*='mobile-padding'] { padding-top: 15px; } td[class*='outer-padding'] { padding: 0 !important; } td[class*='body-cell-left-pad'] { padding-left: 20px; padding-right: 20px; } } </style></head><body class='body' style='padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none' bgcolor='#ffffff'><table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff'><tr> <td class='outer-padding' valign='top' align='left'> <center> <table class='w320' cellspacing='0' cellpadding='0' width='600' height='723'> <tr> <td class='col-1 hide-for-mobile'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-mobile' style='padding:30px 0 10px 0;'> <img width='130' height='41' src='http://gsmktg.com/wp-content/uploads/2016/06/resize-logo.png' alt='logo' /> </td> </tr> <tr> <td class='hide-for-mobile' height='150' valign='top' > <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata, West Bengal 700026</span> </td> </tr> <tr> <td class='hide-for-mobile' style='height:180px; width:299px;'> <img width='180' height='299'src='http://gsmktg.com/mobile_images/Email_Image.png' alt='large logo' /> </td> </tr> </table> </td> <td valign='top' class='col-2'> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='body-cell body-cell-left-pad' width='355' height='661' valign='top'> <table cellpadding='0' cellspacing='0'> <tr> <td width='355'> <h1> BookingRequestId: <span> " + bookingRequest.BookingRequestId + " </span> </h1> <h2> <span>" + date + " </span> </h2> </td> </tr> <tr> <td> Hello " + exhibitorDetails.Name + ",<br> <br> We would like you to know that your Booking Request for Stall has been Received . We shall process  your booking request within  next two days and upate you on the booking status. Thanks you for your interest in 'india international mega trade fair - Delhi - NCR - 2017' </br></br> Warm Regards, </br> GS Marketing Associates.</td> </tr> </table><table cellspacing='0' cellpadding='10' width='100%'> <tr> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> <td width='150' style='width:150px;'> <!--<![endif]--> </div> </td> <td class='hide-for-mobile' width='94' style='width:94px;'> &nbsp; </td> </tr> </table> <table cellspacing='0' cellpadding='10' width='100%'> <tr> <td width='355' class='footer'> <center> <img width='213' height='48' src='http://gsmktg.com/mobile_images/thankyou.png'> </center> </td> </tr> </table> <table cellspacing='0' cellpadding='0' width='100%'> <tr> <td class='hide-for-desktop-text'> <b> <span>GS Marketing Associates</span> </b> <br> <span>108/9, Manohar Pukur Road, Kalighat,Kolkata,<br>West Bengal 700026</span> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </center> </td></tr></table></body></html>";
                string fileName = "test";
                string subject = "GS Marketing Associate Booking Request";
                //Task.Run(() => SendMail(htmlPage, fileName, exhibitor, subject));
                Task.Run(() => SendMultipleMail(htmlPage, fileName, emailIds, subject));

                _bookingRequestRepository.Add(bookingRequest);
                unitOfWork.SaveChanges();
                return Ok("Added Successfully");
            }
        }

        /// <summary>
        /// Get Booking of Exhibitor for Event 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitorId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("booking/exhibitor/{exhibitorId:guid}/event/{eventId:guid}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetExhibitor(Guid organizerId, Guid exhibitorId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Exhibitor exhibitorDetails = _exhibitorRepository.GetById(exhibitorId);
                if (exhibitorDetails == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                List<ExhibitorDTO> exhibitorList = new List<ExhibitorDTO>(); //TODO: Make change in single DTO  

                ExhibitorDTO exhibitor = new ExhibitorDTO
                {
                    Id = exhibitorDetails.Id,
                    Name = exhibitorDetails.Name,
                    CompanyName = exhibitorDetails.CompanyName,
                    EmailId = exhibitorDetails.EmailId,
                    PhoneNo = exhibitorDetails.PhoneNo,
                    Address = exhibitorDetails.Address
                };
                exhibitorList.Add(exhibitor);

                var result = new
                {
                    totalCount = 0,
                    totalPages = 0,
                    listOfBookingDTO = exhibitorList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Booking Exhibitor List for Event 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("booking/exhibitorList/salesPerson/{salesPersonId:guid}/event/{eventId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetExhibitorByLead(Guid organizerId, Guid salesPersonId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var eventLeadExhibitorsMapCount = _eventLeadExhibitorMapBookingRepository.FindExhibitorListByEventLeadMapCount(salesPerson.Partner, eventDetails);
                    var eventLeadExhibitorsMap = _eventLeadExhibitorMapBookingRepository.FindExhibitorListByEventLeadMap(salesPerson.Partner, eventDetails, pageNumber, pageSize);

                    var bookingCount = eventLeadExhibitorsMapCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<ExhibitorDTO> listOfExhibitor = new List<ExhibitorDTO>();
                    foreach (EventLeadExhibitorMap singleEventLeadMap in eventLeadExhibitorsMap)
                    {
                        ExhibitorDTO singleExhibitor = new ExhibitorDTO
                        {
                            Id = singleEventLeadMap.Exhibitor.Id,
                            Name = singleEventLeadMap.Exhibitor.Name,
                            CompanyName = singleEventLeadMap.Exhibitor.CompanyName,
                            EmailId = singleEventLeadMap.Exhibitor.EmailId,
                            PhoneNo = singleEventLeadMap.Exhibitor.PhoneNo,
                            Address = singleEventLeadMap.Exhibitor.Address
                        };
                        listOfExhibitor.Add(singleExhibitor);
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfExhibitor
                    };
                    return Ok(result);
                }
                else
                {
                    var eventLeadExhibitorMapCriteria = new EventLeadExhibitorMapSearchCriteria { SalesPersonId = salesPerson.Id, EventId = eventDetails.Id };
                    var eventLeadExhibitorMapSepcification = new EventLeadExhibitorMapSpecificationForSearch(eventLeadExhibitorMapCriteria);
                    var eventLeadExhibitorMapCount = _eventLeadExhibitorMapRepository.Count(eventLeadExhibitorMapSepcification);
                    var eventLeadExhibitorMap = _eventLeadExhibitorMapRepository.Find(eventLeadExhibitorMapSepcification)
                                                .OrderByDescending(x => x.CreatedOn)
                                                .Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize);

                    var totalCount = eventLeadExhibitorMapCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    List<ExhibitorDTO> listOfExhibitor = new List<ExhibitorDTO>();
                    foreach (EventLeadExhibitorMap singleEventLeadMap in eventLeadExhibitorMap)
                    {
                        ExhibitorDTO singleExhibitor = new ExhibitorDTO
                        {
                            Id = singleEventLeadMap.Exhibitor.Id,
                            Name = singleEventLeadMap.Exhibitor.Name,
                            CompanyName = singleEventLeadMap.Exhibitor.CompanyName,
                            EmailId = singleEventLeadMap.Exhibitor.EmailId,
                            PhoneNo = singleEventLeadMap.Exhibitor.PhoneNo,
                            Address = singleEventLeadMap.Exhibitor.Address
                        };
                        listOfExhibitor.Add(singleExhibitor);
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingDTO = listOfExhibitor
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Add Exhibitor and Allocate to Sales Person
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="exhibitorAllocationStallDTO"></param>
        /// <returns></returns>
        [Route("booking/exhibitor/{salesPersonId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(ExhibitorAllocationStallDTO))]
        public IHttpActionResult PostExhibitor(Guid organizerId, Guid salesPersonId, ExhibitorAllocationStallDTO exhibitorAllocationStallDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(exhibitorAllocationStallDTO.EventId);
                if (eventDetails == null)
                    return NotFound();

                var exhibitorLoginCriteria = new ExhibitorLoginSearchCriteria { ExhibitorName = exhibitorAllocationStallDTO.Name, EmailId = exhibitorAllocationStallDTO.EmailId };
                var exhibitorLoginSepcification = new ExhibitorLoginSpecificationForSearch(exhibitorLoginCriteria);
                var exhibitorDetails = _exhibitorRepository.Find(exhibitorLoginSepcification);

                Exhibitor createExhibitor = new Exhibitor();

                if (exhibitorDetails.Count() == 0)
                {
                    createExhibitor = Exhibitor.Create(exhibitorAllocationStallDTO.Name, exhibitorAllocationStallDTO.EmailId, exhibitorAllocationStallDTO.PhoneNo, exhibitorAllocationStallDTO.CompanyName, null, null, exhibitorAllocationStallDTO.Address, exhibitorAllocationStallDTO.PinCode, null, 0);
                    createExhibitor.Organizer = organizer;
                    createExhibitor.CreatedBy = salesPerson.Id;
                    if (exhibitorAllocationStallDTO.State != null)
                    {
                        State state = _stateRepository.GetById(exhibitorAllocationStallDTO.State);
                        createExhibitor.State = state;
                    }

                    if (exhibitorAllocationStallDTO.Country != null)
                    {
                        Country country = _countryRepository.GetById(exhibitorAllocationStallDTO.Country);
                        createExhibitor.Country = country;
                    }

                    if (exhibitorAllocationStallDTO.CategoryId.Count() != 0)
                    {
                        foreach (Guid categoryId in exhibitorAllocationStallDTO.CategoryId)
                        {
                            Category categoryAdd = _categoryRepository.GetById(categoryId);
                            createExhibitor.Categories = new List<Category>();
                            createExhibitor.Categories.Add(categoryAdd);
                        }
                    }

                    if (exhibitorAllocationStallDTO.ExhibitorTypeId != null)
                    {
                        ExhibitorType exhibitorType = _exhibitorTypeRepository.GetById(exhibitorAllocationStallDTO.ExhibitorTypeId);
                        createExhibitor.ExhibitorType = exhibitorType;
                    }

                    if (exhibitorAllocationStallDTO.ExhibitorIndustryTypeId != null)
                    {
                        ExhibitorIndustryType exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorAllocationStallDTO.ExhibitorIndustryTypeId);
                        createExhibitor.ExhibitorIndustryType = exhibitorIndustryType;
                    }

                    if (exhibitorAllocationStallDTO.ExhibitorStatusId != null)
                    {
                        ExhibitorStatus exhibitorStatus = _exhibitorStatusRepository.GetById(exhibitorAllocationStallDTO.ExhibitorStatusId);
                        createExhibitor.ExhibitorStatus = exhibitorStatus;
                    }
                    _exhibitorRepository.Add(createExhibitor);
                }
                else
                {
                    createExhibitor = exhibitorDetails.FirstOrDefault();
                    if (createExhibitor.CompanyName == null && exhibitorAllocationStallDTO.CompanyName != null)
                    {
                        createExhibitor.CompanyName = exhibitorAllocationStallDTO.CompanyName;
                    }

                    if (createExhibitor.Country == null && exhibitorAllocationStallDTO.Country != null)
                    {
                        Country country = _countryRepository.GetById(exhibitorAllocationStallDTO.Country);
                        createExhibitor.Country = country;
                    }

                    if (createExhibitor.Categories.Count() == 0 && exhibitorAllocationStallDTO.CategoryId.Count() != 0)
                    {
                        foreach (Guid categoryId in exhibitorAllocationStallDTO.CategoryId)
                        {
                            Category categoryAdd = _categoryRepository.GetById(categoryId);
                            createExhibitor.Categories.Add(categoryAdd);
                        }
                    }

                    if (createExhibitor.State == null && exhibitorAllocationStallDTO.State != null)
                    {
                        State state = _stateRepository.GetById(exhibitorAllocationStallDTO.State);
                        createExhibitor.State = state;
                    }

                    if (exhibitorAllocationStallDTO.ExhibitorTypeId != null)
                    {
                        ExhibitorType exhibitorType = _exhibitorTypeRepository.GetById(exhibitorAllocationStallDTO.ExhibitorTypeId);
                        createExhibitor.ExhibitorType = exhibitorType;
                    }

                    if (exhibitorAllocationStallDTO.ExhibitorIndustryTypeId != null)
                    {
                        ExhibitorIndustryType exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(exhibitorAllocationStallDTO.ExhibitorIndustryTypeId);
                        createExhibitor.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                    if (exhibitorAllocationStallDTO.ExhibitorStatusId != null)
                    {
                        ExhibitorStatus exhibitorStatus = _exhibitorStatusRepository.GetById(exhibitorAllocationStallDTO.ExhibitorStatusId);
                        createExhibitor.ExhibitorStatus = exhibitorStatus;
                    }
                    _exhibitorRepository.Update(createExhibitor);
                }
                var eventLeadExhibitorMapCriteria = new EventLeadExhibitorMapSearchCriteria { ExhibitorId = createExhibitor.Id, EventId = eventDetails.Id };
                var eventLeadExhibitorMapSepcification = new EventLeadExhibitorMapSpecificationForSearch(eventLeadExhibitorMapCriteria);
                var eventLeadExhibitorMap = _eventLeadExhibitorMapRepository.Find(eventLeadExhibitorMapSepcification);

                if (eventLeadExhibitorMap.Count() == 0)
                {
                    EventLeadExhibitorMap eventLeadExhibitor = new EventLeadExhibitorMap();
                    eventLeadExhibitor.Exhibitor = createExhibitor;
                    eventLeadExhibitor.Login = salesPerson;
                    eventLeadExhibitor.Event = eventDetails;
                    _eventLeadExhibitorMapRepository.Add(eventLeadExhibitor);
                }
                unitOfWork.SaveChanges();
                return GetExhibitor(organizerId, createExhibitor.Id, eventDetails.Id);
            }
        }

        //[Route("event/{eventId:guid}/bookingRequest/{bookingRequestId:guid}/delete")]
        //public IHttpActionResult Delete(Guid organizerId, Guid eventId, Guid bookingRequestId)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
        //    {

        //        var organizer = _organizerRepository.GetById(organizerId);
        //        if (organizer == null)
        //            return NotFound();
        //        var eventDetails = _eventRepository.GetById(eventId);
        //        if (eventDetails == null)
        //            return NotFound();

        //        var bookingRequestDetails = _bookingRequestRepository.GetById(bookingRequestId);
        //        if (bookingRequestDetails == null)
        //            return NotFound();

        //        var criteria = new BookingRequestSalesPersonMapSearchCriteria { BookingRequestId = bookingRequestId, EventId = eventDetails.Id };
        //        var sepcification = new BookingRequestSalesPersonMapSpecificationForSearch(criteria);
        //        var bookingRequestSalesPersonMapList = _bookingRequestSalesPersonMapRepository.Find(sepcification);

        //        foreach (BookingRequestSalesPersonMap bookingRequestSalesPersonMap in bookingRequestSalesPersonMapList)
        //        {
        //            _bookingRequestSalesPersonMapRepository.Delete(bookingRequestSalesPersonMap.Id);
        //        }

        //        var bookingRequestInstallmetsCriteria = new BookingRequestInstallmentSearchCriteria { BookingRequestId = bookingRequestId };
        //        var bookingRequestInstallmetSpecification = new BookingRequestInstallmentSpecificationForSearch(bookingRequestInstallmetsCriteria);
        //        var bookingRequestInstallmetList = _bookingRequestInstallmentRepository.Find(bookingRequestInstallmetSpecification);
        //        foreach (BookingRequestInstallment bookingRequestInstallment in bookingRequestInstallmetList)
        //        {
        //            _bookingRequestInstallmentRepository.Delete(bookingRequestInstallment.Id);
        //        }

        //        var bookingRequestStallCriteria = new BookingRequestStallSearchCriteria { BookingRequestId = bookingRequestId, EventId = eventDetails.Id };
        //        var bookingRequestStallSpecification = new BookingRequestStallSpecificationForSearch(bookingRequestStallCriteria);
        //        var bookingRequestStallList = _bookingRequestStallRepository.Find(bookingRequestStallSpecification);
        //        foreach (BookingRequestStall bookingRequeststall in bookingRequestStallList)
        //        {
        //            Stall stallDetails = _stallRepository.GetById(bookingRequeststall.Stall.Id);
        //            stallDetails.IsRequested = false;
        //            _bookingRequestStallRepository.Delete(bookingRequeststall.Id);
        //        }
        //        _bookingRequestRepository.Delete(bookingRequestDetails.Id);
        //        unitOfWork.SaveChanges();
        //    }
        //    return Ok("deleted");
        //}


        private void SendMail(string htmlPage, string fileName, Exhibitor exhibitor, string subject)
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

        private void SendMultipleMail(string htmlPage, string fileName, List<string> emailList, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.SendMultiple(emailList, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }
        }

        private string GetBookingRequestId(Event currentEvent, string count)
        {
            var city = currentEvent.Venue.City;
            var BookingRequestId = city.Substring(0, 2).ToUpper() + "N" + count.PadLeft(4, '0');
            return BookingRequestId;
        }

        private string GetPaymentGateWayTxnId()
        {
            TokenGenerationService txnIdPaymentGateWaySvc = new TokenGenerationService();
            txnIdPaymentGateWaySvc.Exclusions = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
            txnIdPaymentGateWaySvc.Minimum = 16;
            txnIdPaymentGateWaySvc.Maximum = 20;

            return txnIdPaymentGateWaySvc.Generate();
        }
    }
}
