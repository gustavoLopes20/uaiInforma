using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class SessoesUsuarios : AppDataObject
    {
        public Usuarios Usuario { get; set; }
        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? UltimoLogin { get; set; }

        [NotMapped]
        private string _token = null;

        public SessoesUsuarios()
        {
            _token = GenerateUniqueRID(true);
        }


        [MaxLength(64)]
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

        [NotMapped]
        public List<PermissoesUsuarios> PermissoesUsuario { get; set; }
    }
}
