using ApiEcoMapa.DTOs;

namespace ApiEcoMapa.Interfaces.Services
{
    public interface IPontoSustentavelService
    {
        Task<IEnumerable<PontoSustentavelDTO>> GetPontosSustentaveis();
        Task<PontoSustentavelDTO> GetPontoSustentavelById(int id);
        Task AddPontoSustentavel(PontoSustentavelDTO pontoSustentavelDto);
        Task UpdatePontoSustentavel(PontoSustentavelDTO pontoSustentavelDto);
        Task RemovePontoSustentavel(int id);
    }

}