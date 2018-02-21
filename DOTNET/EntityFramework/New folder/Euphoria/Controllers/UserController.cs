using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/Users")]
    public class UserController : ApiController
    {
        private IRepository<User> _usersRepository = new EntityFrameworkRepository<User>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var user = _usersRepository.GetById(id);
            if (user == null)
                return NotFound();

            var userDTO = new UserDTO() { UserName = user.UserName, Password = user.Password };
            return Ok(userDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, UserDTO user)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var userToUpdate = _usersRepository.GetById(id);
                if (userToUpdate == null)
                    return NotFound();

                userToUpdate.Update(user.UserName, user.Password);

                unitOfWork.SaveChanges();
            }
            return Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(UserDTO userDTO)
        {
            var user = Kernel.Model.User.Create(userDTO.UserName, userDTO.Password);
            _usersRepository.Add(user);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            _usersRepository.Delete(id);
            return Ok();
        }
    }
}
