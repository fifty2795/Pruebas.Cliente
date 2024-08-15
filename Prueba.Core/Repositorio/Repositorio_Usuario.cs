using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prueba.Core.Interfaces;
using Prueba.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Repositorio
{
    public class Repositorio_Usuario
    {
        private readonly MvcContext _dbContext;
        private readonly IUsuario _iUsuario;

        public Repositorio_Usuario(MvcContext mvcContext, IUsuario iUsuario) 
        {
            _dbContext = mvcContext;
            _iUsuario = iUsuario;
        }

        public async Task AgregarUsuarios(List<TblUsuario> lstUsuarios)
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

                    await _dbContext.Database.ExecuteSqlRawAsync("EXEC [SP_InsertUsuario] @nombre, @apellido_paterno, @apellido_materno, @identificacion, @cargo, @activo, @fecha_creacion", nombre, apellidoPaterno, apellidoMaterno, identificacion, cargo, activo, fechaCreacion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
