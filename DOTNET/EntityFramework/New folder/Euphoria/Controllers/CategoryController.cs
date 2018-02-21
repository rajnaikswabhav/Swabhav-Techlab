using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/categories")]
    public class CategoryController : ApiController
    {
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();

        /// <summary>
        /// Get all Category
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(CategoryDTO[]))]
        public IHttpActionResult Get(Guid organizerId)
        {
            var organizer = _organizerRepository.GetById(organizerId);
            if (organizer == null)
                return NotFound();

            var specification = new GetAllSpecification<Category>();
            var categories = _categoryRepository.Find(specification).OrderBy(x => x.Name);

            return Ok(categories.Select(x => GetDTO(organizer, x)));
        }

        /// <summary>
        /// Get single category
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(CategoryDTO[]))]
        public IHttpActionResult Get(Guid organizerId, Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var category = _categoryRepository.GetById(id);

                if (category == null || category.Organizer.Id != organizerId)
                    return NotFound();

                var categoryDTO = GetDTO(organizer, category);
                return Ok(categoryDTO);
            }
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [Route("")]
        [ModelValidator]
        public IHttpActionResult Post(Guid organizerId, CategoryDTO categoryDTO)
        {
            var categoryCriteria = new CategorySearchCriteria { Name = categoryDTO.Name };
            var categorySpecification = new CategorySpecificationForSearch(categoryCriteria);
            var categoryList = _categoryRepository.Find(categorySpecification);
            if (categoryList.Count() == 0)
            {
                var category = Category.Create(categoryDTO.Name, categoryDTO.Description); // DTO to entity mapping
                _categoryRepository.Add(category);
            }
            else
            {
                throw new ValidationException("Category All Ready Added.");
            }
            return Get(organizerId);
        }

        private CategoryDTO GetDTO(Organizer organizer, Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
