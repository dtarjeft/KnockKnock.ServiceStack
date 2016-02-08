using System;
using System.Collections.Generic;
using System.Linq;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using ServiceStack.ServiceInterface;

namespace KnockKnockSS.ServiceInterface
{
    public class KnockKnockMongo : Service
    {
        internal IMongoClient Mongo { get; set; } = new MongoClient();
        internal Func<IMongoDatabase> Database;
        public KnockKnockMongo()
        {
            Database = () => Mongo.GetDatabase("test");
        }
        public KnockDto Get(KnockGetV1 request)
        {
            var repo = Database().GetCollection<Potato>("KnockKnock");

            return repo.Find(Builders<Potato>.Filter.Eq(k => k.Id, request.Id)).FirstOrDefault();
        }

        public ICollection<KnockDto> Get(ByLocationGetV1 request)
        {
            var db = Database().GetCollection<Potato>("KnockKnock");
            return db.Find( //TODO: Make this less bullshit.
                Builders<Potato>.Filter.Near(k => k.PotatoLoc, request.Longitude, request.Latitude, request.Radius, 0))
                .ToEnumerable()
                .Cast<KnockDto>()
                .ToList();
        } 

        public void Any(KnockPostV1 request)
        {
            var db = new MongoClient().GetDatabase("test");
            var collection = db.GetCollection<Potato>("KnockKnock");
            collection.FindOneAndReplace(Builders<Potato>.Filter.Eq(k => k.Id, request.Knock.Id), new Potato(request.Knock),
                new FindOneAndReplaceOptions<Potato, Potato>() { IsUpsert = true });
        }

    }

    internal class Potato : KnockDto
    {
        public Potato()
        {
            
        }
        public Potato(KnockDto other)
        {  
            Id = other.Id;
            FeedId = other.FeedId;
            Content = other.Content;
            Message = other.Message;
            Location = other.Location;
            PotatoLoc = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(other.Location.Longitude, other.Location.Latitude));
        }
        [BsonId]
        public override long Id { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> PotatoLoc { get; set; }
    }
}