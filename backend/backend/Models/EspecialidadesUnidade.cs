using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class EspecialidadesUnidades : AppDataObject
    {

        public long EspecialidadeId { get; set; }
        [ForeignKey("EspecialidadeId")]
        public Especialidades Especialidade { get; set; }

        public long UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public Unidades Unidade { get; set; }

    }
}
