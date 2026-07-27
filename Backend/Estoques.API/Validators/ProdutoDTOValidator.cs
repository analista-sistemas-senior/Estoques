using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoDTOValidator : AbstractValidator<ProdutoDTO>
    {
        public ProdutoDTOValidator()
        {
            RuleFor(p => p.IDProdutoTipo).GreaterThan(0);
            RuleFor(p => p.IDProdutoSituacao).GreaterThan(0);
            RuleFor(p => p.IDProdutoFabricante).GreaterThan(0);
            RuleFor(p => p.NMProduto).NotEmpty().MaximumLength(255);
            RuleFor(p => p.DSProduto).NotEmpty().MaximumLength(1024);
            RuleFor(p => p.INProdutoCor).IsInEnum();
            RuleFor(p => p.QTProduto).GreaterThanOrEqualTo(0).LessThan(int.MaxValue);
            RuleFor(p => p.LKProdutoImagem).MaximumLength(1024);
        }
    }
}