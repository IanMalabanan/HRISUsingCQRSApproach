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
    public class GetEmployeeByEmpID : IRequest<EmployeeModel>
    {
        public string EmpID { get; set; }
    }

    public class GetPaymentRequestByCodeQueryHandler : IRequestHandler<GetEmployeeByEmpID, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;


        public GetPaymentRequestByCodeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeModel> Handle(GetEmployeeByEmpID request, CancellationToken cancellationToken)
        {
            var _result = await _employeeRepository.GetEmployeeByID(request.EmpID);

            EmployeeModel model = new EmployeeModel();

            model.EmpID = _result.EmpID;
            model.BatchNo = _result.BatchNo;
            model.SerialID = _result.SerialID;
            model.FirstName = _result.FirstName;
            model.MiddleName = _result.MiddleName;
            model.LastName = _result.LastName;
            //model.DepartmentDetails = new DepartmentModel
            //{
            //    Code = _result.DepartmentDetails.Code,
            //    Description = _result.DepartmentDetails.Description
            //};
            //model.DepartmentSectionDetails = new DepartmentSectionModel
            //{
            //    Code = _result.DepartmentSectionDetails.Code,
            //    Description = _result.DepartmentSectionDetails.Description,
            //    DepartmentCode = _result.DepartmentSectionDetails.DepartmentCode
            //};

            return model;
        }
    }
}
