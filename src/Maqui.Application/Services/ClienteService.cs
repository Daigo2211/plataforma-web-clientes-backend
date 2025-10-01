using AutoMapper;
using FluentValidation;
using Maqui.Application.Common;
using Maqui.Application.DTOs.Request;
using Maqui.Application.DTOs.Response;
using Maqui.Application.Interfaces;
using Maqui.Domain.Entities;
using Maqui.Domain.Enums;
using Maqui.Domain.Interfaces;

namespace Maqui.Application.Services;

public sealed class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IValidator<ClienteCreateRequestDto> _createValidator;
    private readonly IValidator<ClienteUpdateRequestDto> _updateValidator;

    public ClienteService(
        IClienteRepository repository,
        IFileService fileService,
        IMapper mapper,
        IValidator<ClienteCreateRequestDto> createValidator,
        IValidator<ClienteUpdateRequestDto> updateValidator)
    {
        _repository = repository;
        _fileService = fileService;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<Result<IEnumerable<ClienteResponseDto>>> GetAllAsync()
    {
        var clientes = await _repository.GetAllAsync();
        var clientesDto = _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
        return Result<IEnumerable<ClienteResponseDto>>.Success(clientesDto);
    }

    public async Task<Result<ClienteResponseDto>> GetByIdAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id);

        if (cliente == null)
            return Result<ClienteResponseDto>.Failure("Cliente no encontrado");

        var clienteDto = _mapper.Map<ClienteResponseDto>(cliente);
        return Result<ClienteResponseDto>.Success(clienteDto);
    }

    public async Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateRequestDto request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result<ClienteResponseDto>.Failure("Errores de validación", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

        if (await _repository.ExistsAsync(request.NumeroDocumento))
            return Result<ClienteResponseDto>.Failure("Ya existe un cliente con este número de documento");

        if (!await _fileService.ValidateFileSizeAsync(request.HojaVida.Length, 5))
            return Result<ClienteResponseDto>.Failure("La hoja de vida excede el tamaño máximo de 5 MB");

        var cliente = _mapper.Map<Cliente>(request);

        var fotoPath = await _fileService.SaveFileAsync(request.Foto, request.FotoFileName, "fotos");
        var hojaVidaPath = await _fileService.SaveFileAsync(request.HojaVida, request.HojaVidaFileName, "hojas-vida");

        cliente.RutaFoto = fotoPath;
        cliente.RutaHojaVida = hojaVidaPath;

        var createdCliente = await _repository.AddAsync(cliente);
        var clienteDto = _mapper.Map<ClienteResponseDto>(createdCliente);

        return Result<ClienteResponseDto>.Success(clienteDto, "Cliente creado exitosamente");
    }

    public async Task<Result<ClienteResponseDto>> UpdateAsync(ClienteUpdateRequestDto request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result<ClienteResponseDto>.Failure("Errores de validación", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

        var cliente = await _repository.GetByIdAsync(request.Id);
        if (cliente == null)
            return Result<ClienteResponseDto>.Failure("Cliente no encontrado");

        cliente.Nombres = request.Nombres;
        cliente.Apellidos = request.Apellidos;
        cliente.FechaNacimiento = request.FechaNacimiento;
        cliente.FechaModificacion = DateTime.UtcNow;

        if (request.Foto != null && request.FotoFileName != null)
        {
            if (!await _fileService.ValidateFileSizeAsync(request.Foto.Length, 5))
                return Result<ClienteResponseDto>.Failure("La foto excede el tamaño máximo de 5 MB");

            await _fileService.DeleteFileAsync(cliente.RutaFoto);
            cliente.RutaFoto = await _fileService.SaveFileAsync(request.Foto, request.FotoFileName, "fotos");
        }

        if (request.HojaVida != null && request.HojaVidaFileName != null)
        {
            if (!await _fileService.ValidateFileSizeAsync(request.HojaVida.Length, 5))
                return Result<ClienteResponseDto>.Failure("La hoja de vida excede el tamaño máximo de 5 MB");

            await _fileService.DeleteFileAsync(cliente.RutaHojaVida);
            cliente.RutaHojaVida = await _fileService.SaveFileAsync(request.HojaVida, request.HojaVidaFileName, "hojas-vida");
        }

        await _repository.UpdateAsync(cliente);
        var clienteDto = _mapper.Map<ClienteResponseDto>(cliente);

        return Result<ClienteResponseDto>.Success(clienteDto, "Cliente actualizado exitosamente");
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        if (cliente == null)
            return Result<bool>.Failure("Cliente no encontrado");

        await _repository.DeleteAsync(id);
        return Result<bool>.Success(true, "Cliente eliminado exitosamente");
    }
}