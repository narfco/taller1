using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DispatcherApp.Contacts.XML
{
    [DataContract]
    public class Pago
    {
        private ReferenciaFactura _referenciaFactura;

        private double _totalPagar;

        [DataMember]
        public ReferenciaFactura referenciaFactura
        {
            get
            {
                return _referenciaFactura;
            }
            set
            {
                _referenciaFactura = value;
            }
        }

        [DataMember]
        public double totalPagar
        {
            get
            {
                return _totalPagar;
            }
            set
            {
                _totalPagar = value;
            }
        }
    }
}