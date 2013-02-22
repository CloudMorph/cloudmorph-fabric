using System.Collections.Generic;
using NUnit.Framework;

namespace LibraryService.Tests
{
    [TestFixture]
    public class LibraryClientTests
    {
        [Test]
        public void GettingPackageWithAllDependencies()
        {
            var libraryService = new LibraryService();

            var repositories = new [] { "Packages", @"C:\Dev\Tools\NuGetFeed", "https://nuget.org/api/v2"};

            libraryService.RepositoryLocations = repositories;

            libraryService.GetPackage("NuGet.Server");
        }

        [Test]
        public void UnpackAlldependencies()
        {
            var libraryService = new LibraryService();

            libraryService.UnpackPackage("NuGet.Server", "Unpack");
        }
    }
}