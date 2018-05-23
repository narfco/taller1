using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DispatcherApp.Models.Response
{
    public class AgreementJsonResponse
    {
        [JsonProperty("idFactura")]
        public int IdFactura { get; set; }

        [JsonProperty("valorFactura")]
        public double ValorFactura { get; set; }

        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }
    }
}
