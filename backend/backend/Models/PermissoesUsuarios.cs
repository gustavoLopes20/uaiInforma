using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class PermissoesUsuarios : AppDataObject
    {
        public long UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; }

        public int  Component { get; set; }
        public bool Consultar { get; set; } = true; // permissao tipo - 1
        public bool Incluir { get; set; } = true; // permissao tipo - 2
        public bool Editar { get; set; } = true; // permissao tipo - 3
        public bool Excluir { get; set; } = false; // permissao tipo - 4
    }
}
