using System;
using TpApi.Abstracts;
using System.Text.Json.Serialization;
namespace TpApi.Model
{
    public class Entity : IEntity
    {
        [JsonIgnore] // Evita la referencia circular al serializar a JSON
        public int Id { get; set; }
    }
}
