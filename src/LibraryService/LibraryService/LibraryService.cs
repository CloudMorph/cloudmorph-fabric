using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Ninject;
using NuGet;

namespace LibraryService
{
    public class LibraryService
    {
        private List<string> _repositoryLocations;
        private List<IPackageRepository> _repositories;

        public void GetPackages()
        {
            //var repository = PackageRepositoryFactory.Default.CreateRepository("https://nuget.org/api/v2");
            var repository = PackageRepositoryFactory.Default.CreateRepository(@"C:\Dev\Tools\NuGetFeed");

            foreach(var package in repository.GetPackages())
            {
                Console.WriteLine(package.GetFullName());
            }
        }

        private IServerPackageRepository Repository
        {
            get
            {
                // It's bad to use the container directly but we aren't in the loop when this 
                // class is created
                return NinjectBootstrapper.Kernel.Get<IServerPackageRepository>();
            }
        }

        public void SyncPackages()
        {
            var rrr = Repository as ServerPackageRepository;

            var repositoryRemote = PackageRepositoryFactory.Default.CreateRepository("https://nuget.org/api/v2");
            var repositoryLocal = PackageRepositoryFactory.Default.CreateRepository(@"C:\Dev\Tools\NuGetFeed");

            foreach (var package in repositoryLocal.GetPackages())
            {
                //Console.WriteLine(package.GetFullName());

                var remotePackage = repositoryRemote.FindPackage(package.Id);

                if (remotePackage.Version != package.Version)
                {

                    Console.WriteLine("Sync!");

                    //string name = remotePackage.GetFullName();

                    //File.OpenWrite()
                    //remotePackage.GetStream()

                    //var wrapPackage = new PackageWrapper(remotePackage);

                    rrr.AddPackage(remotePackage);
                    //repositoryLocal.StartOperation()
                }
            }
        }

        public void GetPackage(string packageId)
        {
            GetPackage(packageId, null);
        }

        public void GetPackage(string packageId, IVersionSpec version)
        {
/*
            if (repositoryRemote == null)
            {
                //var repositoryRemote = PackageRepositoryFactory.Default.CreateRepository("https://nuget.org/api/v2");
                repositoryRemote = PackageRepositoryFactory.Default.CreateRepository(@"C:\Dev\Tools\NuGetFeed");
            }
            if (repositoryLocal == null)
            {
                repositoryLocal = GetLocalRepository();
            }
*/
            var packageInfo = FindPackage(packageId, version, false, false);

            if (packageInfo == null)
                throw new NotImplementedException();

            var localRepository = _repositories[0];
            if (localRepository != packageInfo.Item1)
            {
                localRepository.AddPackage(packageInfo.Item2);
            }

            foreach (var deps in packageInfo.Item2.DependencySets)
            {
                foreach (var dep in deps.Dependencies)
                {
                    GetPackage(dep.Id, dep.VersionSpec);
                }
            }
        }

        private Tuple<IPackageRepository, IPackage> FindPackage(string packageId, IVersionSpec versionSpec, bool allowPrereleaseVersions, bool allowUnlisted)
        {
            foreach (var repository in _repositories)
            {
                var package = repository.FindPackage(packageId, versionSpec, allowPrereleaseVersions, allowUnlisted);

                if (package != null)
                    return new Tuple<IPackageRepository, IPackage>(repository, package);
            }
            return null;
        }

        private IPackageRepository GetLocalRepository()
        {
            var localPath = GetLocalPath();
            //repositoryLocal = PackageRepositoryFactory.Default.CreateRepository();
            var repository = new LocalPackageRepositoryEx(localPath);
            return repository;
        }

        private static string GetLocalPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var localPath = new Uri(path).LocalPath;
            localPath = Path.Combine(localPath, @"NewPackages");
            return localPath;
        }

        public void UnpackPackage(string packageId, string folder)
        {
            var repositoryLocal = GetLocalRepository();

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            UnpackPackageWithDependencies(packageId, folder, repositoryLocal);
        }

        private void UnpackPackageWithDependencies(string packageId, string folder, IPackageRepository repository)
        {
            var package = repository.FindPackage(packageId);
            UnpackPackage(folder, package);

            var dependencies = package.GetCompatiblePackageDependencies(new FrameworkName(".NETFramework,Version=v4.0")).ToList();

            foreach (var dependency in dependencies)
            {
                var pack = repository.FindPackage(dependency.Id, dependency.VersionSpec, false, false);
                UnpackPackage(folder, pack);
            }
        }

        private void UnpackPackage(string folder, IPackage package)
        {
            var framework = new FrameworkName(".NETFramework,Version=v4.0");
            var libFiles = package.GetLibFiles();

            foreach (var file in libFiles)
            {
                if (file.TargetFramework.Version != framework.Version || file.TargetFramework.Identifier != framework.Identifier)
                    continue;

                string fileOut = Path.Combine(folder, file.EffectivePath);

                if (Path.GetExtension(fileOut) == ".dll")
                    UnpackFile(fileOut, file);
            }
        }

        private void UnpackFile(string fileOut, IPackageFile file)
        {
            var stream = File.OpenWrite(fileOut);
            file.GetStream().CopyTo(stream);
        }

        public IEnumerable<string> RepositoryLocations
        {
            set 
            { 
                _repositoryLocations = new List<string>(value);
                _repositories = new List<IPackageRepository>();
                foreach (var location in _repositoryLocations)
                {
                    Uri uriLocation = null;
                    if (Uri.IsWellFormedUriString(location, UriKind.Relative))
                    {
                        var localPath = GetLocalPath();
                        uriLocation = new Uri(new Uri(localPath), location);
                    }
                    else
                    {
                        uriLocation = new Uri(location);
                    }
                    if (uriLocation.IsFile)
                        _repositories.Add(new LocalPackageRepositoryEx(location));
                    else
                        _repositories.Add(PackageRepositoryFactory.Default.CreateRepository(location));
                }
            }
            get { return _repositoryLocations; }
        }
    }
}