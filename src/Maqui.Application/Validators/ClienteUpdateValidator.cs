using FluentValidation;
using Maqui.Application.DTOs.Request;

namespace Maqui.Application.Validators;

public sealed class ClienteUpdateValidator : AbstractValidator<ClienteUpdateRequestDto>
{
    public ClienteUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID debe ser mayor a 0");

        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("Los nombres son obligatorios")
            .MaximumLength(100).WithMessage("Los nombres no pueden exceder 100 caracteres");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("Los apellidos son obligatorios")
            .MaximumLength(100).WithMessage("Los apellidos no pueden exceder 100 caracteres");

        RuleFor(x => x.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria")
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser anterior a hoy");

        When(x => x.HojaVidaFileName != null, () =>
        {
            RuleFor(x => x.HojaVidaFileName)
                .Must(x => x!.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                .WithMessage("La hoja de vida debe ser un archivo PDF");

            RuleFor(x => x.HojaVida)
                .NotEmpty().WithMessage("Debe proporcionar el archivo de hoja de vida");
        });

        When(x => x.FotoFileName != null, () =>
        {
            RuleFor(x => x.FotoFileName)
                .Must(x => x!.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                           x!.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                .WithMessage("La foto debe ser formato JPG o JPEG");

            RuleFor(x => x.Foto)
                .NotEmpty().WithMessage("Debe proporcionar el archivo de foto");
        });
    }
}