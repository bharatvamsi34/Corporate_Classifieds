using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementRepository _adPost;
        public AdvertisementController(IAdvertisementRepository adPost)
        {
            _adPost = adPost;
        }
        [HttpGet]
        public IEnumerable<AdvertisementCard> Get()
        {
            return _adPost.GetData();
        }
        [HttpGet("ad/{id}/{empId}")]
        public ViewAdvertisementDetails Get(int id, int empId)
        {
            return _adPost.GetDetails(id, empId);
        }
        [HttpGet("employee/{id}")]
        public IEnumerable<AdvertisementCard> GetDataByEmployeeId(int id)
        {
            return _adPost.GetDataByEmployeeId(id);
        }
        [HttpPost]
        public void Create([FromBody] NewAdvertisement item)
        {
            _adPost.CreateNewAdvertisement(item);
        }
        [HttpPost("offer")]
        public void Create([FromBody] MakeOffer item)
        {
            _adPost.MakeAnOffer(item);
        }
        [HttpPut()]
        public void Edit([FromBody] NewAdvertisement item)
        {
            _adPost.UpdateAdvertisement(item);
        }
        [HttpPut("repost/{id}")]
        public void Edit(int id)
        {
            _adPost.RepostAdvertisement(id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _adPost.DeleteAdvertisement(id);
        }
    }
}
