using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using TpApi.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace TpApi.Model;

public class Proveedor : Entity
{
    [Column(TypeName = "varchar(100)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Correo { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Cuil { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaAlta { get; set; }

    // Un proveedor tiene una colección de muchos productos
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
