using System.IO;
using NuGet;

namespace LibraryService
{
    public class LocalPackageRepositoryEx : LocalPackageRepository
    {
        public LocalPackageRepositoryEx(string physicalPath) : base(physicalPath)
        {
        }

        public LocalPackageRepositoryEx(string physicalPath, bool enableCaching) : base(physicalPath, enableCaching)
        {
        }

        public LocalPackageRepositoryEx(IPackagePathResolver pathResolver, IFileSystem fileSystem) : base(pathResolver, fileSystem)
        {
        }

        public LocalPackageRepositoryEx(IPackagePathResolver pathResolver, IFileSystem fileSystem, bool enableCaching) : base(pathResolver, fileSystem, enableCaching)
        {
        }

        protected override string GetPackageFilePath(string id, SemanticVersion version)
        {
            //var packageDirectory = this.PathResolver.GetPackageDirectory(id, version);
            var packageFileName = this.PathResolver.GetPackageFileName(id, version);
            //return Path.Combine(packageDirectory, packageFileName);

            if (Path.GetExtension(packageFileName) != ".nupkg")
                packageFileName += ".nupkg";

            return packageFileName;
        }

        protected override string GetPackageFilePath(IPackage package)
        {
            //var packageDirectory = this.PathResolver.GetPackageDirectory(package);
            var packageFileName = this.PathResolver.GetPackageFileName(package);
            //return Path.Combine(packageDirectory, packageFileName);

            if (Path.GetExtension(packageFileName) != ".nupkg")
                packageFileName += ".nupkg";

            return packageFileName;
        }
    }
}