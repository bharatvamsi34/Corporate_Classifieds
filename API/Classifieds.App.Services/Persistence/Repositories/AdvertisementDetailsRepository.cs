using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class AdvertisementDetailsRepository : Repository<AdvertisementDetails>, IAdvertisementDetailsRepository
    {
        private readonly IDatabase db;

        public AdvertisementDetailsRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
