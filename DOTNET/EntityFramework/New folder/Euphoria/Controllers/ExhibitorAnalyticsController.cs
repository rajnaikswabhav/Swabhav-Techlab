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
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/exhibitorsAnalytics")]
    public class ExhibitorAnalyticsController : ApiController
    {
        private readonly IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private readonly IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private readonly IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private readonly IRepository<ExhibitorIndustryType> _exhibitorIndustryTypeRepository = new EntityFrameworkRepository<ExhibitorIndustryType>();
        private TokenGenerationService _tokenService = new TokenGenerationService();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        

    }
}
