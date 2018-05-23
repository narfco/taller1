using RegistryRoutingApp.Models;
using TinyCsvParser.Mapping;

namespace RegistryRoutingApp.Mapper
{
    public class RegistryMapping: CsvMapping<RegistryServices>
    {
        public RegistryMapping()
        {
            MapProperty(0, x => x.IdAgreement);
            MapProperty(1, x => x.NameEnterprise);
            MapProperty(2, x => x.EndpointUrl);
            MapProperty(3, x => x.FormatTransformation);
        }
    }
}
