using AutoMapper;
using HRIS.Application.Common.Exceptions;
using HRIS.Application.Common.Extensions;
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
    public class UpdateEmployee : IRequest<Employee>
    {
        public Employee model { get; set; }
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Employee> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            var _entity = await _employeeRepository.GetEmployeeByEmpID(request.model.EmpID);
            if (_entity == null)
            {
                throw new NotFoundException($"Employee with empid {request.model.EmpID} does not exist.");
            }

            _entity.FirstName = request.model.FirstName;
            _entity.MiddleName = request.model.MiddleName;
            _entity.LastName = request.model.LastName;
            //_entity.DepartmentCode = request.model.DepartmentCode;
            //_entity.DepartmentSectionCode = request.model.DepartmentSectionCode;
            //_entity.CivilStatusCode = request.model.CivilStatusCode;

            _entity.ValidateRequired();

            await _employeeRepository.Validate(_entity, Domain.Enums.CRUDType.UPDATE);

            await _employeeRepository.UpdateAsync(_entity);

            return _entity;
        }
    }
}
