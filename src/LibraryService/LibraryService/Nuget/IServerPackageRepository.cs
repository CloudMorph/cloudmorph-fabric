using NuGet;

namespace LibraryService
{
    public interface IServerPackageRepository
    {
        void RemovePackage(string packageId, SemanticVersion version);
        Package GetMetadataPackage(IPackage package); 
    }
}