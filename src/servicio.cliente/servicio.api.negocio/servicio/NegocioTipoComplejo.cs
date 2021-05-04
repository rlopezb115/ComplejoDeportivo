namespace servicio.api.negocio.servicio
{
    using ADO.contrato;
    using ADO.entidades;
    using negocio.contrato;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NegocioTipoComplejo : INegocioTipoComplejo
    {
        private readonly IRepositorioTipoComplejo _repositorio;

        public NegocioTipoComplejo(IRepositorioTipoComplejo repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TipoComplejo>> ObtenerListadoAsync()
        {
            return await _repositorio.ObtenerListadoAsync();
        }
    }
}