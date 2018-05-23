using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DispatcherApp.Models.Enums;
using DispatcherApp.Models.Request;
using DispatcherApp.Models.Response;
using DispatcherApp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DispatcherApp.Repositories
{
    public class AgreementOperationsRepository : IAgreementOperationsRepository
    {
        private JsonServices _jsonServices;
        private XmlServices _xmlServices;
        private readonly ITransformationRepository _transformationRepository;

        public AgreementOperationsRepository(
            ITransformationRepository transformationRepository,
            JsonServices jsonServices,
            XmlServices xmlServices)
        {
            _jsonServices = jsonServices;
            _xmlServices = xmlServices;
            _transformationRepository = transformationRepository;
        }

        public async Task<object> AgreementOperations(AgreementServicesRequest request)
        {
            object response = null;
            if (request.Transformation.Formato.Equals(FormatsType.Json))
            {
                var result = await JsonOperations(request);
                response = JsonConvert.DeserializeObject<AgreementJsonResponse>(result);
            }
            else if (request.Transformation.Formato.Equals(FormatsType.Xml))
            {
                var result = await XmlOperations(request);
                XDocument doc = XDocument.Parse(result);
                response = doc;
            }

            return response;
        }

        public async Task<string> JsonOperations(AgreementServicesRequest request)
        {
            var response = string.Empty;
            switch (request.Transformation.Operacion)
            {
                case OperationType.Consultar:
                    response = await _jsonServices.GetAsync(
                        request.ServiceUri,
                        request.Transformation.IdFactura);
                    break;
                case OperationType.Pagar:
                    var resp = await _transformationRepository.TransformationCall(request.Transformation);
                    var contenido = JObject.Parse(resp).GetValue("mensaje");
                    response = await _jsonServices.PostAsync(
                        request.ServiceUri,
                        request.Transformation.IdFactura,
                        contenido.ToString());
                    break;
                case OperationType.Compensar:
                    response = await _jsonServices.DeleteAsync(
                        request.ServiceUri,
                        request.Transformation.IdFactura);
                    break;
            }
            return response;
        }

        public async Task<string> XmlOperations(AgreementServicesRequest request)
        {
            var response = string.Empty;
            switch (request.Transformation.Operacion)
            {
                case OperationType.Consultar:
                    var responseTransformationConsultar =
                        await _transformationRepository.TransformationCall(request.Transformation);
                    var contenidoConsultar = JObject.Parse(responseTransformationConsultar).GetValue("mensaje");
                    response = await _xmlServices.GetAsync(
                        request.ServiceUri,
                        contenidoConsultar.ToString());

                    break;
                case OperationType.Pagar:
                    var responseTransformationPagar =
                        await _transformationRepository.TransformationCall(request.Transformation);
                    var contenidoPagar = JObject.Parse(responseTransformationPagar).GetValue("mensaje");
                    response = await _xmlServices.PostAsync(
                        request.ServiceUri,
                        contenidoPagar.ToString());

                    break;
                case OperationType.Compensar:
                    var responseTransformationCompensar =
                        await _transformationRepository.TransformationCall(request.Transformation);
                    var contenidoCompensar = JObject.Parse(responseTransformationCompensar).GetValue("mensaje");
                    response = await _xmlServices.DeleteAsync(
                        request.ServiceUri,
                        contenidoCompensar.ToString());

                    break;
            }
            return response;
        }
    }
}
