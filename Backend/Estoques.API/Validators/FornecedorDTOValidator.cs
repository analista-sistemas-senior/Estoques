using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class FornecedorDTOValidator : AbstractValidator<FornecedorDTO>
    {
        public FornecedorDTOValidator()
        {
            RuleFor(f => f.NMFornecedor).NotEmpty().MaximumLength(255);
            RuleFor(f => f.TXEndereco).MaximumLength(255);
            RuleFor(f => f.TXAnotacao).MaximumLength(1024);
        }
    }
}