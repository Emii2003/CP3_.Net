using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        // Método de validação, mas vamos usar FluentValidation para validações mais específicas.
        public void Validate()
        {
            var validator = new BarcoDtoValidation();
            var result = validator.Validate(this);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            // Validação para garantir que o Nome seja obrigatório e tenha um tamanho mínimo
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do barco é obrigatório.")
                .Length(3, 100).WithMessage("O nome do barco deve ter entre 3 e 100 caracteres.");

            // Validação para garantir que o Modelo seja obrigatório
            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("O modelo do barco é obrigatório.")
                .Length(3, 50).WithMessage("O modelo do barco deve ter entre 3 e 50 caracteres.");

            // Validação para garantir que o Ano seja um número positivo e maior que 1900
            RuleFor(x => x.Ano)
                .GreaterThan(1900).WithMessage("O ano de fabricação deve ser maior que 1900.");

            // Validação para garantir que o Tamanho seja maior que zero
            RuleFor(x => x.Tamanho)
                .GreaterThan(0).WithMessage("O tamanho do barco deve ser maior que zero.");
        }
    }
}
