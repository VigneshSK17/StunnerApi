using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using YouZack.FromJsonBody;

namespace StunnerApi.Controllers;

[ApiController]
[OpenApiRule]
[Route("/api/[controller]")]
public class UserController(IUserRepository _userRepository) : Controller {

    [HttpGet]
    [Authorize]
    public async Task<List<string>> Get() {
        return await _userRepository.GetUsernames();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromJsonBody] string username, [FromJsonBody] string password) {
        bool userCreated = await _userRepository.Create(username, password);
        return await Task.FromResult<IActionResult>(!userCreated ? Conflict() : Ok($"user {username} created"));
    }
}