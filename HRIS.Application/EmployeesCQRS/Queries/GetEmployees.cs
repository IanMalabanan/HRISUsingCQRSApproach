using AutoMapper;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Queries
{
    public class GetEmployees : IRequest<IEnumerable<EmployeeModel>> 
    {
    }

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployees, IEnumerable<EmployeeModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeModel>> Handle(GetEmployees request, CancellationToken cancellationToken)
        {
            var _result = await _employeeRepository.GetEmployees();

            return _result;

        }
    }
}
