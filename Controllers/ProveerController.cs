using TpApi.Abstracts;
using TpApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace TpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveerController : ControllerBase
    {
        private readonly IApplication<Proveedor> _proveedores;
        public ProveerController(IApplication<Proveedor> proveedores)
        {
            _proveedores = proveedores;
        }

        [HttpGet]
        public ActionResult GetProveedores()
        {
            var query = from proveedor in _proveedores.GetAll()
                        select new
                        {    
                            Id = proveedor.Id,
                            Nombre = proveedor.Nombre,
                            Correo = proveedor.Correo,
                            Cuil = proveedor.Cuil,
                            FechaAlta = proveedor.FechaAlta,
                            Productos = proveedor.Productos != null  
                                ? proveedor.Productos.Select(p => new { p.Id, p.Nombre, p.Precio }) 
                                : null //Transformar lista a Coleccion
                        };
            return Ok(query);
        }

        [HttpGet("{id}")]
        public ActionResult GetProveedor(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest("El Id no puede ser nulo ni cero.");
            }

            try
            {
                var proveedor = _proveedores.GetAll().FirstOrDefault(p => p.Id == id);
                
                if (proveedor == null)
                {
                    return NotFound();
                }
                var resultado = new {
                    Id = proveedor.Id,
                    Nombre = proveedor.Nombre,
                    Correo = proveedor.Correo,
                    Cuil = proveedor.Cuil,
                    FechaAlta = proveedor.FechaAlta,
                    Productos = proveedor.Productos?.Select(p => new { p.Id, p.Nombre, p.Precio })
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Proveedor> CreateProveedor(Proveedor proveedor)
        {
            proveedor.Productos ??= new List<Producto>();

            _proveedores.Create(proveedor);
            
            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id}")]
        public ActionResult<Proveedor> UpdateProveedor(int id, Proveedor proveedor)
        {
            var existingProveedor = _proveedores.GetAll().FirstOrDefault(p => p.Id == id);
            if (existingProveedor == null)
            {
                return NotFound();
            }

            existingProveedor.Nombre = proveedor.Nombre;
            existingProveedor.Correo = proveedor.Correo;
            existingProveedor.Cuil = proveedor.Cuil;
            existingProveedor.FechaAlta = proveedor.FechaAlta;
            
            _proveedores.Update(existingProveedor);

            return Ok(existingProveedor);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProveedor(int id)
        {
            var existingProveedor = _proveedores.GetAll().FirstOrDefault(p => p.Id == id);
            if (existingProveedor == null)
            {
                return NotFound();
            }

            _proveedores.Delete(existingProveedor.Id);
            return NoContent();
        }
    }
}
