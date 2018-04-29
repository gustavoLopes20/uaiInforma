using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.CommunicationModels.Access.Request
{
    public class PlantoesResquetModel
    {
        public long Unidade { get; set; }
        public long Setor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string DataInicioApp { get; set; }
        public long Medico { get; set; }
    }
}
