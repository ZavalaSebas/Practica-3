using APIKN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIKN.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("ConsultaUsuarios")]
        public List<TUsuario> ConsultaUsuarios()
        {
            try
            {
                using (var context = new BDKNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.TUsuario
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<TUsuario>();
            }
        }

        [HttpGet]
        [Route("ConsultaUsuario")]
        public TUsuario ConsultaUsuario(long q)
        {
            try
            {
                using (var context = new BDKNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.TUsuario
                            where x.ConUsuario == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("ConsultarProvincias")]
        public List<System.Web.Mvc.SelectListItem> ConsultarProvincias()
        {
            try
            {
                using (var context = new BDKNEntities())
                {
                    var datos = (from x in context.TProvincia
                                 select x).ToList();

                    List<System.Web.Mvc.SelectListItem> direcciones = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        direcciones.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ConProvincia.ToString(),
                            Text = item.Descripcion
                        });
                    }

                    return direcciones;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpPut]
        [Route("ActualizarCuenta")]
        public string ActualizarCuenta(UsuarioEnt entidad)
        {
            try
            {
                using (var context = new BDKNEntities())
                {
                    context.ActualizarCuentaSP(entidad.Identificacion, entidad.Nombre, entidad.Correo, entidad.ConProvincia, entidad.ConUsuario);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoUsuario")]
        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            using (var context = new BDKNEntities())
            {
                context.ActualizarEstadoUsuarioSP(entidad.ConUsuario);
                return "OK";
            }
        }

    }
}
