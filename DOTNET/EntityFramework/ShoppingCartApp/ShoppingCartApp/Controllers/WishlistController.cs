using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Framework.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    [RoutePrefix("api/v1/ShoppingCart/Wishlist")]
    public class WishlistController : ApiController
    {
        private EntityFrameworkRepository<Wishlist> erf = new EntityFrameworkRepository<Wishlist>();


        [Route("GetAllWishlist")]
        public IHttpActionResult Get()
        {
            return Ok(erf.Get());
        }

        [Route("GetWishlist/{wishlistId}")]
        public IHttpActionResult GetById(Guid wishlistId)
        {
            return Ok(erf.GetById(wishlistId));
        }

        [Route("AddWishlist")]
        public IHttpActionResult PostWishlist(Wishlist wishlist)
        {
            erf.Add(wishlist);
            return Ok("wishlist Added...");
        }

        [Route("DeleteWishlist/{wishlistId}")]
        public IHttpActionResult DeleteWishlist(Guid wishlistId)
        {
            erf.Delete(wishlistId);
            return Ok("Wishlist Deleted");
        }

        [Route("UpdateWishlist")]
        public IHttpActionResult PutUpdateWishlist(Wishlist wishlist)
        {
            erf.Update(wishlist);
            return Ok("Data Updated");
        }
    }
}