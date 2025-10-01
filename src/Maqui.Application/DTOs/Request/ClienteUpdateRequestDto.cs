namespace Maqui.Application.DTOs.Request;

public sealed class ClienteUpdateRequestDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public byte[]? HojaVida { get; set; }
    public string? HojaVidaFileName { get; set; }
    public byte[]? Foto { get; set; }
    public string? FotoFileName { get; set; }
}