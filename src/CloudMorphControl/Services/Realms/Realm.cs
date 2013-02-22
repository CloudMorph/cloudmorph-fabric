using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace CloudMorphControl.Services.Realms
{
    [DataContract]
    [RestService("/realms")]
    public class Realm
    {
        [DataMember]
        public string Name { get; set; }
    }
}