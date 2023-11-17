using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebKN.Entities;
using WebKN.Models;

namespace WebKN.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel modelUsuario = new UsuarioModel();

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            long conUsuario = long.Parse(Session["ConUsuario"].ToString());
            var datos = modelUsuario.ConsultaUsuarios().Where(x => x.ConUsuario != conUsuario);
            return View(datos);
        }

        [HttpGet]
        public ActionResult ActualizarEstadoUsuario(long q)
        {
            var entidad = new UsuarioEnt();
            entidad.ConUsuario = q;

            string respuesta = modelUsuario.ActualizarEstadoUsuario(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del usuario";
                return View();
            }            
        }


        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            long q = long.Parse(Session["ConUsuario"].ToString());
            var datos = modelUsuario.ConsultaUsuario(q);
            Session["Nombre"] = datos.Nombre;
            ViewBag.Direcciones = modelUsuario.ConsultarProvincias();
            return View(datos);
        }

        [HttpPost]
        public ActionResult PerfilUsuario(UsuarioEnt entidad)
        {
            string respuesta = modelUsuario.ActualizarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar su información";
                ViewBag.Direcciones = modelUsuario.ConsultarProvincias();
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(long q)
        {
            var datos = modelUsuario.ConsultaUsuario(q);
            ViewBag.Direcciones = modelUsuario.ConsultarProvincias();
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioEnt entidad)
        {
            string respuesta = modelUsuario.ActualizarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar su información";
                ViewBag.Direcciones = modelUsuario.ConsultarProvincias();
                return View();
            }
        }
        

    }
}