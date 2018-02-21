
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
    [RoutePrefix("api/v1/organizers/{organizerId}/event")]
    public class EventController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<Venue> _venueRepository = new EntityFrameworkRepository<Venue>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<EventExhibitionMap> _eventExhibitionMapRepository = new EntityFrameworkRepository<EventExhibitionMap>();
        private IRepository<EventTicketType> _eventTicketTypeRepository = new EntityFrameworkRepository<EventTicketType>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        private IRepository<LayoutPlan> _layoutPlanRepository = new EntityFrameworkRepository<LayoutPlan>();
        private IRepository<Section> _sectionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();

        /// <summary>
        /// Get Active Event List 
        /// </summary>
        /// <param name="OrganizerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [Route("{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(OrganizerDTO))]
        public IHttpActionResult GetActiveEventList(Guid OrganizerId, int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(OrganizerId);
                if (organizer == null)
                    return NotFound();

                var eventCriteria = new EventSearchCriteria { IsActive = true };
                var eventSepcification = new EventSpecificationForSearch(eventCriteria);
                var eventCount = _eventRepository.Count(eventSepcification);
                var activeEventList = _eventRepository.Find(eventSepcification)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize);

                var totalCount = eventCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (organizer == null)
                    return NotFound();

                List<ActiveEventDTO> activeEventDTOList = new List<ActiveEventDTO>();

                foreach (Event singleEvent in activeEventList)
                {
                    ActiveEventDTO activeEventDTO = new ActiveEventDTO
                    {
                        EventId = singleEvent.Id,
                        EventName = singleEvent.Name,
                        Address = singleEvent.Address,
                        City = singleEvent.Venue.City,
                        StartDate = singleEvent.StartDate.ToString("dd-MMM-yyyy"),
                        EndDate = singleEvent.EndDate.ToString("dd-MMM-yyyy")
                    };
                    activeEventDTOList.Add(activeEventDTO);
                }

                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfBookingDTO = activeEventDTOList
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// Get Exhibition List of Active Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("exhibitionList")]
        [ResponseType(typeof(ExhibitionHomeDTO[]))]
        public IHttpActionResult Get(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventList = _eventRepository.Find(new GetAllSpecification<Event>()).Where(x => x.isActive == true);
                if (eventList.Count() == 0)
                    return NotFound();

                List<ExhibitionHomeDTO> exhibitionHomeDTOList = new List<ExhibitionHomeDTO>();
                foreach (Event singleEvent in eventList)
                {
                    var eventExhibitioncriteria = new EventExhibitionMapSearchCriteria { EventId = singleEvent.Id };
                    var eventExhibiotnSepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitioncriteria);
                    var eventExhibitionMapList = _eventExhibitionMapRepository.Find(eventExhibiotnSepcification);

                    foreach (EventExhibitionMap singleEventExhibition in eventExhibitionMapList)
                    {
                        Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibition.Exhibition.Id);
                        ExhibitionHomeDTO singleExhibitionHomeDTO = new ExhibitionHomeDTO
                        {
                            Id = exhibition.Id,
                            Name = exhibition.Name,
                            Description = exhibition.Description,
                            EndDate = singleEvent.EndDate.ToString("dd-MMM-yyyy"),
                            StartDate = singleEvent.StartDate.ToString("dd-MMM-yyyy"),
                            isActive = singleEvent.isActive,
                            BannerImage = exhibition.BannerImage,
                            Bannertext = exhibition.Bannertext,
                            Logo = exhibition.Logo,
                            BgImage = exhibition.BgImage,
                            Latitude = singleEvent.Latitude,
                            Longitude = singleEvent.Longitude,
                            VenueId = singleEvent.Venue.Id,
                            VenueName = singleEvent.Venue.City,
                            EventId = singleEvent.Id
                        };
                        exhibitionHomeDTOList.Add(singleExhibitionHomeDTO);
                    }
                }
                return Ok(exhibitionHomeDTOList);
            }
        }

        /// <summary>
        /// Get single Active Event 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("activeEvent")]
        [ResponseType(typeof(EventDTO[]))]
        public IHttpActionResult GetAllActiveEvent(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventCriteria = new EventSearchCriteria { IsActive = true };
                var eventSepcification = new EventSpecificationForSearch(eventCriteria);
                var eventDetails = _eventRepository.Find(eventSepcification).OrderBy(x => x.StartDate).FirstOrDefault();

                EventDTO singleEventDTO = new EventDTO()
                {
                    Id = eventDetails.Id,
                    Name = eventDetails.Name,
                    Address = eventDetails.Address,
                    StartDate = eventDetails.StartDate.ToString("dd-MMM-yyyy"),
                    EndDate = eventDetails.EndDate.ToString("dd-MMM-yyyy"),
                    BookingStartDate = eventDetails.BookingStartDate.ToString("dd-MMM-yyyy"),
                    EventTime = eventDetails.Time,
                    isActive = eventDetails.isActive,
                    VenueId = eventDetails.Venue.Id.ToString()
                };

                VenueDTO venueDTO = new VenueDTO()
                {
                    Id = eventDetails.Venue.Id,
                    City = eventDetails.Venue.City
                };
                singleEventDTO.VenueDTO = venueDTO;
                return Ok(singleEventDTO);
            }
        }

        /// <summary>
        /// Get Ticketing Active Exhibiton List
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId}/activeTicketingExhibitions")]
        [ResponseType(typeof(TicketTypeDTO[]))]
        public IHttpActionResult GetActiveTicketingExhibitions(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                //ToDO: Add ticketing active

                List<ExhibitionByVenueDTO> exhibitionDTOMap = new List<ExhibitionByVenueDTO>();
                if (eventDetails.IsTicketingActive == true)
                {
                    var eventExhibitioncriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetails.Id };
                    var eventExhibiotnSepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitioncriteria);
                    var eventExhibitionMapList = _eventExhibitionMapRepository.Find(eventExhibiotnSepcification).Where(x => x.Exhibition.isActive == true);

                    List<ExhibitionDTO> exhibitionList = new List<ExhibitionDTO>();
                    foreach (EventExhibitionMap singleEventExhibition in eventExhibitionMapList)

                    {
                        Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibition.Exhibition.Id);

                        exhibitionList.Add(GetDTO_EX(exhibition, eventDetails));
                    }
                    exhibitionDTOMap.Add(GetDTO_ListEX(eventDetails.StartDate.ToString("dd-MMM-yyyy"), exhibitionList, eventDetails.Id, eventDetails));
                }
                return Ok(exhibitionDTOMap);
            }
        }

        /// <summary>
        /// List of Visitors in that event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [Route("{eventId}/eventVisitorsList/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(TicketTypeDTO[]))]
        public IHttpActionResult GetAllVisitorsForEvent(Guid organizerId, Guid eventId, int pageSize, int pageNumber, string startDate = null, string endDate = null)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                List<EventTicket> eventTicketingList = new List<EventTicket>();
                var eventTicketsCount = 0;
                DateTime startDateForSearch = DateTime.MinValue;
                DateTime endDateForSearch = DateTime.MinValue;
                if (!string.IsNullOrEmpty(startDate))
                {
                    startDateForSearch = Convert.ToDateTime(startDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    endDateForSearch = Convert.ToDateTime(endDate);
                }
                if (startDateForSearch != DateTime.MinValue && endDateForSearch != DateTime.MinValue)
                {
                    var eventTicketCriteria = new EventTicketReportSearchCriteria { StartDateForSearch = startDateForSearch, EndDateForSearch = endDateForSearch, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, IsMobile = false, PaymentCompleted = true };
                    var eventTicketSepcification = new EventTicketReportSpecificationForSearch(eventTicketCriteria);
                    eventTicketsCount = _eventTicketRepository.Count(eventTicketSepcification);
                    eventTicketingList = _eventTicketRepository.Find(eventTicketSepcification).OrderByDescending(x => x.Visitor.CreatedOn)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
                }
                else
                {
                    var eventTicketCriteria = new EventTicketReportSearchCriteria { StartDate = eventDetails.StartDate, EndDate = eventDetails.EndDate, IsPayatLocation = false, IsPayOnline = false, IsWeb = false, IsMobile = false, PaymentCompleted = true };
                    var eventTicketSepcification = new EventTicketReportSpecificationForSearch(eventTicketCriteria);
                    eventTicketsCount = _eventTicketRepository.Count(eventTicketSepcification);
                    eventTicketingList = _eventTicketRepository.Find(eventTicketSepcification).OrderByDescending(x => x.Visitor.CreatedOn)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
                }

                var totalCount = eventTicketsCount;
                var totalPages = Math.Ceiling((double)totalCount / pageSize);

                if (eventTicketsCount == 0)
                    return NotFound();

                List<VisitorDTO> visitorDetailsList = new List<VisitorDTO>();
                foreach (EventTicket singleEventTicket in eventTicketingList)
                {
                    VisitorDTO visitorDTO = GetDTO(singleEventTicket.Visitor);
                    visitorDetailsList.Add(visitorDTO);
                }
                var result = new
                {
                    totalCount = totalCount,
                    totalPages = totalPages,
                    listOfVisitorsDTO = visitorDetailsList
                };
                return Ok(result);
            }
        }

        //ToDo: Need to change API After Updateing app.
        /// <summary>
        /// Get Event By Venue
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [Route("getByVenue/{venueId:guid}")]
        [ResponseType(typeof(EventDTO[]))]
        public IHttpActionResult GetAllEventByVenue(Guid organizerId, Guid venueId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var venue = _venueRepository.GetById(venueId);
                if (venue == null)
                    return NotFound();

                var eventCriteria = new EventSearchCriteria { VenueId = venueId };
                var eventSepcification = new EventSpecificationForSearch(eventCriteria);
                var events = _eventRepository.Find(eventSepcification);

                List<EventDTO> listOfEvents = new List<EventDTO>();
                foreach (Event singleEvent in events)
                {
                    EventDTO singleEventDTO = new EventDTO()
                    {
                        Id = singleEvent.Id,
                        Name = singleEvent.Name,
                        Address = singleEvent.Address,
                        StartDate = singleEvent.StartDate.ToString("dd-MMM-yyyy"),
                        EndDate = singleEvent.EndDate.ToString("dd-MMM-yyyy"),
                        BookingStartDate = singleEvent.BookingStartDate.ToString("dd-MMM-yyyy"),
                        EventTime = singleEvent.Time,
                        isActive = singleEvent.isActive
                    };
                    listOfEvents.Add(singleEventDTO);
                }
                return Ok(listOfEvents);
            }
        }

        // Need to change API After Updateing app.
        /// <summary>
        /// Get Active Event By Venue
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [Route("venue/{venueId:guid}")]
        [ResponseType(typeof(EventDTO[]))]
        public IHttpActionResult GetByVenue(Guid organizerId, Guid venueId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var venue = _venueRepository.GetById(venueId);
                if (venue == null)
                    return NotFound();

                var eventCriteria = new EventSearchCriteria { VenueId = venueId, IsTicketingActive = true };
                var eventSepcification = new EventSpecificationForSearch(eventCriteria);
                var events = _eventRepository.Find(eventSepcification);
                if (events.Count() != 0)
                {
                    List<EventDTO> listOfEvents = new List<EventDTO>();
                    foreach (Event singleEvent in events)
                    {
                        EventDTO singleEventDTO = new EventDTO()
                        {
                            Id = singleEvent.Id,
                            Name = singleEvent.Name,
                            Address = singleEvent.Address,
                            StartDate = singleEvent.StartDate.ToString(),
                            EndDate = singleEvent.EndDate.ToString(),
                            BookingStartDate = singleEvent.BookingStartDate.ToString(),
                            EventTime = singleEvent.Time,
                            isActive = singleEvent.isActive
                        };
                        var eventExhibitioncriteria = new EventExhibitionMapSearchCriteria { EventId = singleEvent.Id };
                        var eventExhibiotnSepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitioncriteria);
                        var eventExhibitionMapList = _eventExhibitionMapRepository.Find(eventExhibiotnSepcification);

                        foreach (EventExhibitionMap singleEventExhibition in eventExhibitionMapList)
                        {
                            Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibition.Exhibition.Id);
                            singleEventDTO.EventExhibitions.Add(new EventExhibitionDTO
                            {
                                ExhibitionId = exhibition.Id,
                                ExhibitionName = exhibition.Name,
                                StartDate = singleEventExhibition.StartDate.ToString(),
                                EndDate = singleEventExhibition.EndDate.ToString()
                            });
                        }
                        listOfEvents.Add(singleEventDTO);
                    }
                    return Ok(listOfEvents);
                }
                else
                    return NotFound();
            }
        }

        /// <summary>
        /// Get Single Event Details 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}")]
        [ResponseType(typeof(EventDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                EventDTO singleEventDTO = new EventDTO()
                {
                    Id = eventDetail.Id,
                    Name = eventDetail.Name,
                    Address = eventDetail.Address,
                    StartDate = eventDetail.StartDate.ToString(),
                    EndDate = eventDetail.EndDate.ToString(),
                    BookingStartDate = eventDetail.BookingStartDate.ToString(),
                    EventTime = eventDetail.Time,
                    isActive = eventDetail.isActive
                };

                var eventExhibitioncriteria = new EventExhibitionMapSearchCriteria { EventId = eventDetail.Id };
                var eventExhibiotnSepcification = new EventExhibitionMapSpecificationForSearch(eventExhibitioncriteria);
                var eventExhibitionMapList = _eventExhibitionMapRepository.Find(eventExhibiotnSepcification);

                foreach (EventExhibitionMap singleEventExhibition in eventExhibitionMapList)
                {
                    Exhibition exhibition = _exhibitionRepository.GetById(singleEventExhibition.Exhibition.Id);
                    singleEventDTO.EventExhibitions.Add(new EventExhibitionDTO
                    {
                        ExhibitionId = exhibition.Id,
                        ExhibitionName = exhibition.Name,
                        StartDate = singleEventExhibition.StartDate.ToString(),
                        EndDate = singleEventExhibition.EndDate.ToString()
                    });
                }

                var eventTicketTypecriteria = new EventTicketTypeSearchCriteria { EventId = eventDetail.Id };
                var eventTicketTypeSepcification = new EventTicketTypeSpecificationForSearch(eventTicketTypecriteria);
                var eventTicketTypeList = _eventTicketTypeRepository.Find(eventTicketTypeSepcification);

                foreach (EventTicketType singleTicketType in eventTicketTypeList)
                {
                    singleEventDTO.EventTicketType.Add(new EventTicketTypeDTO
                    {
                        Id = singleTicketType.Id,
                        Type = singleTicketType.Name,
                        businessHrs = singleTicketType.BusinessHrs,
                        nonBusinessHrs = singleTicketType.NonBusinessHrs,
                        businessHrsDiscount = singleTicketType.BusinessHrsDiscount,
                        nonBusinessHrsDiscount = singleTicketType.NonBusinessHrsDiscount,
                        Description = singleTicketType.Description
                    });
                }
                return Ok(singleEventDTO);
            }
        }

        /// <summary>
        /// Get Ticket Types of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/eventTicketTypes")]
        [ResponseType(typeof(TicketTypeDTO))]
        public IHttpActionResult GetEventTicketTypes(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var eventTicketTypecriteria = new EventTicketTypeSearchCriteria { EventId = eventDetails.Id };
                var eventTicketTypeSepcification = new EventTicketTypeSpecificationForSearch(eventTicketTypecriteria);
                var eventTicketTypeList = _eventTicketTypeRepository.Find(eventTicketTypeSepcification);

                var AllAvailableListOfDate = getAllAvailableDatesForBooking(eventDetails.StartDate, eventDetails.EndDate);
                var AllWeeekendListOfDate = getAllWeekendDatesForBooking(eventDetails.StartDate, eventDetails.EndDate);

                List<TicketDateDTO> listOfDates = new List<TicketDateDTO>();
                foreach (EventTicketType ticketType in eventTicketTypeList)
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

                return Ok(listOfDates.OrderBy(x => x.DisplayOrder));
            }
        }

        /// <summary>
        /// Get All Countries of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/country")]
        [ResponseType(typeof(CountryDTO[]))]
        public IHttpActionResult GetCountry(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var countries = _countryRepository.Find(new GetAllSpecification<Country>());
                if (countries == null)
                    return NotFound();

                return Ok(countries.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Get All States of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/State")]
        [ResponseType(typeof(StateDTO[]))]
        public IHttpActionResult GetState(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                var states = _stateRepository.Find(new GetAllSpecification<State>());
                if (states == null)
                    return NotFound();

                return Ok(states.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Get All Pavilion list of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/Pavilion")]
        [ResponseType(typeof(StateDTO[]))]
        public IHttpActionResult GetPavilion(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();
                var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);

                //Todo: check last child layout
                var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                if (layoutPlan == null)
                    return NotFound();

                var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id, Type = "HANGER" };
                var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                var sections = _sectionRepository.Find(sectionSepcification).OrderBy(x => x.Name);

                //var sectionList = sections.Where(x => x.SectionType.Name.ToUpper().Equals("HANGER"));

                return Ok(sections.Select(x => GetDTO(x)));
            }
        }

        /// <summary>
        /// Add Event 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        [ResponseType(typeof(EventDTO))]
        public IHttpActionResult Post(Guid organizerId, EventDTO eventDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event createEvent = Event.Create(eventDTO.Name, eventDTO.Address, Convert.ToDateTime(eventDTO.StartDate), Convert.ToDateTime(eventDTO.EndDate), Convert.ToDateTime(eventDTO.BookingStartDate), eventDTO.isActive, eventDTO.EventTime, false);
                var venue = _venueRepository.GetById(new Guid(eventDTO.VenueId));
                createEvent.CreatedBy = new Guid(eventDTO.UpdatedById);
                createEvent.Venue = venue;
                _eventRepository.Add(createEvent);

                foreach (EventExhibitionDTO exhibition in eventDTO.EventExhibitions)
                {
                    var exhibitions = _exhibitionRepository.GetById(exhibition.ExhibitionId);
                    EventExhibitionMap eventExhibitionMap = EventExhibitionMap.Create(Convert.ToDateTime(exhibition.StartDate),
                        Convert.ToDateTime(exhibition.EndDate));
                    eventExhibitionMap.Event = createEvent;
                    eventExhibitionMap.Exhibition = exhibitions;
                    eventExhibitionMap.CreatedBy = new Guid(eventDTO.UpdatedById);
                    _eventExhibitionMapRepository.Add(eventExhibitionMap);
                }

                foreach (EventTicketTypeDTO eventTicketType in eventDTO.EventTicketType)
                {
                    EventTicketType eventTicketTypes = EventTicketType.Create(eventTicketType.Type, eventTicketType.businessHrs,
                        eventTicketType.nonBusinessHrs, eventTicketType.businessHrsDiscount,
                        eventTicketType.nonBusinessHrsDiscount, eventTicketType.Description);
                    eventTicketTypes.Event = createEvent;
                    eventTicketTypes.CreatedBy = new Guid(eventDTO.UpdatedById);
                    _eventTicketTypeRepository.Add(eventTicketTypes);
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, createEvent.Id);
            }
        }

        /// <summary>
        /// Edit Event Status
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/active")]
        [ModelValidator]
        public IHttpActionResult Put(Guid organizerId, Guid eventId, ActivateEventDTO Active)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var events = _eventRepository.GetById(eventId);
                if (events != null)
                {
                    events.isActive = Active.Active;
                    _eventRepository.Add(events);
                    unitOfWork.SaveChanges();
                    return Ok();
                }
                else
                    return NotFound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventTicketTypeId"></param>
        /// <param name="ticketType"></param>
        /// <returns></returns>
        [Route("eventTicketType")]
        [ModelValidator]
        public IHttpActionResult Put(Guid organizerId, Guid eventTicketTypeId, EventTicketTypeDTO ticketType)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventTicketType = _eventTicketTypeRepository.GetById(eventTicketTypeId);

                if (eventTicketType == null)
                    return NotFound();

                eventTicketType.Id = ticketType.Id;
                eventTicketType.Name = ticketType.Type;
                eventTicketType.BusinessHrs = ticketType.businessHrs;
                eventTicketType.BusinessHrsDiscount = ticketType.businessHrsDiscount;
                eventTicketType.NonBusinessHrs = ticketType.nonBusinessHrs;
                eventTicketType.NonBusinessHrsDiscount = ticketType.nonBusinessHrsDiscount;
                eventTicketType.Description = ticketType.Description;

                unitOfWork.SaveChanges();
                return Get(organizerId, eventTicketType.Event.Id);
            }
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

        private CountryDTO GetDTO(Country country)
        {
            return new CountryDTO
            {
                Name = country.Name
            };
        }
        private ActiveEventDTO GetDTO(Event activeEvent)
        {
            return new ActiveEventDTO
            {
                EventId = activeEvent.Id,
                EventName = activeEvent.Name,
                Address = activeEvent.Address,
                City = activeEvent.Venue.City,
                StartDate = activeEvent.StartDate.ToString("dd-MMM-yyyy"),
                EndDate = activeEvent.EndDate.ToString("dd-MMM-yyyy")
            };
        }
        private PavilionDTO GetDTO(Section section)
        {
            return new PavilionDTO
            {
                Id = section.Id,
                Name = section.Name
            };
        }
        private StateDTO GetDTO(State state)
        {
            return new StateDTO
            {
                Name = state.Name
            };
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
        private static ExhibitionDTO GetDTO_EX(Exhibition exhibition, Event eventDetails)
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
                Latitude = eventDetails.Latitude,
                Longitude = eventDetails.Longitude,
                VenueId = eventDetails.Venue.Id
            };
        }

        private static VisitorDTO GetDTO(Visitor visitor)
        {
            VisitorDTO visitorDTO = new VisitorDTO();

            visitorDTO.Id = visitor.Id;
            visitorDTO.FirstName = visitor.FirstName;
            visitorDTO.LastName = visitor.LastName;
            visitorDTO.Address = visitor.Address;
            visitorDTO.Pincode = visitor.Pincode;
            visitorDTO.MobileNo = visitor.MobileNo;
            visitorDTO.EmailId = visitor.EmailId;
            if (visitor.DateOfBirth != null)
            {
                visitorDTO.DateOfBirth = visitor.DateOfBirth.Value.ToString("dd-MMM-yyyy");
            }
            visitorDTO.DateOfBirth = visitor.DateOfBirth.ToString();
            visitorDTO.Gender = visitor.Gender;
            visitorDTO.Education = visitor.Education;
            visitorDTO.Income = visitor.Income;
            visitorDTO.IsMobileVerified = visitor.isMobileNoVerified;

            return (visitorDTO);
        }
    }
}
