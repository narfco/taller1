using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryRoutingApp.Models
{
    public class RegistryResponse
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public Uri EndpointUrl { get; set; }
        public string Formato { get; set; }
    }
}
