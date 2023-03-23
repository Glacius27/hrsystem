using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using shraredclasses.DTO;

namespace auth.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("UserID")]
        public string UserID { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        public string Password { get; set; }

        public User (CreateUserRequestDTO createUserRequest)
        {
            this.FirstName = createUserRequest.FirstName;
            this.LastName = createUserRequest.LastName;
            this.Email = createUserRequest.Email;
        }

    }
    //public class CreateUserRequest
    //{
    //    [JsonProperty("FirstName")]
    //    public string FirstName { get; set; }
    //    [JsonProperty("lastname")]
    //    public string LastName { get; set; }
    //    [JsonProperty("email")]
    //    public string Email { get; set; }
    //}
}

