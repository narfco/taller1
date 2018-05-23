using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DispatcherApp.Contacts.XML
{
    public class ReferenciaFactura
    {
        protected string _referenciaFactura;
        public string referenciaFactura
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
    }
}