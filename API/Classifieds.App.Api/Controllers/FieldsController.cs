using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldsRepository _repository;

        public FieldsController(IFieldsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<Fields> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public IEnumerable<Fields> Get(int id)
        {
            return GetDetails(id);
        }

        [HttpPost]
        public void Create([FromBody] Fields item)
        {
            _repository.Create(item);
        }

        [HttpPut()]
        public void Edit([FromBody] Fields item)
        {
            _repository.Edit(item);
        }
        [HttpDelete("{items}")]
        public void Delete(string items)
        {
            DeleteFields(items);
        }

        private IEnumerable<Fields> GetDetails(int id)
        {
            IEnumerable<Fields> val = _repository.GetAll();
            return from Field in val where Field.CategoryId == id select Field;
        }
        private void DeleteFields(string items)
        {
            foreach (var id in items.Split(',').ToList())
                _repository.Delete(Convert.ToInt32(id));
        }
    }
}
