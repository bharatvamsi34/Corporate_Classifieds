using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class FieldsRepository : Repository<Fields>, IFieldsRepository
    {
        private readonly IDatabase db;

        public FieldsRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
    }
}
