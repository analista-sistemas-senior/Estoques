using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoFabricanteDTOValidator : AbstractValidator<ProdutoFabricanteDTO>
    {
        public ProdutoFabricanteDTOValidator()
        {
            RuleFor(pf => pf.NMProdutoFabricante).NotEmpty().MaximumLength(255);
        }
    }
}