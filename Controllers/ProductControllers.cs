using TpApi.Abstracts;
using TpApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace TpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IApplication<Producto> _productos;

        public ProductController(IApplication<Producto> productos)
        {
            _productos = productos;
        }

        [HttpGet]
        public ActionResult GetProductos()
        {
            var query = from producto in _productos.GetAll()
                        select new
                        {    
                            Id = producto.Id,
                            Nombre = producto.Nombre,
                            Descripcion = producto.Descripcion,
                            Precio = producto.Precio,
                            ProveedorId = producto.ProveedorId,
                            Proveedor = producto.Proveedor != null 
                                ? new { producto.Proveedor.Id, producto.Proveedor.Nombre } 
                                : null //Transformar lista a Coleccion
                        };
            return Ok(query);
        }

        [HttpGet("{id}")]
        public ActionResult GetProducto(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest("El Id no puede ser nulo ni cero.");
            }

            try
            {
                var producto = _productos.GetAll().FirstOrDefault(p => p.Id == id);
                
                if (producto == null)
                {
                    return NotFound();
                }
                var resultado = new
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    ProveedorId = producto.ProveedorId,
                    Proveedor = producto.Proveedor != null 
                        ? new { producto.Proveedor.Id, producto.Proveedor.Nombre, producto.Proveedor.Correo } 
                        : null //Proyeccion 
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Producto> CreateProducto(Producto producto)
        {
            if (producto.ProveedorId <= 0)
            {
                return BadRequest("Se debe especificar un ProveedorId válido para el producto.");
            }

            _productos.Create(producto);
            
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> UpdateProducto(int id, Producto producto)
        {
            var existingProducto = _productos.GetAll().FirstOrDefault(p => p.Id == id);
            if (existingProducto == null)
            {
                return NotFound();
            }
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            existingProducto.Descripcion = producto.Descripcion;
            
            if (producto.ProveedorId > 0)
            {
                existingProducto.ProveedorId = producto.ProveedorId;
            }

            _productos.Update(existingProducto);

            return Ok(existingProducto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProducto(int id)
        {
            var existingProducto = _productos.GetAll().FirstOrDefault(p => p.Id == id);
            if (existingProducto == null)
            {
                return NotFound();
            }

            _productos.Delete(existingProducto.Id);
            return NoContent();
        }
    }
}
