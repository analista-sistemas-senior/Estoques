using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class UsuarioRepository(EstoquesDbContext context) : IUsuarioRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<Usuario?> RetornarUsuarioPorId(int idUsuario)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.IDUsuario == idUsuario);
        }

        public async Task<Usuario?> RetornarUsuarioPorLogin(string nmLogin)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.NMLogin == nmLogin);
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}