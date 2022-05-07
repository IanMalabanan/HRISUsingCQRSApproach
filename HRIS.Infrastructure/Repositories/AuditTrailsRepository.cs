using HRIS.Application.Common.Interfaces.Application;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Repositories
{
    public class AuditTrailsRepository : GenericRepositoryAsync<AuditTrailLog>, IAuditTrailsRepository
    {
        private readonly ApplicationDBContext _dbContext;
        //private readonly ICurrentUserService _currentUserService;

        public AuditTrailsRepository(ApplicationDBContext dbContext, IDateTime dateTimeService
            //, ICurrentUserService currentUserService
            ) : base(dbContext, dateTimeService//, currentUserService
                                               )
        {

            SetGetQuery(dbContext.AuditTrails.Where(q => q.IsDeleted == false));
            _dbContext = dbContext;
            //_currentUserService = currentUserService;

        }

        public override async Task<AuditTrailLog> AddAsync(AuditTrailLog entity)
        {
            foreach(EntityEntry entry in _dbContext.ChangeTracker.Entries())
            {
                if(entry.Entity.GetType() != typeof(AuditTrailLog))
                {
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
            }
            var _result = await base.AddAsync(entity);

            return _result;
        }
    }
}
