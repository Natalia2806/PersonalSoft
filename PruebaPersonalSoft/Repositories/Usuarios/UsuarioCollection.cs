using Amazon.Auth.AccessControlPolicy;
using MongoDB.Bson;
using MongoDB.Driver;
using PruebaPersonalSoft.Models;

namespace PruebaPersonalSoft.Repositories.Usuarios
{
    public class UsuarioCollection : IUsuarioCollection
    {
        internal MongoDBRepository _repository = new();
        private IMongoCollection<Usuario> _UsuarioCollection;

        public UsuarioCollection()
        {
            _UsuarioCollection = _repository.database.GetCollection<Usuario>("Usuarios");
        }

        public async Task DeleteUsuario(string id)
        {
            var filter = Builders<Usuario>.Filter.Eq(usuario => usuario.Id, new ObjectId(id));
            await _UsuarioCollection.DeleteOneAsync(filter);
        }

        public async Task<List<Usuario>> GetAllUsuario()
        {
            return await _UsuarioCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Usuario> IniciarSesion(string correo, string password)
        {
            var filter = Builders<Usuario>.Filter.Eq(usuario => usuario.Correo, correo) & Builders<Usuario>.Filter.Eq(usuario => usuario.Password, password);

            return await _UsuarioCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertUsuario(Usuario usuario)
        {
            await _UsuarioCollection.InsertOneAsync(usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            var filter = Builders<Usuario>
                .Filter
                .Eq(usuario => usuario.Id, usuario.Id);

            await _UsuarioCollection.ReplaceOneAsync(filter, usuario);
        }
    }
}
