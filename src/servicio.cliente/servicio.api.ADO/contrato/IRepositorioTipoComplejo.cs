namespace servicio.api.ADO.contrato
{
    using ADO.entidades;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositorioTipoComplejo
    {
        Task<List<TipoComplejo>> ObtenerListadoAsync();
    }
}