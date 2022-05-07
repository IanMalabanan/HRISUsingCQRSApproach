using HRIS.Application.Common.Exceptions;
using HRIS.Application.Employees.Commands;
using HRIS.Application.Employees.Queries;
using HRIS.Domain.Entities;
using HRIS.Domain.Exceptions;
using HRIS.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiControllerBase
    {
        [HttpGet]
        [Route("getallemployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var _result = await Mediator.Send(new GetEmployees() { });

                return Ok(_result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getemployeebyempid")]
        public async Task<ActionResult> GetByID([FromQuery] string empid)
        {
            try
            {
                var _result = await Mediator.Send(new GetEmployeeByEmpID { EmpID = empid });

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

                return Ok(model);
            }
            catch (Exception e)
            {
                //Logger.Error($"{DateTime.Now} : {e.Message}");
                //Logger.Error($"{DateTime.Now} : {e.StackTrace}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateEmployee(Employee req)
        {
            try
            {
                var model = new Employee
                {
                    BatchNo = req.BatchNo,
                    SerialID = req.SerialID,
                    EmpID = req.EmpID,
                    LastName = req.LastName,
                    FirstName = req.FirstName,
                    MiddleName = req.MiddleName,
                    DepartmentCode = req.DepartmentCode,
                    DepartmentSectionCode = req.DepartmentSectionCode,
                    CivilStatusCode = req.CivilStatusCode
                };

                var _result = await Mediator.Send(new CreateEmployee { model = model });

                return Ok(new { status = 200, message = "Employee Created." });

            }
            catch (UnsatisfiedRequiredFieldsException ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return BadRequest(new { status = 400, message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return NotFound(new { status = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return StatusCode(500, new { status = 500, message = ex.Message });
            }
        }


        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateEmployee(Employee req)
        {
            try
            {

                var model = new Employee
                {
                    EmpID = req.EmpID,
                    LastName = req.LastName,
                    FirstName = req.FirstName,
                    MiddleName = req.MiddleName,
                    DepartmentCode = req.DepartmentCode,
                    DepartmentSectionCode = req.DepartmentSectionCode,
                    CivilStatusCode = req.CivilStatusCode
                };

                var _result = await Mediator.Send(new UpdateEmployee { model = model });

                return Ok(new { status = 200, message = "Employee Details Updated." });

            }
            catch (UnsatisfiedRequiredFieldsException ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return BadRequest(new { status = 400, message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return NotFound(new { status = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                //Logger.Error($"{DateTime.Now} : {ex.Message}");
                //Logger.Error($"{DateTime.Now} : {ex.StackTrace}");
                return StatusCode(500, new { status = 500, message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("softdelete")]
        public async Task<ActionResult> Delete(Employee _employee)
        {
            var model = new Employee
            {
                EmpID = _employee.EmpID,
            };

            var _result = await Mediator.Send(new DeleteEmployee { employee = model });

            return Ok(new { status = 200, message = "Deleted Created." });
        }
    }
}
