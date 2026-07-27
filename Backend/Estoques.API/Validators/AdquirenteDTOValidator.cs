using FluentValidation;
using Estoques.Service.DTOs;

namespace Estoques.API.Validators { 
    public class AdquirenteDTOValidator : AbstractValidator<AdquirenteDTO>
    {
        public AdquirenteDTOValidator()
        {
            RuleFor(a => a.NMAdquirente).NotEmpty().MaximumLength(255);
            RuleFor(a => a.TXEndereco).MaximumLength(255);
            RuleFor(a => a.TXAnotacao).MaximumLength(1024);
        }
    }
}