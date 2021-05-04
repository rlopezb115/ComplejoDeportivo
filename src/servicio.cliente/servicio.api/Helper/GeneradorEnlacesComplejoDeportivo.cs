namespace servicio.api.Helper
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using servicio.api.common.respuesta;
    using servicio.api.Dto;
    using servicio.api.Model;

    public class GeneradorEnlacesComplejoDeportivo
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlacesComplejoDeportivo(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            this.urlHelperFactory = urlHelperFactory;
            this.actionContextAccessor = actionContextAccessor;
        }

        private IUrlHelper ConstruirURLHelper()
        {
            return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public RespuestaTablaDatos<ComplejoDeportivoCompletoDto> GenerarEnlaces(RespuestaTablaDatos<ComplejoDeportivoCompletoDto> complejos)
        {
            complejos.data.ForEach(a => GenerarEnlaces(a));
            return complejos;
        }

        public void GenerarEnlaces(ComplejoDeportivoCompletoDto complejo)
        {
            var _urlHelper = ConstruirURLHelper();
            complejo.Enlaces.Add(new Enlace(_urlHelper.Link("ObtenerComplejo", new { id = complejo.ComplejoId }), rel: "seleccionar", metodo: "GET", id: complejo.ComplejoId.ToString()));
            complejo.Enlaces.Add(new Enlace(_urlHelper.Link("ActualizarComplejo", new { id = complejo.ComplejoId }), rel: "actualizar", metodo: "PUT", id: complejo.ComplejoId.ToString()));
            complejo.Enlaces.Add(new Enlace(_urlHelper.Link("EliminarComplejo", new { id = complejo.ComplejoId }), rel: "eliminar", metodo: "DELETE", id: complejo.ComplejoId.ToString()));
        }
    }
}