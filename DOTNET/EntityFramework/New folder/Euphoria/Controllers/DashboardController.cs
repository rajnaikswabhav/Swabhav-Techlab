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
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/dashboard")]
    public class DashboardController : ApiController
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
        private IRepository<LayoutPlan> _layoutPlanRepository = new EntityFrameworkRepository<LayoutPlan>();
        private IRepository<Section> _sectionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();
        private IRepository<VisitorCardPayment> _visitorCardPaymentRepository = new EntityFrameworkRepository<VisitorCardPayment>();
        private IRepository<PaymentDetails> _bookingPaymentRepository = new EntityFrameworkRepository<PaymentDetails>();
        private VisitorEventTicketRepository<EventTicket> _visitorEventTicketIRepository = new VisitorEventTicketRepository<EventTicket>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<ExhibitorEnquiry> _exhibitorEnquiryRepository = new EntityFrameworkRepository<ExhibitorEnquiry>();
        private IRepository<EventLeadExhibitorMap> _eventLeadExhibitorMapRepository = new EntityFrameworkRepository<EventLeadExhibitorMap>();
        private IRepository<BookingRequestSalesPersonMap> _bookingRequestSalesPersonMapRepository = new EntityFrameworkRepository<BookingRequestSalesPersonMap>();
        private IRepository<BookingSalesPersonMap> _bookingSalesPersonMapRepository = new EntityFrameworkRepository<BookingSalesPersonMap>();
        private IRepository<VisitorFeedback> _visitorFeedbackRepository = new EntityFrameworkRepository<VisitorFeedback>();
        private IRepository<ExhibitorFeedback> _exhibitorFeedbackRepository = new EntityFrameworkRepository<ExhibitorFeedback>();
        private BookingRepository<BookingRequestSalesPersonMap> _bookingRequestSalesPersonMapBookingRepository = new BookingRepository<BookingRequestSalesPersonMap>();
        private BookingRepository<BookingSalesPersonMap> _bookingSalesPersonMapBookingRepository = new BookingRepository<BookingSalesPersonMap>();
        private BookingRepository<EventLeadExhibitorMap> _eventLeadExhibitorMapBookingRepository = new BookingRepository<EventLeadExhibitorMap>();
        private VisitorEventTicketRepository<EventTicket> _visitorEventTicketRepository = new VisitorEventTicketRepository<EventTicket>();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Main Dashboard of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}")]
        [ResponseType(typeof(List<DashboardDTO>))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                DashboardDTO dashboardDTO = new DashboardDTO();

                //task1
                Event eventDetail = _eventRepository.GetById(eventId);
                var exhibitorDetails = _exhibitorRepository.Count(new GetAllSpecification<Exhibitor>());
                var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);
                dashboardDTO.TotalExhibitor = exhibitorDetails;
                dashboardDTO.TotalLeads = 0;

                //Todo: check last child layout
                var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                if (layoutPlan != null)
                {
                    var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id };
                    var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                    var sections = _sectionRepository.Find(sectionSepcification);
                    var hangerList = sections.Where(x => x.SectionType.Name.ToUpper() == "HANGER").ToList();
                    dashboardDTO.TotalHangers = hangerList.Count();
                }
                else
                {
                    dashboardDTO.TotalHangers = 0;
                }

                var exhibitorEnquiryCriteria = new ExhibitorEnquirySearchCriteria { ExhibitorRegistrationType = "true" };
                var exhibitorEnquirySpecification = new ExhibitorEnquirySpecificationForSearch(exhibitorEnquiryCriteria);
                dashboardDTO.TotalExhibitorEnquiry = _exhibitorEnquiryRepository.Count(exhibitorEnquirySpecification);

                var criteria = new EventTicketReportSearchCriteria { StartDate = eventDetail.StartDate, EndDate = eventDetail.EndDate, IsMobile = false, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, PaymentCompleted = false };
                var sepcification = new EventTicketReportSpecificationForSearch(criteria);
                var eventTicketsForDistinctEmailId = _visitorEventTicketIRepository.CountOfVisitors(sepcification, e => e.Visitor.EmailId);

                var eventTicketsCriteria = new EventTicketReportSearchCriteria { StartDate = eventDetail.StartDate, EndDate = eventDetail.EndDate, IsMobile = false, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, PaymentCompleted = true };
                var eventTicketsSepcification = new EventTicketReportSpecificationForSearch(eventTicketsCriteria);
                var totalEventTickets = _visitorEventTicketRepository.SumOfTickets(eventTicketsSepcification);

                var bookingRequestPendingCriteria = new BookingRequestSearchCriteria { EventId = eventId, Status = "PENDING" };
                var pendingBookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequestPendingCriteria);
                var bookingRequestCancelCriteria = new BookingRequestSearchCriteria { EventId = eventId, Status = "CANCELLED" };
                var cancelBookingRequestSepcification = new BookingRequestSpecificationForSearch(bookingRequestCancelCriteria);

                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);

                var bookingPaymentApprovalsCriteria = new BookingPaymentSearchCriteria { EventId = eventId, IsPaymentCompleted = "False" };
                var bookingPaymentApprovalsSepcification = new BookingPaymentSpecificationForSearch(bookingPaymentApprovalsCriteria);

                var bookingApprovedPaymentsCriteria = new BookingPaymentSearchCriteria { EventId = eventId, IsPaymentCompleted = "True" };
                var bookingApprovedPaymentsSepcification = new BookingPaymentSpecificationForSearch(bookingApprovedPaymentsCriteria);

                var partnerSearchCriteria = new LoginSearchCriteria { Role = "Partner" };
                var partnerSpecification = new LoginSpecificationForSearch(partnerSearchCriteria);

                var visitorCardPaymentCriteria = new VisitorCardPaymentSearchCriteria { EventId = eventDetail.Id };
                var visitorCardPaymentSpecification = new VisitorCardPaymentSpecificationForSearch(visitorCardPaymentCriteria);

                dashboardDTO.TotalPartners = _loginRepository.Count(partnerSpecification);
                dashboardDTO.TotalPaymentsApprovals = _bookingPaymentRepository.Count(bookingPaymentApprovalsSepcification);
                dashboardDTO.TotalApprovedPayments = _bookingPaymentRepository.Count(bookingApprovedPaymentsSepcification);
                dashboardDTO.TotalStallBooking = _bookingRepository.Count(bookingSepcification);
                dashboardDTO.TotalBookingRequest = _bookingRequestRepository.Count(pendingBookingRequestSepcification);
                dashboardDTO.TotalCancellation = _bookingRequestRepository.Count(cancelBookingRequestSepcification);
                dashboardDTO.TotalVisitors = eventTicketsForDistinctEmailId;
                dashboardDTO.TotalVisitorComplaints = 0;
                dashboardDTO.TotalVisitorFeedback = 0;
                dashboardDTO.TotalCardPayments = _visitorCardPaymentRepository.Count(visitorCardPaymentSpecification);
                dashboardDTO.TotalTickets = totalEventTickets ?? 0;
                dashboardDTO.TotalExhibitorComplants = 0;
                dashboardDTO.TotalExhibitorFeedback = 0;
                return Ok(dashboardDTO);
            }
        }

        /// <summary>
        /// Get Account Dashboard
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/account")]
        [ResponseType(typeof(List<DashboardDTO>))]
        public IHttpActionResult GetAccountDashBoard(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                AccountDashboardDTO accountDashboardDTO = new AccountDashboardDTO();
                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                //var exhibitorDetails = _exhibitorRepository.Count(new GetAllSpecification<Exhibitor>());
                var bookingSearchCriteria = new BookingSearchCriteria { EventId = eventId };
                var bookingSepcification = new BookingSpecificationForSearch(bookingSearchCriteria);

                var bookingPaymentApprovalCriteria = new BookingPaymentSearchCriteria { EventId = eventId, IsPaymentCompleted = "false" };
                var bookingPaymentApprovalsSepcification = new BookingPaymentSpecificationForSearch(bookingPaymentApprovalCriteria);

                var bookingPaymentCriteria = new BookingPaymentSearchCriteria { EventId = eventId, IsPaymentCompleted = "True" };
                var bookingApprovedPaymentSepcification = new BookingPaymentSpecificationForSearch(bookingPaymentCriteria);
                accountDashboardDTO.TotalApprovedPayments = _bookingPaymentRepository.Count(bookingApprovedPaymentSepcification);
                accountDashboardDTO.TotalPaymentsApproval = _bookingPaymentRepository.Count(bookingPaymentApprovalsSepcification);
                accountDashboardDTO.TotalBookings = _bookingRepository.Count(bookingSepcification);

                return Ok(accountDashboardDTO);
            }
        }

        /// <summary>
        /// Get Sales Person Dashboard
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/salesPerson/{salesPersonId:guid}")]
        [ResponseType(typeof(List<DashboardDTO>))]
        public IHttpActionResult GetSalesPersonDahsboard(Guid organizerId, Guid eventId, Guid salesPersonId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                DashboardDTO dashboardDTO = new DashboardDTO();

                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                Login salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                if (salesPerson.Role.RoleName == "Partner")
                {
                    var bookingRequestSalesPersonMapCount = _eventLeadExhibitorMapBookingRepository.FindExhibitorListByEventLeadMapCount(salesPerson.Partner, eventDetail);
                    dashboardDTO.TotalExhibitor = bookingRequestSalesPersonMapCount;
                    dashboardDTO.TotalBookingRequest = _bookingRequestSalesPersonMapBookingRepository.FindBookingRequestOfReportingSalespersonsCount(salesPerson.Partner, eventDetail);
                    var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                    var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);
                    var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();
                    if (layoutPlan != null)
                    {
                        var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id };
                        var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                        var sections = _sectionRepository.Find(sectionSepcification);
                        var hangerList = sections.Where(x => x.SectionType.Name.ToUpper() == "HANGER").Count();
                        dashboardDTO.TotalHangers = hangerList;
                    }
                    var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetail.Id, PartnerId = salesPerson.Partner.Id };
                    var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                    dashboardDTO.TotalPayments = _bookingPaymentRepository.Count(paymentDetailsSepcification);

                    dashboardDTO.TotalStallBooking = _bookingSalesPersonMapBookingRepository.FindBookingOfReportingSalespersonsCount(salesPerson.Partner, eventDetail);
                    return Ok(dashboardDTO);
                }
                else
                {
                    var eventLeadExhibitorMapCriteria = new EventLeadExhibitorMapSearchCriteria { SalesPersonId = salesPerson.Id, EventId = eventDetail.Id };
                    var eventLeadExhibitorMapSepcification = new EventLeadExhibitorMapSpecificationForSearch(eventLeadExhibitorMapCriteria);
                    var eventLeadExhibitorMapCount = _eventLeadExhibitorMapRepository.Count(eventLeadExhibitorMapSepcification);

                    var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                    var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);
                    dashboardDTO.TotalExhibitor = eventLeadExhibitorMapCount;
                    dashboardDTO.TotalLeads = 0;

                    //Todo: check last child layout
                    var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                    if (layoutPlan != null)
                    {
                        var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id, Type = "HANGER" };
                        var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                        var hangerCount = _sectionRepository.Count(sectionSepcification);
                        dashboardDTO.TotalHangers = hangerCount;
                    }
                    var paymentDetailsSearchCriteria = new BookingPaymentSearchCriteria { EventId = eventDetail.Id, SalesPersonId = salesPerson.Id };
                    var paymentDetailsSepcification = new BookingPaymentSpecificationForSearch(paymentDetailsSearchCriteria);
                    dashboardDTO.TotalPayments = _bookingPaymentRepository.Count(paymentDetailsSepcification);

                    var bookingRequeSalesPeronMapSearchCriteria = new BookingRequestSalesPersonMapSearchCriteria { SalesPersonId = salesPersonId, Status = "PENDING" };
                    var bookingRequestSalesPersonMapSepcification = new BookingRequestSalesPersonMapSpecificationForSearch(bookingRequeSalesPeronMapSearchCriteria);
                    dashboardDTO.TotalBookingRequest = _bookingRequestSalesPersonMapRepository.Count(bookingRequestSalesPersonMapSepcification);

                    var bookingRequestSalesPersonMapCriteria = new BookingSalesPersonMapSearchCriteria { SalesPersonId = salesPerson.Id, EventId = eventDetail.Id };
                    var bookingRequestSalesPersonSepcification = new BookingSalesPersonMapSpecificationForSearch(bookingRequestSalesPersonMapCriteria);
                    dashboardDTO.TotalStallBooking = _bookingSalesPersonMapRepository.Count(bookingRequestSalesPersonSepcification);

                    return Ok(dashboardDTO);
                }
            }
        }

        /// <summary>
        /// Get Feedback Count
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/feedbackCount")]
        [ModelValidator]
        [ResponseType(typeof(VisitorCardPaymentDTO))]
        public IHttpActionResult GetFeedbackCount(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                List<ExhibitorReportDTO> feedbackReport = new List<ExhibitorReportDTO>();
                ExhibitorReportDTO singleVisitorFeedbackReportport = new ExhibitorReportDTO();

                var visitorFeedbackCreiteria = new VisitorFeedbackSearchCriteria { EventId = eventDetails.Id };
                var visitorFeedbackSpecification = new VisitorFeedbackSpecificationForSearch(visitorFeedbackCreiteria);
                var visitorFeedbackCount = _visitorFeedbackRepository.Count(visitorFeedbackSpecification);
                singleVisitorFeedbackReportport.Value = "Visitor";
                singleVisitorFeedbackReportport.TotalCount = visitorFeedbackCount;
                feedbackReport.Add(singleVisitorFeedbackReportport);
                var exhibitorFeedbackCreiteria = new ExhibitorFeedbackSearchCriteria { EventId = eventDetails.Id };
                var exhibitorFeedbackSpecification = new ExhibitorFeedbackSpecificationForSearch(exhibitorFeedbackCreiteria);
                var exhibitorFeedbackCount = _exhibitorFeedbackRepository.Count(exhibitorFeedbackSpecification);
                ExhibitorReportDTO singleExhibitorFeedbackReportport = new ExhibitorReportDTO();
                singleExhibitorFeedbackReportport.Value = "Exhibitor";
                singleExhibitorFeedbackReportport.TotalCount = exhibitorFeedbackCount;
                feedbackReport.Add(singleExhibitorFeedbackReportport);
                return Ok(feedbackReport);
            }
        }

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
