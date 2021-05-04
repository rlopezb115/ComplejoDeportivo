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
    public class JefeController : ControllerBase
    {
        private readonly INegocioJefe _negocio;
        private readonly ILogger<JefeController> _logger;
        private readonly IMapper _mapper;

        public JefeController(
            ILogger<JefeController> logger,
            INegocioJefe negocio,
            IMapper mapper)
        {
            _negocio = negocio;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<ComplejoController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<JefeDto>>> Get()
        {
            List<Jefe> response = null;

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
            return Ok(_mapper.Map<List<JefeDto>>(response));
        }
    }
}