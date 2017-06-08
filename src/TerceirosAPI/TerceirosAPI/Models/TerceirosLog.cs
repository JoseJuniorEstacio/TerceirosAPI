using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;


namespace TerceirosAPI.Models
{

    [DataContract]
    public class TerceirosLog
    {

        [BsonId]
        public ObjectId _id { get; set; }

        [DataMember]
        public string MongoId
        {
            get { return _id.ToString(); }
            set { _id = ObjectId.Parse(value); }
        }

        [DataMember]
        public string TerceiroId { get; set; }

        [DataMember]
        public string Cpf { get; set; }

        [DataMember]
        public dynamic Terceiro { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
