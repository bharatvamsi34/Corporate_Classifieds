using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private readonly IDatabase db;

        public OfferRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
