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
    [RoutePrefix("api/v1/organizers/{organizerId}/visitorsAnalytics")]
    public class VisitorAnalyticsController : ApiController
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
        private VisitorEventTicketRepository<EventTicket> _visitorEventTicketRepository = new VisitorEventTicketRepository<EventTicket>();
        private readonly IRepository<DiscountCoupon> _discountCouponRepository = new EntityFrameworkRepository<DiscountCoupon>();
        private readonly IRepository<DiscountType> _discountTypeRepository = new EntityFrameworkRepository<DiscountType>();
        private readonly IRepository<VisitorDiscountCouponMap> _visitorDiscountCouponMapRepository = new EntityFrameworkRepository<VisitorDiscountCouponMap>();

        
    }
}
