using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Domain.Entities;
using HRIS.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;


namespace HRIS.Infrastructure
{
    public class ApplicationDBContext : DbContext
    {
        //private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDBContext(
            DbContextOptions options,
            //ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
           // _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }
        //public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<CivilStatus> CivilStats { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentSection> DepartmentSections { get; set; }

        public DbSet<AuditTrailLog> AuditTrails { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var _currentUser = "";//!string.IsNullOrEmpty(_currentUserService.UserId) ? _currentUserService.UserId : "Backend";


                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy = "";
                            entry.Entity.CreatedDate = _dateTime.Now;
                            break;

                        case EntityState.Modified:
                            entry.Entity.LastModifiedBy = _currentUser;
                            entry.Entity.LastModifiedDate = _dateTime.Now;
                            break;
                    }
                }

                var result = await base.SaveChangesAsync(cancellationToken);

                await DispatchEvents();

                return result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentSection>().HasKey(col => new { col.Code, col.DepartmentCode});

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();

            base.OnModelCreating(modelBuilder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }

    }
}
