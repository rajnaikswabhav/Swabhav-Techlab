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
    [RoutePrefix("api/v1/organizers/{organizerId}/country")]
    public class CountryController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();

        /// <summary>
        /// Get all Country
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(List<CountryDTO>))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var states = _countryRepository.Find(new GetAllSpecification<Country>());
                if (states == null)
                    return NotFound();

                return Ok(states.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Add Country 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="countryDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, CountryDTO countryDTO)
        {
            var country = Country.Create(countryDTO.Name); // DTO to entity mapping
            _countryRepository.Add(country);
            return Get(organizerId);
        }

        /// <summary>
        /// Edit Country 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="countryDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult put(Guid organizerId, CountryDTO countryDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var country = _countryRepository.GetById(countryDTO.Id);
                country.Update(country.Name, countryDTO.Color); // DTO to entity mapping
                unitOfWork.SaveChanges();
                return Get(organizerId);
            }
        }

        private static CountryDTO GetDTO(Country country)
        {
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
            };
        }
    }
}
