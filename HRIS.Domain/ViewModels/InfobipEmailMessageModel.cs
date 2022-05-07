using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Domain.ViewModels
{
    public class InfobipEmailMessageModel
    {
        public string Id { get; set; }
        public string From { get; set; }

        public string To { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string Format { get; set; }
        public IEnumerable<Dictionary<string, string>> Payload { get; set; }



    }
}
