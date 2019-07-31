using Classifieds.App.Services.Core.Repositories;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDatabase db;
        private String table;

        public Repository(IDatabase db)
        {
            this.db = db;
            this.table = GetTable();
        }

        public TEntity Get(int id)
        {
            return db.SingleOrDefault<TEntity>($"SELECT * FROM {table} WHERE Id=@0", id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Query<TEntity>($"SELECT * FROM {table}").ToList();
        }

        public void Create(TEntity entity)
        {
            Console.WriteLine(entity.ToString());
            db.Insert(table, "Id", true, entity);

        }

        public void Edit(TEntity entity)
        {
            db.Update(table, "Id", entity);
        }

        public void Delete(int id)
        {
            db.Delete<TEntity>(id);
        }

        public String GetTable()
        {
            String[] typeOfTable = typeof(TEntity).ToString().Split('.');
            string table = typeOfTable[typeOfTable.Count() - 1];
            return table;
        }
    }
}