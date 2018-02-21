using Modules.EventManagement;
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

namespace Euphoria.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/Venues")]
    public class VenueController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Venue> _venueRepository = new EntityFrameworkRepository<Venue>();
        private IRepository<TicketType> _ticketTypeRepository = new EntityFrameworkRepository<TicketType>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<EventExhibitionMap> _eventExhibitionMapRepository = new EntityFrameworkRepository<EventExhibitionMap>();

        /// <summary>
        /// Get all Venue
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(VenueDTO[]))]
        public IHttpActionResult Get(Guid organizerId)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            var venues = _venueRepository.Find(new GetAllSpecification<Venue>()).OrderBy(x => x.Order);

            return Ok(venues.Select(x => GetDTO(x)));
        }

        /// <summary>
        /// Get Single Venue
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(VenueDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var venue = _venueRepository.GetById(id);
                if (venue == null || venue.Organizer.Id != organizerId)
                    return NotFound();

                VenueDTO venueDTO = GetDTO(venue);

                return Ok(venueDTO);
            }
        }

        /// <summary>
        /// Get TicketType of Venue
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="venueId"></param>
        /// <param name="exhibitionId"></param>
        /// <returns></returns>
        [Route("{venueId:guid}/exhibition/{exhibitionId:guid}/tickettypes")]
        [ResponseType(typeof(TicketTypeDTO[]))]
        public IHttpActionResult GetTicketType(Guid organizerId, Guid venueId, Guid exhibitionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var venue = _venueRepository.GetById(venueId);
                if (venue == null)
                    return NotFound();

                var exhibitionCriteria = new ExhibitionSearchCriteria { ExhibitionId = exhibitionId };
                var exhibitionSepcification = new ExhibitionSpecificationForSearch(exhibitionCriteria);
                var exhibitions = _exhibitionRepository.Find(exhibitionSepcification);

                var AllAvailableListOfDate = getAllAvailableDatesForBooking(exhibitions.FirstOrDefault().StartDate, exhibitions.FirstOrDefault().EndDate);
                var AllWeeekendListOfDate = getAllWeekendDatesForBooking(exhibitions.FirstOrDefault().StartDate, exhibitions.FirstOrDefault().EndDate);

                var criteria = new TicketTypeSearchCriteria { VenueId = venueId.ToString() };
                var sepcification = new TicketTypeSpecificationForSearch(criteria);
                var ticketTypes = _ticketTypeRepository.Find(sepcification);
                if (ticketTypes == null)
                    return NotFound();

                List<TicketDateDTO> listOfDates = new List<TicketDateDTO>();
                foreach (TicketType ticketType in ticketTypes)
                {
                    TicketDateDTO dateDto = new TicketDateDTO();
                    PriceDetailDTO priceDTO = new PriceDetailDTO();
                    dateDto.PriceDetails = priceDTO;

                    if (ticketType.Name.ToUpper().Equals("SINGLE"))
                    {
                        dateDto.Id = ticketType.Id;
                        dateDto.Type = TypeOfTicket.Single.ToString();
                        dateDto.AllAvailableListOfDate = AllAvailableListOfDate.Select(x => x.ToString("dd-MMM-yyyy")).ToList();

                        dateDto.PriceDetails.BusinessHrs = ticketType.BusinessHrs;
                        dateDto.PriceDetails.BusinessHrsDiscount = ticketType.BusinessHrsDiscount;
                        dateDto.PriceDetails.NonBusinessHrs = ticketType.NonBusinessHrs;
                        dateDto.PriceDetails.NonBusinessHrsDiscount = ticketType.NonBusinessHrsDiscount;
                        dateDto.DisplayOrder = 1;
                    }
                    else if (ticketType.Name.ToUpper().Equals("WEEKEND"))
                    {
                        dateDto.Id = ticketType.Id;
                        dateDto.Type = TypeOfTicket.Weekend.ToString();
                        dateDto.AllAvailableListOfDate = AllWeeekendListOfDate.Select(x => x.ToString("dd-MMM-yyyy")).ToList();

                        dateDto.PriceDetails.BusinessHrs = ticketType.BusinessHrs;
                        dateDto.PriceDetails.BusinessHrsDiscount = ticketType.BusinessHrsDiscount;
                        dateDto.PriceDetails.NonBusinessHrs = ticketType.NonBusinessHrs;
                        dateDto.PriceDetails.NonBusinessHrsDiscount = ticketType.NonBusinessHrsDiscount;
                        dateDto.DisplayOrder = 2;
                    }
                    else
                    {
                        dateDto.Id = ticketType.Id;
                        dateDto.Type = TypeOfTicket.AllDays.ToString();
                        dateDto.AllAvailableListOfDate = new List<string>();
                        dateDto.PriceDetails.BusinessHrs = ticketType.BusinessHrs;
                        dateDto.PriceDetails.BusinessHrsDiscount = ticketType.BusinessHrsDiscount;
                        dateDto.PriceDetails.NonBusinessHrs = ticketType.NonBusinessHrs;
                        dateDto.PriceDetails.NonBusinessHrsDiscount = ticketType.NonBusinessHrsDiscount;
                        dateDto.DisplayOrder = 3;
                    }

                    listOfDates.Add(dateDto);
                }
                //  var ticketlist = ticketType.Select(x => GetDTO(x));
                return Ok(listOfDates.OrderBy(x => x.DisplayOrder));
            }
        }

        /// <summary>
        /// Get Exhibitions of Venue
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [Route("{venueId:guid}/exhibitions")]
        [ResponseType(typeof(TicketTypeDTO[]))]
        public IHttpActionResult GetExhibitions(Guid organizerId, Guid venueId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var venue = _venueRepository.GetById(venueId);
                if (venue == null)
                    return NotFound();

                //ToDO: Add ticketing active
                var eventCriteria = new EventSearchCriteria { VenueId = venueId };
                var eventSepcification = new EventSpecificationForSearch(eventCriteria);
                var eventDetails = _eventRepository.Find(eventSepcification).Where(x => x.isActive == true);

                List<ExhibitionByVenueDTO> exhibitionDTOMap = new List<ExhibitionByVenueDTO>();
                foreach (Event singleEvent in eventDetails)
                {
                    var eventExhibitioncriteria = new EventExhibitionMapSearchCriteria { EventId = singleEvent.Id };
                    var eventExhibiotnSepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitioncriteria);
                    var eventExhibitionMapList = _eventExhibitionMapRepository.Find(eventExhibiotnSepcification).Where(x => x.Exhibition.isActive == true);

                    List<ExhibitionDTO> exhibitionList = new List<ExhibitionDTO>();
                    foreach (EventExhibitionMap singleEventExhibition in eventExhibitionMapList)

                    {
                        Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibition.Exhibition.Id);

                        exhibitionList.Add(GetDTO_EX(exhibition, venueId, singleEvent));
                    }

                    exhibitionDTOMap.Add(GetDTO_ListEX(singleEvent.StartDate.ToString("dd-MMM-yyyy"), exhibitionList, singleEvent.Id, singleEvent));
                }
                return Ok(exhibitionDTOMap);
            }
        }

        private static ExhibitionByVenueDTO GetDTO_ListEX(string date, List<ExhibitionDTO> exhibition, Guid EventId, Event eventDetails)
        {
            return new ExhibitionByVenueDTO
            {
                Date = date,
                Address = eventDetails.Address,
                EventId = EventId,
                Exhibition = exhibition
            };
        }
        private static ExhibitionDTO GetDTO_EX(Exhibition exhibition, Guid venueId, Event eventDetails)
        {
            return new ExhibitionDTO
            {
                Id = exhibition.Id,
                Name = exhibition.Name,
                Description = exhibition.Description,
                Address = eventDetails.Address,
                EndDate = eventDetails.EndDate.ToString("dd-MMM-yyyy"),
                StartDate = eventDetails.StartDate.ToString("dd-MMM-yyyy"),
                isActive = eventDetails.isActive,
                BgImage = exhibition.BgImage,
                Logo = exhibition.Logo,
                Latitude = exhibition.Latitude,
                Longitude = exhibition.Longitude,
                VenueId = venueId
            };
        }

        private static ExhibitionDTO GetDTO(Exhibition exhibition)
        {
            return new ExhibitionDTO
            {
                Id = exhibition.Id,
                isActive = exhibition.isActive,
                Description = exhibition.Description,
                EndDate = exhibition.EndDate.ToString("dd-MMM-yyyy"),
                Name = exhibition.Name,
                StartDate = exhibition.StartDate.ToString("dd-MMM-yyyy"),
                BgImage = exhibition.BgImage,
                Logo = exhibition.Logo
            };
        }

        private TicketTypeDTO GetDTO(TicketType ticketType)
        {
            return new TicketTypeDTO
            {
                Id = ticketType.Id,
                Type = ticketType.Name,
                //Price = ticketType.Price
            };

        }

        private static VenueDTO GetDTO(Venue venue)
        {
            return new VenueDTO
            {
                Id = venue.Id,
                City = venue.City
                //State = venue.State,
                //Address = venue.Address
            };
        }

        private static List<DateTime> getAllAvailableDatesForBooking(DateTime startDateOfExhibition, DateTime endDateOfExhibition)
        {
            List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

            DateTime currentValidStartDate;

            //This is used for sending only live dates comented 
            //if (startDateOfExhibition <= DateTime.UtcNow)
            //    currentValidStartDate = DateTime.UtcNow;
            //else
            currentValidStartDate = startDateOfExhibition.AddDays(1);

            for (DateTime date = currentValidStartDate; date <= endDateOfExhibition; date = date.AddDays(1))
            {
                allAvailableDatesForBooking.Add(date);
            }

            return (allAvailableDatesForBooking);
        }

        private static List<DateTime> getAllWeekendDatesForBooking(DateTime startDateOfExhibition, DateTime endDateOfExhibition)
        {
            List<DateTime> allAvailableWeekendsForbooking = new List<DateTime>();

            DateTime currentValidStartDate;

            if (startDateOfExhibition <= DateTime.UtcNow)
                currentValidStartDate = DateTime.UtcNow;
            else
                currentValidStartDate = startDateOfExhibition;

            for (DateTime date = currentValidStartDate; date <= endDateOfExhibition; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    allAvailableWeekendsForbooking.Add(date);
                }
            }
            return (allAvailableWeekendsForbooking);
        }
    }
}
