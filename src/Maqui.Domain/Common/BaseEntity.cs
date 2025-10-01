namespace Maqui.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool EsActivo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
}