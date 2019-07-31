using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using System.Collections.Generic;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        void ReportAd(Report report);
        IEnumerable<AdvertisementCard> GetReportedAds();
        void RemoveReportedAd(AdminMessage item);
        void DiscardReport(int id);
    }
}
