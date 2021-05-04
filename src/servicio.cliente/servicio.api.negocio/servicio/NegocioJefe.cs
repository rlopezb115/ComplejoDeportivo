namespace servicio.api.negocio.servicio
{
    using ADO.contrato;
    using ADO.entidades;
    using negocio.contrato;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NegocioJefe : INegocioJefe
    {
        private readonly IRepositorioJefe _repositorio;

        public NegocioJefe(IRepositorioJefe repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Jefe>> ObtenerListadoAsync()
        {
            return await _repositorio.ObtenerListadoAsync();
        }
    }
}