using FluentValidation;
using Estoques.Service.DTOs.Autenticacao;

namespace Estoques.API.Validators { 
    public class AutenticacaoEntradaDTOValidator : AbstractValidator<AutenticacaoEntradaDTO>
    {
        public AutenticacaoEntradaDTOValidator()
        {
            RuleFor(a => a.NMLogin).NotEmpty().MaximumLength(255);
            RuleFor(a => a.CDSenha).NotEmpty().MaximumLength(40);
        }
    }
}