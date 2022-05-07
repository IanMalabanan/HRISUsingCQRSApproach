using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.ViewModels
{
    public class AuditTrailsModel
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
