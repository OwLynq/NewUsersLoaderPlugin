using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NewUsersLoaderPlugin
{
    [DataContract]
    internal class DummyUserResponse
    {
        [DataMember]
        public List<DummyUser> users { get; set; }
    }

    [DataContract]
    internal class DummyUser
    {
        [DataMember] public string firstName { get; set; }
        [DataMember] public string lastName { get; set; }
        [DataMember] public string phone { get; set; }
    }
}
