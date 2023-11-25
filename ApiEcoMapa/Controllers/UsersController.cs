using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Controllers;
using ApiEcoMapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEcoMapa.Controllers
{
    [SwaggerTag("Criação e login de usúario.")]
    public class UsersController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [SwaggerOperation(
        Summary = "Cria novo usúario.",
        Description = "Retorna resultado da criação do usuário.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com toke.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult> CreateUser([FromBody] User model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string mensagemErro = "Usuário ou senha inválidos.";

                if (result.Errors.Any())
                {
                    mensagemErro += $" Retorno: {result.Errors?.FirstOrDefault()?.Description ?? "Não foi possível identificar o erro."}";
                }

                return BadRequest(mensagemErro);
            }

            return Ok(model);
        }

        [HttpPost("Login")]
        [SwaggerOperation(
        Summary = "Realiza login de usuário.",
        Description = "Retorna token e tempo de expiração.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com toke.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login inválido.");
                return BadRequest(ModelState);
            }

            return BuildToken(userInfo);
        }

        private UserToken BuildToken(User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddHours(0.5);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}