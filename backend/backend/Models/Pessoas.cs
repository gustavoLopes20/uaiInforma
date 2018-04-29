using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Pessoas : AppDataObject
    {
        [MaxLength(64)]
        public string Nome { get; set; }

        public int TipoPessoa { get; set; } = 1;

        [MaxLength(20)]
        public string Cep { get; set; }

        [MaxLength(45)]
        public string Logradouro { get; set; }

        public int Num { get; set; }

        [MaxLength(60)]
        public string Complemento { get; set; }

        [MaxLength(45)]
        public string Bairro { get; set; }

        [MaxLength(5)]
        public string Estado { get; set; }

        [MaxLength(45)]
        public string Cidade { get; set; }

        [MaxLength(45)]
        public string Telefone { get; set; }

    }
}
