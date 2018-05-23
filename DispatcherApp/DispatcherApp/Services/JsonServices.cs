using Hystrix.Dotnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherApp.Services
{
    public class JsonServices
    {
        private readonly IHystrixCommand hystrixCommand;
        public JsonServices(IHystrixCommandFactory hystrixCommandFactory)
        {
            hystrixCommand = hystrixCommandFactory.GetHystrixCommand("JsonGroup", "JsonCommand");
        }

        public async Task<string> GetAsync(Uri url, int IdFactura)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    using (var client = new HttpClient())
                    {
                        var resp = await client.GetAsync($"{url}{IdFactura}");
                        respuesta = await resp.Content.ReadAsStringAsync();
                    }
                    return true;
                });

            return respuesta;
        }
        public async Task<string> PostAsync(Uri url, int IdFactura, string contenido)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    using (var client = new HttpClient())
                    {
                        var content = new StringContent(
                            contenido, 
                            Encoding.UTF8, 
                            "application/json");

                        var resp = await client.PostAsync($"{url}{IdFactura}", content);
                        respuesta = await resp.Content.ReadAsStringAsync();
                    }
                    return true;
                });

            return respuesta;
        }
        public async Task<string> DeleteAsync(Uri url, int IdFactura)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    using (var client = new HttpClient())
                    {
                        var resp = await client.DeleteAsync($"{url}{IdFactura}");
                         respuesta = await resp.Content.ReadAsStringAsync();
                    }
                    return true;
                });

            return respuesta;
        }
    }
}
