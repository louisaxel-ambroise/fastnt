﻿using FasTnT.IntegrationTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FasTnT.IntegrationTests.API.EpcisEndpoints.v1_2.XML.ListSubscriptionIDs
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class WhenPerformingAnInvalidPollRequest : BaseIntegrationTest
    {
        public override async Task Act()
        {
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "YWRtaW46UEBzc3cwcmQ=");
            Result = await Client.PostAsync("/v1_2/Query.svc", new StringContent(File.ReadAllText("Requests/Queries/Poll_Unknown.xml"), Encoding.UTF8, "application/xml"));
        }

        [Assert]
        public void ItShouldReturnHttp400BadRequest() => Assert.AreEqual(HttpStatusCode.BadRequest, Result.StatusCode);

        [Assert]
        public async Task ItShouldReturnANotNullContent()
        {
            var content = await Result.Content.ReadAsStringAsync();

            Assert.IsNotNull(content);
        }

        [Assert]
        public async Task ItShouldReturnAValidXmlContent()
        {
            var content = await Result.Content.ReadAsStringAsync();
            var xmlDocument = XDocument.Parse(content);

            Assert.IsNotNull(xmlDocument);
        }

        [Assert]
        public async Task ItShouldReturnANoSuchNameExceptionResult()
        {
            var content = await Result.Content.ReadAsStringAsync();
            var xmlDocument = XDocument.Parse(content);
            var soapBody = xmlDocument.Root.Element(XName.Get("Body", "http://schemas.xmlsoap.org/soap/envelope/"));

            Assert.AreEqual("NoSuchNameException", soapBody.Elements().First().Name.LocalName);
        }
    }
}
