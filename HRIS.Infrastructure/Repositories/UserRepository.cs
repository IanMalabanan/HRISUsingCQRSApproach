using GlobalBusinessComponents;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Repositories
{
    public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UserRepository(ApplicationDBContext dbContext, IDateTime dateTimeService) : base(dbContext, dateTimeService)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            try
            {
                var loginHelper = new clsObjectControl();
                var encryptedPassword = loginHelper.EncryptDecrypt(clsObjectControl.ProcessType.Encrypt, password, "1983-01-14");

                var _usr = await _dbContext.Users.Where(u => u.UserName.ToLower() == userName.ToLower() && u.Password == encryptedPassword).FirstOrDefaultAsync();

                if (_usr == null)
                    return null;
                else if (!_usr.Active)
                    return null;

                return await GetUserInfoAsync(_usr.UserName);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public async Task<User> GetUserInfoAsync(string userName)
        {
            try
            {
                var _usr = await _dbContext.Users.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

                if (_usr == null)
                    return new User();

                return _usr;

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        //public async Task<Business.Entities.User> GetUserInfoBaseOnDateAsync(string userName, DateTime ReferenceDate)
        //{
        //    try
        //    {
        //        using (var cntxt = new FrebasContext())
        //        {
        //            var _usr = await cntxt.User.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

        //            if (_usr == null)
        //                return null;

        //            ///if Contractor
        //            if (_usr.UserTypeCode == "CONT")
        //            {

        //                var _dteToday = ReferenceDate.Date;
        //                var _dteTomorrow = DateTime.Today.AddDays(1).Date;
        //                var _userTypeReference = Convert.ToInt32(_usr.UserTypeReference);
        //                var _cont = await (from ctp in cntxt.ContractorPersonnel.Where(c => c.ID == _userTypeReference)
        //                                   join cau in cntxt.ContractorPersonnelAuthorized.Where(a => (a.DateFrom <= _dteToday && (a.DateTo ?? _dteTomorrow) >= _dteToday))
        //                                   //join cau in cntxt.ContractorPersonnelAuthorized.Where(a => DbFunctions.TruncateTime(a.DateTo ?? _dteTomorrow) >= _dteToday)
        //                                   on new
        //                                   {
        //                                       contractorCode = ctp.ContractorCode,
        //                                       refId = ctp.ID
        //                                   } equals
        //                                   new
        //                                   {
        //                                       contractorCode = cau.ContractorCode,
        //                                       refId = cau.AuthorizedPersonnelID
        //                                   }
        //                                   select new Business.Entities.User
        //                                   {
        //                                       Id = _usr.UserName,
        //                                       FullName = _usr.FullName,
        //                                       Email = ctp.EmailAddress,
        //                                       IsActive = _usr.Active,
        //                                       RoleCode = _usr.UserTypeCode
        //                                   }).FirstOrDefaultAsync();

        //                if (_cont == null)
        //                    throw new Exception("Contractor Personnel is not authorized.");

        //                return _cont;
        //            }
        //            else if (_usr.UserTypeCode == "EMPL")
        //            {

        //                var _dteToday = DateTime.Today;
        //                var _dteTomorrow = DateTime.Today.AddDays(1).Date;
        //                return await (from upr in cntxt.UserProjectRole.Where(u => u.UserName == userName && (u.DateFrom <= ReferenceDate && (u.DateTo ?? _dteTomorrow) >= ReferenceDate))
        //                              join prl in cntxt.ProjectRole
        //                              on upr.ProjectRoleCode equals prl.Code
        //                              select new Business.Entities.User
        //                              {
        //                                  Id = _usr.UserName,
        //                                  FullName = _usr.FullName,
        //                                  Email = _usr.EmailAddress,
        //                                  RoleCode = upr.ProjectRoleCode,
        //                                  IsActive = _usr.Active,
        //                                  InspectionRadius = prl.Radius
        //                              }).FirstOrDefaultAsync();
        //            }
        //            else
        //            {
        //                return new Business.Entities.User
        //                {
        //                    Id = _usr.UserName,
        //                    FullName = _usr.FullName,
        //                    Email = _usr.EmailAddress,
        //                    RoleCode = _usr.UserTypeCode,
        //                    IsActive = _usr.Active
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException(ex.Message, ex);
        //    }
        //}

        //public async Task<double> GetUserInspectionRadius(string RoleCode)
        //{
        //    using (var cntxt = new FrebasContext())
        //    {
        //        return await cntxt.ProjectRole.Where(r => r.Code == RoleCode).Select(r => (double)(r.Radius ?? 0)).FirstOrDefaultAsync();
        //    }
        //}

    }
}
