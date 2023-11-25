using ApiEcoMapa.Context;
using ApiEcoMapa.Interfaces.Repositories;
using ApiEcoMapa.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcoMapa.Repositories
{
    public class PontoSustentavelRepository : IPontoSustentavelRepository
    {
        private readonly AppDbContext _context;

        public PontoSustentavelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PontoSustentavel>> GetAll()
            => await _context.PontoSustentavel.ToListAsync();

        public async Task<PontoSustentavel> GetById(int id)
            => await _context.PontoSustentavel.Where(ps => ps.Id == id).FirstOrDefaultAsync();

        public async Task<PontoSustentavel> Create(PontoSustentavel pontoSustentavel)
        {
            _context.PontoSustentavel.Add(pontoSustentavel);
            await _context.SaveChangesAsync();
            return pontoSustentavel;
        }

        public async Task<PontoSustentavel> Update(PontoSustentavel pontoSustentavel)
        {
            _context.Entry(pontoSustentavel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pontoSustentavel;
        }

        public async Task<PontoSustentavel> Delete(int id)
        {
            var pontoSustentavel = await GetById(id);
            _context.PontoSustentavel.Remove(pontoSustentavel);
            await _context.SaveChangesAsync();
            return pontoSustentavel;
        }
    }
}