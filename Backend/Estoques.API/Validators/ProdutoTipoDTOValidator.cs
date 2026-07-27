using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoTipoDTOValidator : AbstractValidator<ProdutoTipoDTO>
    {
        public ProdutoTipoDTOValidator()
        {
            RuleFor(pf => pf.NMProdutoTipo).NotEmpty().MaximumLength(255);
        }
    }
}