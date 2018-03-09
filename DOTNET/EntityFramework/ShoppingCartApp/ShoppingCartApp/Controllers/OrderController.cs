using ShoppingCartCore.Framework.Repository.EntityFramework;
using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    [RoutePrefix("api/v1/ShoppingCart/User/{userId}/Order")]
    public class OrderController : ApiController
    {
        private EntityFrameworkRepository<Order> erf = new EntityFrameworkRepository<Order>();


        [Route("GetAllOrders")]
        public IHttpActionResult Get()
        {
            return Ok(erf.Get());
        }

        [Route("GetOrder/{orderId}")]
        public IHttpActionResult GetById(Guid orderId)
        {
            return Ok(erf.GetById(orderId));
        }

        [Route("AddOrder")]
        public IHttpActionResult PostOrder(Order order)
        {
            erf.Add(order);
            return Ok("Order Added...");
        }

        [Route("DeleteOrder/{orderId}")]
        public IHttpActionResult DeleteOrder(Guid orderId)
        {
            erf.Delete(orderId);
            return Ok("Order Deleted");
        }

        [Route("UpdateOrder")]
        public IHttpActionResult PutUpdateOrder(Order order)
        {
            erf.Update(order);
            return Ok("Data Updated");  
        }
    }
}