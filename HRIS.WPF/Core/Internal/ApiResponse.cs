using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace HRIS.WPF.Core.Internal
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public T GetValue<T>()
        {
            T _return;

            _return = JsonConvert.DeserializeObject<T>(Content);

            if (!IsTransformed(_return))
                return default(T);

            return _return;
        }

        public T TryGetValue<T>()
        {
            try
            {
                T _return;

                _return = JsonConvert.DeserializeObject<T>(Content);

                if (!IsTransformed(_return))
                    return default(T);

                return _return;
            }
            catch (JsonSerializationException)
            {
                return default(T);
            }
        }

        public T GetValueOnlyOnSuccess<T>()
        {
            if (!IsSuccessStatusCode)
                return default(T);

            T _return;

            _return = JsonConvert.DeserializeObject<T>(Content);

            if (!IsTransformed(_return))
                return default(T);

            return _return;
        }

        private bool IsTransformed<TR>(TR value)
        {
            if (!value.GetType().IsClass) return true;

            PropertyInfo[] properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in properties)
            {
                var item = p.GetValue(value);

                if (item != null) { return true; }
            }

            return false;
        }
    }
}
