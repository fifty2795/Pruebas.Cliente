using Pruebas.Cliente.Models;
using Pruebas.Cliente.Utilidades;

namespace Pruebas.Cliente.Interface
{
    public interface IUsuario
    {
        public Task<PaginatedList<TblUsuario>> ObtenerUsuarios(string? nombreUsuario, int pageNumber, int pageSize);

        public TblUsuario ObtenerUsuarios(int idUsuario);

        public Task<List<TblUsuario>> ObtenerUsuariosToList(string nombreUsuario);        

        public TblUsuario ActualizarUsuarios(TblUsuario usr);

        public TblUsuario AgregarUsuario(TblUsuario usr);

        public TblUsuario EliminarUsuarios(int idUsuario);

        public void AgregarUsuarios(List<TblUsuario> lstUsuarios);
    }
}
