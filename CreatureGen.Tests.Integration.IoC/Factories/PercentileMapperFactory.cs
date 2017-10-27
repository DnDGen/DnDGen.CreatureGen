using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Percentiles;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
{
    public static class PercentileMapperFactory
    {
        public static PercentileMapper CreateWith(IKernel kernel)
        {
            PercentileMapper mapper = kernel.Get<PercentileXmlMapper>();
            mapper = new PercentileMapperCachingProxy(mapper);

            return mapper;
        }
    }
}