namespace servicio.api.Mapping
{
    using servicio.api.ADO.entidades;
    using servicio.api.Dto;
    using AutoMapper;

    public class SQLServerProfile : Profile
    {
        public SQLServerProfile()
        {
            CreateMap<ComplejoDeportivoCompleto, ComplejoDeportivoCompletoDto>();
            CreateMap<ComplejoDeportivo, ComplejoDeportivoDto>();
            CreateMap<ComplejoDeportivoDto, ComplejoDeportivo>();
            CreateMap<Sede, SedeDto>();
            CreateMap<SedeDto, Sede>();
            CreateMap<Jefe, JefeDto>();
            CreateMap<JefeDto, Jefe>();
            CreateMap<TipoComplejo, TipoComplejoDto>();
            CreateMap<TipoComplejoDto, TipoComplejo>();
        }
    }
}