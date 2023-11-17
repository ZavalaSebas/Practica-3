using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebKN.Entities;
using WebKN.Models;

namespace WebKN.Controllers
{
    public class ProductoController : Controller
    {
        ProductoModel modelProducto = new ProductoModel();

        [HttpGet]
        public ActionResult ConsultaProductos()
        {
            var datos = modelProducto.ConsultaProductos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {
            entidad.Imagen = string.Empty;
            entidad.Estado = true;

            long conProducto = modelProducto.RegistrarProducto(entidad);

            if (conProducto > 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImgProducto.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + conProducto + extension;
                ImgProducto.SaveAs(ruta);

                entidad.Imagen = "/Images/" + conProducto + extension;
                entidad.ConProducto = conProducto;

                modelProducto.ActualizarRutaProducto(entidad);

                return RedirectToAction("ConsultaProductos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult CambiarEstadoProducto(long q)
        {
            var entidad = new ProductoEnt();
            entidad.ConProducto = q;

            string respuesta = modelProducto.CambiarEstadoProducto(entidad.ConProducto);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaProductos", "Producto");
            }
            else
            {
                ViewBag.MensajeProducto = "No se ha podido cambiar el estado del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarProducto(long q)
        {
            var entidad = modelProducto.ConsultaProductos().FirstOrDefault(p => p.ConProducto == q);

            if (entidad != null)
            {
                return View(entidad);
            }
            else
            {
                return RedirectToAction("ConsultaProductos", "Producto");
            }
        }

        [HttpPost]
        public ActionResult ActualizarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {

            return RedirectToAction("ConsultaProductos", "Producto");
        }
    }
}
