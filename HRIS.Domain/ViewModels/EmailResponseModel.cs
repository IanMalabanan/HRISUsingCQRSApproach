using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.ViewModels
{
    public class EmailResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ProviderMsgId { get; set; }
        public string Recipients { get; set; }
        public string RequestMsgId { get; set; }
        public string ResponseMessage { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
    }
}
