using DispatcherApp.Contacts.XML;
using Hystrix.Dotnet;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DispatcherApp.Services
{
    public class XmlServices
    {
        private readonly IHystrixCommand hystrixCommand;

        public XmlServices(IHystrixCommandFactory hystrixCommandFactory)
        {
            hystrixCommand = hystrixCommandFactory.GetHystrixCommand("XmlGroup", "XmlCommand");
        }
        public async Task<string> GetAsync(Uri url, string transformation)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    respuesta = SoapCallingService(url, "consultar", transformation);
                    return true;
                });

            return respuesta;
        }
        public async Task<string> PostAsync(Uri url, string transformation)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    respuesta = SoapCallingService(url, "pagar", transformation);
                    return true;
                });

            return respuesta;
        }
        public async Task<string> DeleteAsync(Uri url, string transformation)
        {
            var respuesta = string.Empty;
            var result = await hystrixCommand.ExecuteAsync(
                async () =>
                {
                    respuesta = SoapCallingService(url, "compensar", transformation);
                    return true;
                });

            return respuesta;
        }

        private static string SoapCallingService(Uri url, string action, string transformacion)
        {
            string respuesta = string.Empty;
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(transformacion);
            HttpWebRequest webRequest = CreateWebRequest(url.ToString(), action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
            using (var response = webRequest.GetResponse())
            {
                using (var rd = new StreamReader(response.GetResponseStream()))
                {
                    respuesta = rd.ReadToEnd();
                }
            }
            return respuesta;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string transformacion)
        {
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:sch=""http://www.servicios.co/pagos/schemas"">
                    <soapenv:Header/>
                      <soapenv:Body>
                         " + transformacion +
                      "</soapenv:Body>" +
                    "</soapenv:Envelope>");
            return soapEnvelopeXml;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
