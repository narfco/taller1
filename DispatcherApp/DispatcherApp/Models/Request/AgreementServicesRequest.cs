using DispatcherApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherApp.Models.Request
{
    public class AgreementServicesRequest
    {
        public TransformationRequest Transformation { get; set; }
        public Uri ServiceUri { get; set; }
    }
}
