using Maqui.Domain.Entities;

namespace Maqui.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente?> GetByNumeroDocumentoAsync(string numeroDocumento);
    Task<Cliente> AddAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(string numeroDocumento);
}