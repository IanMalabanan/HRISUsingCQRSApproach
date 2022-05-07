using HRIS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.Entities.Common
{
    public class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
