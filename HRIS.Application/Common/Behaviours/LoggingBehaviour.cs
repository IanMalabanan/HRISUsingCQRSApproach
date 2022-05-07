using CorrelationId.Abstractions;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        //private readonly ICurrentUserService _currentUserService;
        //private readonly IIdentityService _identityService;
        //private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public LoggingBehaviour(ILogger<TRequest> logger //,ICurrentUserService currentUserService
                                                          //, IIdentityService identityService
                                                          )
        {
            
            //ICorrelationContextAccessor correlationContextAccessor
            _logger = logger;
            //_currentUserService = currentUserService;
            //_identityService = identityService;
            //_correlationContextAccessor = correlationContextAccessor;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            //var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    //userName = await _identityService.GetUserNameAsync(userId);
            //}


            //, _correlationContextAccessor.CorrelationContext.CorrelationId
            _logger.LogInformation("HRIS Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, ""//userId
                             , userName, request);
        }
    }
}
