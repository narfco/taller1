using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DispatcherApp.Models.Enums;
using DispatcherApp.Models.Request;
using DispatcherApp.Repositories;
using Hystrix.Dotnet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DispatcherApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Dispatcher")]
    public class DispatcherController : Controller
    {
        private readonly IAgreementOperationsRepository _agreementOperationsRepository;
        public DispatcherController(
            IAgreementOperationsRepository agreementOperationsRepository)
        {
            _agreementOperationsRepository = agreementOperationsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SendPay([FromBody] AgreementServicesRequest request)
        {
            var response = await _agreementOperationsRepository.AgreementOperations(request);

            if (request.Transformation.Formato.Equals(FormatsType.Json))
            {
                return Content(JsonConvert.SerializeObject(response));
            }
            return Content(response.ToString(), "application/xml");
        }
    }
}