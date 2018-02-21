using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/exhibitions/{exhibitionId:guid}/pavilions")]
    public class PavilionController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Pavilion> _pavilionRepository = new EntityFrameworkRepository<Pavilion>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitionId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PavilionDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid exhibitionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizerId == null)
                    return NotFound();

                var exhibition = _exhibitionRepository.GetById(exhibitionId);
                if (exhibition == null || exhibition.Organizer.Id != organizerId)
                    return NotFound();

                PavilionSearchCriteria pavilionCriteria = new PavilionSearchCriteria { ExhibitionId = exhibition.Id.ToString() };
                PavilionSpecificationForSearch pavilionSepcification = new PavilionSpecificationForSearch(pavilionCriteria);
                var pavilions = _pavilionRepository.Find(pavilionSepcification);

                return Ok(pavilions.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitionId"></param>
        /// <param name="pavilionId"></param>
        /// <returns></returns>
        [Route("{pavilionId:guid}/stalls")]
        [ResponseType(typeof(StallDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid exhibitionId, Guid pavilionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizerId == null)
                    return NotFound();

                var exhibition = _exhibitionRepository.GetById(exhibitionId);
                if (exhibition == null || exhibition.Organizer.Id != organizerId)
                    return NotFound();

                var pavilion = _pavilionRepository.GetById(pavilionId);
                if (pavilion == null)
                    return NotFound();

                var stalls = pavilion.Stalls;

                return Ok(stalls.Select(x => GetStallDTO(x)));
            }
        }

        private StallDTO GetStallDTO(Stall stall)
        {
            return new StallDTO
            {
                StallNo = stall.StallNo
            };
        }

        //private PavilionDTO GetPavilionDTO(Pavilion pavilion, Exhibition exhibition)
        //{
        //    PavilionDTO pavilionDTO = new PavilionDTO
        //    {
        //        Name = pavilion.Name,
        //        Exhibition = new ExhibitionDTO
        //        {
        //            Id = exhibition.Id,
        //            Name = exhibition.Name,
        //            Description = exhibition.Description,
        //            isActive = exhibition.isActive,
        //            StartDate = exhibition.StartDate,
        //            EndDate = exhibition.EndDate
        //        },
        //    };
        //    return pavilionDTO;
        //}

        private PavilionDTO GetDTO(Pavilion pavilion)
        {
            return new PavilionDTO
            {
                Name = pavilion.Name
            };
        }
    }
}
