using ApiEcoMapa.DTOs;
using ApiEcoMapa.Interfaces.Repositories;
using ApiEcoMapa.Interfaces.Services;
using ApiEcoMapa.Models;
using AutoMapper;

namespace ApiEcoMapa.Services
{
    public class PontoSustentavelService : IPontoSustentavelService
    {
        private readonly IPontoSustentavelRepository _pontoSustentavelRepository;
        private readonly IMapper _mapper;

        public PontoSustentavelService(IPontoSustentavelRepository pontoSustentavelRepository, IMapper mapper)
        {
            _pontoSustentavelRepository = pontoSustentavelRepository;
            _mapper = mapper;
        }

        public async Task AddPontoSustentavel(PontoSustentavelDTO pontoSustentavelDto)
        {
            var pontoSustentavelEntity = _mapper.Map<PontoSustentavel>(pontoSustentavelDto);
            await _pontoSustentavelRepository.Create(pontoSustentavelEntity);
        }

        public async Task<IEnumerable<PontoSustentavelDTO>> GetPontosSustentaveis()
        {
            var pontoSustentavelEntity = await _pontoSustentavelRepository.GetAll();
            return _mapper.Map<IEnumerable<PontoSustentavelDTO>>(pontoSustentavelEntity);
        }

        public async Task<PontoSustentavelDTO> GetPontoSustentavelById(int id)
        {
            var pontoSustentavelEntity = await _pontoSustentavelRepository.GetById(id);
            return _mapper.Map<PontoSustentavelDTO>(pontoSustentavelEntity);
        }

        public async Task RemovePontoSustentavel(int id)
        {
            var pontoSustentavelEntity = _pontoSustentavelRepository.GetById(id).Result;
            await _pontoSustentavelRepository.Delete(pontoSustentavelEntity.Id);
        }

        public async Task UpdatePontoSustentavel(PontoSustentavelDTO pontoSustentavelDto)
        {
            var pontoSustentavelEntity = _mapper.Map<PontoSustentavel>(pontoSustentavelDto);
            await _pontoSustentavelRepository.Update(pontoSustentavelEntity);
        }
    }
}