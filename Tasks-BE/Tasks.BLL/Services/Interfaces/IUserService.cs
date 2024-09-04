using Tasks.Common.DTOs;
using Tasks.Common.Responses;

namespace Tasks.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthSuccessResponse> LoginAsync(LoginDTO dto);
        Task<AuthSuccessResponse> RegisterAsync(RegisterDTO dto);
    }
}
