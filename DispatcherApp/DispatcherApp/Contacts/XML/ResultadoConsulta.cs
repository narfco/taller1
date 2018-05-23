using System.Xml.Serialization;

namespace DispatcherApp.Contacts.XML
{
    public class ResultadoConsulta
    {
        private ReferenciaFactura _referenciaFactura;

        private double _totalPagar;

        [XmlElement]
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

        [XmlElement]
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