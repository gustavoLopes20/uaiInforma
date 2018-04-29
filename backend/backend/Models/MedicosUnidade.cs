using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MedicosUnidades : AppDataObject
    {

        public long UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public Unidades Unidade { get; set; }

        public long MedicoId { get; set; }
        [ForeignKey("MedicoId")]
        public Medicos Medico { get; set; }

        public long SetorUnidadeId { get; set; }
        [ForeignKey("SetorUnidadeId")]
        public SetoresUnidades SetorUnidade { get; set; }

        public long EspecialidadeUnidadeId { get; set; }
        [ForeignKey("EspecialidadeUnidadeId")]
        public EspecialidadesUnidades EspecialidadeUnidade { get; set; }

    }
}
