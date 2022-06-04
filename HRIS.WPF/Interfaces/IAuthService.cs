using HRIS.Domain.ViewModels;
using System.Threading.Tasks;

namespace HRIS.WPF.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
