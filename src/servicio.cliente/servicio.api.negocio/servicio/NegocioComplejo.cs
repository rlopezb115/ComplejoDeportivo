namespace servicio.api.negocio.servicio
{
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using ADO.contrato;
    using ADO.entidades;
    using common.extension;
    using common.respuesta;
    using common.solicitud;
    using negocio.contrato;

    public class NegocioComplejo : INegocioComplejo
    {
        private readonly ILogger<NegocioComplejo> _logger;
        private readonly IRepositorioComplejo _repositorio;

        public NegocioComplejo(
            IRepositorioComplejo repositorio,
            ILogger<NegocioComplejo> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<ComplejoDeportivoCompleto> ObtenerAsync(int complejoId)
        {
            ComplejoDeportivoCompleto response = null;
            if (complejoId.EsValido())
            {
                response = await _repositorio.ObtenerAsync(complejoId);
                if (!response.EsValido())
                {
                    _logger.LogError($"No se ha encontrado Complejo con Id: {complejoId}");
                }
            }
            else
            {
                _logger.LogError("El parámetro 'complejoId' no es válido, no se puede continuar con la búsqueda del complejo.");
            }

            return response;
        }

        public async Task<RespuestaTablaDatos<ComplejoDeportivoCompleto>> ObtenerTablaDatosAsync(SolicitudTablaDatos param)
        {
            var response = new RespuestaTablaDatos<ComplejoDeportivoCompleto>();
            if (!param.Tamano.EsValido())
            {
                _logger.LogError("El parámetro 'Tamano' no es válido, no se puede continuar.");
                return response;
            }

            return await _repositorio.ObtenerTablaDatosAsync(param);
        }

        public async Task<ComplejoDeportivo> InsertarAsync(ComplejoDeportivo param)
        {
            if (!param.EsValido())
            {
                _logger.LogError("Problema para registrar el complejo.");
                return param;
            }

            return await _repositorio.InsertarAsync(param);
        }

        public async Task<bool> ActualizarAsync(int complejoId, ComplejoDeportivo param)
        {
            if (!param.EsValido())
            {
                _logger.LogError("Problema para actualizar el complejo.");
                return false;
            }

            return await _repositorio.ActualizarAsync(complejoId, param);
        }

        public async Task<bool> EliminarAsync(int complejoId)
        {
            if (!complejoId.EsValido())
            {
                _logger.LogError("Problema para eliminar el complejo.");
                return false;
            }

            return await _repositorio.EliminarAsync(complejoId);
        }
    }
}
