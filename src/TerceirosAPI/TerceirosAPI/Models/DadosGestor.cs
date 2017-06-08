using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;


namespace TerceirosAPI.Models
{
    public class DadosGestor
    {

        public string Matricula { get; set; }

        public string Nome { get; set; }

        [BsonExtraElements]
        public IDictionary<string, object> Dados { get; set; }
    }
}
