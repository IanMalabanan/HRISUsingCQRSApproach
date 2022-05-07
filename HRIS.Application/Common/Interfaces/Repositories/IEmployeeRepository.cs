using HRIS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
    }
}
