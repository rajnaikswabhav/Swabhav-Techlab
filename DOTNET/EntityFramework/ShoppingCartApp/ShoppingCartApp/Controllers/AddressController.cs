using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Framework.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    [RoutePrefix("api/v1/ShoppingCart/User/{userId}/Address")]
    public class AddressController : ApiController
    {
        private EntityFrameworkRepository<Address> erf = new EntityFrameworkRepository<Address>();

        [Route("AddAddress")]
        public IHttpActionResult PostAddress(Address address)
        {
            erf.Add(address);
            return Ok("Address Added");
        }

        [Route("GetAllAddreses")]
        public IHttpActionResult GetAllAddreses()
        {
            return Ok(erf.Get());
        }

        [Route("DeleteAddress/{addressId}")]
        public IHttpActionResult DeleteAddress(Guid addressId)
        {
            erf.Delete(addressId);
            return Ok("Address Deleted");
        }

        [Route("UpdateAddress")]
        public IHttpActionResult PutEditAddress(Address address)
        {
            erf.Update(address);
            return Ok("Address Updated");
        }

        [Route("GetAddress/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            return Ok(erf.GetById(id));
        }
    }
}