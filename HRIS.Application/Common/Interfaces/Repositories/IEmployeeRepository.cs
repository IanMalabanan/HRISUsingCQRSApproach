using HRIS.Domain.Entities;
using HRIS.Domain.Enums;
using HRIS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Repositories
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployeeByID(string empid);
        Task<Employee> GetEmployeeByEmpID(string empid);
        Task<Employee> FullDeleteEmployee(string empid);
        Task Validate(Employee entity, CRUDType cRUDType);
    }
}
