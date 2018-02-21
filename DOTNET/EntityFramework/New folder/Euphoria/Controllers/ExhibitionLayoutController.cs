using Modules.EventManagement;
using Modules.LayoutManagement;
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
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/eventLayout")]
    public class ExhibitionLayoutController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<LayoutPlan> _layoutPlanRepository = new EntityFrameworkRepository<LayoutPlan>();
        private IRepository<Section> _sectionRepository = new EntityFrameworkRepository<Section>();
        private IRepository<SectionType> _sectionTypeRepository = new EntityFrameworkRepository<SectionType>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Access> _accessRepository = new EntityFrameworkRepository<Access>();
        private IRepository<Stall> _stallRepository = new EntityFrameworkRepository<Stall>();
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<Barrier> _barrierRepository = new EntityFrameworkRepository<Barrier>();
        private IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<State> _stateRepository = new EntityFrameworkRepository<State>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(ExhibitionLayoutDTO))]
        public IHttpActionResult Get(Guid organizerId)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var sectionsList = _sectionRepository.Find(new GetAllSpecification<Section>());

                // main exhition and exhibtion DTO

                var exhibitionSection = sectionsList.Where(x => x.Name.ToUpper().Equals("EXHIBITION"))
                                                   .SingleOrDefault();
                var exhibitionDTO = GetSectionDTO(exhibitionSection);

                // access for exhibition and exhibition dto
                var accessForExhibition = exhibitionSection.Accesses.ToList();
                var accessForExhibitionDTO = accessForExhibition.Select(x => GetAccessDTO(x)).ToList();

                //add acess to exhibitionDTO
                exhibitionDTO.AccessList = accessForExhibitionDTO;

                //hangers and hangerDTO
                var hangers = exhibitionSection.Sections.ToList();
                var hangersDTO = hangers.Select(x => GetSectionDTO(x)).ToList();

                // stalls and stallsDTO for every hanger
                for (int hangerIndex = 0; hangerIndex < hangers.ToArray().Length; hangerIndex++)
                {
                    var stalls = hangers[hangerIndex].Stalls.ToList();
                    var stallsDTO = stalls.Select(x => GetStallDTO(x)).ToList();

                    //access for hanger and hangerDTO
                    var accessForHanger = hangers[hangerIndex].Accesses.ToList();
                    var accessForHangerDTO = accessForHanger.Select(x => GetAccessDTO(x)).ToList();

                    //add stallsDTO to hangersDTO
                    hangersDTO[hangerIndex].StallList = stallsDTO;

                    //add access to hangerDTO
                    hangersDTO[hangerIndex].AccessList = accessForHangerDTO;
                    //TODO: access for stall and stallDTO 
                }

                // add hangers to exhibition
                exhibitionDTO.SectionsList = hangersDTO;
                //add stalls to hangers
                return Ok(new
                {
                    Version = "1.0",
                    Section = exhibitionDTO
                });
            }
        }

        /// <summary>
        /// Get All Sections of Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/sections")]
        [ResponseType(typeof(SectionsLayoutDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);

                //Todo: check last child layout
                var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                if (layoutPlan == null)
                    return NotFound();

                var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id };
                var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                var sections = _sectionRepository.Find(sectionSepcification);

                SectionsLayoutDTO layoutDTO = new SectionsLayoutDTO
                {
                    Version_No = layoutPlan.VersionNo
                };
                var ExhibitionList = sections.Where(x => x.SectionType.Name.ToUpper().Equals("EXHIBITION"));

                if (ExhibitionList.Count() == 0)
                    return NotFound();

                foreach (Section exhibition in ExhibitionList)
                {
                    SectionsDTO exhibitionDTO = new SectionsDTO
                    {
                        Id = exhibition.Id,
                        Name = exhibition.Name,
                        Height = exhibition.Height,
                        Width = exhibition.Width,
                        X_Coordinate = exhibition.X_Coordinate,
                        Y_Coordinate = exhibition.Y_Coordinate,
                        //ExhibitionId=exhibition.Exhibition.Id,
                        SectionTypeId = exhibition.SectionType.Id
                    };
                    foreach (Access access in exhibition.Accesses)
                    {
                        AccesssDTO parentAccess = new AccesssDTO
                        {
                            IsEntry = access.IsEntry,
                            IsExit = access.IsExit,
                            IsEmergencyExit = access.IsEmergencyExit,
                            Height = access.Height,
                            Width = access.Width,
                            X_Coordinate = access.X_Coordinate,
                            Y_Coordinate = access.Y_Coordinate,
                            //ExhibitionId=exhibition.Exhibition.Id,
                        };
                        exhibitionDTO.AccessList.Add(parentAccess);
                    }
                    var hangers = exhibition.Sections;
                    foreach (Section hanger in hangers)
                    {
                        SectionsDTO singleHangerDTO = new SectionsDTO
                        {
                            Id = hanger.Id,
                            Name = hanger.Name,
                            Height = hanger.Height,
                            Width = hanger.Width,
                            X_Coordinate = hanger.X_Coordinate,
                            Y_Coordinate = hanger.Y_Coordinate,
                            ExhibitionId = hanger.Exhibition.Id,
                            SectionTypeId = hanger.SectionType.Id
                        };
                        foreach (Access access in hanger.Accesses)
                        {
                            AccesssDTO parentAccess = new AccesssDTO
                            {
                                IsEntry = access.IsEntry,
                                IsExit = access.IsExit,
                                IsEmergencyExit = access.IsEmergencyExit,
                                Height = access.Height,
                                Width = access.Width,
                                X_Coordinate = access.X_Coordinate,
                                Y_Coordinate = access.Y_Coordinate,
                                //ExhibitionId=exhibition.Exhibition.Id,
                            };
                            singleHangerDTO.AccessList.Add(parentAccess);
                        }
                        exhibitionDTO.SectionsList.Add(singleHangerDTO);
                    }
                    layoutDTO.Sections.Add(exhibitionDTO);
                }
                return Ok(layoutDTO);
            }
        }

        /// <summary>
        /// Get all SectionTypes
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("sectionTypes")]
        [ResponseType(typeof(SectionTypeDTO))]
        public IHttpActionResult GetSectionType(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();
                var sectiontypes = _sectionTypeRepository.Find(new GetAllSpecification<SectionType>());

                return Ok(sectiontypes.Select(x => GetSectionTypeDTO(x)));
            }
        }

        /// <summary>
        /// Get Section list for Event
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("{eventId:guid}/sectionList")]
        [ResponseType(typeof(SectionTypeDTO))]
        public IHttpActionResult GetSection(Guid organizerId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var layoutPlanCriteria = new LayoutPlanSearchCriteria { EventId = eventId };
                var layoutPlansepcification = new LayoutPlanSpecificationForSearch(layoutPlanCriteria);

                //Todo: check last child layout
                var layoutPlan = _layoutPlanRepository.Find(layoutPlansepcification).FirstOrDefault();

                if (layoutPlan == null)
                    return NotFound();

                var sectionCriteria = new SectionSearchCriteria { LayoutId = layoutPlan.Id };
                var sectionSepcification = new SectionSpecificationForSearch(sectionCriteria);
                var sections = _sectionRepository.Find(sectionSepcification);

                var sectionList = sections.Where(x => x.SectionType.Name.ToUpper().Equals("HANGER"));

                List<HangerDTO> hangerList = new List<HangerDTO>();
                foreach (Section section in sectionList)
                {
                    var stallCriteria = new StallSearchCriteria { sectionId = section.Id };
                    var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                    var stalls = _stallRepository.Find(stallSepcification);

                    HangerDTO hanger = new HangerDTO
                    {
                        HangerId = section.Id,
                        exhibitionId = section.Exhibition.Id,
                        HangerName = section.Name,
                        TotalStalls = stalls.Count(),
                        StallsBooked = stalls.Where(x => x.IsBooked == true).Count(),
                        RemainingStalls = stalls.Where(x => x.IsBooked == false).Count(),
                        Hight = section.Height,
                        Width = section.Width
                    };
                    hangerList.Add(hanger);
                }
                return Ok(hangerList);
            }
        }

        /// <summary>
        /// Get Stall For Section 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [Route("{sectionId:guid}/stall")]
        [ResponseType(typeof(StallListDTO))]
        public IHttpActionResult GetStalls(Guid organizerId, Guid sectionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var sectionDetails = _sectionRepository.GetById(sectionId);
                if (sectionDetails == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(sectionDetails.LayoutPlan.Event.Id);

                var stallCriteria = new StallSearchCriteria { sectionId = sectionId };
                var stallSepcification = new StallSpecificationForSearch(stallCriteria);
                var stallLists = _stallRepository.Find(stallSepcification).OrderBy(x => x.StallNo);

                //var barrierSearchCriteria = new BarrierSearchCriteria { SectionId = sectionId };
                //var barrierSpecification = new BarrierSpecificationForSearch(barrierSearchCriteria);
                //var barrierList = _barrierRepository.Find(barrierSpecification).OrderBy(x => x.Order);

                StallBarrierListDTO stallBarrierListDTO = new StallBarrierListDTO();
                foreach (Stall stall in stallLists)
                {
                    StallListDTO singleStall = new StallListDTO
                    {
                        Id = stall.Id,
                        StallNo = stall.StallNo,
                        Price = stall.Price,
                        IsBooked = stall.IsBooked,
                        Height = stall.Height,
                        Width = stall.Width,
                        X_Coordinate = stall.X_Coordinate,
                        Y_Coordinate = stall.Y_Coordinate,
                        StallSize = stall.StallSize,
                        IsRequested = stall.IsRequested
                    };

                    if (stall.Partner != null)
                    {
                        singleStall.PartnerId = stall.Partner.Id;
                        singleStall.PartnerColor = stall.Partner.Color;
                    }
                    else if (stall.ExhibitorIndustryType != null)
                    {
                        singleStall.PartnerColor = stall.ExhibitorIndustryType.Color;
                    }
                    else if (stall.Country != null)
                    {
                        singleStall.PartnerColor = stall.Country.Color;
                    }
                    else if (stall.State != null)
                    {
                        singleStall.PartnerColor = stall.State.Color;
                    }

                    if (eventDetails != null)
                    {
                        stall.Event = eventDetails;
                    }
                    stallBarrierListDTO.StallListDTO.Add(singleStall);
                }

                //foreach (Barrier barrier in barrierList)
                //{
                //    BarrierDTO barrierDTO = new BarrierDTO
                //    {
                //        Order = barrier.Order,
                //        X_Coordinate = barrier.X_Coordinate,
                //        Y_Coordinate = barrier.Y_Coordinate
                //    };
                //    stallBarrierListDTO.BarrierDTO.Add(barrierDTO);
                //}
                return Ok(stallBarrierListDTO);
            }
        }

        /// <summary>
        /// Add Stall List 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="sectionId"></param>
        /// <param name="eventId"></param>
        /// <param name="stallBarrierListDTO"></param>
        /// <returns></returns>
        [Route("{sectionId:guid}/stall/{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(StallsDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid sectionId, Guid eventId, StallBarrierListDTO stallBarrierListDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var eventDetails = _eventRepository.GetById(eventId);
                if (eventDetails == null)
                    return NotFound();

                Section section = _sectionRepository.GetById(sectionId);
                if (section != null)
                {
                    foreach (StallListDTO stall in stallBarrierListDTO.StallListDTO)
                    {
                        Stall singleStall = Stall.Create(stall.StallNo, stall.Price, stall.IsBooked, stall.Height, stall.Width, stall.X_Coordinate, stall.Y_Coordinate, stall.StallSize, stall.IsRequested);
                        singleStall.Section = section;
                        singleStall.Event = eventDetails;
                        _stallRepository.Add(singleStall);
                    }

                    foreach (BarrierDTO singleBarrier in stallBarrierListDTO.BarrierDTO)
                    {
                        Barrier barrier = Barrier.Create(singleBarrier.Order, singleBarrier.X_Coordinate, singleBarrier.Y_Coordinate);
                        barrier.Section = section;
                        _barrierRepository.Add(barrier);
                    }
                    unitOfWork.SaveChanges();
                    return GetStalls(organizerId, sectionId);
                }
                else
                    return NotFound();
            }
        }

        /// <summary>
        /// Add Section Layout
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <param name="sectionsLayoutDTO"></param>
        /// <returns></returns>
        [Route("{eventId:guid}")]
        [ModelValidator]
        [ResponseType(typeof(SectionsLayoutDTO))]
        public IHttpActionResult Post(Guid organizerId, Guid eventId, SectionsLayoutDTO sectionsLayoutDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event EventData = _eventRepository.GetById(eventId);
                if (EventData == null)
                    return NotFound();

                sectionsLayoutDTO.Version_No = "Version 001";
                LayoutPlan createLayoutPlan = LayoutPlan.Create(sectionsLayoutDTO.Version_No, false);
                createLayoutPlan.Event = EventData;
                _layoutPlanRepository.Add(createLayoutPlan);

                foreach (SectionsDTO sections in sectionsLayoutDTO.Sections)
                {
                    SectionType sectionType = _sectionTypeRepository.GetById(sections.SectionTypeId);
                    Exhibition exhibition = _exhibitionRepository.GetById(sections.ExhibitionId);
                    Section parentSection = Section.Create(sections.Name, sections.Height, sections.Width, sections.X_Coordinate, sections.Y_Coordinate);
                    parentSection.LayoutPlan = createLayoutPlan;
                    parentSection.SectionType = sectionType;
                    _sectionRepository.Add(parentSection);

                    foreach (AccesssDTO singleAcess in sections.AccessList)
                    {
                        Access access = Access.Create(singleAcess.IsEntry, singleAcess.IsExit, singleAcess.IsEmergencyExit, singleAcess.Height, singleAcess.Width, singleAcess.X_Coordinate, singleAcess.Y_Coordinate);
                        access.Section = parentSection;
                        _accessRepository.Add(access);
                    }
                    foreach (SectionsDTO singleChildSection in sections.SectionsList)
                    {
                        SectionType childSectionType = _sectionTypeRepository.GetById(singleChildSection.SectionTypeId);
                        Section childSection = Section.Create(singleChildSection.Name, singleChildSection.Height, singleChildSection.Width, singleChildSection.X_Coordinate, singleChildSection.Y_Coordinate);
                        childSection.LayoutPlan = createLayoutPlan;
                        childSection.SectionType = childSectionType;
                        childSection.Exhibition = exhibition;
                        parentSection.Sections.Add(childSection);
                        _sectionRepository.Add(childSection);

                        foreach (AccesssDTO singleChildAcess in singleChildSection.AccessList)
                        {
                            Access access = Access.Create(singleChildAcess.IsEntry, singleChildAcess.IsExit, singleChildAcess.IsEmergencyExit, singleChildAcess.Height, singleChildAcess.Width, singleChildAcess.X_Coordinate, singleChildAcess.Y_Coordinate);
                            access.Section = childSection;
                            _accessRepository.Add(access);
                        }
                    }
                }
                unitOfWork.SaveChanges();
                return Get(organizerId, eventId);
            }
        }

        /// <summary>
        /// Edit Stall Status
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="stallListDTO"></param>
        /// <returns></returns>
        [Route("stalls/status")]
        [ModelValidator]
        public IHttpActionResult put(Guid organizerId, List<StallIdListDTO> stallListDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                foreach (StallIdListDTO stallList in stallListDTO)
                {
                    Stall stalls = _stallRepository.GetById(stallList.StallId);
                    if (stalls != null)
                    {
                        stalls.Update(stalls.StallNo, stalls.Price, stallList.IsBooked, stalls.Height, stalls.Width, stalls.X_Coordinate, stalls.Y_Coordinate, stalls.StallSize, stalls.IsRequested);
                    }
                }
                unitOfWork.SaveChanges();
                return GetStalls(organizerId, stallListDTO.FirstOrDefault().HangerId);
            }
        }

        /// <summary>
        /// Edit Stall Reserve for Partner , country, Sales, State 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="stallAllocationDTO"></param>
        /// <returns></returns>
        [Route("addStallAllocation")]
        [ModelValidator]
        public IHttpActionResult putAddPartnerStalls(Guid organizerId, StallAllocationDTO stallAllocationDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                foreach (StallIdListDTO stallList in stallAllocationDTO.StallIdListDTO)
                {
                    Stall stalls = _stallRepository.GetById(stallList.StallId);
                    if (stalls == null)
                        return NotFound();

                    if (stallAllocationDTO.PartnerId != Guid.Empty)
                    {
                        var partnerDetails = _loginRepository.GetById(stallAllocationDTO.PartnerId);
                        if (partnerDetails == null)
                            return NotFound();
                        stalls.Partner = partnerDetails.Partner;
                    }
                    else
                    {
                        stalls.Partner = null;
                    }

                    if (stallAllocationDTO.ExhibitorIndustryTypeId != Guid.Empty)
                    {
                        var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(stallAllocationDTO.ExhibitorIndustryTypeId);
                        if (exhibitorIndustryType == null)
                            return NotFound();
                        stalls.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                    else
                    {
                        stalls.ExhibitorIndustryType = null;
                    }

                    if (stallAllocationDTO.StateId != Guid.Empty)
                    {
                        var stateDetails = _stateRepository.GetById(stallAllocationDTO.StateId);
                        if (stateDetails == null)
                            return NotFound();
                        stalls.State = stateDetails;
                    }
                    else
                    {
                        stalls.State = null;
                    }

                    if (stallAllocationDTO.CountryId != Guid.Empty)
                    {
                        var countryDetails = _countryRepository.GetById(stallAllocationDTO.CountryId);
                        if (countryDetails == null)
                            return NotFound();
                        stalls.Country = countryDetails;
                    }
                    else
                    {
                        stalls.Country = null;
                    }
                }
                unitOfWork.SaveChanges();
                return GetStalls(organizerId, stallAllocationDTO.StallIdListDTO.FirstOrDefault().HangerId);
            }
        }

        /// <summary>
        /// Edit Stall Dereserve Stalls
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="stallIdListDTO"></param>
        /// <returns></returns>
        [Route("dereserveStallAllocation")]
        [ModelValidator]
        public IHttpActionResult putDereservationStalls(Guid organizerId, List<StallIdListDTO> stallIdListDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                foreach (StallIdListDTO stallList in stallIdListDTO)
                {
                    Stall stalls = _stallRepository.GetById(stallList.StallId);
                    if (stalls.ExhibitorIndustryType != null)
                        stalls.ExhibitorIndustryType = null;

                    if (stalls.Partner != null)
                        stalls.Partner = null;

                    if (stalls.Country != null)
                        stalls.Country = null;

                    if (stalls.State != null)
                        stalls.State = null;
                }
                unitOfWork.SaveChanges();
                return GetStalls(organizerId, stallIdListDTO.FirstOrDefault().HangerId);
            }
        }

        /// <summary>
        /// Edit Stall Add ExhibitorIndustryType
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="ExhibitorIndustryTypeId"></param>
        /// <param name="stallListDTO"></param>
        /// <returns></returns>
        [Route("addIndustryTypeStall/{ExhibitorIndustryTypeId:guid}")]
        [ModelValidator]
        public IHttpActionResult putAddIndustryTypeStalls(Guid organizerId, Guid ExhibitorIndustryTypeId, List<StallIdListDTO> stallListDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibitorIndustryType = _exhibitorIndustryTypeRepository.GetById(ExhibitorIndustryTypeId);
                if (exhibitorIndustryType == null)
                    return NotFound();

                foreach (StallIdListDTO stallList in stallListDTO)
                {
                    Stall stalls = _stallRepository.GetById(stallList.StallId);
                    if (stalls != null)
                    {
                        stalls.ExhibitorIndustryType = exhibitorIndustryType;
                    }
                }
                unitOfWork.SaveChanges();
                return GetStalls(organizerId, stallListDTO.FirstOrDefault().HangerId);
            }
        }

        private AccessDTO GetAccessDTO(Access access)
        {
            return new AccessDTO
            {
                Height = access.Height,
                Width = access.Width,
                IsEmergencyExit = access.IsEmergencyExit,
                IsEntry = access.IsEntry,
                IsExit = access.IsExit,
                X_Coordinate = access.X_Coordinate,
                Y_Coordinate = access.Y_Coordinate
            };
        }

        private StallsDTO GetStallDTO(Stall stall)
        {
            StallsDTO stallDTO = new StallsDTO
            {
                Id = stall.Id,
                StallNo = stall.StallNo,
                IsBooked = stall.IsBooked,
                Price = stall.Price,
                Height = stall.Height,
                Width = stall.Width,
                X_Coordinate = stall.X_Coordinate,
                Y_Coordinate = stall.Y_Coordinate,
                IsRequested = stall.IsRequested,

            };
            if (stall.Partner != null)
            {
                stallDTO.PartnerId = stall.Partner.Id;
                stallDTO.PartnerColor = stall.Partner.Color;
            }
            return stallDTO;
        }

        private SectionDTO GetSectionDTO(Section section)
        {
            return new SectionDTO
            {
                Id = section.Id,
                Name = section.Name,
                Height = section.Height,
                Width = section.Width,
                X_Coordinate = section.X_Coordinate,
                Y_Coordinate = section.Y_Coordinate
            };
        }

        private static SectionTypeDTO GetSectionTypeDTO(SectionType sectionType)
        {
            return new SectionTypeDTO
            {
                Id = sectionType.Id,
                sectionType = sectionType.Name
            };
        }
    }
}
