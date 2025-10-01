using Microsoft.EntityFrameworkCore;
using Maqui.Domain.Entities;
using Maqui.Domain.Interfaces;
using Maqui.Infrastructure.Data.Context;

namespace Maqui.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .Where(c => c.EsActivo)
            .OrderByDescending(c => c.FechaCreacion)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes
            .Where(c => c.Id == id && c.EsActivo)
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente?> GetByNumeroDocumentoAsync(string numeroDocumento)
    {
        return await _context.Clientes
            .Where(c => c.NumeroDocumento == numeroDocumento && c.EsActivo)
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente> AddAsync(Cliente cliente)
    {
        cliente.FechaCreacion = DateTime.UtcNow;
        cliente.EsActivo = true;

        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        cliente.FechaModificacion = DateTime.UtcNow;

        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente != null)
        {
            cliente.EsActivo = false;
            cliente.FechaModificacion = DateTime.UtcNow;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string numeroDocumento)
    {
        return await _context.Clientes
            .AnyAsync(c => c.NumeroDocumento == numeroDocumento && c.EsActivo);
    }
}