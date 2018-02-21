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
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/exhibitor/{organizerId:guid}/tickettypes")]
    public class TicketTypeController : ApiController
    {
        private IRepository<TicketType> _ticketTypeRepository = new EntityFrameworkRepository<TicketType>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(TicketTypeDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            var ticketType = _ticketTypeRepository.GetById(id);
            if (ticketType == null)
                return NotFound();

            var ticketTypeDTO = new TicketType()
            {
                Name = ticketType.Name,
                BusinessHrs = ticketType.BusinessHrs,
                NonBusinessHrs=ticketType.NonBusinessHrs,
                BusinessHrsDiscount=ticketType.BusinessHrsDiscount,
                NonBusinessHrsDiscount=ticketType.NonBusinessHrsDiscount,
                Description=ticketType.Description
            };
            return Ok(ticketTypeDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, TicketTypeDTO ticketType)
        {
            var ticketTypeEntity = TicketType.Create(ticketType.Type, ticketType.PriceDetails.businessHrs,ticketType.PriceDetails.nonBusinessHrs, ticketType.Description);
            _ticketTypeRepository.Add(ticketTypeEntity);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid organizerId, Guid id, TicketTypeDTO ticketType)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var entityToUpdate = _ticketTypeRepository.GetById(id);
                if (entityToUpdate == null)
                    return NotFound();

                entityToUpdate.Update(ticketType.Type, ticketType.PriceDetails.businessHrs,ticketType.PriceDetails.nonBusinessHrs, ticketType.Description);

                unitOfWork.SaveChanges();
            }
            return Get(organizerId, id);
        }
    }
}
