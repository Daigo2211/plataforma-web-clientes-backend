using Maqui.Domain.Enums;

namespace Maqui.API.Contracts.Requests;

public sealed class ClienteCreateRequest
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public IFormFile HojaVida { get; set; } = null!;
    public IFormFile Foto { get; set; } = null!;
}
