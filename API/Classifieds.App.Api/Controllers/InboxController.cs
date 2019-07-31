using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        private IInboxRepository _repository;
        public InboxController(IInboxRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public IEnumerable<InboxDetails> GetAds(int id)
        {
            return _repository.GetByEmployeeId(id);
        }
        [HttpGet("chat/{ids}")]
        public IEnumerable<Inbox> GetChat(string ids)
        {
            return _repository.GetChatById(Convert.ToInt32(ids.Split(',').ToList().First()), Convert.ToInt32(ids.Split(',').ToList().Last()));
        }
        [HttpPost]
        public void Post([FromBody] Inbox item)
        {
            _repository.Create(item);
        }
    }
}
