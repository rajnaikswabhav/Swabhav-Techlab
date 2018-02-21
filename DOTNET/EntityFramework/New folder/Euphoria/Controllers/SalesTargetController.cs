using Modules.EventManagement;
using Modules.SecurityManagement;
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
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.LayoutManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/salesTarget")]
    public class SalesTargetController : ApiController
    {
        private IRepository<SalesTarget> _salesTargetRepository = new EntityFrameworkRepository<SalesTarget>();
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private SalesTargetRepository<SalesTarget> _salesTargetCountRepository = new SalesTargetRepository<SalesTarget>();
        private BookingRepository<StallBooking> _stallBookingCountRepository = new BookingRepository<StallBooking>();

        /// <summary>
        /// Get all Sales Target for Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}")]
        [ResponseType(typeof(CategoryDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var salesTargetSearchCriteria = new SalesTargetSearchCriteria { EventId = eventDetails.Id };
                var salesTargetSearchSpecification = new SalesTargetSpecificationForSearch(salesTargetSearchCriteria);
                var salesTargets = _salesTargetRepository.Find(salesTargetSearchSpecification);
                List<SalesTargetListingDTO> salesTargetsDTO = new List<SalesTargetListingDTO>();
                foreach (SalesTarget singleSalesTarget in salesTargets)
                {
                    SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                    salesTargetDTO.Id = singleSalesTarget.Id;
                    if (singleSalesTarget.ExhibitorIndustryType != null)
                    {
                        salesTargetDTO.ExhibitorIndustryType = singleSalesTarget.ExhibitorIndustryType.IndustryType;
                    }
                    if (singleSalesTarget.ExhibitorType != null)
                    {
                        salesTargetDTO.ExhibitorType = singleSalesTarget.ExhibitorType.Type;
                    }
                    if (singleSalesTarget.State != null)
                    {
                        salesTargetDTO.State = singleSalesTarget.State.Name;
                    }
                    if (singleSalesTarget.Country != null)
                    {
                        salesTargetDTO.Country = singleSalesTarget.Country.Name;
                    }
                    salesTargetDTO.Target = singleSalesTarget.Target;
                    salesTargetDTO.TargetAchived = 0;
                    salesTargetsDTO.Add(salesTargetDTO);
                }
                return Ok(salesTargetsDTO);
            }
        }

        /// <summary>
        /// Get Target Details by Type
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="typeOfTarget"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/type/{typeOfTarget}")]
        [ResponseType(typeof(CategoryDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, string typeOfTarget)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                List<SalesTargetListingDTO> salesTargetDTOList = new List<SalesTargetListingDTO>();
                if (typeOfTarget.ToUpper() == "ExhibitorIndustryType".ToUpper())
                {
                    var salesTargetList = _salesTargetCountRepository.SalesTargetByExhibitorIndustryType();
                    foreach (var salesTargetIndustryType in salesTargetList)
                    {
                        var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(new Guid(salesTargetIndustryType.Key));
                        SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                        var stallCount = _stallBookingCountRepository.countOfBookedStallsByExhibitorIndustry(new Guid(salesTargetIndustryType.Key), eventDetails.Id);
                        salesTargetDTO.Id = new Guid(salesTargetIndustryType.Key);
                        salesTargetDTO.ExhibitorIndustryType = exhibitorIndustryType.IndustryType;
                        salesTargetDTO.Target = salesTargetIndustryType.Value;
                        salesTargetDTO.TargetAchived = stallCount;
                        salesTargetDTOList.Add(salesTargetDTO);
                    }
                }
                else if (typeOfTarget.ToUpper() == "ExhibitorType".ToUpper())
                {
                    //var salesTargetList = _salesTargetCountRepository.SalesTargetByExhibitorIndustryType();

                    //foreach (var singleSalesTarget in salesTargetList)
                    //{
                    //    SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();

                    //    if (singleSalesTarget.ExhibitorType != null)
                    //    {
                    //        salesTargetDTO.ExhibitorType = singleSalesTarget.ExhibitorType.Type;
                    //    }

                    //    salesTargetDTO.Target = singleSalesTarget.Target;
                    //    salesTargetDTO.TargetAchived = 0;
                    //    salesTargetsDTO.Add(salesTargetDTO);
                    //}
                }
                else if (typeOfTarget.ToUpper() == "SalesPerson".ToUpper())
                {
                    var salesTargets = _salesTargetCountRepository.SalesTargetBySalesPerson();
                    foreach (var singleSalesTarget in salesTargets)
                    {
                        SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                        var salesPerson = _loginRepository.GetById(new Guid(singleSalesTarget.Key));
                        var stallCount = _stallBookingCountRepository.countOfBookedStallsBySalesPerson(new Guid(singleSalesTarget.Key), eventDetails.Id);
                        salesTargetDTO.Id = new Guid(singleSalesTarget.Key);
                        salesTargetDTO.SalesPerson = salesPerson.UserName;
                        salesTargetDTO.Target = singleSalesTarget.Value;
                        salesTargetDTO.TargetAchived = stallCount;
                        salesTargetDTOList.Add(salesTargetDTO);
                    }
                }
                else if (typeOfTarget.ToUpper() == "Partner".ToUpper())
                {
                    var salesTargets = _salesTargetCountRepository.SalesTargetByPartner();
                    foreach (var singleSalesTarget in salesTargets)
                    {
                        SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                        var salesPerson = _loginRepository.GetById(new Guid(singleSalesTarget.Key));
                        var stallCount = _stallBookingCountRepository.countOfBookedStallsBySalesPerson(new Guid(singleSalesTarget.Key), eventDetails.Id);
                        salesTargetDTO.Id = new Guid(singleSalesTarget.Key);
                        salesTargetDTO.SalesPerson = salesPerson.UserName;
                        salesTargetDTO.Target = singleSalesTarget.Value;
                        salesTargetDTO.TargetAchived = stallCount;
                        salesTargetDTOList.Add(salesTargetDTO);
                    }
                }
                else if (typeOfTarget.ToUpper() == "Country".ToUpper())
                {
                    var salesTargets = _salesTargetCountRepository.SalesTargetByCountry();
                    foreach (var singleSalesTarget in salesTargets)
                    {
                        SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                        var country = _countryRepository.GetById(new Guid(singleSalesTarget.Key));
                        var stallCount = _stallBookingCountRepository.countOfBookedStallsByCountry(new Guid(singleSalesTarget.Key), eventDetails.Id);
                        salesTargetDTO.Id = new Guid(singleSalesTarget.Key);
                        salesTargetDTO.Country = country.Name;
                        salesTargetDTO.Target = singleSalesTarget.Value;
                        salesTargetDTO.TargetAchived = stallCount;
                        salesTargetDTOList.Add(salesTargetDTO);
                    }
                }
                else if (typeOfTarget.ToUpper() == "State".ToUpper())
                {
                    var salesTargets = _salesTargetCountRepository.SalesTargetByState();
                    foreach (var singleSalesTarget in salesTargets)
                    {
                        SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                        var state = _stateRepository.GetById(new Guid(singleSalesTarget.Key));
                        var stallCount = _stallBookingCountRepository.countOfBookedStallsByState(new Guid(singleSalesTarget.Key), eventDetails.Id);
                        salesTargetDTO.Id = new Guid(singleSalesTarget.Key);
                        salesTargetDTO.State = state.Name;
                        salesTargetDTO.Target = singleSalesTarget.Value;
                        salesTargetDTO.TargetAchived = stallCount;
                        salesTargetDTOList.Add(salesTargetDTO);
                    }
                }
                return Ok(salesTargetDTOList);
            }
        }

        /// <summary>
        /// Get Target Details by Sales Person 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesPersonId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/salesPerson/{salesPersonId:guid}")]
        [ResponseType(typeof(CategoryDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId, Guid salesPersonId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var salesPerson = _loginRepository.GetById(salesPersonId);
                if (salesPerson == null)
                    return NotFound();

                var salesTargetSearchCriteria = new SalesTargetSearchCriteria { EventId = eventDetails.Id, SalesPersonId = salesPerson.Id };
                var salesTargetSearchSpecification = new SalesTargetSpecificationForSearch(salesTargetSearchCriteria);
                var salesTargets = _salesTargetRepository.Find(salesTargetSearchSpecification);
                List<SalesTargetListingDTO> salesTargetsDTO = new List<SalesTargetListingDTO>();
                foreach (SalesTarget singleSalesTarget in salesTargets)
                {
                    SalesTargetListingDTO salesTargetDTO = new SalesTargetListingDTO();
                    if (singleSalesTarget.ExhibitorIndustryType != null)
                    {
                        salesTargetDTO.ExhibitorIndustryType = singleSalesTarget.ExhibitorIndustryType.IndustryType;
                    }
                    if (singleSalesTarget.ExhibitorType != null)
                    {
                        salesTargetDTO.ExhibitorType = singleSalesTarget.ExhibitorType.Type;
                    }
                    if (singleSalesTarget.State != null)
                    {
                        salesTargetDTO.State = singleSalesTarget.State.Name;
                    }
                    if (singleSalesTarget.Country != null)
                    {
                        salesTargetDTO.Country = singleSalesTarget.Country.Name;
                    }
                    salesTargetDTO.Target = singleSalesTarget.Target;

                    salesTargetsDTO.Add(salesTargetDTO);
                }
                return Ok(salesTargetsDTO);
            }
        }

        /// <summary>
        /// Add Target Data
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesTargetDTOList"></param>
        /// <returns></returns>
        [Route("{eventId:guid}")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, Guid eventId, List<SalesTargetDTO> salesTargetDTOList)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {

                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                ExhibitorIndustryType exhibitorIndustryType = new ExhibitorIndustryType();
                ExhibitorType exhibitorType = new ExhibitorType();
                Login salesPerson = new Login();
                State state = new State();
                Country country = new Country();
                foreach (SalesTargetDTO salesTargetDTO in salesTargetDTOList)
                {
                    var salesTarget = SalesTarget.Create(salesTargetDTO.Target);
                    if (salesTargetDTO.SalesPersonId != Guid.Empty)
                    {
                        salesPerson = _loginRepository.GetById(salesTargetDTO.SalesPersonId);
                        if (salesPerson == null)
                            return NotFound();
                        salesTarget.Login = salesPerson;
                    }
                    if (salesTargetDTO.ExhibitorIndustryTypeId != Guid.Empty)
                    {
                        exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(salesTargetDTO.ExhibitorIndustryTypeId);
                        if (exhibitorIndustryType == null)
                            return NotFound();
                        salesTarget.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                    if (salesTargetDTO.ExhibitorTypeId != Guid.Empty)
                    {
                        exhibitorType = _exhibitorTypeRepository.GetById(salesTargetDTO.ExhibitorTypeId);
                        if (exhibitorType == null)
                            return NotFound();
                        salesTarget.ExhibitorType = exhibitorType;
                    }
                    if (salesTargetDTO.StateId != Guid.Empty)
                    {
                        state = _stateRepository.GetById(salesTargetDTO.StateId);
                        if (state == null)
                            return NotFound();
                        salesTarget.State = state;
                    }
                    if (salesTargetDTO.CountryId != Guid.Empty)
                    {
                        country = _countryRepository.GetById(salesTargetDTO.CountryId);
                        if (country == null)
                            return NotFound();
                        salesTarget.Country = country;
                    }
                    salesTarget.Event = eventDetails;

                    _salesTargetRepository.Add(salesTarget);
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, eventDetails.Id);
            }
        }

        /// <summary>
        /// Edit Target Details
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="salesTargetId"></param>
        /// <param name="salesTargetDTO"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/salesTarget/{salesTargetId:guid}")]
        [ModelValidator]
        public IHttpActionResult Put(Guid organizerId, Guid eventId, Guid salesTargetId, SalesTargetDTO salesTargetDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var salesTarget = _salesTargetRepository.GetById(salesTargetId);
                if (salesTarget == null)
                    return NotFound();

                ExhibitorIndustryType exhibitorIndustryType = new ExhibitorIndustryType();
                ExhibitorType exhibitorType = new ExhibitorType();
                Login salesPerson = new Login();
                State state = new State();
                Country country = new Country();
                if (salesTargetDTO.Target != 0)
                {
                    salesTarget.Target = salesTargetDTO.Target;
                }

                if (salesTargetDTO.SalesPersonId != Guid.Empty)
                {
                    salesPerson = _loginRepository.GetById(salesTargetDTO.SalesPersonId);
                    if (salesPerson == null)
                        return NotFound();
                    salesTarget.Login = salesPerson;
                }

                if (salesTargetDTO.ExhibitorIndustryTypeId != Guid.Empty)
                {
                    exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(salesTargetDTO.ExhibitorIndustryTypeId);
                    if (exhibitorIndustryType == null)
                        return NotFound();
                    salesTarget.ExhibitorIndustryType = exhibitorIndustryType;
                }

                if (salesTargetDTO.ExhibitorTypeId != Guid.Empty)
                {
                    exhibitorType = _exhibitorTypeRepository.GetById(salesTargetDTO.ExhibitorTypeId);
                    if (exhibitorType == null)
                        return NotFound();
                    salesTarget.ExhibitorType = exhibitorType;
                }

                if (salesTargetDTO.StateId != Guid.Empty)
                {
                    state = _stateRepository.GetById(salesTargetDTO.StateId);
                    if (state == null)
                        return NotFound();
                    salesTarget.State = state;
                }

                if (salesTargetDTO.CountryId != Guid.Empty)
                {
                    country = _countryRepository.GetById(salesTargetDTO.CountryId);
                    if (country == null)
                        return NotFound();
                    salesTarget.Country = country;
                }

                unitOfWork.SaveChanges();
                return Get(organizerId, salesTarget.Event.Id);
            }
        }
    }
}
