using DispatcherApp.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatcherApp.Repositories
{
    public interface ITransformationRepository
    {
        Task<string> TransformationCall(TransformationRequest request);
    }
}
