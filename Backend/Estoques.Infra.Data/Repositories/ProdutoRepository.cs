using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoRepository(EstoquesDbContext context) : IProdutoRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<Produto?> RetornarProdutoPorId(int idProduto)
        {
            return await _context.Produto.AsNoTracking().Include(p => p.ProdutoTipo).Include(p => p.ProdutoSituacao).Include(p => p.ProdutoFabricante).FirstOrDefaultAsync(p => p.IDProduto == idProduto);
        }

        public async Task<Produto?> RetornarProdutoPorIdEIdUsuario(int idProduto, int idUsuario)
        {
            return await _context.Produto.AsNoTracking().Include(p => p.ProdutoTipo).Include(p => p.ProdutoSituacao).Include(p => p.ProdutoFabricante).FirstOrDefaultAsync(p => p.IDProduto == idProduto && p.IDUsuario == idUsuario);
        }

        public async Task<List<Produto>> RetornarProdutosPorIdUsuario(int idUsuario)
        {
            return await _context.Produto.AsNoTracking().Include(p => p.ProdutoTipo).Include(p => p.ProdutoSituacao).Include(p => p.ProdutoFabricante).Where(p => p.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> ExcluirProduto(int idProduto, int idUsuario)
        {
            try { return await _context.Produto.Where(p => p.IDProduto == idProduto && p.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}