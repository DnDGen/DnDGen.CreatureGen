using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Collections;
using Ninject;

namespace CharacterGen.Bootstrap.Factories
{
    public static class CollectionsMapperFactory
    {
        public static CollectionsMapper CreateWith(IKernel kernel)
        {
            CollectionsMapper mapper = kernel.Get<CollectionsXmlMapper>();
            mapper = new CollectionsMapperCachingProxy(mapper);

            return mapper;
        }
    }
}