using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IUserFetchService _fetchService;

        public UsersController(IUserRegistrationService registrationService, IUserFetchService fetchService)
        {
            _registrationService = registrationService;
            _fetchService = fetchService;
        }

        [HttpPost]
        public IActionResult RegisterUser(string name, string email)
        {
            _registrationService.RegisterUser(name, email);
            return Ok("Registration success!");
        }

        [HttpPost("UserAsync")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest request)
        {

            var isExistingEmail = await _registrationService.EmailExistsAsync(request.Email);
            
            if(isExistingEmail)
            {
                return BadRequest("Email already exists");
            }
            
            var success = await _registrationService.RegisterUserAsync(request.Name, request.Email);
            
            if(!success)
            {
                return BadRequest("Registration failed");
            }
            return Created();
        }

        [HttpPost("FetchUserAsync")]
        public async Task<IActionResult> FetchUserByEmailAsync([FromBody] UserFetchRequest request)
        {

            var user = await _fetchService.FetchUserByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Failed to fetch user");
            }
            return Ok(user);
        }
    }
}
