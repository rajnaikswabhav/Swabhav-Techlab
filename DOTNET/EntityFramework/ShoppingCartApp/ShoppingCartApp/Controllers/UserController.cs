using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Framework.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCartApp.Controllers
{
    [RoutePrefix("api/v1/ShoppingCart/User")]
    public class UserController : ApiController
    {
        private EntityFrameworkRepository<User> erf = new EntityFrameworkRepository<User>();

        [Route("AddUser")]
        public IHttpActionResult PostUser(User user)
        {
            erf.Add(user);
            return Ok("Data Added");
        }

        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUser()
        {
            return Ok(erf.Get());
        }

        [Route("DeleteUser/{userId}")]
        public IHttpActionResult DeleteUser(Guid userId)
        {
            erf.Delete(userId);
            return Ok("Data Deleted");
        }

        [Route("UpdateUser")]
        public IHttpActionResult PutEditUser(User user)
        {
            erf.Update(user);
            return Ok("Data Updated");
        }

        [Route("GetUser/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            return Ok(erf.GetById(id));
        }
    }
}