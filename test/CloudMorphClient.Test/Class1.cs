using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudMorphControl.Services.Realms;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;

namespace CloudMorphClient.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Async_Call_HelloWorld_with_ServiceClients_on_PreDefined_Routes()
        {
            var client = new JsonServiceClient("http://localhost:2003/svc");

            Realm response = null;
            //client.SendAsync<Realm>(new Realm { Name = "World!" },
            //    r => response = r, (r, e) => Assert.Fail("NetworkError"));

            response = client.Get<Realm>("/realms");

            //Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.That(response.Name, Is.EqualTo("Hello, World!"));
        }
    }
}
