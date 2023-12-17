using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class BasicAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock systemClock,
    IUserRepository userRepository
        ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, systemClock) {

    private readonly IUserRepository _userRepository = userRepository;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {

        string? authorizationHeader = Request.Headers["Authorization"].ToString();
        if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase)) {

            var token = authorizationHeader.Substring("Basic ".Length).Trim();
            var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialsAsEncodedString.Split(':');

            var authUser = await _userRepository.Authenticate(credentials[0], credentials[1]);
            if (authUser != null) {
                var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var result = AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                Request.HttpContext.Items["user_id"] = authUser.Id;
                return await Task.FromResult(result);
            }

        }

        Response.StatusCode = 401;
        Response.Headers.Append("WWW-Authenticate", "Basic realm=\"test.com\"");
        return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }
}