using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class CommentsRepository : Repository<Comments>, ICommentsRepository
    {
        private readonly IDatabase db;

        public CommentsRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
