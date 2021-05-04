namespace servicio.api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using servicio.api.ADO.entidades;
    using servicio.api.common.extension;
    using servicio.api.common.respuesta;
    using servicio.api.common.solicitud;
    using servicio.api.Dto;
    using servicio.api.Helper;
    using servicio.api.negocio.contrato;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [EnableCors("PolicyCORSRegular")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplejoController : ControllerBase
    {
        private readonly ILogger<ComplejoController> _logger;
        private readonly INegocioComplejo _negocio;
        private readonly IMapper _mapper;

        public ComplejoController(
            ILogger<ComplejoController> logger,
            INegocioComplejo negocio,
            IMapper mapper)
        {
            _logger = logger;
            _negocio = negocio;
            _mapper = mapper;
        }

        // GET: api/<ComplejoController>
        [HttpGet(Name = "ObtenerComplejos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(HATEOASComplejoDeportivoFilterAttribute))]
        public async Task<ActionResult<RespuestaTablaDatos<ComplejoDeportivoCompletoDto>>> Get()
        {
            int.TryParse(Request.Query["draw"][0].ToString(), out int draw);
            int.TryParse(Request.Query["start"][0].ToString(), out int start);
            int.TryParse(Request.Query["length"][0].ToString(), out int length);
            string search = Request.Query["search[value]"][0].ToString();

            RespuestaTablaDatos<ComplejoDeportivoCompleto> response = null;
            try
            {
                response = await _negocio.ObtenerTablaDatosAsync(new SolicitudTablaDatos
                {
                    Iniciar = start,
                    Tamano = length,
                    Buscar = search
                });
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

            return Ok(new RespuestaTablaDatos<ComplejoDeportivoCompletoDto>
            {
                draw = draw,
                recordsTotal = response.recordsTotal,
                recordsFiltered = response.recordsFiltered,
                data = _mapper.Map<List<ComplejoDeportivoCompletoDto>>(response.data ?? new List<ComplejoDeportivoCompleto>())
            });
        }

        // GET api/<ComplejoController>/5
        [HttpGet("{id}", Name = "ObtenerComplejo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(HATEOASComplejoDeportivoFilterAttribute))]
        public async Task<ActionResult<ComplejoDeportivoCompletoDto>> Get(int id)
        {
            ComplejoDeportivoCompleto response = null;
            try
            {
                response = await _negocio.ObtenerAsync(id);
                if (!response.EsValido())
                {
                    return NotFound();
                }
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

            return Ok(_mapper.Map<ComplejoDeportivoCompletoDto>(response));
        }

        // POST api/<ComplejoController>
        [HttpPost(Name = "CrearComplejo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComplejoDeportivo>> Post([FromBody] ComplejoDeportivo param)
        {
            ComplejoDeportivo response = null;
            try
            {
                response = await _negocio.InsertarAsync(param);
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

            return Ok(response);
        }

        // PUT api/<ComplejoController>/5
        [HttpPut("{id}", Name = "ActualizarComplejo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] ComplejoDeportivo param)
        {
            bool response = false;
            try
            {
                response = await _negocio.ActualizarAsync(id, param);
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

            return Ok(response);
        }

        // DELETE api/<ComplejoController>/5
        [HttpDelete("{id}", Name = "EliminarComplejo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool response = false;
            try
            {
                response = await _negocio.EliminarAsync(id);
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

            return Ok(response);
        }
    }
}