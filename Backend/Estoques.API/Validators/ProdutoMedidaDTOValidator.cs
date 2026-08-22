using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoMedidaDTOValidator : AbstractValidator<ProdutoMedidaDTO>
    {
        public ProdutoMedidaDTOValidator()
        {
            RuleFor(pm => pm.MDProdutoMedida).NotEmpty().MaximumLength(255);
        }
    }
}