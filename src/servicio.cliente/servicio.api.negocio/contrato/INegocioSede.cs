namespace servicio.api.negocio.contrato
{
    using ADO.entidades;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INegocioSede
    {
        Task<List<Sede>> ObtenerListadoAsync();
    }
}