using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hcom.App.Services.Core
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
        Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        Task<TR> PutAsync<T, TR>(string uri, T data, string authToken = "");
        Task DeleteAsync(string uri, string authToken = "");
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = "");
        Task<string> UploadImageAsync(Stream image, string fileName, string uri, string authToken = "");
    }
}
