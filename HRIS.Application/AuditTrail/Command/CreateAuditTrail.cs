using AutoMapper;
using HRIS.Domain.Entities;
using HRIS.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Application.AuditTrailFunction.Command
{
    public class CreateAuditTrail : IRequest<AuditTrailLogs>
    {
        public AuditLogsModel Log { get; set; }
    }

    public class CreateAuditTrailHandler : IRequestHandler<CreateAuditTrail, AuditTrailLogs>
    {
        private readonly IAuditTrailsRepository _auditTrailsRepository;
        private readonly IMapper _mapper;


        public CreateAuditTrailHandler(IAuditTrailsRepository auditTrailsRepository, IMapper mapper)
        {
            _auditTrailsRepository = auditTrailsRepository;
            _mapper = mapper;
        }

        public async Task<AuditTrailLogs> Handle(CreateAuditTrail request, CancellationToken cancellationToken)
        {

            var _entity = _mapper.Map<AuditTrailLogs>(request.Log);

            var _result = await _auditTrailsRepository.AddAsync(_entity);

            return _result;
        }
    }
    
}
