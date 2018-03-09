using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Framework.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    public class WishlistController : ApiController
    {
        private EntityFrameworkRepository<Wishlist> erf = new EntityFrameworkRepository<Wishlist>();


        [Route("GetAllWishlist")]
        public IHttpActionResult Get()
        {
            return Ok(erf.Get());
        }

        [Route("GetById/{wishlistId}")]
        public IHttpActionResult GetById(Guid wishlistId)
        {
            return Ok(erf.GetById(wishlistId));
        }

        [Route("AddWishlist")]
        public IHttpActionResult PostOrder(Wishlist wishlist)
        {
            erf.Add(wishlist);
            return Ok("wishlist Added...");
        }

        [Route("DeleteWishlist/{wishlistId}")]
        public IHttpActionResult DeleteOrder(Guid wishlistId)
        {
            erf.Delete(wishlistId);
            return Ok("Wishlist Deleted");
        }

        [Route("UpdateWishlist")]
        public IHttpActionResult PutUpdateOrder(Wishlist wishlist)
        {
            erf.Update(wishlist);
            return Ok("Data Updated");
        }
    }
}