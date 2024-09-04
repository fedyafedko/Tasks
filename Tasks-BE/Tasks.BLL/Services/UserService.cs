using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tasks.BLL.Services.Interfaces;
using Tasks.Common.DTOs;
using Tasks.Common.Exceptions;
using Tasks.Common.Responses;
using Tasks.Entities;

namespace Tasks.BLL.Services
{
    public class UserService : IUserService
    {
        public UserManager<User> _userManager;
        public ITokenService _tokenService;
        public IMapper _mapper;

        public UserService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthSuccessResponse> RegisterAsync(RegisterDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
                throw new AlreadyExistsException($"User with specified email already exists. Email: {dto.Email}");

            user = _mapper.Map<User>(dto);

            user.CreatedAt = DateTime.Now;
            user.UpdateAt = DateTime.Now;

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new UserManagerException($"User manager operation failed:\n", result.Errors);
            }

            return new AuthSuccessResponse() { AccessToken = await _tokenService.GenerateAccessTokenAsync(user) };
        }

        public async Task<AuthSuccessResponse> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new NotFoundException($"Unable to find user by specified email. Email: {dto.Email}");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
                throw new IncorrectParametersException($"User input incorrect password. Password: {dto.Password}");

            return new AuthSuccessResponse() { AccessToken = await _tokenService.GenerateAccessTokenAsync(user) };
        }
    }
}
