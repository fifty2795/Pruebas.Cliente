using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;
using Pruebas.Cliente.Utilidades;

namespace Pruebas.Cliente.Repositorio
{
    public class Repositorio_Usuario : IUsuario
    {
        private readonly MvcContext _dbContext;

        public Repositorio_Usuario(MvcContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<TblUsuario>> ObtenerUsuarios(string? nombreUsuario, int pageNumber, int pageSize)
        {
            try
            {
                return await PaginatedList<TblUsuario>.CreateAsync(_dbContext.TblUsuarios.AsNoTracking().Where(x => x.Nombre.Contains(nombreUsuario)), pageNumber, pageSize);
                //return _dbContext.TblUsuarios.Where(x => x.Nombre.Contains(nombreUsuario)).ToList();                
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

        public async Task<List<TblUsuario>> ObtenerUsuariosToList(string nombreUsuario)
        {
            try
            {
                return await _dbContext.TblUsuarios.Where(x => x.Nombre.Contains(nombreUsuario)).ToListAsync();
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
                foreach (var item in lstUsuarios)
                {
                    var nombre = new SqlParameter("@nombre", item.Nombre);
                    var apellidoPaterno = new SqlParameter("@apellido_paterno", item.ApellidoPaterno);
                    var apellidoMaterno = new SqlParameter("@apellido_materno", item.ApellidoMaterno);
                    var identificacion = new SqlParameter("@identificacion", item.Identificacion);
                    var cargo = new SqlParameter("@cargo", item.Cargo);
                    var activo = new SqlParameter("@activo", item.Activo);
                    var fechaCreacion = new SqlParameter("@fecha_creacion", item.FechaCreacion);

                    _dbContext.Database.ExecuteSqlRaw("EXEC [SP_InsertUsuario] @nombre, @apellido_paterno, @apellido_materno, @identificacion, @cargo, @activo, @fecha_creacion", nombre, apellidoPaterno, apellidoMaterno, identificacion, cargo, activo, fechaCreacion);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
