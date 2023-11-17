using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using WebKN.Entities;
using System.Configuration;

namespace WebKN.Models
{
    public class ProductoModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public long RegistrarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }

        public List<ProductoEnt> ConsultaProductos()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaProductos";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;
            }
        }

        public string ActualizarRutaProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarRutaProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        public string CambiarEstadoProducto(long conProducto)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "CambiarEstadoProducto?conProducto=" + conProducto;
                var res = client.PutAsync(urlApi, null).Result; // No se necesita enviar datos en el cuerpo para cambiar el estado
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ActualizarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarProducto";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

    }
}

