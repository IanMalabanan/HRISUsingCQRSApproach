using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using AspNet.Security.OpenIdConnect.Extensions;
using OpenIddict.Core;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using Novell.Directory.Ldap;
using System.Diagnostics;
using OpenIddict.Server.AspNetCore;
using HRIS.API.Model;
using OpenIddict;
using AspNet.Security.OpenIdConnect.Primitives;
using HRIS.API.System;
using Hcom.Web.Api.Core;

namespace HRIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountManager _accountManager;
        protected readonly ILogger<object> _logger;
        //protected readonly IUserService _userService;
        public AuthorizationController(
                IOptions<IdentityOptions> identityOptions,
                SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager,
                IConfiguration config,
                ILogger<AuthorizationController> logger,
                IAccountManager accountManager//,
                //IUserService userService
                )
        {
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountManager = accountManager;
            //_userService = userService;

        }

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange([FromHeader(Name = "loc")] string loc, OpenIdConnectRequest request)
        {
            var TempPassword = request.Password;
            var TempUser = request.Username;
            //try
            //{

            var curlong = "";
            var curlat = "";
            if (loc != null && loc.Split('|').Count() > 1)
            {
                curlong = loc.Split('|')[0];
                curlat = loc.Split('|')[1];
            }

            _logger.LogInformation(curlong + "," + curlat);

            string userName = request.Username;

            bool isAdUser = request.Username.Contains('\\');

            if (isAdUser) TempUser = request.Username.Split('\\')[1];


            if (request.IsPasswordGrantType())
            {

                var user = await _userManager.FindByEmailAsync(TempUser) ?? await _userManager.FindByNameAsync(TempUser);
                if (user == null)
                {
                    if (isAdUser)
                    {
                        var roleName = "AD";
                        if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
                        {
                            ApplicationRole applicationRole = new ApplicationRole(roleName, roleName);

                            var result1 = await this._accountManager.CreateRoleAsync(applicationRole, ApplicationPermissions.GetAllPermissionValues());

                            if (!result1.Item1)
                                throw new Exception($"Error creating role");
                        }


                        var x = await _accountManager.CreateUserAsync(new ApplicationUser()
                        {
                            UserName = TempUser,
                            IsEnabled = true,
                            Email = TempUser + "@filinvestland.com",
                        }, new string[] { roleName }, "HcomP@ssw0rd123");
                        user = await _userManager.FindByEmailAsync(TempUser) ?? await _userManager.FindByNameAsync(TempUser);
                    }
                    else
                    {
                        //Check HRIS Database
                        var xx = await _userService.AuthenticateUserAsync(TempUser, TempPassword);
                        if (xx != null)
                        {

                            var roleName = "FREBAS_HCOM";
                            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
                            {
                                ApplicationRole applicationRole = new ApplicationRole(roleName, roleName);

                                var result1 = await this._accountManager.CreateRoleAsync(applicationRole, new string[] { });

                                if (!result1.Item1)
                                    throw new Exception($"Error creating role");
                            }

                            var x = await _accountManager.CreateUserAsync(new ApplicationUser()
                            {
                                UserName = TempUser,
                                IsEnabled = true,
                                Email = (xx.Email == null) ? TempUser + "@gmail.com" : xx.Email,

                            }, new string[] { roleName }, TempPassword);
                            
                            //check if the creation of user is successful
                            if (x.Item1 == false)
                            {
                                var errMsg = x.Item2[0].ToString();
                                return BadRequest(new OpenIdConnectResponse
                                {
                                    Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                                    ErrorDescription = x.Item2[0].ToString()
                                });
                            }
                            user = await _userManager.FindByEmailAsync(TempUser) ?? await _userManager.FindByNameAsync(TempUser);

                        }
                        else
                        {
                            return BadRequest(new OpenIdConnectResponse
                            {
                                Error = OpenIdConnectConstants.Errors.InvalidGrant,
                                ErrorDescription = "Please check that your email and password is correct"
                            });
                        }
                    }
                }
                else //Auth Server user
                {
                    // Ensure the user is enabled.
                    if (!user.IsEnabled)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "The specified user account is disabled"
                        });
                    }

                    // Validate the username/password parameters and ensure the account is not locked out.
                    var result = await _signInManager.CheckPasswordSignInAsync(user, TempPassword, true);

                    // Ensure the user is not already locked out.
                    if (result.IsLockedOut)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "The specified user account has been suspended"
                        });
                    }

                    // Reject the token request if two-factor authentication has been enabled by the user.
                    if (result.RequiresTwoFactor)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "Invalid login procedure"
                        });
                    }

                    // Ensure the user is allowed to sign in.
                    if (result.IsNotAllowed)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "The specified user is not allowed to sign in"
                        });
                    }

                    if (!result.Succeeded)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "Please check that your email and password is correct"
                        });
                    }

                }

                // Create a new authentication ticket.
                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }//PasswordGrant
            else if (request.IsRefreshTokenGrantType())
            {
                // Retrieve the claims principal stored in the refresh token.
                var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                // Retrieve the user profile corresponding to the refresh token.
                // Note: if you want to automatically invalidate the refresh token
                // when the user password/roles change, use the following line instead:
                // var user = _signInManager.ValidateSecurityStampAsync(info.Principal);
                var user = await _userManager.GetUserAsync(info.Principal);
                if (user == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The refresh token is no longer valid"
                    });
                }

                // Ensure the user is still allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user))
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The user is no longer allowed to sign in"
                    });
                }

                // Create a new authentication ticket, but reuse the properties stored
                // in the refresh token, including the scopes originally granted.
                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported"
            });
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError(e.StackTrace);
            //    return BadRequest(new OpenIdConnectResponse
            //    {
            //        Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
            //        ErrorDescription = "The specified grant type is not supported"
            //    });
            //}
        }
        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(),
                //OpenIddictServerDefaults.AuthenticationScheme
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);


            //if (!request.IsRefreshTokenGrantType())
            //{
            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.SetScopes(new[]
            {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Phone,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            //ticket.SetResources("ECOMSS-api");

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                    continue;


                var destinations = new List<string> { OpenIdConnectConstants.Destinations.AccessToken };

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Subject && ticket.HasScope(OpenIdConnectConstants.Scopes.OpenId)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Role)) ||
                    (claim.Type == CustomClaimTypes.Permission && ticket.HasScope(OpenIddictConstants.Claims.Role)))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.SetDestinations(claim, destinations);
                //OpenIddict.Abstractions.OpenIddictExtensions.SetDestinations(claim,destinations);
                //claim.SetDestinations(destinations);
            }


            var identity = principal.Identity as ClaimsIdentity;


            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Profile))
            {
                if (!string.IsNullOrWhiteSpace(user.JobTitle))
                    AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.AddClaim(identity, CustomClaimTypes.JobTitle, user.JobTitle, OpenIdConnectConstants.Destinations.IdentityToken);
                //identity.AddClaim(CustomClaimTypes.JobTitle, user.JobTitle, OpenIdConnectConstants.Destinations.IdentityToken);

                if (!string.IsNullOrWhiteSpace(user.FullName))
                    AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.AddClaim(identity, CustomClaimTypes.FullName, user.FullName, OpenIdConnectConstants.Destinations.IdentityToken);
                //identity.AddClaim(CustomClaimTypes.FullName, user.FullName, OpenIdConnectConstants.Destinations.IdentityToken);

                if (!string.IsNullOrWhiteSpace(user.Configuration))
                    AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.AddClaim(identity, CustomClaimTypes.Configuration, user.Configuration, OpenIdConnectConstants.Destinations.IdentityToken);
                //identity.AddClaim(CustomClaimTypes.Configuration, user.Configuration, OpenIdConnectConstants.Destinations.IdentityToken);



            }

            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Email))
            {
                if (!string.IsNullOrWhiteSpace(user.Email))
                    AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.AddClaim(identity, CustomClaimTypes.Email, user.Email, OpenIdConnectConstants.Destinations.IdentityToken);
                //identity.AddClaim(CustomClaimTypes.Email, user.Email, OpenIdConnectConstants.Destinations.IdentityToken);
            }

            if (ticket.HasScope(OpenIdConnectConstants.Scopes.Phone))
            {
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    AspNet.Security.OpenIdConnect.Extensions.OpenIdConnectExtensions.AddClaim(identity, CustomClaimTypes.Phone, user.PhoneNumber, OpenIdConnectConstants.Destinations.IdentityToken);
                //identity.AddClaim(CustomClaimTypes.Phone, user.PhoneNumber, OpenIdConnectConstants.Destinations.IdentityToken);
            }


            //ticket.SetAccessTokenLifetime(TimeSpan.FromHours(25));
            //ticket.SetIdentityTokenLifetime(TimeSpan.FromHours(25));
            ticket.SetAccessTokenLifetime(TimeSpan.FromDays(25));
            ticket.SetIdentityTokenLifetime(TimeSpan.FromDays(25));

            return ticket;
        }
    }
}
