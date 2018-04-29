using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.CommunicationModels.Access.Response
{
    public class SessoesUsuariosResponseModel
    {
        public string Token { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; } = true;
        public string UserName { get; set; }
        public long UserId { get; set; }
        public int UserNivel { get; set; }
        public string UserRID { get; set; }
        public List<PermissoesUsuarios> PermissoesUser { get; set; }
    }
}
