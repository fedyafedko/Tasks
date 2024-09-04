using Microsoft.AspNetCore.Mvc;
using Tasks.BLL.Services.Interfaces;
using Tasks.Common.DTOs;
using Tasks.Common.Responses;

namespace Tasks_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> This endpoint returns an access token.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto);

            return Ok(result);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> This endpoint returns an access token.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _userService.LoginAsync(dto);

            return Ok(result);
        }
    }
}
