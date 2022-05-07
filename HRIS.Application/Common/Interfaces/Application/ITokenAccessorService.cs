using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces.Application
{
    public interface ITokenAccessorService
    {
        Task<string> GetAccessTokenAsync();
    }
}
