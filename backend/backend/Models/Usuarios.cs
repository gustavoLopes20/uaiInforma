using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Usuarios : AppDataObject
    {
        [MaxLength(64), Required]
        public string Login { get; set; }

        [MaxLength(128), Required]
        public string Senha { get; set; }

        [MaxLength(70), Required]
        public string Email { get; set; }

        public bool EmailCmf { get; set; } = true;

        [MaxLength(256)]
        public string TokenCmfEmail { get; set; }

        public int Nivel { get; set; } = 0; //(0 - usuário comum / 1 - Administrador / 2 - Desenvolvedor / 3 - Administrador e Desenvolvedor)

        public long PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public Pessoas Pessoa { get; set; }
    }
}
