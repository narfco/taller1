using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DispatcherApp.Models.Request;
using DispatcherApp.Services;

namespace DispatcherApp.Repositories
{
    public class TransformationRepository : ITransformationRepository
    {
        private readonly TransformationServices _transformationServices;
        public TransformationRepository(TransformationServices transformationServices)
        {
            _transformationServices = transformationServices;
        }
        public async Task<string> TransformationCall(TransformationRequest request)
        {
            var result = await _transformationServices.PostAsync(request);
            return result;
        }
    }
}
