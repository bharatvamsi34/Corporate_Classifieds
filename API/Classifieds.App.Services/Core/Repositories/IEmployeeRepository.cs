using Classifieds.App.Models;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee EmployeeLogin(string email, string password);
        void UpdateEmployeDetails(Employee item);
    }
}
