using HRIS.Domain.Entities;
using HRIS.Domain.Enums;
using HRIS.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common.Extensions
{
    public static class EntityValidationExtentions
    {
        #region "VALIDATES EXISTANCE"
        //public static void ValidateExists(this PPAccounts entity, DbSet<PPAccounts> context, CRUDType cRUDType)
        //{
        //    PPAccounts _result = null;

        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.Name == entity.Name).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => q.Name == entity.Name && q.Id != entity.Id).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        throw new EntityAlreadyExistException($"Account with name {entity.Name} already exist.");
        //    }
        //}

        //public static void ValidateExists(this PPProjects entity, DbSet<PPProjects> context, CRUDType cRUDType)
        //{
        //    PPProjects _result = null;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.SystemName == entity.SystemName && q.PPSystemInfoId == entity.PPSystemInfoId).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => (q.SystemName == entity.SystemName && q.Id != entity.Id) && q.PPSystemInfoId == entity.PPSystemInfoId).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        throw new EntityAlreadyExistException($"Account with name {entity.SystemName} already exist.");
        //    }

        //}

        //public static void ValidateExists(this PPPaymentFor entity, DbSet<PPPaymentFor> context, CRUDType cRUDType)
        //{
        //    PPPaymentFor _result = null;
        //    string _alreadyExist = string.Empty;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.Code == entity.Code || q.Name == entity.Name).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => (q.Code == entity.Code || q.Name == entity.Name) && q.Id != entity.Id).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        List<string> _sameFields = new List<string>();
        //        if (_result.Code == entity.Code)
        //        {
        //            _sameFields.Add($"Code {entity.Code}");
        //        }
        //        if (_result.Name == entity.Name)
        //        {
        //            _sameFields.Add($"Name {entity.Name}");
        //        }

        //        _alreadyExist = string.Join(" and ", _sameFields);

        //        throw new EntityAlreadyExistException($"Payment Trans with {_alreadyExist} already exist.");
        //    }
        //}



        public static void ValidateExists(this Employee entity, DbSet<Employee> context, CRUDType cRUDType)
        {
            Employee _result = null;

            switch (cRUDType)
            {
                case CRUDType.CREATE:
                    _result = context.Where(q => q.EmpID == entity.EmpID).Where(q => q.IsDeleted == false).FirstOrDefault();
                    break;
                case CRUDType.UPDATE:
                    _result = context.Where(q => q.EmpID == entity.EmpID).Where(q => q.IsDeleted == false).FirstOrDefault();
                    break;
            }

            if (_result != null)
            {
                throw new EntityAlreadyExistException($"Employee with name {entity.FirstName} {entity.LastName} already exist.");
            }
        }

        //public static void ValidateExists(this PPUsers entity, DbSet<PPUsers> context, CRUDType cRUDType)
        //{
        //    PPUsers _result = null;
        //    string _alreadyExist = string.Empty;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.Username == entity.Username || q.Email == entity.Email).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => (q.Username == entity.Username || q.Email == entity.Email) && q.Id != entity.Id).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        List<string> _sameFields = new List<string>();
        //        if (_result.Username == entity.Username)
        //        {
        //            _sameFields.Add($"Username {entity.Username}");
        //        }
        //        if (_result.Email == entity.Email)
        //        {
        //            _sameFields.Add($"Email {entity.Email}");
        //        }

        //        _alreadyExist = string.Join(" and ", _sameFields);

        //        throw new EntityAlreadyExistException($"user with {_alreadyExist} already exist.");
        //    }
        //}

        //public static void ValidateExists(this PPPaymentProcessor entity, DbSet<PPPaymentProcessor> context, CRUDType cRUDType)
        //{
        //    PPPaymentProcessor _result = null;
        //    string _alreadyExist = string.Empty;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.Code == entity.Code).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => q.Id == entity.Id).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }
        //}

        //public static void ValidateExists(this PPPaymentChannel entity, DbSet<PPPaymentChannel> context, CRUDType cRUDType)
        //{
        //    PPPaymentChannel _result = null;
        //    string _alreadyExist = string.Empty;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context.Where(q => q.Code == entity.Code || q.Name == entity.Name).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => (q.Code == entity.Code || q.Name == entity.Name) && q.Id != entity.Id).Where(q => q.IsDeleted == false).FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        List<string> _sameFields = new List<string>();
        //        if (_result.Code == entity.Code)
        //        {
        //            _sameFields.Add($"Code {entity.Code}");
        //        }
        //        if (_result.Name == entity.Name)
        //        {
        //            _sameFields.Add($"Name {entity.Name}");
        //        }

        //        _alreadyExist = string.Join(" and ", _sameFields);

        //        throw new EntityAlreadyExistException($"Payment Channel with {_alreadyExist} already exist.");
        //    }
        //}

        //public static void ValidateExists(this PPSystemInfo entity, DbSet<PPSystemInfo> context, CRUDType cRUDType)
        //{
        //    PPSystemInfo _result = null;
        //    string _alreadyExist = string.Empty;
        //    switch (cRUDType)
        //    {
        //        case CRUDType.CREATE:
        //            _result = context
        //                .Where(q => (q.URL == entity.URL || q.IpAddress == entity.IpAddress) && q.PPAccountsId == entity.PPAccountsId)
        //                .Where(q => q.IsDeleted == false).AsSplitQuery().AsNoTracking().FirstOrDefault();
        //            break;
        //        case CRUDType.UPDATE:
        //            _result = context.Where(q => ((q.URL == entity.URL || q.IpAddress == entity.IpAddress) && q.PPAccountsId == entity.PPAccountsId) && q.Id != entity.Id).Where(q => q.IsDeleted == false).AsSplitQuery().AsNoTracking().FirstOrDefault();
        //            break;

        //    }


        //    if (_result != null)
        //    {
        //        List<string> _sameFields = new List<string>();
        //        if (_result.URL == entity.URL)
        //        {
        //            _sameFields.Add($"System Name {entity.SystemName}");
        //        }
        //        if (_result.IpAddress == entity.IpAddress)
        //        {
        //            _sameFields.Add($"Ip Address {entity.IpAddress}");
        //        }

        //        _alreadyExist = string.Join(" and ", _sameFields);

        //        throw new EntityAlreadyExistException($"user with {_alreadyExist} already exist.");
        //    }
        //}
        #endregion

        #region "VALIDATES FORMAT"


        #endregion

        #region "VALIDATES PAYMENT REQUEST"
        //public static async void ValidateExpiredPaymentRequest(this PPPaymentRequest entity, DbSet<PPPaymentRequest> context)
        //{
        //    var _result = await context.Where(q => q.Id == entity.Id && q.Expiry >= DateTime.Now).FirstOrDefaultAsync();
        //    if (_result == null)
        //    {
        //        throw new PaymentRequestExpiredException("Invalid Payment Request: Expired.");
        //    }
        //}
        #endregion
    }
}
