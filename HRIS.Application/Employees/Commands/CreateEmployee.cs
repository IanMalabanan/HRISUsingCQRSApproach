using AutoMapper;
using HRIS.Application.Common.Extensions;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.Entities;
using HRIS.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Commands
{
    public class CreateEmployee : IRequest<Employee>
    {
        public Employee model { get; set; }
    }

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Employee> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var _entity = _mapper.Map<Employee>(request.model);
            _entity.ValidateRequired();

            //await _roleRepository.Validate(_entity, Domain.Enums.CRUDType.CREATE);
            var _result = await _employeeRepository.AddAsync(_entity);

            return _result;
        }
    }
}
