using Microsoft.AspNetCore.Mvc;
using WebAndroid.Interfaces;
using WebAndroid.Models;


namespace WebAndroid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request) => Ok(await _accountService.LoginAsync(request));

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromForm] UserCreationModel model) => Ok(await _accountService.CreateAsync(model));
    }
}
