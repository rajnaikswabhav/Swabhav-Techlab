using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/images")]
    public class ImagesController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        
        /// <summary>
        /// Get Images By Type
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        [Route("{imageType}")]
        public IHttpActionResult Get(Guid organizerId, string imageType)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            var data = new object();

            if (imageType.ToUpper() == "ceremony".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/ceremony/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/ceremony/15.jpg"},
            };
            }
            else if (imageType.ToUpper() == "cosmetics".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/cosmetics/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/cosmetics/15.jpg"},
            };
            }
            else if (imageType.ToUpper() == "electronics".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/electronics/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/11.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/electronics/12.jpg"}
            };
            }
            else if (imageType.ToUpper() == "countries".ToUpper())
            {
                data = new[]
                {
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/countries/17.jpg"},
            };
            }
            else if (imageType.ToUpper() == "food".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/food/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/17.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/18.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/19.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/food/20.jpg"},
            };
            }
            else if (imageType.ToUpper() == "furniture".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/furniture/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/furniture/9.jpg"},
            };
            }
            else if (imageType.ToUpper() == "government".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/government/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/17.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/18.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/19.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/20.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/21.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/22.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/23.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/24.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/25.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/26.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/27.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/28.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/29.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/30.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/31.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/32.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/33.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/34.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/35.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/36.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/37.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/38.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/39.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/40.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/41.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/42.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/43.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/government/44.jpg"}
            };
            }
            else if (imageType.ToUpper() == "hoardings".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/hoardings/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/21.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/22.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/23.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/24.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/25.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/hoardings/26.jpg"},
            };
            }
            else if (imageType.ToUpper() == "lifestyle".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/lifestyle/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/lifestyle/9.jpg"},
            };
            }
            else if (imageType.ToUpper() == "Awards".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/Our Awards/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/11.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/Our Awards/13.jpg"},
            };
            }
            else if (imageType.ToUpper() == "press".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/press Releases/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/press Releases/10.jpg"},
            };
            }
            else if (imageType.ToUpper() == "products".ToUpper())
            {
                data = new[]
                {
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/17.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/18.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/19.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/20.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/21.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/22.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/23.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/24.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/25.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/26.jpg"},
            };
            }
            else if (imageType.ToUpper() == "uniqueproducts".ToUpper())
            {
                data = new[]
                {
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/13.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/17.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/18.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/19.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/20.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/21.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/22.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/23.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/24.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/25.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/products/26.jpg"},
            };
            }
            else if (imageType.ToUpper() == "stalls".ToUpper())
            {
                data = new[]
                {
                 new {url = "http://gsmktg.com/mobile_images/gallery/stalls/1.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/2.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/3.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/4.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/5.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/6.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/7.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/8.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/9.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/10.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/11.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/12.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/14.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/15.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/16.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/17.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/18.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/19.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/20.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/21.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/22.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/23.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/24.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/25.jpg"},
                 new{url = "http://gsmktg.com/mobile_images/gallery/stalls/26.jpg"},
            };
            }
            else if (imageType.ToUpper() == "video".ToUpper())
            {
                data = new[]
                {
                 new {url = "Ef_rSN-vHFw"},
                 new{url = "R6PygjprsQ0"},

            };
            }
            return Json(data); ;
        }
    }
}
