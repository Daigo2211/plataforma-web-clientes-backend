namespace Maqui.Application.DTOs.Response;

public sealed class ClienteResponseDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string RutaHojaVida { get; set; } = string.Empty;
    public string RutaFoto { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}