using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class SetoresUnidades : AppDataObject
    {

        public long UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public Unidades Unidade { get; set; }

        public long SetorId { get; set; }
        [ForeignKey("SetorId")]
        public Setores Setor { get; set; }

    }
}
