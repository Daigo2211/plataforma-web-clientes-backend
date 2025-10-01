namespace Maqui.API.Contracts.Requests;

public sealed class ClienteUpdateRequest
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public IFormFile? HojaVida { get; set; }
    public IFormFile? Foto { get; set; }
}
