using HRIS.Domain.Entities.Common;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Application
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
