using Maqui.Application.Common;
using Maqui.Application.DTOs.Request;
using Maqui.Application.DTOs.Response;

namespace Maqui.Application.Interfaces;

public interface IClienteService
{
    Task<Result<IEnumerable<ClienteResponseDto>>> GetAllAsync();
    Task<Result<ClienteResponseDto>> GetByIdAsync(int id);
    Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateRequestDto request);
    Task<Result<ClienteResponseDto>> UpdateAsync(ClienteUpdateRequestDto request);
    Task<Result<bool>> DeleteAsync(int id);
}