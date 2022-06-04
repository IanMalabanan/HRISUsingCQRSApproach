using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
namespace HRIS.WPF.Core
{
    public class BaseService
    {
        protected IBlobCache Cache;

        //public BaseService(IBlobCache cache)
        //{
        //    Cache = cache ?? BlobCache.LocalMachine;
        //}


        public BaseService()
        {
            Cache = BlobCache.LocalMachine;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public void InvalidateCache()
        {
            Cache.InvalidateAll();
        }
    }
}
