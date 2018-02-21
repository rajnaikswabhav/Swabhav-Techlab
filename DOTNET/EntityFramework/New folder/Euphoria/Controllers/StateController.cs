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
    [RoutePrefix("api/v1/organizers/{organizerId}/states")]
    public class StateController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();

        /// <summary>
        /// Get all States
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<StateDTO>))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var states = _stateRepository.Find(new GetAllSpecification<State>());
                if (states == null)
                    return NotFound();

                return Ok(states.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Add State 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="stateDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, StateDTO stateDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var state = State.Create(stateDTO.Name); // DTO to entity mapping

                _stateRepository.Add(state);
                unitOfWork.SaveChanges();
                return Get(organizerId);
            }
        }

        /// <summary>
        /// Edit State
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="stateDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult put(Guid organizerId, StateDTO stateDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var state = _stateRepository.GetById(stateDTO.Id);
                if (state == null)
                    return NotFound();

                state.Update(state.Name, stateDTO.Color); // DTO to entity mapping
                unitOfWork.SaveChanges();
                return Get(organizerId);
            }
        }

        private static StateDTO GetDTO(State state)
        {
            return new StateDTO
            {
                Id = state.Id,
                Name = state.Name,
            };
        }
    }
}
