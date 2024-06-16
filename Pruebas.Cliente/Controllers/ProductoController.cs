using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProducto _producto;

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }
       
        public IActionResult ProductoIndex()
        {            
            var lstProducto = _producto.ObtenerProductos(string.Empty);

            return View(lstProducto);
        }

        //public IActionResult EditarProductos(int id)
        //{
        //    var producto = _producto.ObtenerProductos(id);

        //    ViewBag.IdProducto = id;

        //    return View(producto);
        //}

        [HttpPost]

        public IActionResult EditarProductos(TblProducto Producto, int IdProducto)
        {
            try
            {
                var pro = _producto.ActualizarProducto(Producto);

                return View(pro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult AgregarProducto()
        {
            TblProducto producto = new TblProducto();

            return View(producto);
        }

        [HttpPost]
        public IActionResult AgregarProducto(TblProducto producto)
        {
            _producto.AgregarProducto(producto);

            return View(producto);
        }

        //public IActionResult EliminarProducto(int id)
        //{
        //    var producto = _producto.ObtenerProducto(id);

        //    ViewBag.IdProducto = id;

        //    return View(producto);
        //}

        public IActionResult EliminarsProductoConfirm(int idProducto)
        {
            var producto = _producto.EliminarProducto(idProducto);

            return Json(producto);
        }
    }
}
