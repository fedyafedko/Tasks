using Tasks.Entities;

namespace Tasks.BLL.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(User user);
    }
}
