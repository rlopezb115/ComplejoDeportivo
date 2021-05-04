namespace servicio.api.negocio.contrato
{
    using ADO.entidades;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INegocioJefe
    {
        Task<List<Jefe>> ObtenerListadoAsync();
    }
}
