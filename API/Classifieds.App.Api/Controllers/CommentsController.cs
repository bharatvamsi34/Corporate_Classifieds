using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository _repository;
        public CommentsController(ICommentsRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public void Post([FromBody] Comments comment)
        {
            _repository.Create(comment);
        }
    }
}
