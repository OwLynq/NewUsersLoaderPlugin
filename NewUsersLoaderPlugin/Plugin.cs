using NewUsersLoaderPlugin;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;

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
            int newUsersCount = 0;
            try
            {
                string url = "https://dummyjson.com/users";
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                
                {
                    var serializer = new DataContractJsonSerializer(typeof(DummyUserResponse));
                    var result = (DummyUserResponse)serializer.ReadObject(stream);
                    newUsersCount = result.users.Count;
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
                logger.Error(ex, "Plugin error while loading users");
            }
            logger.Info($"Loaded {newUsersCount} users");
            return usersList.Cast<DataTransferObject>();
        }
    }   
}
