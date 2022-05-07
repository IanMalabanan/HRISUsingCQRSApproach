using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HRIS.API.Services
{
    public class AuthService //: IAuthService
    {
        //private AuthenticationStateProvider _authStateProvider;
        //private NavigationManager _uriHelper;
        //private IUserService _userService;
        //private string _sid;
        //private string _username;
        //private PPUsers _user;
        //private CurrentModule _currentModule = new CurrentModule();
        //public AuthService(AuthenticationStateProvider authStateProvider, NavigationManager uriHelper, IUserService userService)
        //{
        //    _authStateProvider = authStateProvider;
        //    _uriHelper = uriHelper;
        //    _userService = userService;

        //}

        //public async Task CheckActivated()
        //{
        //    var authState = await _authStateProvider.GetAuthenticationStateAsync();
        //    var user = authState.User;
        //    await ModuleChecker();

        //    if (user.Identity.IsAuthenticated)
        //    {
        //        _sid = user.Claims.Where(q => q.Type == "sid").FirstOrDefault().Value;
        //        _username = user.Claims.Where(q => q.Type == "username").FirstOrDefault().Value;

                
        //        _user = await _userService.GetUserByObject(_sid);
        //        if (_user == null)
        //        {
        //            _user = await _userService.GetUserByUsername(_username);
        //        }
                

        //        if (_user != null && _sid != _user.UserObject)
        //        {
        //            _user.UserObject = _sid;

        //            //await _userService.UpdateUser(_user);
        //        }
        //    }
        //    else
        //    {
        //        if (_currentModule.Module != "activate")
        //        {
        //            _uriHelper.NavigateTo("login", true);
        //        }
        //    }
        //}

        

        //public async Task CheckRole()
        //{
        //    if (_user != null)
        //    {
        //        if (_currentModule.Module != null)
        //        {
        //            var _access = _user.UserRoles.Roles.UserAccess.Where(q => q.Module.ToLower() == _currentModule.Module.ToLower() && q.Access == "W").FirstOrDefault();

        //            if (_access == null)
        //            {
                        
        //                _uriHelper.NavigateTo("", true);
        //            }
        //        }
        //    }
        //}

        //public async Task<List<PPUserAccounts>> GetCompanies()
        //{
        //    if (_user != null)
        //    {
        //        return _user.PPAccounts;
        //    }
        //    return new List<PPUserAccounts>();
        //}

        //public async Task<List<PPUserAccess>> GetRole()
        //{
        //    return _user.UserRoles.Roles.UserAccess.Where(q => q.Access == "W").ToList();
        //}

        //private async Task ModuleChecker()
        //{
            
        //    var validator = _uriHelper.Uri.Split("/")[3];
        //    if (validator != string.Empty)
        //    {
        //        _currentModule.Module = validator;
        //        try
        //        {
        //            _currentModule.SubModule = _uriHelper.Uri.Split("/")[4];
        //        }
        //        catch(Exception e)
        //        {
        //            _currentModule.SubModule = "";
        //        }
                
        //    }


        //}
    }
}
