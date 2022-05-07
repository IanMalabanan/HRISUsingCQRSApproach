using HRIS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRIS.Domain.Entities
{
    [Table("t_AuditTrails", Schema = "dbo")]

    public class AuditTrailLogs : SoftDeletableEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string PageAccessed { get; set; }
        public string Remarks { get; set; }
        public string IPAddress { get; set; }
    }
}
