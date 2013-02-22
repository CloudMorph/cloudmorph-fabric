using System;
using System.Configuration;
using System.IO;

namespace LibraryService
{
    public class PackageUtility
    {
        private static Lazy<string> _packagePhysicalPath = new Lazy<string>(ResolvePackagePath);

        public static string PackagePhysicalPath
        {
            get
            {
                return _packagePhysicalPath.Value;
            }
        }

        private static string ResolvePackagePath()
        {
            // The packagesPath could be an absolute path (rooted and use as is)
            // or a virtual path (and use as a virtual path)
            string path = ConfigurationManager.AppSettings["packagesPath"];

            var path1 =
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            var pp = new Uri(path1).LocalPath;

            if (String.IsNullOrEmpty(path))
            {
                // Default path
                return Path.Combine(pp, "Packages");
            }

            if (path.StartsWith("~/"))
            {
                return path;
            }

            return path;
        }
    }
}