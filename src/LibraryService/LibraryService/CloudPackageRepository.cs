using System.Linq;
using NuGet;

namespace LibraryService
{
    public class CloudPackageRepository : PackageRepositoryBase, IPackageLookup, IServerPackageRepository
    {
        public CloudPackageRepository(string physicalPath)
        {
            
        }

        public override IQueryable<IPackage> GetPackages()
        {
            throw new System.NotImplementedException();
        }

        public override string Source
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool SupportsPrereleasePackages
        {
            get { throw new System.NotImplementedException(); }
        }

        public IPackage FindPackage(string packageId, SemanticVersion version)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string packageId, SemanticVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void RemovePackage(string packageId, SemanticVersion version)
        {
            throw new System.NotImplementedException();
        }

        public Package GetMetadataPackage(IPackage package)
        {
            throw new System.NotImplementedException();
        }
    }
}