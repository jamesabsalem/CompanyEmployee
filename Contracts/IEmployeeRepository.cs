using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.MetaData;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid  companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
