using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.ViewModels
{
    public class NotificationsModel
    {
        public int NotifId { get; set; }
        public string Code { get; set; }
        public int StatusCode { get; set; }
        public string JSONResponse { get; set; }
    }
}
