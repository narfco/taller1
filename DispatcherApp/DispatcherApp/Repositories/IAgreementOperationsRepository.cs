using DispatcherApp.Models.Request;
using DispatcherApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherApp.Repositories
{
    public interface IAgreementOperationsRepository
    {
        Task<object> AgreementOperations(AgreementServicesRequest request);
    }
}
