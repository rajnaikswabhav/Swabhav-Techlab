using Modules.EventManagement;
using Modules.LayoutManagement;
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
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/bookingPayment")]
    public class BookingPaymentController : ApiController
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
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<BookingSalesPersonMap> _bookingSalesPersonMapRepository = new EntityFrameworkRepository<BookingSalesPersonMap>();
        private IRepository<BookingRequestSalesPersonMap> _bookingRequestSalesPersonMapRepository = new EntityFrameworkRepository<BookingRequestSalesPersonMap>();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private IRepository<EventLeadExhibitorMap> _eventLeadExhibitorMapRepository = new EntityFrameworkRepository<EventLeadExhibitorMap>();
        private IRepository<PaymentDetails> _paymentDetailsRepository = new EntityFrameworkRepository<PaymentDetails>();
        private IRepository<BookingInstallment> _bookingInstallmentRepository = new EntityFrameworkRepository<BookingInstallment>();
        private BookingRepository<PaymentDetails> _bookingPaymentRepository = new BookingRepository<PaymentDetails>();
        private BookingRepository<StallBooking> _stallBookingCountRepository = new BookingRepository<StallBooking>();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Get Payment of Single Booking
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

                BookingPaymentDTO singleBookingRequestDTO = new BookingPaymentDTO();
                var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { BookingId = booking.Id };
                var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                        .OrderByDescending(x => x.CreatedOn);

                if (paymentDetailsList.Count() == 0)
                    return NotFound();

                List<BookingPaymentDTO> paymentDetailsDTOList = new List<BookingPaymentDTO>();
                var totalStallsCount = _stallBookingCountRepository.StallBookingCount(booking);

                var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(booking, booking.Event);
                foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                {
                    BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                    {
                        Id = singlePaymentDetail.Id,
                        AmountPaid = singlePaymentDetail.AmountPaid,
                        EventName = singlePaymentDetail.Event.Name,
                        BankName = singlePaymentDetail.BankName,
                        BankBranch = singlePaymentDetail.BankBranch,
                        ChequeNo = singlePaymentDetail.ChequeNo,
                        ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                        CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                        PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MMM-yyyy"),
                        PaymentID = singlePaymentDetail.PaymentID,
                        PaymentStatus = singlePaymentDetail.PaymentStatus,
                        IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                        UTRNo = singlePaymentDetail.UTRNo,
                        TotalAmount = booking.FinalAmountTax,
                        TotalAmountPaid = totalAmountPaid,
                        RemainingAmount = booking.FinalAmountTax - totalAmountPaid,
                        TotalStalls = totalStallsCount
                    };

                    if (singlePaymentDetail.PaymentStatus == 1)
                    {
                        paymentDetailDTO.PaymentStatusName = "Unclear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 2)
                    {
                        paymentDetailDTO.PaymentStatusName = "Clear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 3)
                    {
                        paymentDetailDTO.PaymentStatusName = "Rejected";
                    }

                    if (singlePaymentDetail.PaymentMode == 1)
                    {
                        paymentDetailDTO.PaymentMode = "Cash";
                    }
                    else if (singlePaymentDetail.PaymentMode == 2)
                    {
                        paymentDetailDTO.PaymentMode = "Credit Card";
                    }
                    else if (singlePaymentDetail.PaymentMode == 3)
                    {
                        paymentDetailDTO.PaymentMode = "Cheque";
                    }
                    else if (singlePaymentDetail.PaymentMode == 4)
                    {
                        paymentDetailDTO.PaymentMode = "Net Banking";
                    }
                    else
                    {
                        paymentDetailDTO.PaymentMode = "Invalid Payment";
                    }

                    if (singlePaymentDetail.Login != null)
                    {
                        paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                    }
                    paymentDetailsDTOList.Add(paymentDetailDTO);

                }
                return Ok(paymentDetailsDTOList);
            }
        }

        /// <summary>
        ///Get Payment Details of Single Payment 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [Route("{paymentId:guid}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid paymentId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var paymentDetails = _paymentDetailsRepository.GetById(paymentId);
                if (paymentDetails == null)
                    return NotFound();

                var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(paymentDetails.Booking, paymentDetails.Event);
                var totalStallsCount = _stallBookingCountRepository.StallBookingCount(paymentDetails.Booking);
                BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                {
                    Id = paymentDetails.Id,
                    AmountPaid = paymentDetails.AmountPaid,
                    EventName = paymentDetails.Event.Name,
                    BankName = paymentDetails.BankName,
                    BankBranch = paymentDetails.BankBranch,
                    ChequeNo = paymentDetails.ChequeNo,
                    ExhibitorName = paymentDetails.Exhibitor.Name,
                    CompanyName = paymentDetails.Exhibitor.CompanyName,
                    PaymentDate = paymentDetails.PaymentDate.ToString("dd-MMM-yyyy"),
                    PaymentID = paymentDetails.PaymentID,
                    PaymentStatus = paymentDetails.PaymentStatus,
                    IsPaymentApprove = paymentDetails.IsPaymentApprove,
                    UTRNo = paymentDetails.UTRNo,
                    TotalAmountPaid = totalAmountPaid,
                    TotalAmount = paymentDetails.Booking.FinalAmountTax,
                    RemainingAmount = paymentDetails.Booking.FinalAmountTax - totalAmountPaid,
                    TotalStalls = totalStallsCount

                };

                if (paymentDetails.PaymentStatus == 1)
                {
                    paymentDetailDTO.PaymentStatusName = "Unclear";
                }
                else if (paymentDetails.PaymentStatus == 2)
                {
                    paymentDetailDTO.PaymentStatusName = "Clear";
                }
                else if (paymentDetails.PaymentStatus == 3)
                {
                    paymentDetailDTO.PaymentStatusName = "Rejected";
                }

                if (paymentDetails.PaymentMode == 1)
                {
                    paymentDetailDTO.PaymentMode = "Cash";
                }
                else if (paymentDetails.PaymentMode == 2)
                {
                    paymentDetailDTO.PaymentMode = "Credit Card";
                }
                else if (paymentDetails.PaymentMode == 3)
                {
                    paymentDetailDTO.PaymentMode = "Cheque";
                }
                else if (paymentDetails.PaymentMode == 4)
                {
                    paymentDetailDTO.PaymentMode = "Net Banking";
                }
                else
                {
                    paymentDetailDTO.PaymentMode = "Invalid Payment";
                }
                return Ok(paymentDetailDTO);
            }
        }

        /// <summary>
        /// Search Payment By Company Name 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="companyName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/search/{companyName}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetByCompanyNameSearch(Guid organizerId, Guid eventId, string companyName, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                BookingPaymentDTO singleBookingRequestDTO = new BookingPaymentDTO();
                var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetails.Id, CompanyName = companyName };
                var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                var paymentDetailsListCount = _paymentDetailsRepository.Count(paymentDetailsSepcification);
                var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

                var bookingCount = paymentDetailsListCount;
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (paymentDetailsListCount == 0)
                    return NotFound();

                List<BookingPaymentDTO> paymentDetailsDTOList = new List<BookingPaymentDTO>();

                foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                {
                    var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(singlePaymentDetail.Booking, singlePaymentDetail.Event);
                    var totalStallsCount = _stallBookingCountRepository.StallBookingCount(singlePaymentDetail.Booking);
                    BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                    {
                        Id = singlePaymentDetail.Id,
                        AmountPaid = singlePaymentDetail.AmountPaid,
                        EventName = singlePaymentDetail.Event.Name,
                        BankName = singlePaymentDetail.BankName,
                        BankBranch = singlePaymentDetail.BankBranch,
                        ChequeNo = singlePaymentDetail.ChequeNo,
                        ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                        CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                        PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MM-yyyy"),
                        PaymentID = singlePaymentDetail.PaymentID,
                        PaymentStatus = singlePaymentDetail.PaymentStatus,
                        IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                        UTRNo = singlePaymentDetail.UTRNo,
                        TotalAmountPaid = totalAmountPaid,
                        RemainingAmount = singlePaymentDetail.Booking.FinalAmountTax - totalAmountPaid,
                        TotalAmount = singlePaymentDetail.Booking.FinalAmountTax,
                        TotalStalls = totalStallsCount
                    };

                    if (singlePaymentDetail.PaymentStatus == 1)
                    {
                        paymentDetailDTO.PaymentStatusName = "Unclear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 2)
                    {
                        paymentDetailDTO.PaymentStatusName = "Clear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 3)
                    {
                        paymentDetailDTO.PaymentStatusName = "Rejected";
                    }

                    if (singlePaymentDetail.PaymentMode == 1)
                    {
                        paymentDetailDTO.PaymentMode = "Cash";
                    }
                    else if (singlePaymentDetail.PaymentMode == 2)
                    {
                        paymentDetailDTO.PaymentMode = "Credit Card";
                    }
                    else if (singlePaymentDetail.PaymentMode == 3)
                    {
                        paymentDetailDTO.PaymentMode = "Cheque";
                    }
                    else if (singlePaymentDetail.PaymentMode == 4)
                    {
                        paymentDetailDTO.PaymentMode = "Net Banking";
                    }
                    else
                    {
                        paymentDetailDTO.PaymentMode = "Invalid Payment";
                    }

                    if (singlePaymentDetail.Login != null)
                    {
                        paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                    }
                    paymentDetailsDTOList.Add(paymentDetailDTO);
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingPaymentsDTO = paymentDetailsDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Approved Payments 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/paymentApprovals/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetByEventId(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                BookingPaymentDTO singleBookingRequestDTO = new BookingPaymentDTO();
                var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetails.Id, IsPaymentCompleted = "False" };
                var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                var paymentDetailsListCount = _paymentDetailsRepository.Count(paymentDetailsSepcification);
                var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                                            .Take(pageSize);

                var bookingCount = paymentDetailsListCount;
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (paymentDetailsListCount == 0)
                    return NotFound();
                List<BookingPaymentDTO> paymentDetailsDTOList = new List<BookingPaymentDTO>();

                foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                {
                    var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(singlePaymentDetail.Booking, singlePaymentDetail.Event);
                    var totalStallsCount = _stallBookingCountRepository.StallBookingCount(singlePaymentDetail.Booking);
                    BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                    {
                        Id = singlePaymentDetail.Id,
                        AmountPaid = singlePaymentDetail.AmountPaid,
                        EventName = singlePaymentDetail.Event.Name,
                        BankName = singlePaymentDetail.BankName,
                        BankBranch = singlePaymentDetail.BankBranch,
                        ChequeNo = singlePaymentDetail.ChequeNo,
                        ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                        CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                        PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MMM-yyyy"),
                        PaymentID = singlePaymentDetail.PaymentID,
                        PaymentStatus = singlePaymentDetail.PaymentStatus,
                        IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                        UTRNo = singlePaymentDetail.UTRNo,
                        TotalAmountPaid = totalAmountPaid,
                        RemainingAmount = singlePaymentDetail.Booking.FinalAmountTax - totalAmountPaid,
                        TotalAmount = singlePaymentDetail.Booking.FinalAmountTax,
                        TotalStalls = totalStallsCount
                    };

                    if (singlePaymentDetail.PaymentStatus == 1)
                    {
                        paymentDetailDTO.PaymentStatusName = "Unclear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 2)
                    {
                        paymentDetailDTO.PaymentStatusName = "Clear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 3)
                    {
                        paymentDetailDTO.PaymentStatusName = "Rejected";
                    }

                    if (singlePaymentDetail.PaymentMode == 1)
                    {
                        paymentDetailDTO.PaymentMode = "Cash";
                    }
                    else if (singlePaymentDetail.PaymentMode == 2)
                    {
                        paymentDetailDTO.PaymentMode = "Credit Card";
                    }
                    else if (singlePaymentDetail.PaymentMode == 3)
                    {
                        paymentDetailDTO.PaymentMode = "Cheque";
                    }
                    else if (singlePaymentDetail.PaymentMode == 4)
                    {
                        paymentDetailDTO.PaymentMode = "Net Banking";
                    }
                    else
                    {
                        paymentDetailDTO.PaymentMode = "Invalid Payment";
                    }

                    if (singlePaymentDetail.Login != null)
                    {
                        paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                    }
                    paymentDetailsDTOList.Add(paymentDetailDTO);
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingPaymentsDTO = paymentDetailsDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Approved Payments of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/approvedPayments/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetApprovedPaymentByEventId(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                BookingPaymentDTO singleBookingRequestDTO = new BookingPaymentDTO();
                var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetails.Id, IsPaymentCompleted = "true" };
                var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                var paymentDetailsListCount = _paymentDetailsRepository.Count(paymentDetailsSepcification);
                var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Skip((pageNumber - 1) * pageSize)
                                                            .Take(pageSize);

                var bookingCount = paymentDetailsListCount;
                var totalCount = bookingCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (paymentDetailsListCount == 0)
                    return NotFound();
                List<BookingPaymentDTO> paymentDetailsDTOList = new List<BookingPaymentDTO>();

                foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                {
                    var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(singlePaymentDetail.Booking, singlePaymentDetail.Event);
                    var totalStallsCount = _stallBookingCountRepository.StallBookingCount(singlePaymentDetail.Booking);
                    BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                    {
                        Id = singlePaymentDetail.Id,
                        AmountPaid = singlePaymentDetail.AmountPaid,
                        EventName = singlePaymentDetail.Event.Name,
                        BankName = singlePaymentDetail.BankName,
                        BankBranch = singlePaymentDetail.BankBranch,
                        ChequeNo = singlePaymentDetail.ChequeNo,
                        ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                        CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                        PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MMM-yyyy"),
                        PaymentID = singlePaymentDetail.PaymentID,
                        PaymentStatus = singlePaymentDetail.PaymentStatus,
                        IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                        UTRNo = singlePaymentDetail.UTRNo,
                        TotalAmountPaid = totalAmountPaid,
                        RemainingAmount = singlePaymentDetail.Booking.FinalAmountTax - totalAmountPaid,
                        TotalAmount = singlePaymentDetail.Booking.FinalAmountTax,
                        TotalStalls = totalStallsCount
                    };

                    if (singlePaymentDetail.PaymentStatus == 1)
                    {
                        paymentDetailDTO.PaymentStatusName = "Unclear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 2)
                    {
                        paymentDetailDTO.PaymentStatusName = "Clear";
                    }
                    else if (singlePaymentDetail.PaymentStatus == 3)
                    {
                        paymentDetailDTO.PaymentStatusName = "Rejected";
                    }

                    if (singlePaymentDetail.PaymentMode == 1)
                    {
                        paymentDetailDTO.PaymentMode = "Cash";
                    }
                    else if (singlePaymentDetail.PaymentMode == 2)
                    {
                        paymentDetailDTO.PaymentMode = "Credit Card";
                    }
                    else if (singlePaymentDetail.PaymentMode == 3)
                    {
                        paymentDetailDTO.PaymentMode = "Cheque";
                    }
                    else if (singlePaymentDetail.PaymentMode == 4)
                    {
                        paymentDetailDTO.PaymentMode = "Net Banking";
                    }
                    else
                    {
                        paymentDetailDTO.PaymentMode = "Invalid Payment";
                    }

                    if (singlePaymentDetail.Login != null)
                    {
                        paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                    }
                    paymentDetailsDTOList.Add(paymentDetailDTO);
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingPaymentsDTO = paymentDetailsDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        ///Get all Payments By SalesPerson  
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/salesPerson/{salesPersonId:guid}/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult GetBySalesId(Guid organizerId, Guid eventId, Guid salesPersonId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                List<BookingPaymentDTO> paymentDetailsDTOList = new List<BookingPaymentDTO>();
                BookingPaymentDTO singleBookingRequestDTO = new BookingPaymentDTO();
                if (salesPerson.Role.RoleName.ToUpper().Equals(("Partner").ToUpper()))
                {
                    var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetails.Id, PartnerId = salesPerson.Partner.Id };
                    var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                    var paymentDetailsListCount = _paymentDetailsRepository.Count(paymentDetailsSepcification);
                    var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                            .OrderByDescending(x => x.CreatedOn)
                                            .Skip((pageNumber - 1) * pageSize)
                                                                .Take(pageSize);

                    var bookingCount = paymentDetailsListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    if (paymentDetailsListCount == 0)
                        return NotFound();
                    foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                    {
                        var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(singlePaymentDetail.Booking, singlePaymentDetail.Event);
                        var totalStallsCount = _stallBookingCountRepository.StallBookingCount(singlePaymentDetail.Booking);
                        BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                        {
                            Id = singlePaymentDetail.Id,
                            AmountPaid = singlePaymentDetail.AmountPaid,
                            EventName = singlePaymentDetail.Event.Name,
                            BankName = singlePaymentDetail.BankName,
                            BankBranch = singlePaymentDetail.BankBranch,
                            ChequeNo = singlePaymentDetail.ChequeNo,
                            ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                            CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                            PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MMM-yyyy"),
                            PaymentID = singlePaymentDetail.PaymentID,
                            PaymentStatus = singlePaymentDetail.PaymentStatus,
                            IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                            UTRNo = singlePaymentDetail.UTRNo,
                            TotalAmountPaid = totalAmountPaid,
                            RemainingAmount = singlePaymentDetail.Booking.FinalAmountTax - totalAmountPaid,
                            TotalAmount = singlePaymentDetail.Booking.FinalAmountTax,
                            TotalStalls = totalStallsCount
                        };

                        if (singlePaymentDetail.PaymentStatus == 1)
                        {
                            paymentDetailDTO.PaymentStatusName = "Unclear";
                        }
                        else if (singlePaymentDetail.PaymentStatus == 2)
                        {
                            paymentDetailDTO.PaymentStatusName = "Clear";
                        }
                        else if (singlePaymentDetail.PaymentStatus == 3)
                        {
                            paymentDetailDTO.PaymentStatusName = "Rejected";
                        }

                        if (singlePaymentDetail.PaymentMode == 1)
                        {
                            paymentDetailDTO.PaymentMode = "Cash";
                        }
                        else if (singlePaymentDetail.PaymentMode == 2)
                        {
                            paymentDetailDTO.PaymentMode = "Credit Card";
                        }
                        else if (singlePaymentDetail.PaymentMode == 3)
                        {
                            paymentDetailDTO.PaymentMode = "Cheque";
                        }
                        else if (singlePaymentDetail.PaymentMode == 4)
                        {
                            paymentDetailDTO.PaymentMode = "Net Banking";
                        }
                        else
                        {
                            paymentDetailDTO.PaymentMode = "Invalid Payment";
                        }

                        if (singlePaymentDetail.Login != null)
                        {
                            paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                        }
                        paymentDetailsDTOList.Add(paymentDetailDTO);
                    }
                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingPaymentsDTO = paymentDetailsDTOList
                    };
                    return Ok(result);
                }
                else
                {
                    var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetails.Id, SalesPersonId = salesPersonId };
                    var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                    var paymentDetailsListCount = _paymentDetailsRepository.Count(paymentDetailsSepcification);
                    var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification)
                                            .OrderByDescending(x => x.CreatedOn)
                                            .Skip((pageNumber - 1) * pageSize)
                                                                .Take(pageSize);

                    var bookingCount = paymentDetailsListCount;
                    var totalCount = bookingCount;
                    var totalPages = Math.Ceiling((double)totalCount / pageSize);

                    if (paymentDetailsListCount == 0)
                        return NotFound();
                    foreach (PaymentDetails singlePaymentDetail in paymentDetailsList)
                    {
                        var totalAmountPaid = _bookingPaymentRepository.SumOfTotalPaidAmountByBooking(singlePaymentDetail.Booking, singlePaymentDetail.Event);
                        var totalStallsCount = _stallBookingCountRepository.StallBookingCount(singlePaymentDetail.Booking);
                        BookingPaymentDTO paymentDetailDTO = new BookingPaymentDTO
                        {
                            Id = singlePaymentDetail.Id,
                            AmountPaid = singlePaymentDetail.AmountPaid,
                            EventName = singlePaymentDetail.Event.Name,
                            BankName = singlePaymentDetail.BankName,
                            BankBranch = singlePaymentDetail.BankBranch,
                            ChequeNo = singlePaymentDetail.ChequeNo,
                            ExhibitorName = singlePaymentDetail.Exhibitor.Name,
                            CompanyName = singlePaymentDetail.Exhibitor.CompanyName,
                            PaymentDate = singlePaymentDetail.PaymentDate.ToString("dd-MMM-yyyy"),
                            PaymentID = singlePaymentDetail.PaymentID,
                            PaymentStatus = singlePaymentDetail.PaymentStatus,
                            IsPaymentApprove = singlePaymentDetail.IsPaymentApprove,
                            UTRNo = singlePaymentDetail.UTRNo,
                            TotalAmountPaid = totalAmountPaid,
                            RemainingAmount = singlePaymentDetail.Booking.FinalAmountTax - totalAmountPaid,
                            TotalAmount = singlePaymentDetail.Booking.FinalAmountTax,
                            TotalStalls = totalStallsCount
                        };

                        if (singlePaymentDetail.PaymentStatus == 1)
                        {
                            paymentDetailDTO.PaymentStatusName = "Unclear";
                        }
                        else if (singlePaymentDetail.PaymentStatus == 2)
                        {
                            paymentDetailDTO.PaymentStatusName = "Clear";
                        }
                        else if (singlePaymentDetail.PaymentStatus == 3)
                        {
                            paymentDetailDTO.PaymentStatusName = "Rejected";
                        }

                        if (singlePaymentDetail.PaymentMode == 1)
                        {
                            paymentDetailDTO.PaymentMode = "Cash";
                        }
                        else if (singlePaymentDetail.PaymentMode == 2)
                        {
                            paymentDetailDTO.PaymentMode = "Credit Card";
                        }
                        else if (singlePaymentDetail.PaymentMode == 3)
                        {
                            paymentDetailDTO.PaymentMode = "Cheque";
                        }
                        else if (singlePaymentDetail.PaymentMode == 4)
                        {
                            paymentDetailDTO.PaymentMode = "Net Banking";
                        }
                        else
                        {
                            paymentDetailDTO.PaymentMode = "Invalid Payment";
                        }

                        if (singlePaymentDetail.Login != null)
                        {
                            paymentDetailDTO.SalesPerson = singlePaymentDetail.Login.UserName;
                        }
                        paymentDetailsDTOList.Add(paymentDetailDTO);
                    }

                    var result = new
                    {
                        totalCount = totalCount,
                        totalPages = totalPages,
                        listOfBookingPaymentsDTO = paymentDetailsDTOList
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// Get Payment By Booking 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="bookingId"></param>
        /// <param name="bookingPaymentDTO"></param>
        /// <returns></returns>
        [Route("booking/{bookingId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(BookingPaymentDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid bookingId, BookingPaymentDTO bookingPaymentDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Booking bookingDetails = _bookingRepository.GetById(bookingId);
                if (bookingId == null)
                    return NotFound();

                string paymentId = GetPaymentGateWayTxnId();
                PaymentDetails paymentDetails = PaymentDetails.Create(bookingPaymentDTO.AmountPaid, Convert.ToDateTime(bookingPaymentDTO.PaymentDate), paymentId, Convert.ToInt16(bookingPaymentDTO.PaymentMode), bookingPaymentDTO.ChequeNo, bookingPaymentDTO.BankName, bookingPaymentDTO.BankBranch, bookingPaymentDTO.UTRNo, bookingPaymentDTO.PaymentStatus, bookingPaymentDTO.IsPaymentApprove);

                var bookingSalesPersonSearchCriteria = new BookingSalesPersonMapSearchCriteria { BookingId = bookingDetails.Id };
                var bookingSalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingSalesPersonSearchCriteria);
                var bookingSalesPersonList = _bookingSalesPersonMapRepository.Find(bookingSalesPersonSepcification);

                BookingInstallment bookingInstallmentDetails = _bookingInstallmentRepository.GetById(bookingPaymentDTO.BookingInstalmentId);
                if (bookingInstallmentDetails == null)
                    return NotFound();

                paymentDetails.Exhibitor = bookingDetails.Exhibitor;
                paymentDetails.Event = bookingDetails.Event;
                paymentDetails.Booking = bookingDetails;
                paymentDetails.Login = bookingSalesPersonList.FirstOrDefault().Login;
                paymentDetails.BookingInstallment = bookingInstallmentDetails;
                _paymentDetailsRepository.Add(paymentDetails);

                var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { BookingId = bookingDetails.Id };
                var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                var paymentDetailsList = _paymentDetailsRepository.Find(paymentDetailsSepcification);
                double totalAmountPaid = 0;

                foreach (PaymentDetails payment in paymentDetailsList)
                {
                    if (payment.IsPaymentApprove == true)
                        totalAmountPaid = totalAmountPaid + payment.AmountPaid;
                }

                foreach (BookingInstallmentDTO bookingInstallmentDTO in bookingPaymentDTO.BookingInstallmentDTO)
                {
                    BookingInstallment bookingInstallment = _bookingInstallmentRepository.GetById(bookingInstallmentDTO.BookingInstallmentId);
                    bookingInstallment.Update(bookingInstallmentDTO.Percent, bookingInstallmentDTO.Amount, bookingInstallment.IsPaid, bookingInstallment.Order, bookingInstallment.PaymentDate);
                }

                if ((bookingDetails.FinalAmountTax - totalAmountPaid) <= 0)
                {
                    bookingDetails.Update(bookingDetails.BookingId, bookingDetails.Action, bookingDetails.Status, bookingDetails.BookingDate, bookingDetails.BookingAccepted, true, bookingDetails.TotalAmount, bookingDetails.FinalAmountTax, bookingDetails.DiscountPercent, bookingDetails.DiscountAmount, bookingDetails.Comment);
                }

                unitOfWork.SaveChanges();
                return GetByBookingId(organizerId, bookingDetails.Id);
            }
        }

        /// <summary>
        /// Update Approve Payment
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="paymentDetailsId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Route("{paymentDetailsId:guid}/approve/{status}")]
        [ModelValidator]
        [ResponseType(typeof(BookingPaymentDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid paymentDetailsId, string status)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                PaymentDetails paymentDetails = _paymentDetailsRepository.GetById(paymentDetailsId);
                if (paymentDetails == null)
                    return NotFound();

                if (status.ToUpper().Equals(("Approve").ToUpper()))
                {
                    paymentDetails.Update(paymentDetails.AmountPaid, paymentDetails.PaymentDate, paymentDetails.PaymentID, paymentDetails.PaymentMode, paymentDetails.ChequeNo, paymentDetails.BankName, paymentDetails.BankBranch, paymentDetails.UTRNo, 2, true);

                    BookingInstallment bookingInstalment = paymentDetails.BookingInstallment;
                    if (bookingInstalment != null)
                    {
                        bookingInstalment.Update(bookingInstalment.Percent, bookingInstalment.Amount, true, bookingInstalment.Order, bookingInstalment.PaymentDate);
                    }
                }
                else if (status.ToUpper().Equals(("Reject").ToUpper()))
                {
                    paymentDetails.Update(paymentDetails.AmountPaid, paymentDetails.PaymentDate, paymentDetails.PaymentID, paymentDetails.PaymentMode, paymentDetails.ChequeNo, paymentDetails.BankName, paymentDetails.BankBranch, paymentDetails.UTRNo, 3, false);
                }
                unitOfWork.SaveChanges();
                return GetByEventId(organizerId, paymentDetails.Event.Id, 5, 1);
            }
        }

        /// <summary>
        /// Edit Payment Details
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="paymentDetailsId"></param>
        /// <param name="bookingPaymentDTO"></param>
        /// <returns></returns>
        [Route("{paymentDetailsId:guid}/edit")]
        [ModelValidator]
        [ResponseType(typeof(BookingPaymentDTO))]
        public IHttpActionResult PutPaymentDetails(Guid organizerId, Guid paymentDetailsId, BookingPaymentDTO bookingPaymentDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                PaymentDetails paymentDetails = _paymentDetailsRepository.GetById(paymentDetailsId);
                if (bookingPaymentDTO == null)
                    return NotFound();

                paymentDetails.Update(bookingPaymentDTO.AmountPaid, Convert.ToDateTime(bookingPaymentDTO.PaymentDate), bookingPaymentDTO.PaymentID, Convert.ToInt16(bookingPaymentDTO.PaymentMode), bookingPaymentDTO.ChequeNo, bookingPaymentDTO.BankName, bookingPaymentDTO.BankBranch, bookingPaymentDTO.UTRNo, bookingPaymentDTO.PaymentStatus, false);

                unitOfWork.SaveChanges();
                return Ok("Updated Successfully");
            }
        }

        //private BookingPaymentDTO GetPaymentDTO(PaymentDetails bookingPayment)
        //{
        //    return new BookingPaymentDTO
        //    {
        //        Id = bookingPayment.Id,
        //        AmountPaid = bookingPayment.AmountPaid,
        //        EventName = bookingPayment.Event.Name,
        //        BankName = bookingPayment.BankName,
        //        BankBranch = bookingPayment.BankBranch,
        //        ChequeNo = bookingPayment.ChequeNo,
        //        ExhibitorName = bookingPayment.Exhibitor.Name,
        //        PaymentDate = bookingPayment.PaymentDate.ToString("dd-MMM-yyyy"),
        //        PaymentID = bookingPayment.PaymentID,
        //        PaymentMode = bookingPayment.PaymentMode,
        //        PaymentStatus = bookingPayment.PaymentStatus,
        //        IsPaymentApprove = bookingPayment.IsPaymentApprove,
        //        UTRNo = bookingPayment.UTRNo
        //    };
        //}

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
