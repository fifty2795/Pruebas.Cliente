using Pruebas.Cliente.Models;
using Pruebas.Cliente.Utilidades;

namespace Pruebas.Cliente.Interface
{
    public interface IProducto
    {
        public Task<PaginatedList<TblProducto>> ObtenerProductos(string? nombreProducto, int pageNumber, int pageSize);

        public TblProducto ObtenerProductos(int idProducto);

        public TblProducto ActualizarProducto(TblProducto pro);

        public TblProducto AgregarProducto(TblProducto pro);

        public TblProducto EliminarProducto(int idProducto);
    }
}
