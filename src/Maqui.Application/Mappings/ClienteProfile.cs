using AutoMapper;
using Maqui.Application.DTOs.Request;
using Maqui.Application.DTOs.Response;
using Maqui.Domain.Entities;
using Maqui.Domain.Enums;

namespace Maqui.Application.Mappings;

public sealed class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteCreateRequestDto, Cliente>()
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => (TipoDocumento)src.TipoDocumento))
            .ForMember(dest => dest.RutaHojaVida, opt => opt.Ignore())
            .ForMember(dest => dest.RutaFoto, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EsActivo, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.FechaModificacion, opt => opt.Ignore());

        CreateMap<Cliente, ClienteResponseDto>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.Apellidos}"))
            .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => GetTipoDocumentoString(src.TipoDocumento)));
    }

    private static string GetTipoDocumentoString(TipoDocumento tipo) => tipo switch
    {
        TipoDocumento.DNI => "DNI",
        TipoDocumento.RUC => "RUC",
        TipoDocumento.CarnetExtranjeria => "CARNET DE EXTRANJERÍA",
        _ => "Desconocido"
    };
}