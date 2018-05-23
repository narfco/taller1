using DispatcherApp.Models.Enums;
using Newtonsoft.Json;

namespace DispatcherApp.Models.Request
{
    public class TransformationRequest
    {
        public int IdFactura { get; set; }
        public int ValorFactura { get; set; }
        public FormatsType Formato { get; set; }
        public OperationType Operacion { get; set; }
    }
}
