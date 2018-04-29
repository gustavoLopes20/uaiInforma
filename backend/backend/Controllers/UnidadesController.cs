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
    public class UnidadesController : BaseController
    {

        public UnidadesController(BackendContext context) : base(context)
        { }

        // GET api/Unidades
        [HttpGet]
        public List<Unidades> Get()
        {

            var unidades = context.Unidades.ToList();

            foreach (var u in unidades)
            {
                u.especialidades = context.EspecialidadesUnidades
                    .Include(und => und.Unidade)
                    .Include(e => e.Especialidade)
                    .Where(a => a.UnidadeId == u.Id).ToList();

                u.setores = context.SetoresUnidades
                    .Include(und => und.Unidade)
                    .Include(s => s.Setor)
                    .Where(a => a.UnidadeId == u.Id).ToList();

                u.medicos = context.MedicosUnidades
                    .Include(und => und.Unidade)
                    .Include(m => m.Medico)
                    .Where(a => a.UnidadeId == u.Id).ToList();
            }

            return unidades;
        }

        // GET api/Unidades/{rid}
        [HttpGet("{rid}")]
        public Unidades PorRid([FromRoute] string rid)
        {
            return context.Unidades.FirstOrDefault(a => a.RID == rid);
        }

        // POST api/Unidades
        [HttpPost]
        public DefaultResponseModel Post([FromBody] Unidades Model)
        {
            if (Autenticou)
            {
                context.Unidades.Update(Model);
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

        // DELETE api/Unidades/5
        [HttpPost]
        [Route("Delete")]
        public DefaultResponseModel Delete([FromBody] Unidades Model)
        {

            if (Autenticou)
            {
                context.Unidades.Remove(Model);
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
