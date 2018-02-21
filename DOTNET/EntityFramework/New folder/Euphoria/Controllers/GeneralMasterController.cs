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
    [RoutePrefix("api/v1/organizers/{organizerId}/general")]
    public class GeneralMasterController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<GeneralMaster> _generalMasterRepository = new EntityFrameworkRepository<GeneralMaster>();

        /// <summary>
        /// Get GeneralMaster Data By type
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="generalTableType"></param>
        /// <returns></returns>
        [Route("{generalTableType}")]
        [ResponseType(typeof(List<GeneralMasterDTO>))]
        public IHttpActionResult Get(Guid organizerId, string generalTableType)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var generalMasterSearchCriteria = new GeneralMasterSearchCriteria { GeneralTableType = generalTableType };
                var generalMasterSepcification = new GeneralMasterSpecificationForSearch(generalMasterSearchCriteria);
                var generalMasterTable = _generalMasterRepository.Find(generalMasterSepcification).OrderBy(x=>x.Key);
                if (generalMasterTable.Count() == 0)
                    return NotFound();
                List<GeneralTableDTO> generalMasterTableDTO = new List<GeneralTableDTO>();

                foreach (GeneralMaster singleGeneralmaster in generalMasterTable)
                {
                    GeneralTableDTO singleGeneralTableDTO = new GeneralTableDTO
                    {
                        Key = singleGeneralmaster.Key,
                        Value = singleGeneralmaster.Value
                    };
                    generalMasterTableDTO.Add(singleGeneralTableDTO);
                }
                return Ok(generalMasterTableDTO);
            }
        }

        /// <summary>
        /// Add GeneralMaster Data
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="generalMasterDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        [ResponseType(typeof(List<GeneralMasterDTO>))]
        public IHttpActionResult Post(Guid organizerId, List<GeneralMasterDTO> generalMasterDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                foreach (GeneralMasterDTO singleGeneralMasterDTO in generalMasterDTO)
                {
                    GeneralMaster createGeneralMaster = GeneralMaster.Create(singleGeneralMasterDTO.Type, singleGeneralMasterDTO.Value, singleGeneralMasterDTO.Key, singleGeneralMasterDTO.Active);
                    _generalMasterRepository.Add(createGeneralMaster);
                }

                unitOfWork.SaveChanges();

                return Get(organizerId, generalMasterDTO.FirstOrDefault().Type);
            }
        }
    }
}
