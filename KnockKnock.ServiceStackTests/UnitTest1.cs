using System;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using KnockKnockSS.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;

namespace KnockKnock.ServiceStackTests
{
    [TestFixture]
    public class UnitTest1
    {
        [OneTimeSetUp]
        public void Populate()
        {
            
            var knock = new KnockDto()
            {
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
            var svc = new KnockKnockMongo();
            svc.Any(new KnockPostV1 {Knock = knock});
            
            Console.WriteLine(knock.Id);
        }

        [Test]
        public void Posts()
        {
            
        }
    }
}
