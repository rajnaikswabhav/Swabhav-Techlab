using ShoppingCartCore.Framework.Repository.EntityFramework;
using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    [RoutePrefix("api/v1/ShoppingCart/Item")]
    public class LineItemController : ApiController
    {
        private EntityFrameworkRepository<LineItem> erf = new EntityFrameworkRepository<LineItem>();


        [Route("GetAllItems")]
        public IHttpActionResult Get()
        {
            return Ok(erf.Get());
        }

        [Route("GetById/{itemId}")]
        public IHttpActionResult GetById(Guid itemId)
        {
            return Ok(erf.GetById(itemId));
        }

        [Route("AddItem")]
        public IHttpActionResult PostOrder(LineItem item)
        {
            erf.Add(item);
            return Ok("LineItem Added...");
        }

        [Route("DeleteItem/{itemId}")]
        public IHttpActionResult DeleteOrder(Guid itemId)
        {
            erf.Delete(itemId);
            return Ok("Item Deleted");
        }

        [Route("UpdateItem")]
        public IHttpActionResult PutUpdateOrder(LineItem item)
        {
            erf.Update(item);
            return Ok("Data Updated");
        }
    }
}