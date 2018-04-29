using backend.CommunicationModels;
using backend.Models;
using backend.WebCore;
using backend.WebCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class SetoresController : BaseController
    {

        public SetoresController(BackendContext context) : base(context)
        { }

        // GET api/Setores
        [HttpGet]
        public List<Setores> Get()
        {
            return context.Setores.ToList();
        }

        // GET api/Setores/{rid}
        [HttpGet("{rid}")]
        public Setores PorRid([FromRoute] string rid)
        {
            var consulta = context.Setores.FirstOrDefault(a => a.RID == rid);

            return consulta;
        }

        // POST api/Setores
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] Setores Model)
        {
            if (Autenticou)
            {
                context.Setores.Add(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Salvo com sucesso!"
                };
            }


            return new DefaultResponseModel
            {
                Mensagem = "Erro ao salvar",
                Sucesso = false
            };
        }

        // DELETE 
        [HttpPost]
        [Route("Delete")]
        public DefaultResponseModel Delete([FromBody] Setores Model)
        {
            if (Autenticou)
            {
                context.Setores.Remove(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Excluido com sucesso!"
                };
            }
            return new DefaultResponseModel
            {
                Mensagem = "Erro ao salvar",
                Sucesso = false
            };
        }

    }
}
