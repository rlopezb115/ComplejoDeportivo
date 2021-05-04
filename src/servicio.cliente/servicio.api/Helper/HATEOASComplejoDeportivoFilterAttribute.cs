namespace servicio.api.Helper
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using servicio.api.common.respuesta;
    using servicio.api.Dto;
    using System;
    using System.Threading.Tasks;

    public class HATEOASComplejoDeportivoFilterAttribute : HATEOASFilterAttribute
    {
        private readonly GeneradorEnlacesComplejoDeportivo generadorEnlaces;

        public HATEOASComplejoDeportivoFilterAttribute(GeneradorEnlacesComplejoDeportivo generadorEnlaces)
        {
            this.generadorEnlaces = generadorEnlaces ?? throw new ArgumentNullException(nameof(generadorEnlaces));
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var incluirHATEOAS = DebeIncluirHATEOAS(context);

            if (!incluirHATEOAS)
            {
                await next();
                return;
            }

            var result = context.Result as ObjectResult;
            var model = result.Value as ComplejoDeportivoCompletoDto;
            if (model == null)
            {
                var modelList = result.Value as RespuestaTablaDatos<ComplejoDeportivoCompletoDto> ?? throw new ArgumentNullException("objeto no identificado");
                result.Value = generadorEnlaces.GenerarEnlaces(modelList);
                await next();
            }
            else
            {
                generadorEnlaces.GenerarEnlaces(model);
                await next();
            }
        }
    }
}