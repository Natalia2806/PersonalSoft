using PruebaPersonalSoft.Models;

namespace PruebaPersonalSoft.Repositories.Polizas
{
    public interface IPolizaCollection
    {
        Task InsertPoliza(Poliza poliza);
        Task UpdatePoliza(Poliza poliza);
        Task DeletePoliza(string id);
        Task<List<Poliza>> GetAllPoliza();
        Task<Poliza> GetPolizaByParameters(string placaVehiculo, string numPoliza);
    }
}
