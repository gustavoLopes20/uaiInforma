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
    public class EspecialidadesController : BaseController
    {
        public EspecialidadesController(BackendContext context) : base(context)
        { }

        // GET api/Especialidades
        [HttpGet]
        public List<Especialidades> Get()
        {
            return context.Especialidades.ToList();
        }

        // GET api/Especialidades/{rid}
        [HttpGet("{rid}")]
        public Especialidades PorRid([FromRoute] string rid)
        {
            return context.Especialidades.First(a => a.RID == rid);
        }

        // POST api/Especialidades
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] Especialidades Model)
        {

            if (Autenticou)
            {
                context.Especialidades.Update(Model);
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
        public DefaultResponseModel Delete([FromBody] Especialidades Model)
        {
            if (Autenticou)
            {
                context.Especialidades.Remove(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Excluido com sucesso."
                };
            }
           
            return new DefaultResponseModel
            {
                Mensagem = "Erro ao salvar!",
                Sucesso = false
            };
        }

    }
}
