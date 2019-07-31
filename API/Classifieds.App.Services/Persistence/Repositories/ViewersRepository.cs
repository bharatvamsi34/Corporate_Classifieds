using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class ViewersRepository : Repository<Viewers>, IViewersRepository
    {
        private readonly IDatabase db;

        public ViewersRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
