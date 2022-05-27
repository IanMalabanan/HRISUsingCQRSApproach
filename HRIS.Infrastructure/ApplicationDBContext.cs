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
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDBContext(
            DbContextOptions options,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<CivilStatus> CivilStats { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentSection> DepartmentSections { get; set; }

        public DbSet<AuditTrailLog> AuditTrails { get; set; }

        public DbSet<User> Users { get; set; }

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
