using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Specification;

namespace Euphoria.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/v1/organizers/{organizerId}/exhibitions")]
    public class ExhibitionsController : ApiController
    {
        private readonly IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private readonly IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private readonly IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();

        /// <summary>
        /// Get All Exhibition
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(ExhibitionHomeDTO[]))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitions = organizer.Exhibitions.Where(x => x.isActive == true);
                List<ExhibitionHomeDTO> exhibitonsHomeDTO = new List<ExhibitionHomeDTO>();
                foreach (Exhibition exhibition in exhibitions)
                {
                    ExhibitionHomeDTO singleExhibition = new ExhibitionHomeDTO()
                    {
                        Id = exhibition.Id,
                        Name = exhibition.Name,
                        Description = exhibition.Description,
                        EndDate = exhibition.EndDate.ToString("dd-MMM-yyyy"),
                        StartDate = exhibition.StartDate.ToString("dd-MMM-yyyy"),
                        isActive = exhibition.isActive,
                        BannerImage = exhibition.BannerImage,
                        Bannertext = exhibition.Bannertext,
                        Logo = exhibition.Logo,
                        BgImage = exhibition.BgImage,
                        Latitude = exhibition.Latitude,
                        Longitude = exhibition.Longitude,
                        VenueId = exhibition.Venue.Id,
                        VenueName = exhibition.Venue.City
                    };
                    exhibitonsHomeDTO.Add(singleExhibition);
                }
                return Ok(exhibitonsHomeDTO);
            }
        }

        /// <summary>
        /// Get Single Exhibition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(ExhibitionDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var exhibition = _exhibitionRepository.GetById(id);
                if (exhibition == null || exhibition.Organizer.Id != organizerId)
                    return NotFound();
                Guid venueId = exhibition.Venue.Id;
                ExhibitionDTO exhibitionDTO = GetDTO_SingleEX(exhibition, venueId);
                return Ok(exhibitionDTO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exhibitionId"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("{exhibitionId:guid}/country")]
        [ResponseType(typeof(CountryDTO[]))]
        public IHttpActionResult GetCountry(Guid organizerId, Guid exhibitionId)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var exhibition = _exhibitionRepository.GetById(exhibitionId);
                if (exhibition == null || exhibition.Organizer.Id != organizerId)
                    return NotFound();

                var criteria = new CountrySearchCriteria { ExhibitionId = exhibitionId.ToString() };
                var sepcification = new CountrySpecificationForSearch(criteria);
                var countries = _countryRepository.Find(sepcification);

                if (countries == null)
                    return NotFound();

                return Ok(countries.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exhibitionId"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("{exhibitionId:guid}/State")]
        [ResponseType(typeof(StateDTO[]))]
        public IHttpActionResult GetState(Guid organizerId, Guid exhibitionId)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var exhibition = _exhibitionRepository.GetById(exhibitionId);
                if (exhibition == null || exhibition.Organizer.Id != organizerId)
                    return NotFound();

                var criteria = new StateSearchCriteria { ExhibitionId = exhibitionId.ToString() };
                var sepcification = new StateSpecificationForSearch(criteria);
                var states = _stateRepository.Find(sepcification);

                if (states == null)
                    return NotFound();

                return Ok(states.Select(x => GetDTO(x)));
            }
        }

        private static ExhibitionHomeDTO GetDTO(Exhibition exhibition)
        {
            return new ExhibitionHomeDTO
            {
                Id = exhibition.Id,
                Name = exhibition.Name,
                Description = exhibition.Description,
                EndDate = exhibition.EndDate.ToString("dd-MMM-yyyy"),
                StartDate = exhibition.StartDate.ToString("dd-MMM-yyyy"),
                isActive = exhibition.isActive,
                BannerImage = exhibition.BannerImage,
                Bannertext = exhibition.Bannertext,
                Logo = exhibition.Logo,
                BgImage = exhibition.BgImage,
                Latitude = exhibition.Latitude,
                Longitude = exhibition.Longitude,
            };
        }

        private static ExhibitionDTO GetDTO_SingleEX(Exhibition exhibition, Guid VenueId)
        {
            return new ExhibitionDTO
            {
                Id = exhibition.Id,
                Name = exhibition.Name,
                Address = exhibition.Address,
                Description = exhibition.Description,
                EndDate = exhibition.EndDate.ToString("dd-MMM-yyyy"),
                StartDate = exhibition.StartDate.ToString("dd-MMM-yyyy"),
                isActive = exhibition.isActive,
                BgImage = exhibition.BgImage,
                Logo = exhibition.Logo,
                Latitude = exhibition.Latitude,
                Longitude = exhibition.Longitude,
                VenueId = exhibition.Venue.Id
            };
        }

        private static ExhibitionByVenueDTO GetDTO_ListEX(string date, List<ExhibitionDTO> exhibition)
        {
            return new ExhibitionByVenueDTO
            {
                Date = date,
                Address = exhibition.FirstOrDefault().Address,
                Exhibition = exhibition
            };
        }

        private static ExhibitionDTO GetDTO_EX(Exhibition exhibition)
        {
            return new ExhibitionDTO
            {
                Id = exhibition.Id,
                Name = exhibition.Name,
                Description = exhibition.Description,
                Address = exhibition.Address,
                EndDate = exhibition.EndDate.ToString("dd-MMM-yyyy"),
                StartDate = exhibition.StartDate.ToString("dd-MMM-yyyy"),
                isActive = exhibition.isActive,
                BgImage = exhibition.BgImage,
                Logo = exhibition.Logo
            };
        }

        private CountryDTO GetDTO(Country country)
        {
            return new CountryDTO
            {
                Name = country.Name
            };
        }

        private CountryDTO GetDTO(State state)
        {
            return new CountryDTO
            {
                Name = state.Name
            };
        }
    }
}
