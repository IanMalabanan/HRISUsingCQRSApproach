using HRIS.WPF.Core.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.WPF.Interfaces
{
    internal interface IApiRepository
    {
        Task<ApiResponse> GetAsync(string uri);
        Task<ApiResponse> GetAsync(string uri, bool allowanonymous);
        Task<ApiResponse> PostAsync<T>(string uri, T data);
        Task<T> PutAsync<T>(string uri, T data);
        Task<TR> PutAsync<T, TR>(string uri, T data);
        Task DeleteAsync(string uri);
        Task<R> PostAsync<T, R>(string uri, T data);
        Task<ApiResponse> PostFormUrlEncodedContentAsync(string uri, IEnumerable<KeyValuePair<string, string>> data);
        Task<string> UploadImageAsync(Stream image, string fileName, string uri);
    }
}