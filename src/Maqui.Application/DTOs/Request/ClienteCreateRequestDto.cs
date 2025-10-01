using Maqui.Domain.Enums;

namespace Maqui.Application.DTOs.Request;

public sealed class ClienteCreateRequestDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public byte[] HojaVida { get; set; } = Array.Empty<byte>();
    public string HojaVidaFileName { get; set; } = string.Empty;
    public byte[] Foto { get; set; } = Array.Empty<byte>();
    public string FotoFileName { get; set; } = string.Empty;
}