using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Interface
{
    public interface IUsuario
    {
        public List<TblUsuario> ObtenerUsuarios(string? nombreUsuario);

        public TblUsuario ObtenerUsuarios(int idUsuario);

        public TblUsuario ActualizarUsuarios(TblUsuario usr);

        public TblUsuario AgregarUsuario(TblUsuario usr);

        public TblUsuario EliminarUsuarios(int idUsuario);

        public void AgregarUsuarios(List<TblUsuario> lstUsuarios);
    }
}
