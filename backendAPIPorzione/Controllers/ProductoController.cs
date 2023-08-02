using AutoMapper;
using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace backendAPIPorzione.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //readonly PorzioneapiContext _context;
        readonly IProductoRepository _productoRepository;
        readonly IMapper _mapper;

        public ProductoController(IProductoRepository productoRepository, IMapper mapper)
        {
            //_context = context;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> PostProductos([FromBody] ProductoDto productoDto)
        {
            try
            {
                if (productoDto is null)
                    return BadRequest();


                Producto modelo = _mapper.Map<Producto>(productoDto);

                await _productoRepository.CreateEntity(modelo);

                return CreatedAtRoute("GetProductoById", new { modelo.Id }, modelo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetProductoById")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetProductoById(int id)
        {
            try
            {
                Producto modelo = await _productoRepository.GetEntity(p => p.Id == id);

                if (modelo is null)
                    return NotFound();

                return Ok(modelo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetProductos(int page, int pageSize)
        {
            try
            {
                List<Producto> listaProductos = await _productoRepository.GetAll();
                List<Producto> productosPaginados = listaProductos.Skip((page - 1)* pageSize).Take(pageSize).ToList();

                return Ok(productosPaginados);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RemoveProducto(int id)
        {
            try
            {
                Producto modelo = await _productoRepository.GetEntity(p => p.Id == id);

                if (modelo is null)
                    return NotFound();

                await _productoRepository.Delete(modelo);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }  
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] UpdateProductoDto productoDto)
        {
            try
            {
                Producto modelo = await _productoRepository.GetEntity(p => p.Id == id, tracked:false);

                if (modelo is null)
                    return BadRequest();

                modelo = _mapper.Map<Producto>(productoDto);
                await _productoRepository.UpdateProducto(modelo);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        
    }
}
