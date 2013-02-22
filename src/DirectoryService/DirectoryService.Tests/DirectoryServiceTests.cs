using System.Threading;
using NUnit.Framework;

namespace DirectoryService.Tests
{
    [TestFixture]
    public class DirectoryServiceTests
    {
        [Test]
        public void RegisterServicebyType()
        {
            var directoryService = new DirectoryServices.DirectoryService();

            directoryService.RegisterMeAsService("library");

            Thread.Sleep(360000);
        }
    }
}