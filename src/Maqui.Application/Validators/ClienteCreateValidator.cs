using FluentValidation;
using Maqui.Application.DTOs.Request;

namespace Maqui.Application.Validators;

public sealed class ClienteCreateValidator : AbstractValidator<ClienteCreateRequestDto>
{
    public ClienteCreateValidator()
    {
        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("Los nombres son obligatorios")
            .MaximumLength(100).WithMessage("Los nombres no pueden exceder 100 caracteres");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("Los apellidos son obligatorios")
            .MaximumLength(100).WithMessage("Los apellidos no pueden exceder 100 caracteres");

        RuleFor(x => x.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria")
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser anterior a hoy");

        RuleFor(x => x.TipoDocumento)
            .IsInEnum().WithMessage("Tipo de documento inválido");

        RuleFor(x => x.NumeroDocumento)
            .NotEmpty().WithMessage("El número de documento es obligatorio")
            .MaximumLength(20).WithMessage("El número de documento no puede exceder 20 caracteres");

        RuleFor(x => x.HojaVida)
            .NotEmpty().WithMessage("La hoja de vida es obligatoria");

        RuleFor(x => x.HojaVidaFileName)
            .NotEmpty().WithMessage("El nombre de la hoja de vida es obligatorio")
            .Must(x => x.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            .WithMessage("La hoja de vida debe ser un archivo PDF");

        RuleFor(x => x.Foto)
            .NotEmpty().WithMessage("La foto es obligatoria");

        RuleFor(x => x.FotoFileName)
            .NotEmpty().WithMessage("El nombre de la foto es obligatorio")
            .Must(x => x.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                       x.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
            .WithMessage("La foto debe ser formato JPG o JPEG");
    }
}