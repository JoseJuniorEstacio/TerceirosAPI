using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TerceirosAPI.Models
{
    public class Endereco
    {
        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string Cep { get; set; }

        [BsonExtraElements]
        public IDictionary<string, object> Dados { get; set; }
    }
}
