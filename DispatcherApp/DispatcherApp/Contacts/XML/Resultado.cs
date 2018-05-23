using System.Xml.Serialization;

namespace DispatcherApp.Contacts.XML
{
    public class Resultado
    {
        private ReferenciaFactura _referenciaFactura;

        private string _mensaje;

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
        public string mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
            }
        }
    }
}