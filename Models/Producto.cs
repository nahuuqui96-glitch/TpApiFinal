namespace TpApi.Model;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Producto : Entity
{   
    [Column(TypeName = "varchar(100)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Descripcion { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }    

    public int ProveedorId { get; set; }

    [ForeignKey("ProveedorId")]
    [JsonIgnore] // Evita la referencia circular al serializar a JSON
    public virtual Proveedor? Proveedor { get; set; }
}

