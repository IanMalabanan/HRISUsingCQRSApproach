using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.EmployeesCQRS.Commands
{
    public class DeleteEmployee : IRequest<bool>
    {
        public Employee employee { get; set; }
    }

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        //private ILogger _logger;
        private IDateTime _dateTime;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper
            //, ILogger<DeleteRoleQuery> logger
            , IDateTime dateTime)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            //_logger = logger;
            _dateTime = dateTime;
        }

        public async Task<bool> Handle(DeleteEmployee request, CancellationToken cancellationToken)
        {
            try
            {
                await _employeeRepository.SoftDeleteAsync(request.employee);

                return true;
            }
            catch (Exception e)
            {
                //_logger.LogError($"{_dateTime.Now.ToString("yyyyMMddHHmmss")} : {e.Message} : {e.StackTrace}");
                return false;
            }
        }
    }
}
