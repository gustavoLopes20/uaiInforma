using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.WebCore.Mvc
{
    public class BaseController : Controller
    {
        protected readonly BackendContext context;
        private SessoesUsuarios _sessaoUsuario = null;

        public BaseController(BackendContext _context)
        {
            context = _context;
        }

        public string CriptSenha(string input = "")
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash2 = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash2.Length; i++)
            {
                sb.Append(hash2[i].ToString("X2"));
            }
            return sb.ToString();
        }

        protected string TokenSessao
        {
            get
            {
                var header = HttpContext.Request.Headers.ToList();
                SessoesUsuarios session;

                foreach (string token1 in Request.Headers["access_token"])
                {
                    if (token1 != "")
                    {
                        session = context.SessoesUsuarios.Include(a => a.Usuario).FirstOrDefault(a => a.Token == token1);

                        if (session != null)
                        {
                            SessaoUsuario = session;

                            return session.Token;
                        }
                    }
                }
                return null;
            }
        }

        public SessoesUsuarios SessaoUsuario
        {
            get
            {
                if (TokenSessao != "")
                {
                    _sessaoUsuario = context.SessoesUsuarios.Include(u => u.Usuario).FirstOrDefault(a => a.Token == TokenSessao);
                    if (_sessaoUsuario != null)
                    {
                        var permissoes = context.PermissoesUsuarios.Where(a => a.UsuarioId == _sessaoUsuario.UsuarioId).ToList();
                        _sessaoUsuario.PermissoesUsuario = permissoes;
                    }
                    return _sessaoUsuario;
                }
                else
                {
                    return _sessaoUsuario = null;
                }
            }
            set
            {
                _sessaoUsuario = value;
            }
        }

        public bool Autenticou
        {
            get
            {
                return SessaoUsuario != null;
            }
        }

    }
}
