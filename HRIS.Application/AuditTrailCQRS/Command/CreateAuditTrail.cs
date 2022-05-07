using AutoMapper;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Domain.Entities;
using HRIS.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.AuditTrailCQRS.Command
{
    public class CreateAuditTrail : IRequest<AuditTrailLog>
    {
        public AuditLogsModel Log { get; set; }
    }

    public class CreateAuditTrailHandler : IRequestHandler<CreateAuditTrail, AuditTrailLog>
    {
        private readonly IAuditTrailsRepository _auditTrailsRepository;
        private readonly IMapper _mapper;


        public CreateAuditTrailHandler(IAuditTrailsRepository auditTrailsRepository, IMapper mapper)
        {
            _auditTrailsRepository = auditTrailsRepository;
            _mapper = mapper;
        }

        public async Task<AuditTrailLog> Handle(CreateAuditTrail request, CancellationToken cancellationToken)
        {

            var _entity = _mapper.Map<AuditTrailLog>(request.Log);

            var _result = await _auditTrailsRepository.AddAsync(_entity);

            return _result;
        }
    }
    
}
