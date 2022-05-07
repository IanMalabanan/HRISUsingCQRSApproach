using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.ViewModels
{
    public class AuditLogsModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string PageAccessed { get; set; }
        public string Remarks { get; set; }
        public string IPAddress { get; set; }
    }
}
