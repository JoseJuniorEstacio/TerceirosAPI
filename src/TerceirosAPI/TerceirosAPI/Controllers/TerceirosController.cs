using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TerceirosAPI.Models;
using TerceirosAPI.Database;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;

namespace TerceirosAPI.Controllers
{
    [Route("api/terceiros")]
    public class TerceirosController : Controller
    {

        TerceiroContext dbContext = new TerceiroContext();
        // GET api/values
        [HttpGet]
        public string Get()
        {
            
            List <Terceiro> terceiroList = dbContext.Terceiros.Find<Terceiro>(m => true).ToList();
            /*foreach (Terceiro terc in terceiroList)
            {
                if (terc.DadosTerceiro != null) {
                    JObject json = JObject.Parse(terc.DadosTerceiro);
                    terc.DadosTerceiro = json;
                }
            }*/
            var result = JsonConvert.SerializeObject(terceiroList,Formatting.Indented);
            return result;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Terceiro Get(string id)
        {
            var entity = dbContext.Terceiros.Find(m => m.MongoId == id).FirstOrDefault();
            return entity;
        }

        // GET api/cpf/XXX
        [HttpGet]
        [Route("cpf/{cpf}")]
        public Terceiro GetByCPF(string cpf)
        {
            var entity = dbContext.Terceiros.Find(m => m.Cpf == cpf).FirstOrDefault();
            return entity;
        }

        // GET api/values/5
        [HttpGet]
        [Route("nome/{nome}")]
        public IEnumerable<Terceiro> GetByNome(string nome)
        {
            var collection = dbContext.Terceiros;
            var query = collection.AsQueryable<Terceiro>().Where(c => c.Nome.Contains(nome));
            return query;
        }

        // GET api/values/5
        [HttpGet]
        [Route("status/{status}")]
        public IEnumerable<Terceiro> GetByStatus(string status)
        {
            var collection = dbContext.Terceiros;
            var query = collection.AsQueryable<Terceiro>().Where(c => c.Status.Equals(status));
            return query;
        }

        // GET api/values/5
        [HttpGet]
        [Route("gestor/{gestor}")]
        public IEnumerable<Terceiro> GetByGestor(string gestor)
        {
            var collection = dbContext.Terceiros;
            var query = collection.AsQueryable<Terceiro>().Where(c => c.DadosGestor.Matricula.Equals(gestor));
            return query;
        }

        // POST api/values
        [HttpPost]
        public IEnumerable<Terceiro> PostTerceiroFromBody([FromBody]Terceiro value)
        {

            dbContext.Terceiros.InsertOne(value);

            List<Terceiro> terceiroList = dbContext.Terceiros.Find<Terceiro>(m => true).ToList();
            return terceiroList;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IEnumerable<Terceiro> Put(string id, [FromBody]Terceiro value)
        {
            var entity = dbContext.Terceiros.Find(m => m.MongoId == id).FirstOrDefault();
            value._id = entity._id;
            value.CreatedOn = entity.CreatedOn;
            value.Status = entity.Status;

            var terceiroLog = new TerceirosLog();

            terceiroLog.TerceiroId = entity.MongoId;
            terceiroLog.Cpf = entity.Cpf;
            terceiroLog.Terceiro = JsonConvert.SerializeObject(entity);

            dbContext.Terceiros.ReplaceOne(m => m.MongoId == id, value);

            dbContext.TerceirosLog.InsertOne(terceiroLog);
            List<Terceiro> terceiroList = dbContext.Terceiros.Find<Terceiro>(m => true).ToList();
            return terceiroList;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IEnumerable<Terceiro> Delete(string id)
        {
            var entity = dbContext.Terceiros.Find(m => m.MongoId == id).FirstOrDefault();
            entity.UpdatedOn = DateTime.Now;
            entity.Status = "N";

            var terceiroLog = new TerceirosLog();

            terceiroLog.TerceiroId = entity.MongoId;
            terceiroLog.Cpf = entity.Cpf;
            terceiroLog.Terceiro = JsonConvert.SerializeObject(entity);

            dbContext.Terceiros.ReplaceOne(m => m.MongoId == id, entity);
            dbContext.TerceirosLog.InsertOne(terceiroLog);
            List<Terceiro> terceiroList = dbContext.Terceiros.Find<Terceiro>(m => true).ToList();
            return terceiroList;
        }
    }
}
