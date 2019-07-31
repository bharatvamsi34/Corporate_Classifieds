using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<CategoryDetails> Get()
        {
            return _repository.GetCategoryDetails();
        }

        [HttpGet("{id}")]
        public CategoryDetails Get(int id)
        {
            return _repository.GetCategoryById(id);
        }

        [HttpPost]
        public void Create([FromBody] CategoryDetails item)
        {
            _repository.CreateNewCategory(item);
        }

        [HttpPut()]
        public void Edit([FromBody] CategoryDetails item)
        {
            _repository.UpdateCategoryDetails(item);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.DeleteCategory(id);
        }
    }
}
