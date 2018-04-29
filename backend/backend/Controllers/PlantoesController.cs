using backend.CommunicationModels;
using backend.CommunicationModels.Access.Request;
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
    public class PlantoesController : BaseController
    {

        public PlantoesController(BackendContext context) : base(context)
        { }

        // Post api/Plantoes/Consulta
        [HttpPost]
        [Route("Consulta")]
        public List<Plantoes> Consulta([FromBody] PlantoesResquetModel Model)
        {
            var plantoes = new List<Plantoes>();

            if (Model.DataInicio != DateTime.MinValue && Model.DataFinal != DateTime.MinValue)
            {
                plantoes = context.Plantoes
                    .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e=> e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .Where(a => (a.UnidadeId == Model.Unidade) && (a.Data >= Model.DataInicio && a.Data <= Model.DataFinal) && (a.SetorUnidadeId == Model.Setor)).ToList();

            }
            else if (Model.DataInicio == DateTime.MinValue && Model.DataFinal != DateTime.MinValue)
            {
                return plantoes;
            }
            else if (Model.DataInicio != DateTime.MinValue && Model.DataFinal == DateTime.MinValue)
            {
                plantoes = context.Plantoes
                    .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .Where(a => (a.UnidadeId == Model.Unidade) && (a.Data >= Model.DataInicio) && (a.SetorUnidadeId == Model.Setor)).ToList();

            }
            else
            {
                plantoes = context.Plantoes
                    .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .Where(a => (a.UnidadeId == Model.Unidade) && (a.SetorUnidadeId == Model.Setor)).ToList();
            }

            return plantoes;
        }


        // Post api/Plantoes/Medico
        [HttpPost]
        [Route("Medico")]
        public List<Plantoes> ConsultaApp([FromBody] PlantoesResquetModel Model)
        {
            var plantoes = new List<Plantoes>();

            if (Model.Setor == 0)
            {
                plantoes = context.Plantoes
                    .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .Where(a => (a.UnidadeId == Model.Unidade) && (a.DataFMT == Model.DataInicioApp) && (a.MedicosUnidadeId == Model.Medico)).ToList();

            }
            else
            {
                plantoes = context.Plantoes
                     .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .Where(a => (a.UnidadeId == Model.Unidade) && (a.DataFMT == Model.DataInicioApp) && (a.SetorUnidadeId == Model.Setor)).ToList();
            }

            return plantoes;
        }






        // GET api/Plantoes/{rid}
        [HttpGet("{rid}")]
        public Plantoes PorRid([FromRoute] string rid)
        {
            return context.Plantoes
                    .Include(u => u.Unidade)
                    .Include(e => e.EspecialidadeUnidade).ThenInclude(e => e.Especialidade)
                    .Include(s => s.SetorUnidade).ThenInclude(s => s.Setor)
                    .Include(m => m.MedicoUnidade).ThenInclude(m => m.Medico)
                    .FirstOrDefault(a => a.RID == rid);
        }

        // POST api/Plantoes
        [HttpPost]
        public DefaultResponseModel Salvar([FromBody] Plantoes Model)
        {

            if (Autenticou)
            {

                string horasF = Model.Horario_Final.TimeOfDay.ToString();
                string horasI = Model.Horario_Inicial.TimeOfDay.ToString();


                Model.Hora_Final = horasF;
                Model.Hora_Inicial = horasI;
                Model.DataFMT = Model.Data.ToShortDateString();

                context.Plantoes.Update(Model);
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
        public DefaultResponseModel Delete([FromBody] Plantoes Model)
        {
            if (Autenticou)
            {
                context.Plantoes.Remove(Model);
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
