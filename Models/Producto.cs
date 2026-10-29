namespace TpApi.Model;

using System.ComponentModel.DataAnnotations.Schema;

public class Producto : Entity
{   
    [Column(TypeName = "varchar(100)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Descripcion { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }    

    // Clave Foranea hacia Proveedor
    public int ProveedorId { get; set; }

    [ForeignKey("ProveedorId")]
    public virtual Proveedor? Proveedor { get; set; }
}

