using System.Collections.Generic;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        void Create(TEntity entity);

        void Edit(TEntity entity);

        void Delete(int id);
    }
}
