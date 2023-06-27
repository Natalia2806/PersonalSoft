using MongoDB.Bson;
using MongoDB.Driver;
using PruebaPersonalSoft.Models;

namespace PruebaPersonalSoft.Repositories.Polizas
{
    public class PolizaCollection : IPolizaCollection
    {
        internal MongoDBRepository _repository = new();
        private IMongoCollection<Poliza> _PolizaCollection;

        public PolizaCollection()
        {
            _PolizaCollection = _repository.database.GetCollection<Poliza>("Polizas");
        }

        public async Task DeletePoliza(string id)
        {
            var filter = Builders<Poliza>.Filter.Eq(poliza => poliza.Id, new ObjectId(id));
            await _PolizaCollection.DeleteOneAsync(filter);
        }

        public async Task<List<Poliza>> GetAllPoliza()
        {
            return await _PolizaCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Poliza> GetPolizaByParameters(string placaVehiculo, string numPoliza)
        {
            var filter = Builders<Poliza>.Filter.Eq(x => x.PlacaAutomotor, placaVehiculo) |
             Builders<Poliza>.Filter.Eq(x => x.NumeroPoliza, numPoliza);

            return await _PolizaCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertPoliza(Poliza poliza)
        {
            await _PolizaCollection.InsertOneAsync(poliza);
        }

        public async Task UpdatePoliza(Poliza poliza)
        {
            var filter = Builders<Poliza>
                .Filter
                .Eq(poliza => poliza.Id, poliza.Id);

            await _PolizaCollection.ReplaceOneAsync(filter, poliza);

        }
    }
}
