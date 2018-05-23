using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryRoutingApp.Models;
using RegistryRoutingApp.Repositories;

namespace RegistryRoutingApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Registry")]
    public class RegistryController : Controller
    {
        private readonly IRegistryRepository _registryRepository;
        public RegistryController(IRegistryRepository registryRepository)
        {
            _registryRepository = registryRepository;
        }

        [HttpGet]
        public async Task<RegistryResponse> GetConfigRouting(int id)
        {
            var result = _registryRepository.GetRegistryByID(id);
            return result.Result;
        }

    }
}