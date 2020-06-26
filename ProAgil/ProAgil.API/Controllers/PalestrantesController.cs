using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using ProAgil.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestrantesController : ControllerBase
    {
        private readonly IProAgilRepository _context;
        public PalestrantesController(IProAgilRepository context)
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
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var result = await _context.GetAllPalestrantesByNameAsync(name, true);
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
                var result = await _context.GetAllPalestranteByIdAsync(id, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Palestrante model)
        {
            try
            {
                _context.Add(model);
                if (await _context.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{model.Id}", model);
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
        public async Task<IActionResult> Put(int id, [FromBody] Palestrante model)
        {
            try
            {
                _context.Update(model);
                if (await _context.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{model.Id}", model);
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
                var evento = await _context.GetAllPalestranteByIdAsync(id, false);
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