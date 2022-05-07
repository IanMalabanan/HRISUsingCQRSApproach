using CorrelationId.Abstractions;
using HRIS.Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Behaviours
{
    //public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly ILogger<TRequest> _logger;
    //    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    //    public UnhandledExceptionBehaviour(ILogger<TRequest> logger, ICorrelationContextAccessor correlationContextAccessor)
    //    {
    //        _logger = logger;
    //        _correlationContextAccessor = correlationContextAccessor;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        try
    //        {
    //            return await next();
    //        }
    //        catch (ForbiddenAccessException ex)
    //        {
    //            var requestName = typeof(TRequest).Name;

    //            _logger.LogError(ex, "Cti.Permits.WebUI Request: Forbidden Access Exception for Request {Name} {{\"CorrelationId\":\"{Correlation}\"}} {@Request} {{\"Policy\":\"{Policy}\"}}",
    //                requestName, _correlationContextAccessor.CorrelationContext.CorrelationId, request, ex.Policy);

    //            throw;
    //        }
    //        catch (ValidationException ex)
    //        {
    //            var requestName = typeof(TRequest).Name;

    //            _logger.LogError(ex, "Cti.Permits.WebUI Request: Validation Exception for Request {Name} {{\"CorrelationId\":\"{Correlation}\"}} {@Request} \n{{\"Errors\":\"{@Errors}\"}}",
    //                requestName, _correlationContextAccessor.CorrelationContext.CorrelationId, request, ex.Errors);

    //            throw;
    //        }
    //        catch (Exception ex)
    //        {
    //            var requestName = typeof(TRequest).Name;

    //            _logger.LogError(ex, "Cti.Permits.WebUI Request: Unhandled Exception for Request {Name} {@Request}", 
    //                requestName, request);

    //            throw;
    //        }
    //    }
    //}
}
