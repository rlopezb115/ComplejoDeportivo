namespace servicio.api.ADO.contrato
{
    using ADO.entidades;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositorioSede
    {
        Task<List<Sede>> ObtenerListadoAsync();
    }
}
