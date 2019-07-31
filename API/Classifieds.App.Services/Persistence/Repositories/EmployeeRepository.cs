using Classifieds.App.Models;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly IDatabase db;

        public EmployeeRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }

        public Employee EmployeeLogin(string email, string password)
        {
            return db.SingleOrDefault<Employee>("Where Email = @0 AND Password = @1", email, password);
        }
        public void UpdateEmployeDetails(Employee item)
        {
            db.Update<Employee>($"set Name ='{item.Name}',Email = @0,Location = '{item.Location}',Phone = '{item.Phone}' where Id = {item.Id}", item.Email);
        }
    }
}
