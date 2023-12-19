using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using YouZack.FromJsonBody;

namespace StunnerApi.Controllers;

[ApiController]
[OpenApiRule]
[Route("/api/[controller]")]
public class UserController(IUserRepository _userRepository) : Controller {

    [HttpPost]
    public async Task<IActionResult> SignUp([FromJsonBody] string username, [FromJsonBody] string password) {
        User? userCreated = await _userRepository.CreateUser(username, password);
        return await Task.FromResult<IActionResult>(userCreated == null ? Conflict() : Ok());
    }

    [HttpGet]
    [Authorize]
    public async Task<int> Login() {
        int? userId = (int?)Request.HttpContext.Items["user_id"];
        return await Task.FromResult(userId.GetValueOrDefault());
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Remove() {
        int? userId = (int?)Request.HttpContext.Items["user_id"];
        bool isDeleted = await _userRepository.DeleteUser(userId.GetValueOrDefault());
        return await Task.FromResult<IActionResult>(isDeleted ? NoContent() : Conflict());
    }
}