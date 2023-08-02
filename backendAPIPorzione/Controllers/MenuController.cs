using AutoMapper;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.Dto;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendAPIPorzione.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IMenuRepository _menuRepository;
        readonly IProductoRepository _productoRepository;
        readonly IDetalleRepository _detalleRepository;

        public MenuController(IMapper mapper, IMenuRepository menuRepository, IProductoRepository productoRepository, IDetalleRepository detalleRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
            _productoRepository = productoRepository;
            _detalleRepository = detalleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] MenuDto menuDto)
        {
            try
            {
                //MENU
                if (menuDto is null || menuDto.Productos.Count == 0)
                    return BadRequest();

                List<Producto> listaProductos = await _productoRepository.GetAll();

                var lista = menuDto.Productos.Select(x => x.Id).Intersect(listaProductos.Select(lp => lp.Id));

                if (menuDto.Productos.Count != lista.ToList().Count)
                    return BadRequest();

                Menu modeloMenu = _mapper.Map<Menu>(menuDto);

                await _menuRepository.CreateEntity(modeloMenu);

                //DETALLE
                /*DetalleDto detalle = new DetalleDto();

                detalle.IdMenu = modeloMenu.Id;

                Detalle modeloDetalle = new Detalle();
                modeloDetalle.IdMenu = detalle.IdMenu;

                foreach (var item in menuDto.Productos)
                {
                    modeloDetalle.Id = 0;
                    modeloDetalle.IdProducto = item.Id;
                    await _detalleRepository.CreateEntity(modeloDetalle);
                }*/

                await _detalleRepository.Crear(menuDto, modeloMenu.Id);



                return Created("exito", menuDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var modelo = await _menuRepository.GetMenu(id);
            //MenuDto menuDto = _mapper.Map<MenuDto>(modelo);

            if (modelo is null)
                return NotFound();

            return Ok(modelo);
        }
    }
}
