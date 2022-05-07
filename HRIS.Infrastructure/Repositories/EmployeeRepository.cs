using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Repositories
{
    public class EmployeeRepository : EmployeeModel, IEmployeeRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public EmployeeRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            var data = await (from emp in _dbContext.Employees
                              join dep in _dbContext.Departments on emp.DepartmentCode equals dep.Code
                              join sec in _dbContext.DepartmentSections
                              on new { A = emp.DepartmentCode, B = emp.DepartmentSectionCode }
                              equals new { A = sec.DepartmentCode, B = sec.Code }
                              select new
                              {
                                  BatchNo,
                                  SerialID,
                                  EmpID,
                                  LastName,
                                  FirstName,
                                  MiddleName,
                                  DepartmentCode = dep.Code,
                                  DepartmentName = dep.Description,
                                  SectionCode = sec.Code,
                                  SectionName = sec.Description
                              }
                       ).Distinct().ToListAsync();

            var _result = data.Select(x => new EmployeeModel
            {
                BatchNo = x.BatchNo,
                SerialID = x.SerialID,
                EmpID = x.EmpID,
                LastName = x.LastName,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                DepartmentDetails = new DepartmentModel
                {
                    Code = x.DepartmentCode,
                    Description = x.DepartmentName
                },
                DepartmentSectionDetails = new DepartmentSectionModel
                {
                    Code = x.SectionCode,
                    Description = x.SectionName,
                    DepartmentCode = x.DepartmentCode
                }
            });

            return _result;
        }
    }
}
