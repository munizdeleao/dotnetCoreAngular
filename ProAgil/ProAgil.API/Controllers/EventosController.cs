using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IProAgilRepository _context;
        public EventosController(IProAgilRepository context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _context.GetAllEventoAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _context.GetAllEventoByTemaAsync(tema, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.GetAllEventoByIdAsync(id, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Evento evento)
        {
            try
            {
                _context.Add(evento);
                if (await _context.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{evento.Id}", evento);
                }
                else
                {
                    throw new Exception("Erro ao adicionar evento");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Evento evento)
        {
            try
            {
                _context.Update(evento);
                if (await _context.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{evento.Id}", evento);
                }
                else
                {
                    throw new Exception("Erro ao alterar evento");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _context.GetAllEventoByIdAsync(id, false);
                _context.Delete(evento);
                if (await _context.SaveChangesAsync())
                {
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    throw new Exception("Erro ao adicionar evento");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}