using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;

namespace UsersLoaderPlugin
{
    [Author(Name = "Yaroslav Melnikov")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> input)
        {
            logger.Info("Loading new users");
            var usersList = new List<DataTransferObject>(input);

            try
            {
                string url = "https://dummyjson.com/users";
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(DummyUserResponse));
                    var result = (DummyUserResponse)serializer.ReadObject(stream);

                    foreach (var user in result.users)
                    {
                        var dto = new EmployeesDTO
                        {
                            Name = $"{user.firstName} {user.lastName}",
                            Phone = user.phone
                        };
                        usersList.Add(dto);                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Plugin error: {ex.Message}");
            }
            logger.Info($"Loaded {usersList.Count()} users");
            return usersList.Cast<DataTransferObject>();
        }
    }

    [DataContract]
    public class DummyUserResponse
    {
        [DataMember]
        public List<DummyUser> users { get; set; }
    }

    [DataContract]
    public class DummyUser
    {
        [DataMember] public string firstName { get; set; }
        [DataMember] public string lastName { get; set; }
        [DataMember] public string phone { get; set; }
    }
}
