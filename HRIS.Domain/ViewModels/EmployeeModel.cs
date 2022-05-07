using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.ViewModels
{
    public class EmployeeModel
    {
        public int BatchNo { get; set; }

        public int SerialID { get; set; }

        public string EmpID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DepartmentModel DepartmentDetails { get; set; }

        public DepartmentSectionModel DepartmentSectionDetails { get; set; }

        public CivilStatusModel CivilStatusDetails { get; set; }
    }
}
