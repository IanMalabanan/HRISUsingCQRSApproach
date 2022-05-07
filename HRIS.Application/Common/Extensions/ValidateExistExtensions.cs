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


        //public static void ValidateRequired(this PPPaymentFor entity)
        //{
        //    List<string> RequiredFields = new List<string> { "Name", "Description" };
        //    List<string> emptyFields = new List<string>();
        //    PropertyInfo[] properties = typeof(PPPaymentFor).GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (RequiredFields.Contains(property.Name) && (String.IsNullOrEmpty(property.GetValue(entity)?.ToString())))
        //        {
        //            emptyFields.Add(property.Name);
        //        }
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}


        //public static void ValidateRequired(this PPRoles entity)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (string.IsNullOrEmpty(entity.Role))
        //    {
        //        emptyFields.Add("Role");
        //    }
        //    if (entity.UserAccess == null || entity.UserAccess.Count == 0)
        //    {
        //        emptyFields.Add("UserAccess");
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPProjects entity, bool excemptValidation = false)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (String.IsNullOrEmpty(entity.SystemName))
        //    {
        //        emptyFields.Add("SystemName");
        //    }
        //    if (String.IsNullOrEmpty(entity.DisplayName))
        //    {
        //        emptyFields.Add("DisplayName");
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPUsers entity, bool excemptValidation = false)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (String.IsNullOrEmpty(entity.Username))
        //    {
        //        emptyFields.Add("Username");
        //    }
        //    if (String.IsNullOrEmpty(entity.Email))
        //    {
        //        emptyFields.Add("Email");
        //    }
        //    if (!excemptValidation)
        //    {
        //        if (entity.PPAccounts == null || entity.PPAccounts.Count == 0)
        //        {
        //            emptyFields.Add("Accounts");
        //        }
        //    }

        //    if (entity.UserRoles == null)
        //    {
        //        emptyFields.Add("Role");
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPPaymentChannel entity, bool excemptValidation = false)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (String.IsNullOrEmpty(entity.Code))
        //    {
        //        emptyFields.Add("Code");
        //    }
        //    if (String.IsNullOrEmpty(entity.Name))
        //    {
        //        emptyFields.Add("Name");
        //    }
        //    if (entity.MinimumAmount == 0)
        //    {
        //        emptyFields.Add("MinimumAmount");
        //    }
        //    if (String.IsNullOrEmpty(entity.ChannelType))
        //    {
        //        emptyFields.Add("ChannelType");
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPPaymentFor entity, bool excemptValidation = false)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (String.IsNullOrEmpty(entity.Code))
        //    {
        //        emptyFields.Add("Code");
        //    }
        //    if (String.IsNullOrEmpty(entity.Name))
        //    {
        //        emptyFields.Add("Name");
        //    }


        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPSystemInfo entity)
        //{
        //    List<string> emptyFields = new List<string>();
        //    if (String.IsNullOrEmpty(entity.SystemName))
        //    {
        //        emptyFields.Add("System Name");
        //    }
        //    if (String.IsNullOrEmpty(entity.Description))
        //    {
        //        emptyFields.Add("Description");
        //    }
        //    if (String.IsNullOrEmpty(entity.URL))
        //    {
        //        emptyFields.Add("URL");
        //    }
        //    if (String.IsNullOrEmpty(entity.IpAddress))
        //    {
        //        emptyFields.Add("IP Address");
        //    }
        //    if (entity.PPAccountsId == 0)
        //    {
        //        emptyFields.Add("Accounts");
        //    }

        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        //public static void ValidateRequired(this PPPaymentRequest entity)
        //{
        //    List<string> emptyFields = new List<string>();


        //    if (string.IsNullOrEmpty(entity.PPPaymentDetails.Payee))
        //    {
        //        emptyFields.Add("Payee");
        //    }
        //    if (string.IsNullOrEmpty(entity.SystemRefId))
        //    {
        //        emptyFields.Add("SystemRefId");
        //    }
        //    if (string.IsNullOrEmpty(entity.PPPaymentDetails.FirstName))
        //    {
        //        emptyFields.Add("FirstName");
        //    }
        //    if (string.IsNullOrEmpty(entity.PPPaymentDetails.LastName))
        //    {
        //        emptyFields.Add("LastName");
        //    }
        //    if (string.IsNullOrEmpty(JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.PPPaymentDetails.UDFJson).TryGetValue("email")))
        //    {
        //        emptyFields.Add("Email");
        //    }
        //    if(entity.PPPaymentDetails.PaymentAmount == 0)
        //    {
        //        emptyFields.Add("PaymentAmount");
        //    }


        //    if (emptyFields.Count() > 0)
        //    {
        //        throw new UnsatisfiedRequiredFieldsException($"{String.Join(", ", emptyFields)} are required fields.");
        //    }
        //}

        #endregion

    }
}
