using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace DirectoryServices
{
    public class DirectoryService
    {
        private EmbeddableDocumentStore _docStore;
        //private RavenDbHttpServer server;

        public void RegisterMeAsService(string library)
        {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8081);

            _docStore = new EmbeddableDocumentStore {DataDirectory = "Data", UseEmbeddedHttpServer = true};
            var ravenConfiguration = _docStore.Configuration;
            ravenConfiguration.Port = 8081;
            ravenConfiguration.WebDir = "Web";
            ravenConfiguration.DataDirectory = "Data";
            ravenConfiguration.DefaultStorageTypeName = "esent";
            _docStore.Initialize();

                
            //server = new RavenDbHttpServer(_docStore.Configuration, _docStore.DocumentDatabase);
            //server.Start();
        }
    }
}