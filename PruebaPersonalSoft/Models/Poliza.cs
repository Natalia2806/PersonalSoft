using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PruebaPersonalSoft.Models
{
    public class Poliza
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string NumeroPoliza { get; set; }
        public string NombreCliente { get; set; }
        public string IdentificacionCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaTomoPoliza{ get; set; }
        public string CoberturasPoliza { get; set; }
        public int ValorMaxPoliza { get; set; }
        public string NombrePlanPoliza { get; set; }
        public string CiudadResidenciaCliente { get; set; }
        public string DireccionResidenciaCliente { get; set; }
        public string PlacaAutomotor { get; set; }
        public string ModeloAutomotor { get; set; }
        public bool InspeccionVehiculo   { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }

    }
}
