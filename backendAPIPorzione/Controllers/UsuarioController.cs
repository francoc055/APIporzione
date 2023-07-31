using AutoMapper;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;
using backendAPIPorzione.Repository;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendAPIPorzione.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        readonly IUsuarioRepository _usuarioRepository;
        readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<Usuario> usuarios = await _usuarioRepository.GetAll();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                Usuario modelo = await _usuarioRepository.GetEntity(u => u.Id == id);

                if (modelo is null)
                    return NotFound();

                return Ok(modelo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                Usuario modelo = await _usuarioRepository.GetEntity(u => u.Id == id);

                if (modelo is null)
                    return NotFound();

                await _usuarioRepository.Delete(modelo);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioDto updateUsuarioDto)
        {
            try
            {
                Usuario modelo = await _usuarioRepository.GetEntity(p => p.Id == id, tracked: false);

                if (modelo is null)
                    return BadRequest();

                string rol = modelo.Rol;
                modelo = _mapper.Map<Usuario>(updateUsuarioDto);
                modelo.Rol = rol;
                await _usuarioRepository.UpdateUsuario(modelo);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
