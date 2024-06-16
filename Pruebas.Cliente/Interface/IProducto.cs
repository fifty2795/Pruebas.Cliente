using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Interface
{
    public interface IProducto
    {
        public List<TblProducto> ObtenerProductos(string? nombreProducto);

        public TblProducto ObtenerProductos(int idProducto);

        public TblProducto ActualizarProducto(TblProducto pro);

        public TblProducto AgregarProducto(TblProducto pro);

        public TblProducto EliminarProducto(int idProducto);
    }
}
