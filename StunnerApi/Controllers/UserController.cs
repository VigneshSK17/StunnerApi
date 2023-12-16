using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;

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
}