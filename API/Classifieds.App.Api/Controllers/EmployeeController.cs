using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _repository.GetAll();
        }
        [HttpGet("{email}/{password}")]
        public Employee Get(string email, string password)
        {
            return _repository.EmployeeLogin(email, password);
        }
        [HttpPost]
        public void Post([FromBody] Employee objects)
        {
            _repository.Create(objects);
        }
        [HttpPut()]
        public void Put([FromBody] Employee objects)
        {
            _repository.UpdateEmployeDetails(objects);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
