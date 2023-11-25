using API.Controllers;
using ApiEcoMapa.DTOs;
using ApiEcoMapa.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEcoMapa.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [SwaggerTag("Localização de pontos sustentáveis.")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PontoSustentavelController : BaseApiController
    {
        private readonly IPontoSustentavelService _pontoSustentavelService;

        public PontoSustentavelController(IPontoSustentavelService pontoSustentavelService)
        {
            _pontoSustentavelService = pontoSustentavelService;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Obtem lista de pontos sustentáveis.",
        Description = "Retorna uma lista de todos os pontos sustentáveis."
        )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com toke.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult<IEnumerable<PontoSustentavelDTO>>> Get()
        {
            var pontosSustentaveisDto = await _pontoSustentavelService.GetPontosSustentaveis();

            if (pontosSustentaveisDto is null)
                return NotFound("Não encontrou pontos sustentáveis.");

            return Ok(pontosSustentaveisDto);
        }

        [HttpGet("{id}", Name = "GetPontoSustentavel")]
        [SwaggerOperation(
        Summary = "Obtem ponto sustentável pelo Id.",
        Description = "Retorna ponto sustentável referente ao Id.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com toke.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult<IEnumerable<PontoSustentavelDTO>>> Get(int id)
        {
            var pontosSustentaveisDto = await _pontoSustentavelService.GetPontoSustentavelById(id);

            if (pontosSustentaveisDto is null)
                return NotFound("Não encontrou ponto sustentável.");

            return Ok(pontosSustentaveisDto);
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Cria um novo ponto sustentável.",
        Description = "Retorna ponto sustentável criado.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com token")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult> Post([FromBody] PontoSustentavelDTO pontoSustentavelDto)
        {
            if (pontoSustentavelDto is null)
                return BadRequest("Dados inválidos.");

            await _pontoSustentavelService.AddPontoSustentavel(pontoSustentavelDto);

            return new CreatedAtRouteResult("GetPontoSustentavel", new { id = pontoSustentavelDto.Id }, pontoSustentavelDto);
        }

        [HttpPut]
        [SwaggerOperation(
        Summary = "Atualiza ponto sustentável existente.",
        Description = "Retorna se foi atualizado ponto sustentável existente.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com token")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult> Put([FromBody] PontoSustentavelDTO pontoSustentavelDto)
        {
            if (pontoSustentavelDto is null)
                return NotFound("Dados inválidos.");

            await _pontoSustentavelService.UpdatePontoSustentavel(pontoSustentavelDto);

            return Ok(pontoSustentavelDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Deleta ponto sustentável pelo Id.",
        Description = "Retorna resultado da exclusão de ponto sustentável.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Sem autorização - Necessário realizar autenticação com token")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Não encontrou nenhum ponto sustentável")]
        public async Task<ActionResult> Delete(int id)
        {
            var pontoSustentavelDto = await _pontoSustentavelService.GetPontoSustentavelById(id);

            if (pontoSustentavelDto is null)
                return NotFound("Ponto sustentável não encontrado.");

            await _pontoSustentavelService.RemovePontoSustentavel(id);

            return Ok(pontoSustentavelDto);
        }
    }
}