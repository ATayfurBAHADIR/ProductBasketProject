using System;
using System.Collections.Generic;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence
{
    public class Maps
    {
        public static void Configure()
        {
            // MongoDB veritabanındaki Collection'ları Map'leme işlemi yapıyoruz. 


            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdField(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Description).SetIsRequired(true);
                map.MapMember(x => x.Quantity).SetIsRequired(true);
                map.MapMember(x => x.UnitPrice).SetIsRequired(true);
                map.MapMember(x => x.PictureUrl).SetIsRequired(true);
                map.MapMember(x => x.Active).SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdField(x => x.Id);
                map.MapMember(x => x.Password).SetIsRequired(true);
                map.MapMember(x => x.Username).SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Settings>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdField(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Value).SetIsRequired(true);
            });


        }
    }
}
