using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using SQLitePCL;
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


    // Activities

    [HttpGet("Activities")]
    [Authorize]
    public async Task<List<ActivityJson>> GetUserActivites() {
        int? userId = (int?)Request.HttpContext.Items["user_id"];
        List<ActivityJson> activities = await _userRepository.GetActivities(userId.GetValueOrDefault());
        return await Task.FromResult(activities);
    }

    [HttpPost("Activities")]
    [Authorize]
    public async Task<IActionResult> CreateUserActivity(
        [FromJsonBody] string title,
        [FromJsonBody] int activityType,
        [FromJsonBody] string dateCreated = "",
        [FromJsonBody] string? subject = null
    ) {

        int? userId = (int?)Request.HttpContext.Items["user_id"];

        int? activityId = Enum.IsDefined(typeof(ActivityType), activityType) ?
            await _userRepository.CreateActivity(userId.GetValueOrDefault(), title, dateCreated, (ActivityType) activityType, subject) :
            await _userRepository.CreateActivity(userId.GetValueOrDefault(), title, dateCreated, subject: subject);

        return await Task.FromResult<IActionResult>(activityId == null ? Conflict(): Ok(activityId));

    }

}