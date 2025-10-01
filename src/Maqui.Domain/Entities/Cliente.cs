using Maqui.Domain.Common;
using Maqui.Domain.Enums;

namespace Maqui.Domain.Entities;

public sealed class Cliente : BaseEntity
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public string RutaHojaVida { get; set; } = string.Empty;
    public string RutaFoto { get; set; } = string.Empty;
}