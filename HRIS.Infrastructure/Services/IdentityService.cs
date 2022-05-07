//using AspNet.Security.OpenIdConnect.Primitives;
using HRIS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    //public class IdentityService : IIdentityService
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;

    //    public IdentityService(IHttpContextAccessor httpContextAccessor)
    //    {
    //        _httpContextAccessor = httpContextAccessor;
    //    }

    //    public Task<bool> AuthorizeAsync(string userId, string policyName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<string> GetUserNameAsync(string userId)
    //    {
    //        throw new NotImplementedException();
    //        //return Task.FromResult(_httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Username));
    //    }

    //    public Task<bool> IsInRoleAsync(string userId, string role)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
