using Microsoft.AspNetCore.Mvc;
using Maqui.Application.DTOs.Request;
using Maqui.Application.Interfaces;
using Maqui.API.Contracts.Requests;

namespace Maqui.API.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _clienteService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _clienteService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ClienteCreateRequest request)
    {
        var dto = new ClienteCreateRequestDto
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            FechaNacimiento = request.FechaNacimiento,
            TipoDocumento = request.TipoDocumento,
            NumeroDocumento = request.NumeroDocumento,
            HojaVida = await GetFileBytes(request.HojaVida),
            HojaVidaFileName = request.HojaVida.FileName,
            Foto = await GetFileBytes(request.Foto),
            FotoFileName = request.Foto.FileName
        };

        var result = await _clienteService.CreateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] ClienteUpdateRequest request)
    {
        var dto = new ClienteUpdateRequestDto
        {
            Id = id,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            FechaNacimiento = request.FechaNacimiento,
            HojaVida = request.HojaVida != null ? await GetFileBytes(request.HojaVida) : null,
            HojaVidaFileName = request.HojaVida?.FileName,
            Foto = request.Foto != null ? await GetFileBytes(request.Foto) : null,
            FotoFileName = request.Foto?.FileName
        };

        var result = await _clienteService.UpdateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clienteService.DeleteAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    private static async Task<byte[]> GetFileBytes(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}