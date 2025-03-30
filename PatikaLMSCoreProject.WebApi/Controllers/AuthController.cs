using Microsoft.AspNetCore.Mvc;
using PatikaLMSCoreProject.Business.Operations.User;
using PatikaLMSCoreProject.Business.Operations.User.Dtos;
using PatikaLMSCoreProject.WebApi.Models;

namespace PatikaLMSCoreProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")] 
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}