using DispatcherApp.Contacts.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DispatcherApp.Models.Response
{
    public class ResultadoConsulta
    {
        public ReferenciaFactura referenciaFactura { get; set; }
        public double totalPagar { get; set; }
    }
}
