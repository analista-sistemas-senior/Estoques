using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoFabricanteRepository(EstoquesDbContext context) : IProdutoFabricanteRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<ProdutoFabricante?> RetornarProdutoFabricantePorId(int idProdutoFabricante)
        {
            return await _context.ProdutoFabricante.AsNoTracking().FirstOrDefaultAsync(pf => pf.IDProdutoFabricante == idProdutoFabricante);
        }

        public async Task<ProdutoFabricante?> RetornarProdutoFabricantePorIdEIdUsuario(int idProdutoFabricante, int idUsuario)
        {
            return await _context.ProdutoFabricante.AsNoTracking().FirstOrDefaultAsync(pf => pf.IDProdutoFabricante == idProdutoFabricante && pf.IDUsuario == idUsuario);
        }

        public async Task<List<ProdutoFabricante>> RetornarProdutosFabricantesPorIdUsuario(int idUsuario)
        {
            return await _context.ProdutoFabricante.AsNoTracking().Where(pf => pf.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ProdutoFabricante> CadastrarProdutoFabricante(ProdutoFabricante produtoFabricante)
        {
            _context.ProdutoFabricante.Add(produtoFabricante);
            await _context.SaveChangesAsync();
            return produtoFabricante;
        }

        public async Task<ProdutoFabricante> AtualizarProdutoFabricante(ProdutoFabricante produtoFabricante)
        {
            _context.ProdutoFabricante.Update(produtoFabricante);
            await _context.SaveChangesAsync();
            return produtoFabricante;
        }

        public async Task<bool> ExcluirProdutoFabricante(int idProdutoFabricante, int idUsuario)
        {
            try { return await _context.ProdutoFabricante.Where(pf => pf.IDProdutoFabricante == idProdutoFabricante && pf.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}