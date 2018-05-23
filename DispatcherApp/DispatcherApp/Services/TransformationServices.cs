using DispatcherApp.Models.Enums;
using DispatcherApp.Models.Request;
using Hystrix.Dotnet;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherApp.Services
{
    public class TransformationServices
    {
        private readonly Uri requestUrl = new Uri("http://localhost:8081/servicios/transformar/v1/transformdata");
        private readonly IHystrixCommand hystrixCommand;
        public TransformationServices(IHystrixCommandFactory hystrixCommandFactory)
        {
            hystrixCommand = hystrixCommandFactory.GetHystrixCommand("TestGroup", "TestCommand");
        }

        public async Task<string> PostAsync(TransformationRequest request)
        {
            var response = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                        using (var client = new HttpClient())
                        {
                            var content = new StringContent(
                                JsonConvert.SerializeObject(
                                new
                                {
                                    idFactura = request.IdFactura,
                                    valorFactura = request.ValorFactura,
                                    formato = request.Formato.ToString(),
                                    operacion = request.Operacion.ToString()
                                }), Encoding.UTF8, "application/json");

                            var resp = await client.PostAsync(requestUrl, content);
                            response = await resp.Content.ReadAsStringAsync();
                        }
                    return true;
                });
            return response;
        }
    }
}
