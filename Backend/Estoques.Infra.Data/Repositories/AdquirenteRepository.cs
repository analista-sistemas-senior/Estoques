using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class AdquirenteRepository(EstoquesDbContext context) : IAdquirenteRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<Adquirente?> RetornarAdquirentePorId(int idAdquirente)
        {
            return await _context.Adquirente.AsNoTracking().FirstOrDefaultAsync(a => a.IDAdquirente == idAdquirente);
        }

        public async Task<Adquirente?> RetornarAdquirentePorIdEIdUsuario(int idAdquirente, int idUsuario)
        {
            return await _context.Adquirente.AsNoTracking().FirstOrDefaultAsync(a => a.IDAdquirente == idAdquirente && a.IDUsuario == idUsuario);
        }

        public async Task<List<Adquirente>> RetornarAdquirentesPorIdUsuario(int idUsuario)
        {
            return await _context.Adquirente.AsNoTracking().Where(a => a.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Adquirente> CadastrarAdquirente(Adquirente adquirente)
        {
            _context.Adquirente.Add(adquirente);
            await _context.SaveChangesAsync();
            return adquirente;
        }

        public async Task<Adquirente> AtualizarAdquirente(Adquirente adquirente)
        {
            _context.Adquirente.Update(adquirente);
            await _context.SaveChangesAsync();
            return adquirente;
        }

        public async Task<bool> ExcluirAdquirente(int idAdquirente, int idUsuario)
        {
            try { return await _context.Adquirente.Where(a => a.IDAdquirente == idAdquirente && a.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}