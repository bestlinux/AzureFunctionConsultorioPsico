using FunctionConsultorio.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConsultorioFunctions;

public class UserFunction
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserFunction> _logger;

    public UserFunction(ILogger<UserFunction> logger,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
    }

    [Function("UserFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "login")] HttpRequestData req, [Microsoft.Azure.Functions.Worker.Http.FromBody] User userInfo)
    {
        var user = await _userManager.FindByEmailAsync(userInfo.Email);

        if (user == null)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Login inválido" });
            return unauthorizedResponse;
        }


        // Validar senha
        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userInfo.Password);

        if (!isPasswordValid)
        {
            var unauthorizedResponse = req.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            await unauthorizedResponse.WriteAsJsonAsync(new { error = "Login inválido" });
            return unauthorizedResponse;
        }


        // Gerar token JWT
        var token = BuildToken(user);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(token);
        return response;
    }

    private UserToken BuildToken(ApplicationUser userInfo)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("consultorio", "https://www.consultoriocida.net"),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Audience"]),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Issuer"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // tempo de expiração do token: 3 horas
        var expiration = DateTime.Now.AddHours(24);

        JwtSecurityToken token = new JwtSecurityToken(
           issuer: null,
           audience: null,
           claims: claims,
           expires: expiration,
           signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}