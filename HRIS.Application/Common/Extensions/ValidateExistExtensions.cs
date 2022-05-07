using HRIS.Domain.Entities;
using HRIS.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class ValidateExistExtensions
    {

        #region "VALIDATES REQUIRED"
        public static void ValidateRequired(this Employee entity)
        {
            List<string> RequiredFields = new List<string> { "EmpID", "LastName", "FirstName", "DepartmentCode", "DepartmentSectionCode" };
            List<string> emptyFields = new List<string>();
            PropertyInfo[] properties = typeof(Employee).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (RequiredFields.Contains(property.Name) && (String.IsNullOrEmpty((string)property.GetValue(entity))))
                {
                    emptyFields.Add(property.Name);
                }
            }

            if (emptyFields.Count() > 0)
            {
                throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
            }
        }

        #endregion

    }
}
