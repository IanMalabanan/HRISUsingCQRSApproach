using HRIS.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Commands
{
    public class SaveEmployee : IRequest<EmployeeModel>
    {
    }
}
