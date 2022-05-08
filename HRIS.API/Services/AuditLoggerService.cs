//using Blazored.SessionStorage;
using HRIS.Application.AuditTrailCQRS.Command;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Domain.Entities;
using HRIS.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.API.Services
{
    public class AuditLoggerService : IAuditLoggerService
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        //private readonly ISessionStorageService _sessionStorageService;

        private readonly ILocationService _locationService;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IDateTime _dateTime;
        private readonly ISender _mediator;
        private AuthenticationState _authState;
        private readonly ILogger _logger;
        public AuditLoggerService(AuthenticationStateProvider authStateProvider, IDateTime dateTime, ISender mediator, ILocationService locationService
            //, ISessionStorageService sessionStorageService
            , ILogger logger)
        {
            _authStateProvider = authStateProvider;
            _dateTime = dateTime;
            _mediator = mediator;
            _locationService = locationService;
            //_sessionStorageService = sessionStorageService;
            _logger = logger;
        }

        public async Task<AuditTrailLog> AddAuditLogAsync(string remarks, string PageAccess)
        {
            var place = string.Empty;
            var ip = string.Empty;
            try
            {
                //var location = await _locationService.GetClientIPAddress();//_locationService.GetLocation();
                //ip = location.ip;

                //var latitude = await _sessionStorageService.GetItemAsStringAsync("latitude");

                //var longitude = await _sessionStorageService.GetItemAsStringAsync("longitude");

                //var alatitude = Convert.FromBase64String(latitude);
                //var alongitude = Convert.FromBase64String(longitude);

                //var blatitude = Encoding.UTF8.GetString(alatitude);
                //var blongitude = Encoding.UTF8.GetString(alongitude);


                //var locationDataDetails = await _locationService.GetAddressByLatitudeAndLongitude(
                //    Convert.ToDecimal(
                //        //location.locationData.TryGetValue("lat")
                //        blatitude),
                //    Convert.ToDecimal(
                //        //location.locationData.TryGetValue("lng")
                //        blongitude)

                //    );

                //place = locationDataDetails.City + ", " + locationDataDetails.Province + "," +
                //            //location.locationData.TryGetValue("countryName")
                //            locationDataDetails.countryName;

            }
            catch (Exception e)
            {
                _logger.Error($"{_dateTime.Now.ToString("yyyyMMddhhmmss")} : {e.Message}");
                _logger.Error($"{_dateTime.Now.ToString("yyyyMMddhhmmss")} : {e.StackTrace}");
            }


            _authState = await _authStateProvider.GetAuthenticationStateAsync();

            var _username = _authState.User.Claims.Where(q => q.Type == "preferred_username").FirstOrDefault()?.Value;

            var log = new AuditLogsModel
            {
                PageAccessed = PageAccess,
                Remarks = remarks,
                Timestamp = _dateTime.Now,
                Username = _username,
                Location = place,
                IPAddress = ip
            };

            var _result = await _mediator.Send(new CreateAuditTrail { Log = log });

            return _result;
        }

        public void Write(LogEvent logEvent)
        {
            throw new NotImplementedException();
        }
    }
}
