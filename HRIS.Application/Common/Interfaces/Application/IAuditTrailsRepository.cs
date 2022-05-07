using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.Interfaces.Application
{
    public interface IAuditTrailsRepository : IGenericRepositoryAsync<AuditTrailLog>
    {
        
    }
}
