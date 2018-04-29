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
    public class MedicosController : BaseController
    {

        public MedicosController(BackendContext context) : base(context)
        { }

        // GET api/Unidades
        [HttpGet]
        public List<Medicos> Get()
        {
            return context.Medicos.ToList();
        }

        // GET api/Medicos/{rid}
        [HttpGet("{rid}")]
        public Medicos PorRid([FromRoute] string rid)
        {
            return context.Medicos.First(a => a.RID == rid);
        }

        // POST api/Medicos
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] Medicos Model)
        {
            if (Autenticou)
            {
                context.Medicos.Update(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Salvo com sucesso."
                };

            }

            return new DefaultResponseModel
            {
                Mensagem = "Erro ao salvar!",
                Sucesso = false
            };

        }

        // DELETE 
        [HttpPost]
        [Route("Delete")]
        public DefaultResponseModel Delete([FromBody] Medicos Model)
        {
            if (Autenticou)
            {
                context.Medicos.Remove(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Excluido com sucesso."
                };
            }
          
            return new DefaultResponseModel
            {
                Mensagem = "Erro ao excluir!",
                Sucesso = false
            };
        }

    }
}
