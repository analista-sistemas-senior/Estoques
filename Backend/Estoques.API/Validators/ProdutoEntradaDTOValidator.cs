using FluentValidation;
using Estoques.Service.DTOs.Produto;

namespace Estoques.API.Validators { 
    public class ProdutoEntradaDTOValidator : AbstractValidator<ProdutoEntradaDTO>
    {
        public ProdutoEntradaDTOValidator()
        {
            RuleFor(p => p.IDProdutoTipo).GreaterThan(0);
            RuleFor(p => p.IDProdutoSituacao).GreaterThan(0);
            RuleFor(p => p.IDProdutoFabricante).GreaterThan(0);
            RuleFor(p => p.NMProduto).NotEmpty().MaximumLength(255);
            RuleFor(p => p.DSProduto).MaximumLength(1024);
            RuleFor(p => p.INProdutoCor).IsInEnum();
            RuleFor(p => p.LKProdutoImagem).MaximumLength(1024);
            RuleFor(p => p.TXAnotacao).MaximumLength(255);
        }
    }
}