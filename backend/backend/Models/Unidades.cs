using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Unidades : AppDataObject
    {
        [MaxLength(80)]
        public string Descricao { get; set; }

        [MaxLength(180)]
        public string Endereco { get; set; }

        [MaxLength(180)]
        public string Bairro { get; set; }

        [MaxLength(80)]
        public string Cidade { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; }

        [MaxLength(45)]
        public string Telefone { get; set; }


        public List<SetoresUnidades> setores;
        public List<EspecialidadesUnidades> especialidades;
        public List<MedicosUnidades> medicos;

    }
}
