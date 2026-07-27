using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoSituacaoDTOValidator : AbstractValidator<ProdutoSituacaoDTO>
    {
        public ProdutoSituacaoDTOValidator()
        {
            RuleFor(ps => ps.NMProdutoSituacao).NotEmpty().MaximumLength(255);
        }
    }
}