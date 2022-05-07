using HRIS.Domain.Entities;
using HRIS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Application
{
    public interface IAuditLoggerService
    {
        Task<AuditTrailLog> AddAuditLogAsync(string remarks, string PageAccess);
    }
}
