using FluentValidation;
using Estoques.Service.DTOs.Usuario;

namespace Estoques.API.Validators { 
    public class UsuarioEntradaDTOValidator : AbstractValidator<UsuarioEntradaDTO>
    {
        public UsuarioEntradaDTOValidator()
        {
            RuleFor(u => u.NMUsuario).NotEmpty().MaximumLength(255);
            RuleFor(u => u.NMLogin).NotEmpty().MaximumLength(255);
            RuleFor(u => u.CDSenha).NotEmpty().MaximumLength(40);
        }
    }
}