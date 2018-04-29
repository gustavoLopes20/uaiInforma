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
    public class MedicosUnidadesController : BaseController
    {

        public MedicosUnidadesController(BackendContext context) : base(context)
        { }

        // GET api/MedicosUnidades
        [HttpGet]
        [Route("Unidade/{und}")]
        public List<MedicosUnidades> Get([FromRoute] string und)
        {
            return context.MedicosUnidades
                .Include(m => m.Medico)
                .Include(u => u.Unidade)
                .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                .Where(a => a.Unidade.RID == und).ToList();
        }

        // GET api/MedicosUnidades/{rid}
        [HttpGet("{rid}")]
        public MedicosUnidades PorRid([FromRoute] string rid)
        {
            return context.MedicosUnidades.FirstOrDefault(a => a.Medico.RID == rid);
        }

        // POST api/MedicosUnidades
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] MedicosUnidades Model)
        {

            if (Autenticou)
            {
                context.MedicosUnidades.Update(Model);
                context.SaveChanges();

                return new DefaultResponseModel
                {
                    Mensagem = "Salvo com sucesso!"
                };

            }

            return new DefaultResponseModel
            {
                Mensagem = "Erro ao savar!",
                Sucesso = false
            };

        }

        // DELETE 
        [HttpPost]
        [Route("Delete")]
        public DefaultResponseModel Delete([FromBody] MedicosUnidades Model)
        {
            if (Autenticou)
            {
                context.MedicosUnidades.Remove(Model);
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
