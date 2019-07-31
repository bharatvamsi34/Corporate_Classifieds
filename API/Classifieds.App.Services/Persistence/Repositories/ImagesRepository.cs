using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class ImagesRepository : Repository<Images>, IImagesRepository
    {
        private readonly IDatabase db;
        public ImagesRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
