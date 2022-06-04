using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HRIS.WPF.Utilities
{
    public class APIConstant
    {
        public static string BaseUrl { get; set; } = ConfigurationManager.AppSettings["BaseUrl"].ToString();

        public static string AccessToken { get; set; }
    }
}
