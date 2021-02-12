using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BRules.Domain.SharedKernel
{
    public record DomainEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? CreatedBy { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
