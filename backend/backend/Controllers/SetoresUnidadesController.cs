using backend.CommunicationModels;
using backend.Models;
using backend.WebCore;
using backend.WebCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class SetoresUnidadesController : BaseController
    {

        public SetoresUnidadesController(BackendContext context) : base(context)
        { }

        // GET api/SetoresUnidades/Unidade/{rid}
        [HttpGet]
        [Route("Unidade/{und}")]
        public List<SetoresUnidades> Get([FromRoute] string und)
        {
            return context.SetoresUnidades
                .Include(u=>u.Unidade)
                .Include(s=> s.Setor)
                .Where(a => a.Unidade.RID == und).ToList();
        }

        // GET api/SetoresUnidades/{rid}
        [HttpGet("{rid}")]
        public SetoresUnidades PorRid([FromRoute] string rid)
        {
       
            return context.SetoresUnidades
                .Include(u => u.Unidade)
                .Include(s => s.Setor)
                .FirstOrDefault(a => a.Setor.RID == rid);
        }

        // POST api/SetoresUnidades
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] SetoresUnidades Model)
        {
            if (Autenticou)
            {
                context.SetoresUnidades.Update(Model);
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
        public DefaultResponseModel Delete([FromBody] SetoresUnidades Model)
        {
            if (Autenticou)
            {
                context.SetoresUnidades.Remove(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Excluido com sucesso!"
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
