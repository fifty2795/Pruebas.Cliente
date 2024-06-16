using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Repositorio
{
    public class Repositorio_Producto : IProducto
    {
        private readonly MvcContext _dbContext;

        public Repositorio_Producto(MvcContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TblProducto> ObtenerProductos(string? nombreProducto)
        {
            try
            {
                return _dbContext.TblProductos.Where(x => x.Nombre.Contains(nombreProducto)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblProducto ObtenerProductos(int idProducto)
        {
            try
            {
                return _dbContext.TblProductos.Where(x => x.IdProducto == idProducto).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblProducto ActualizarProducto(TblProducto pro)
        {
            try
            {
                _dbContext.Entry(pro).State = EntityState.Modified;

                _dbContext.Update(pro);
                _dbContext.SaveChanges();

                return pro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblProducto AgregarProducto(TblProducto pro)
        {
            try
            {
                _dbContext.Entry(pro).State = EntityState.Added;

                _dbContext.Add(pro);
                _dbContext.SaveChanges();

                return pro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblProducto EliminarProducto(int idProducto)
        {
            try
            {
                var pro = _dbContext.TblProductos.SingleOrDefault(x => x.IdProducto == idProducto);

                _dbContext.Entry(pro).State = EntityState.Deleted;

                _dbContext.Remove(pro);
                _dbContext.SaveChanges();

                return pro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}