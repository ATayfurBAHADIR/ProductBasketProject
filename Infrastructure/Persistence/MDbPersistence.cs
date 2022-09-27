using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Infrastructure.Persistence
{
    public static class MDbPersistence
    {
        public static void Configure()
        {
            Maps.Configure();

            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };
            ConventionRegistry.Register("MongoDB Product Basket", pack, t => true);
        }
    }
}
