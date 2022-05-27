using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.EmployeesFunctions.Commands
{
    public class FullDeleteEmployee : IRequest<Employee>
    {
        public string empid { get; set; }
    }

    public class FullDeleteEmployeeHandler : IRequestHandler<FullDeleteEmployee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public FullDeleteEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Employee> Handle(FullDeleteEmployee request, CancellationToken cancellationToken)
        {
            var _entity = await _employeeRepository.GetEmployeeByEmpID(request.empid);
            
            if (_entity == null)
            {
                throw new NotFoundException($"Employee with empid {request.empid} does not exist.");
            }

            await _employeeRepository.FullDeleteEmployee(request.empid);

            return _entity;
        }
    }
}
