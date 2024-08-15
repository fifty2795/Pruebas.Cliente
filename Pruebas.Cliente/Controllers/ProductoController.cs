using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;
using Pruebas.Cliente.Utilidades;

namespace Pruebas.Cliente.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProducto _producto;

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }   

        public async Task<IActionResult> ProductoIndex(string? nombreProducto, int pageNumber)
        {
            int pageSize = 5;

            ViewBag.Busqueda = nombreProducto;

            if (string.IsNullOrEmpty(nombreProducto))
            {
                nombreProducto = string.Empty;
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var response = new ResponseViewModel<TblProducto>();

            try
            {
                if (string.IsNullOrEmpty(nombreProducto))
                {
                    nombreProducto = string.Empty;
                }

                var lstProducto = await _producto.ObtenerProductos(nombreProducto, pageNumber, pageSize);

                response.DataList = lstProducto;
                response.Message = "Productos encontrados";

                return View(response);
            }
            catch (Exception ex)
            {
                var tblProducto = new ResponseViewModel<TblProducto>
                {
                    DataList = new PaginatedList<TblProducto?>(),
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al obtener la lista de productos: {ex.Message}"
                    }
                };
                return View(tblProducto);
            }
        }

        public IActionResult EditarProducto(int id)
        {
            var response = new ResponseViewModel<TblProducto>();
            
            response.Error = new Utilidades.ErrorViewModel();

            var producto = _producto.ObtenerProductos(id);

            response.Data = producto;

            ViewBag.IdProducto = id;

            return View(response);
        }

        [HttpPost]

        public IActionResult EditarProducto(TblProducto producto, int IdProducto)
        {
            var response = new ResponseViewModel<TblProducto>();

            try
            {
                var productoResponse = _producto.ActualizarProducto(producto);

                response.Data = productoResponse;
                response.Message = "Se actualizo el producto";

                return View(response);
            }
            catch (Exception ex)
            {
                var tblProducto = new ResponseViewModel<TblProducto>
                {
                    Data = new TblProducto(),
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al actualizar el producto: {ex.Message}"
                    }
                };

                return View(tblProducto);
            }
        }

        public IActionResult AgregarProducto()
        {
            var response = new ResponseViewModel<TblProducto>();

            response.Data = new TblProducto();
            response.Error = new Utilidades.ErrorViewModel();

            return View(response);
        }

        [HttpPost]
        public IActionResult AgregarProducto(TblProducto producto)
        {  
            var response = new ResponseViewModel<TblProducto>();

            try
            {
                var productoResponse = _producto.AgregarProducto(producto);

                response.Data = productoResponse;
                response.Message = "Se actualizo el producto";

                return View(response);
            }
            catch (Exception ex)
            {
                var tblProducto = new ResponseViewModel<TblProducto>
                {
                    Data = new TblProducto(),
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al actualizar el producto: {ex.Message}"
                    }
                };

                return View(tblProducto);
            }
        }

        public IActionResult EliminarProducto(int id)
        {
            var producto = _producto.ObtenerProductos(id);

            ViewBag.IdProducto = id;

            return View(producto);
        }

        public IActionResult EliminarProductoConfirm(int idProducto)
        {
            try
            {
                var response = new ResponseViewModel<TblProducto>();

                var producto = _producto.EliminarProducto(idProducto);

                response.Success = true;
                response.Message = "Se elimino el producto";

                return Json(response);
            }
            catch (Exception ex)
            {
                var tblProducto = new ResponseViewModel<TblProducto>
                {
                    Data = new TblProducto(),
                    Success = false,
                    Error = new Utilidades.ErrorViewModel
                    {
                        Code = "ERROR_CODE",
                        Message = $"Error al eliminar el producto: {ex.Message}"
                    }
                };
                return Json(tblProducto);
            }
        }
    }
}
