using RegistryRoutingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryRoutingApp.Repositories
{
    public interface IRegistryRepository
    {
        Task<RegistryResponse> GetRegistryByID(int id);
    }
}
