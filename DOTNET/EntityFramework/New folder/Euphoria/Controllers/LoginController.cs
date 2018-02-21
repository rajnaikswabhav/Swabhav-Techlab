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
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/Login")]
    public class LoginController : ApiController
    {
        private IRepository<Login> _loginRepository = new EntityFrameworkRepository<Login>();
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<LoginSession> _loginSessionRepository = new EntityFrameworkRepository<LoginSession>();
        private IRepository<Role> _roleRepository = new EntityFrameworkRepository<Role>();
        private IRepository<Partner> _partnerRepository = new EntityFrameworkRepository<Partner>();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public LoginController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            var login = _loginRepository.GetById(id);
            if (login == null)
                return NotFound();

            var loginDTO = new LoginDTO() { Id = login.Id, UserName = login.UserName, Password = login.Password, RoleName = login.Role.RoleName };
            return Ok(loginDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Route("role/{role}")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult GetDataByRole(Guid organizerId, string role)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new LoginSearchCriteria { Role = role };
                var sepcification = new LoginSpecificationForSearch(criteria);
                var loginList = _loginRepository.Find(sepcification).OrderBy(x => x.UserName);

                if (loginList.Count() == 0)
                    return NotFound();

                List<LoginDTO> listOfLoginDTO = new List<LoginDTO>();
                foreach (Login singleLogin in loginList)
                {
                    var loginDTO = new LoginDTO() { Id = singleLogin.Id, UserName = singleLogin.UserName, Password = singleLogin.Password, RoleName = singleLogin.Role.RoleName };
                    listOfLoginDTO.Add(loginDTO);
                }
                return Ok(listOfLoginDTO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("role")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult GetRole(Guid organizerId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var roles = _roleRepository.Find(new GetAllSpecification<Role>()).OrderBy(x => x.RoleName);
                List<RoleDTO> roleDTOList = new List<RoleDTO>();
                foreach (Role role in roles)
                {
                    RoleDTO singleRole = new RoleDTO()
                    {
                        Id = role.Id,
                        Role = role.RoleName
                    };
                    roleDTOList.Add(singleRole);
                }
                return Ok(roleDTOList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult PostLogin(Guid organizerId, LoginDTO loginDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
                    return Content(HttpStatusCode.NotFound, "Invalid UserName or Password.");

                var criteria = new LoginSearchCriteria { UserName = loginDTO.UserName, Password = loginDTO.Password, RoleId = loginDTO.RoleId };
                var sepcification = new LoginSpecificationForSearch(criteria);
                var login = _loginRepository.Find(sepcification).FirstOrDefault();
                if (login == null)
                    return Content(HttpStatusCode.NotFound, "Invalid Login.");

                LoginDTO loginDetailsDTO = new LoginDTO()
                {
                    Id = login.Id,
                    UserName = login.UserName,
                    Password = login.Password,
                    RoleName = login.Role.RoleName
                };
                if (login.Partner != null)
                {
                    loginDetailsDTO.PartnerId = login.Partner.Id;
                }

                if (login.Role.RoleName.ToUpper() == "STAFF")
                {
                    LoginSession loginSession = LoginSession.Create(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE), null);
                    loginSession.Login = login;
                    _loginSessionRepository.Add(loginSession);
                    unitOfWork.SaveChanges();
                }
                return Ok(loginDetailsDTO);
            }
        }

        [Route("addPartner")]
        [ResponseType(typeof(AddPartnerDTO))]
        public IHttpActionResult PostPartner(Guid organizerId, AddPartnerDTO partnerDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var partnerRoles = _roleRepository.Find(new GetAllSpecification<Role>())
                                    .Where(x => x.RoleName.ToUpper().Equals("Partner".ToUpper()));

                var addPartner = Partner.Create(partnerDTO.Name, partnerDTO.EmailId, partnerDTO.PhoneNo, partnerDTO.Color);
                _partnerRepository.Add(addPartner);

                var addPartnerLogin = Login.Create(partnerDTO.UserName, partnerDTO.Password, "Partner", partnerDTO.EmailId, partnerDTO.Color);
                addPartnerLogin.Role = partnerRoles.FirstOrDefault();
                addPartnerLogin.Partner = addPartner;

                _loginRepository.Add(addPartnerLogin);

                unitOfWork.SaveChanges();
                return Ok("Partner Added Successfully");
            }
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        [Route("logout")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid loginId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Login loginDetails = _loginRepository.GetById(loginId);
                if (loginDetails.RoleName.ToUpper() == "STAFF")
                {
                    var loginSessionCriteria = new LoginSessionSearchCriteria { LoginId = loginId };
                    var loginSessionSepcification = new LoginSessionSpecificationForSearch(loginSessionCriteria);
                    var loginSession = _loginSessionRepository.Find(loginSessionSepcification);

                    LoginSession loginSessoinDetails = _loginSessionRepository.GetById(loginSession.FirstOrDefault().Id);

                    loginSessoinDetails.Update(loginSessoinDetails.StartTime, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE));
                    unitOfWork.SaveChanges();
                }
            }
            return Ok("Logout Successfully");
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="organizerId"></param>
        ///// <param name="loginDTO"></param>
        ///// <returns></returns>
        //[Route("")]
        //[ModelValidator]
        //public IHttpActionResult Post(Guid organizerId ,LoginDTO loginDTO)
        //{
        //    var organizer = _organizerRepository.GetById(organizerId);
        //    if (organizer == null)
        //        return NotFound();

        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
        //    {
        //        var login = Login.Create(loginDTO.UserName, loginDTO.Password, loginDTO.Role);

        //        _loginRepository.Add(login);
        //        unitOfWork.SaveChanges();
        //        return Get(organizerId, login.Id);
        //    }            
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(LoginDTO))]
        public IHttpActionResult Put(Guid organizerId, Guid id, LoginDTO loginDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var loginToUpdate = _loginRepository.GetById(id);
                if (loginToUpdate == null)
                    return NotFound();

                loginToUpdate.Update(loginDTO.UserName, loginDTO.Password, loginDTO.UserName, loginDTO.EmailId, loginDTO.Color);

                unitOfWork.SaveChanges();
                return Get(organizerId, id);
            }
        }
    }
}
