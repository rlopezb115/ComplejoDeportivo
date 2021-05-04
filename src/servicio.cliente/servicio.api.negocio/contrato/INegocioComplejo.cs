namespace servicio.api.negocio.contrato
{
    using ADO.entidades;
    using common.respuesta;
    using common.solicitud;
    using System.Threading.Tasks;

    public interface INegocioComplejo
    {
        Task<ComplejoDeportivoCompleto> ObtenerAsync(int complejoId);
        Task<RespuestaTablaDatos<ComplejoDeportivoCompleto>> ObtenerTablaDatosAsync(SolicitudTablaDatos param);
        Task<ComplejoDeportivo> InsertarAsync(ComplejoDeportivo param);
        Task<bool> ActualizarAsync(int complejoId, ComplejoDeportivo param);
        Task<bool> EliminarAsync(int complejoId);
    }
}