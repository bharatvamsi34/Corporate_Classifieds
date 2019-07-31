using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _adPost;
        public ReportController(IReportRepository adPost)
        {
            _adPost = adPost;
        }
        [HttpGet]
        public IEnumerable<AdvertisementCard> Get()
        {
            return _adPost.GetReportedAds();
        }
        [HttpPost]
        public void Post([FromBody] Report item)
        {
            _adPost.ReportAd(item);
        }
        [HttpPut]
        public void Put([FromBody] AdminMessage item)
        {
            _adPost.RemoveReportedAd(item);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _adPost.DiscardReport(id);
        }
    }
}
