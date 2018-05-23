using CsvHelper;
using RegistryRoutingApp.Mapper;
using RegistryRoutingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace RegistryRoutingApp.Repositories
{
    public class RegistryRepository : IRegistryRepository
    {
        public async Task<RegistryResponse> GetRegistryByID(int id)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
            RegistryMapping csvMapper = new RegistryMapping();
            CsvParser<RegistryServices> csvParser =
                 new CsvParser<RegistryServices>(csvParserOptions, csvMapper);
            RegistryResponse resultList = null;
            var list = new List<RegistryResponse>();
            try
            {
                string fileName = $"Doc//RegistryServices.csv";
                var result = csvParser
                   .ReadFromFile(fileName, Encoding.UTF8).Skip(1)
                   .ToList();

                result.ForEach(x => 
                {
                    list.Add(new RegistryResponse
                    {
                        Id = Convert.ToInt32(x.Result.IdAgreement.ToString()),
                        NombreEmpresa = x.Result.NameEnterprise.ToString(),
                        EndpointUrl = new Uri($"http://{x.Result.EndpointUrl}"),
                        Formato = x.Result.FormatTransformation.ToString()
                    });
                });
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            return list.Where(x=> x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
