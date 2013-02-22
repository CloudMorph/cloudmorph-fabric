using NUnit.Framework;

namespace LibraryService.Tests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        [Test]
        public void GetAllPackages()
        {
            var library = new LibraryService();
            library.GetPackages();
        }

        [Test]
        public void SyncAllPackages()
        {
            var library = new LibraryService();
            library.SyncPackages();
        }
    }
}