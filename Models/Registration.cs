﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggerRegistration.Models
{
    public class Registration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RegistrationId")]
        public string RegistrationId { get; set; }
        
        [BsonElement("EmailId")]
        public string EmailId { get; set; }

        [BsonElement("ErrorAlerts")]
        public int ErrorAlerts { get; set; }

        [BsonElement("CompanyName")]
        public string CompanyName { get; set; }

        [BsonElement("ProjectName")]
        public string ProjectName { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("ServiceName")]
        public string ServiceName { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Emails")]
        public List<Email>? Emails { get; set; }

        [BsonElement("LastEmailAlert")]
        public DateTime? LastEmailAlert { get; set; }
    }
}
