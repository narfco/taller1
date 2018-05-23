using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DispatcherApp.Contacts.XML
{
    [ServiceContract]
    interface PagosInterface
    {
        [OperationContract]
        Task<ResultadoConsulta> Cosultar(ReferenciaFactura referenciaFactura);
        [OperationContract]
        Task<Resultado> Compensar(Pago pago);
        [OperationContract]
        Task<Resultado> Pagar(Pago pago);
    }
}
