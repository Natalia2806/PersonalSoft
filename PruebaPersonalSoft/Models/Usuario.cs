using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PruebaPersonalSoft.Models
{
    public class Usuario
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string? Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }    

        public string Rol { get; set; }
    }
}
