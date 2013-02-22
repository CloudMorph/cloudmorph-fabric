using Ninject.Modules;
using NuGet;

namespace LibraryService
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            //IServerPackageRepository packageRepository = new ServerPackageRepository(PackageUtility.PackagePhysicalPath);
            IServerPackageRepository packageRepository = new CloudPackageRepository(PackageUtility.PackagePhysicalPath);
            Bind<IHashProvider>().To<CryptoHashProvider>();
            Bind<IServerPackageRepository>().ToConstant(packageRepository);
            //Bind<PackageService>().ToSelf();
            //Bind<IPackageAuthenticationService>().To<PackageAuthenticationService>();
        }
    }
}