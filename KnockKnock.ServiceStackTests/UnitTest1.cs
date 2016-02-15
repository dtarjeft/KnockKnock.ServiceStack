﻿using System;
using System.Linq;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using KnockKnockSS.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using Assert = NUnit.Framework.Assert;

namespace KnockKnock.ServiceStackTests
{
    [TestFixture]
    public class UnitTest1
    {
        [OneTimeSetUp]
        public void Populate()
        {
            var svc = new KnockKnockMongo();
            var db = svc.Database<PotatoKnock>();
            db.DeleteMany(Builders<PotatoKnock>.Filter.Empty);
            
            var knock = new KnockDto {
                FeedId = "potato",
                Id = new Random().Next(0, 100000),
                Location = new LocationDto
                {
                    Latitude = 45,
                    Longitude = 60
                },
                Message = "Turn me into a french fry?"
            };
            //using (var svc = new JsonServiceClient("http://localhost:40300/"))
            //{
            //    svc.Post(new KnockPostV1() {KnockDto = knock});
            //}
            svc.Any(new KnockPostV1 {Knock = knock});
            
            Console.WriteLine(knock.Id);
        }

        [Test]
        public void Gets()
        {
            var svc = new KnockKnockMongo();
            var knockDtos = svc.Get(new KnocksByLocationGetV1 {Latitude = 45.4, Longitude = 60.5, Radius = 75000.0});
            Assert.That(knockDtos.Any(), "Got no Knocks. Expected 1.");
        }
    }
}
