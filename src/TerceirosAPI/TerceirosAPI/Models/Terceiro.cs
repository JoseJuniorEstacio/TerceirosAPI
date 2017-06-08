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
    public class Terceiro
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
        [Required]
        public string Cpf { get; set; }

        [DataMember]
        [Required]
        public string Nome { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [DataMember]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [DataMember]
        [Required]
        public string Status { get; set; } = "S";

        [DataMember]
        public DadosGestor DadosGestor { get; set; }

        [DataMember]
        public Endereco Endereco { get; set; }

        [DataMember]
        [BsonExtraElements]
        public IDictionary<string, object> DadosTerceiros { get; set; }


    }
}
