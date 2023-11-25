using ApiEcoMapa.Models;

namespace ApiEcoMapa.Interfaces.Repositories
{
    public interface IPontoSustentavelRepository
    {
        Task<IEnumerable<PontoSustentavel>> GetAll();
        Task<PontoSustentavel> GetById(int id);
        Task<PontoSustentavel> Create(PontoSustentavel pontoSustentavel);
        Task<PontoSustentavel> Update(PontoSustentavel pontoSustentavel);
        Task<PontoSustentavel> Delete(int id);
    }
}