using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Repositorio
{
    public class Repositorio_Usuario : IUsuario
    {
        private readonly PruebasContext _dbContext;

        public Repositorio_Usuario(PruebasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TblUsuario> ObtenerUsuarios(string? nombreUsuario)
        {
            try
            {
                return _dbContext.TblUsuarios.Where(x => x.Nombre.Contains(nombreUsuario)).ToList();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblUsuario ObtenerUsuarios(int idUsuario)
        {
            try
            {
                return _dbContext.TblUsuarios.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblUsuario ActualizarUsuarios(TblUsuario usr)
        {
            try
            {                
                _dbContext.Entry(usr).State = EntityState.Modified;

                _dbContext.Update(usr);
                _dbContext.SaveChanges();

                return usr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblUsuario AgregarUsuario(TblUsuario usr)
        {
            try
            {
                _dbContext.Entry(usr).State = EntityState.Added;

                _dbContext.Add(usr);
                _dbContext.SaveChanges();

                return usr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblUsuario EliminarUsuarios(int idUsuario)
        {
            try
            {
                var usr = _dbContext.TblUsuarios.SingleOrDefault(x => x.IdUsuario == idUsuario);

                _dbContext.Entry(usr).State = EntityState.Deleted;

                _dbContext.Remove(usr);
                _dbContext.SaveChanges();

                return usr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarUsuarios(List<TblUsuario> lstUsuarios)
        {
            try
            {
                //_dbContext.Entry(usr).State = EntityState.Added;

                foreach (var usr in lstUsuarios)
                {
                    _dbContext.Entry(usr).State = EntityState.Added;
                    _dbContext.Add(usr);
                    _dbContext.SaveChanges();
                }                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
