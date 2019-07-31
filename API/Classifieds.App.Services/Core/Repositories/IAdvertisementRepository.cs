using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using System.Collections.Generic;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        IEnumerable<AdvertisementCard> GetData();
        IEnumerable<AdvertisementCard> GetDataByEmployeeId(int id);
        ViewAdvertisementDetails GetDetails(int id, int empId);
        void CreateNewAdvertisement(NewAdvertisement item);
        void UpdateAdvertisement(NewAdvertisement item);
        void MakeAnOffer(MakeOffer item);
        void RepostAdvertisement(int id);
        void DeleteAdvertisement(int id);
    }
}
