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
    public class EspecialidadesUnidadesController : BaseController
    {

        public EspecialidadesUnidadesController(BackendContext context) : base(context)
        { }

        // GET api/EspecialidadesUnidades/Unidade/{rid}
        [HttpGet]
        [Route("Unidade/{und}")]
        public List<EspecialidadesUnidades> Get([FromRoute] string und)
        {
            return context.EspecialidadesUnidades
                          .Include(u => u.Unidade)
                          .Include(e => e.Especialidade)
                          .Where(a => a.Unidade.RID == und).ToList();
        }
        // GET api/EspecialidadesUnidades/{rid}
        [HttpGet("{rid}")]
        public EspecialidadesUnidades PorRid([FromRoute] string rid)
        {
            return context.EspecialidadesUnidades
                .Include(u => u.Unidade)
                .Include(e => e.Especialidade)
                .FirstOrDefault(a => a.Especialidade.RID == rid);
        }

        // POST api/EspecialidadesUnidades
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] EspecialidadesUnidades Model)
        {
            if (Autenticou)
            {
                context.EspecialidadesUnidades.Update(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Salvo com sucesso!"
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
        public DefaultResponseModel Delete([FromBody] EspecialidadesUnidades Model)
        {
            if (Autenticou)
            {
                context.EspecialidadesUnidades.Remove(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Excluido com sucesso!"
                };
            }
            return new DefaultResponseModel
            {
                Mensagem = "Erro ao excluir",
                Sucesso = false
            };
        }

    }
}
