using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;
using Pruebas.Cliente.Utilidades;

namespace Pruebas.Cliente.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        public async Task<IActionResult> Index(string? nombreUsuario, int pageNumber)
        {
            int pageSize = 5;

            ViewBag.Busqueda = nombreUsuario;

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                nombreUsuario = string.Empty;
            }

            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            
            var response = new ResponseViewModel<TblUsuario>();

            try
            {
                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    nombreUsuario = string.Empty;
                }

                var lstUsuario = await _usuario.ObtenerUsuarios(nombreUsuario, pageNumber, pageSize);
                
                response.DataList = lstUsuario;
                response.Message = "Usuarios encontrados";

                return View(response);                
            }
            catch (Exception ex)
            {                
                var tblUsuario = new ResponseViewModel<TblUsuario>
                {
                    DataList = new PaginatedList<TblUsuario?>(),
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al obtener la lista de usuarios: {ex.Message}"
                    }
                };
                return View(tblUsuario);
            }
        }

        public IActionResult EditarUsuario(int id)
        {
            var response = new ResponseViewModel<TblUsuario>();

            response.Error = new Utilidades.ErrorViewModel();

            var usuario = _usuario.ObtenerUsuarios(id);

            response.Data = usuario;

            ViewBag.IdUsuario = id;

            return View(response);
        }

        [HttpPost]

        public IActionResult EditarUsuario(TblUsuario usuario, int IdUsuario)
        {
            var response = new ResponseViewModel<TblUsuario>();

            try
            {
                var usr = _usuario.ActualizarUsuarios(usuario);

                response.Data = usr;
                response.Message = "Se actualizo el usuario";

                return View(response);
            }
            catch (Exception ex)
            {
                var tblUsuario = new ResponseViewModel<TblUsuario>
                {
                    Data = new TblUsuario(),
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al actualizar el usuario: {ex.Message}"
                    }
                };

                return View(tblUsuario);
            }
        }

        public IActionResult AgregarUsuario()
        {
            TblUsuario usuario = new TblUsuario();

            return View(usuario);
        }

        [HttpPost]
        public IActionResult AgregarUsuario(TblUsuario usuario)
        {
            _usuario.AgregarUsuario(usuario);

            return View(usuario);
        }

        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _usuario.ObtenerUsuarios(id);

            ViewBag.IdUsuario = id;

            return View(usuario);
        }

        public IActionResult EliminarUsuarioConfirm(int idUsuario)
        {
            try
            {
                var response = new ResponseViewModel<TblUsuario>();

                var usuario = _usuario.EliminarUsuarios(idUsuario);

                response.Success = true;
                response.Message = "Se elimino el usuario";

                return Json(response);
            }
            catch (Exception ex)
            {
                var tblUsuario = new ResponseViewModel<TblUsuario>
                {
                    Data = new TblUsuario(),
                    Success = false,
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al eliminar el usuario: {ex.Message}"
                    }
                };
                return Json(tblUsuario);
            }
        }

        //[HttpPost]
        public async Task<IActionResult> ExportarExcel(string? nombreUsuario)
        {            
            string excelName = $"Usuarios-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                nombreUsuario = string.Empty;
            }

            try
            {                
                var lstUsuario = await _usuario.ObtenerUsuariosToList(nombreUsuario);             

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Usuarios");

                    // Añadir el encabezado
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Nombre";
                    worksheet.Cell(1, 3).Value = "Apellido Paterno";
                    worksheet.Cell(1, 4).Value = "Apellido Materno";
                    worksheet.Cell(1, 5).Value = "Identificacion";
                    worksheet.Cell(1, 6).Value = "Cargo";
                    worksheet.Cell(1, 7).Value = "Activo";
                    worksheet.Cell(1, 8).Value = "Fecha Creacion";

                    int row = 2;
                    // Añadir los datos                    
                    foreach (var usuario in lstUsuario)
                    {
                        worksheet.Cell(row, 1).Value = usuario.IdUsuario;
                        worksheet.Cell(row, 2).Value = usuario.Nombre;
                        worksheet.Cell(row, 3).Value = usuario.ApellidoPaterno;
                        worksheet.Cell(row, 4).Value = usuario.ApellidoMaterno;
                        worksheet.Cell(row, 5).Value = usuario.Identificacion;
                        worksheet.Cell(row, 6).Value = usuario.Cargo;
                        worksheet.Cell(row, 7).Value = usuario.Activo;
                        worksheet.Cell(row, 8).Value = usuario.FechaCreacion.ToString();

                        row++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile fileInput)
        {
            try
            {
                if (fileInput == null || fileInput.Length == 0)
                {
                    return BadRequest("No se ha proporcionado un archivo.");
                }

                var response = new ResponseViewModel<TblUsuario>();

                var usuarios = new List<TblUsuario>();

                using (var stream = new MemoryStream())
                {
                    await fileInput.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed();

                        var firstRowUsed = worksheet.FirstRowUsed();
                        // Obtener el número de la primera fila utilizada
                        int firstRowNumber = firstRowUsed.RowNumber();

                        foreach (var row in rows)
                        {
                            if (row.RowNumber() == firstRowNumber)
                            {
                                // Omitir la primera fila (encabezado)
                                continue;
                            }

                            var usuario = new TblUsuario();                            

                            //usuario.IdUsuario = row.Cell(1).GetValue<int>();
                            usuario.Nombre = row.Cell(2).GetString();
                            usuario.ApellidoPaterno = row.Cell(3).GetString();
                            usuario.ApellidoMaterno = row.Cell(4).GetString();
                            usuario.Identificacion = row.Cell(5).GetValue<int>();
                            usuario.Cargo = row.Cell(6).GetString();
                            usuario.Activo = row.Cell(7).GetValue<bool>();

                            string fecha = row.Cell(8).GetString();
                            usuario.FechaCreacion = Convert.ToDateTime(fecha);

                            usuarios.Add(usuario);
                        }
                    }
                }    
                // Guardar los productos en la base de datos
                _usuario.AgregarUsuarios(usuarios);
                
                var lstUsuario = await _usuario.ObtenerUsuarios(string.Empty, 1, 5);
                response.DataList = lstUsuario;
                response.Message = "Usuarios encontrados";
                
                return PartialView("UsuariosPartialView", response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
