using PruebaPersonalSoft.Models;

namespace PruebaPersonalSoft.Repositories.Usuarios
{
    public interface IUsuarioCollection
    {
        Task InsertUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(string id);
        Task<List<Usuario>> GetAllUsuario();
        Task<Usuario> IniciarSesion(string correo, string password);
    }
}
