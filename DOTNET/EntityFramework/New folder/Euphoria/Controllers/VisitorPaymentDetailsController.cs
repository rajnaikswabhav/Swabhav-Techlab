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
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/visitorsPaymentDetails")]
    public class VisitorPaymentDetailsController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private IRepository<Section> _sectionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<VisitorPaymentDetails> _visitorPaymentDetailsRepository = new EntityFrameworkRepository<VisitorPaymentDetails>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("listByVisitor/{visitorId:guid}/event/{eventId:guid}")]
        [ResponseType(typeof(ExhibitorDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid visitorId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
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

                var visitorPaymentDetailsSearchCriteria = new VisitorPaymentDetailsSearchCriteria { EventId = eventId };
                var visitorPaymentDetailsSpecificationForSearch = new VisitorPaymentDetailsSpecificationForSearch(visitorPaymentDetailsSearchCriteria);
                var visitorPaymentDetails = _visitorPaymentDetailsRepository.Find(visitorPaymentDetailsSpecificationForSearch).OrderBy(x=>x.CreatedOn);

                if (visitorPaymentDetails.Count() == 0)
                    return NotFound();

                return Ok(visitorPaymentDetails.Select(x => GetVisitorPaymentDetailsDTO(x)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="eventId"></param>
        /// <param name="visitorPaymentDetailsDTO"></param>
        /// <returns></returns>
        [Route("{visitorId:guid}/event/{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(VisitorPaymentDetailsDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid visitorId, Guid eventId, VisitorPaymentDetailsDTO visitorPaymentDetailsDTO)
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

                var exhibitorDetails = _exhibitorRepository.GetById(visitorPaymentDetailsDTO.ExhibitorId);
                if (exhibitorDetails == null)
                    return NotFound();

                var sectionDetails = _sectionRepository.GetById(visitorPaymentDetailsDTO.PavilionId);
                if (sectionDetails == null)
                    return NotFound();

                VisitorPaymentDetails visitorPaymentDetails = VisitorPaymentDetails.Create(visitorPaymentDetailsDTO.Amount, visitorPaymentDetailsDTO.FilePath, Convert.ToDateTime(visitorPaymentDetailsDTO.PurchaseDate), visitorPaymentDetailsDTO.PaymentModeValue, visitorPaymentDetailsDTO.PointsEarned, false);
                visitorPaymentDetails.Event = eventDetails;
                visitorPaymentDetails.Visitor = visitorDetails;
                visitorPaymentDetails.Exhibitor = exhibitorDetails;
                visitorPaymentDetails.Section = sectionDetails;

                _visitorPaymentDetailsRepository.Add(visitorPaymentDetails);
                unitOfWork.SaveChanges();
                return Ok();
            }
        }

        private VisitorPaymentDetailsDTO GetVisitorPaymentDetailsDTO(VisitorPaymentDetails visitorPaymentDetails)
        {
            return new VisitorPaymentDetailsDTO
            {
                Id = visitorPaymentDetails.Id,
                VisitorName = visitorPaymentDetails.Visitor.FirstName + visitorPaymentDetails.Visitor.LastName,
                EventName = visitorPaymentDetails.Event.Name,
                ExhibitorName = visitorPaymentDetails.Exhibitor.Name,
                PavilionName = visitorPaymentDetails.Section.Name,
                Amount = visitorPaymentDetails.Amount,
                PointsEarned = visitorPaymentDetails.PointsEarned,
                FilePath=visitorPaymentDetails.FileName,
                PurchaseDate=visitorPaymentDetails.PurchaseDate.ToString("dd-MMM-yyyy")                
            };
        }
    }
}
