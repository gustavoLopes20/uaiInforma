using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Plantoes : AppDataObject
    {


        public long MedicosUnidadeId { get; set; }
        [ForeignKey("MedicosUnidadeId")]
        public MedicosUnidades MedicoUnidade { get; set; }

        public long SetorUnidadeId { get; set; }
        [ForeignKey("SetorUnidadeId")]
        public SetoresUnidades SetorUnidade { get; set; }

        public long EspecialidadeUnidadeId { get; set; }
        [ForeignKey("EspecialidadeUnidadeId")]
        public EspecialidadesUnidades EspecialidadeUnidade { get; set; }

        public long UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public Unidades Unidade { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Data { get; set; }

        public DateTime Horario_Inicial { get; set; }
        public DateTime Horario_Final { get; set; }
        public string DataFMT { get; set; }

        [StringLength(45)]
        public string Hora_Inicial { get; set; }

        [StringLength(45)]
        public string Hora_Final { get; set; }



    }
}
