using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;
using System.Collections.Generic;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly IDatabase db;

        public ReportRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
        public void ReportAd(Report report)
        {
            db.Execute($";Exec ReportAd @@empId = {report.EmployeeId}, @@adId = {report.AdvertisementId}, @@category = '{report.Category}', @@description ='{report.Description}', @@time = '{report.Time}';");
        }
        public IEnumerable<AdvertisementCard> GetReportedAds()
        {
            return db.Fetch<AdvertisementCard>("Select * from ReportedAdvertisements ORDER BY PostedOn DESC;");
        }
        public void RemoveReportedAd(AdminMessage item)
        {
            db.Execute($";Exec RemoveReport @@adId = {item.AdvertisementId}, @@message = '{item.Message}';");
        }
        public void DiscardReport(int id)
        {
            db.Update<Report>("SET IsDeleted = 1 Where AdvertisementId = @0", id);
        }
    }
}
