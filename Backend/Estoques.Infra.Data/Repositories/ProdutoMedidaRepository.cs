using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoMedidaRepository(EstoquesDbContext context) : IProdutoMedidaRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<ProdutoMedida?> RetornarProdutoMedidaPorId(int idProdutoMedida)
        {
            return await _context.ProdutoMedida.AsNoTracking().FirstOrDefaultAsync(pm => pm.IDProdutoMedida == idProdutoMedida);
        }

        public async Task<ProdutoMedida?> RetornarProdutoMedidaPorIdEIdUsuario(int idProdutoMedida, int idUsuario)
        {
            return await _context.ProdutoMedida.AsNoTracking().FirstOrDefaultAsync(pm => pm.IDProdutoMedida == idProdutoMedida && pm.IDUsuario == idUsuario);
        }

        public async Task<List<ProdutoMedida>> RetornarProdutosMedidasPorIdUsuario(int idUsuario)
        {
            return await _context.ProdutoMedida.AsNoTracking().Where(pm => pm.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ProdutoMedida> CadastrarProdutoMedida(ProdutoMedida produtoMedida)
        {
            _context.ProdutoMedida.Add(produtoMedida);
            await _context.SaveChangesAsync();
            return produtoMedida;
        }

        public async Task<ProdutoMedida> AtualizarProdutoMedida(ProdutoMedida produtoMedida)
        {
            _context.ProdutoMedida.Update(produtoMedida);
            await _context.SaveChangesAsync();
            return produtoMedida;
        }

        public async Task<bool> ExcluirProdutoMedida(int idProdutoMedida, int idUsuario)
        {
            try { return await _context.ProdutoMedida.Where(pm => pm.IDProdutoMedida == idProdutoMedida && pm.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}