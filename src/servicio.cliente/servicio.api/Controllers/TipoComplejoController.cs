using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicio.api.ADO.entidades;
using servicio.api.Dto;
using servicio.api.negocio.contrato;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace servicio.api.Controllers
{
    [EnableCors("PolicyCORSRegular")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoComplejoController : ControllerBase
    {
        private readonly INegocioTipoComplejo _negocio;
        private readonly ILogger<TipoComplejoController> _logger;
        private readonly IMapper _mapper;

        public TipoComplejoController(
            ILogger<TipoComplejoController> logger,
            INegocioTipoComplejo negocio,
            IMapper mapper)
        {
            _negocio = negocio;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<ComplejoController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TipoComplejoDto>>> Get()
        {
            List<TipoComplejo> response = null;

            try
            {
                response = await _negocio.ObtenerListadoAsync();
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        status = StatusCodes.Status500InternalServerError,
                        title = "Error interno del servidor, algo salió mal!!!"
                    });
            }

            return Ok(_mapper.Map<List<TipoComplejoDto>>(response));
        }
    }
}