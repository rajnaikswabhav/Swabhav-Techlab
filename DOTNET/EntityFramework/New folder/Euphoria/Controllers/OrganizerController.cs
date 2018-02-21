using System;
using System.Linq;
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
    [RoutePrefix("api/v1/organizers")]
    public class OrganizerController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();

        public OrganizerController()
        {

        }

        public OrganizerController(IRepository<Organizer> organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(OrganizerDTO))]
        public IHttpActionResult Get(Guid id)
        {
            var organizer = _organizerRepository.GetById(id);
            if (organizer == null)
                return NotFound();

            var organizerDTO = new OrganizerDTO() { Id = organizer.Id, Name = organizer.Name, Description = organizer.Description, TenantId = organizer.TenantId };
            return Ok(organizerDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(OrganizerDTO))]
        public IHttpActionResult Get()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.Find(new GetAllSpecification<Organizer>());
                if (organizer == null)
                    return NotFound();

                return Ok(organizer.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizer"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(OrganizerDTO organizer)
        {
            var orgnizer = Organizer.Create(organizer.TenantId, organizer.Name, organizer.Description); // DTO to entity mapping

            _organizerRepository.Add(orgnizer);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizer"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(OrganizerDTO))]
        public IHttpActionResult Put(Guid id, OrganizerDTO organizer)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizerToUpdate = _organizerRepository.GetById(id);
                if (organizerToUpdate == null)
                    return NotFound();

                organizerToUpdate.Update(organizer.Name, organizer.Description);

                unitOfWork.SaveChanges();
            }
            return Get(id);
        }

        private static OrganizerDTO GetDTO(Organizer organizer)
        {
            return new OrganizerDTO
            {
                Id = organizer.Id,
                Name = organizer.Name,
                Description = organizer.Description,
                TenantId = organizer.TenantId
            };
        }
    }
}
