using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class ProdutoHistoricoDTOValidator : AbstractValidator<ProdutoHistoricoDTO>
    {
        public ProdutoHistoricoDTOValidator()
        {
            RuleFor(ph => ph.IDProduto).GreaterThan(0);
            RuleFor(ph => ph.IDFornecedor).GreaterThan(0);
            RuleFor(ph => ph.INProdutoHistoricoTipo).IsInEnum();
            RuleFor(ph => ph.DTProdutoHistorico).NotEmpty().GreaterThan(DateTime.MinValue);
            RuleFor(ph => ph.QTProdutoHistorico).GreaterThanOrEqualTo(0).LessThan(int.MaxValue);
            RuleFor(ph => ph.VLProdutoHistorico).GreaterThanOrEqualTo(0).LessThan(int.MaxValue);
        }
    }
}