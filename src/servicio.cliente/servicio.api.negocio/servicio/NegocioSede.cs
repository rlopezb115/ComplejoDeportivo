namespace servicio.api.negocio.servicio
{
    using ADO.contrato;
    using ADO.entidades;
    using negocio.contrato;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NegocioSede : INegocioSede
    {
        private readonly IRepositorioSede _repositorio;

        public NegocioSede(IRepositorioSede repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Sede>> ObtenerListadoAsync()
        {
            return await _repositorio.ObtenerListadoAsync();
        }
    }
}