using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoTipoRepository(EstoquesDbContext context) : IProdutoTipoRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<ProdutoTipo?> RetornarProdutoTipoPorId(int idProdutoTipo)
        {
            return await _context.ProdutoTipo.AsNoTracking().FirstOrDefaultAsync(pt => pt.IDProdutoTipo == idProdutoTipo);
        }

        public async Task<ProdutoTipo?> RetornarProdutoTipoPorIdEIdUsuario(int idProdutoTipo, int idUsuario)
        {
            return await _context.ProdutoTipo.AsNoTracking().FirstOrDefaultAsync(pt => pt.IDProdutoTipo == idProdutoTipo && pt.IDUsuario == idUsuario);
        }

        public async Task<List<ProdutoTipo>> RetornarProdutosTiposPorIdUsuario(int idUsuario)
        {
            return await _context.ProdutoTipo.AsNoTracking().Where(pt => pt.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ProdutoTipo> CadastrarProdutoTipo(ProdutoTipo produtoTipo)
        {
            _context.ProdutoTipo.Add(produtoTipo);
            await _context.SaveChangesAsync();
            return produtoTipo;
        }

        public async Task<ProdutoTipo> AtualizarProdutoTipo(ProdutoTipo produtoTipo)
        {
            _context.ProdutoTipo.Update(produtoTipo);
            await _context.SaveChangesAsync();
            return produtoTipo;
        }

        public async Task<bool> ExcluirProdutoTipo(int idProdutoTipo, int idUsuario)
        {
            try { return await _context.ProdutoTipo.Where(pt => pt.IDProdutoTipo == idProdutoTipo && pt.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}