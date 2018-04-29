using backend.CommunicationModels;
using backend.CommunicationModels.Access.Request;
using backend.CommunicationModels.Access.Response;
using backend.Models;
using backend.WebCore;
using backend.WebCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class AcessoController : BaseController
    {
        public AcessoController(BackendContext context) : base(context)
        {
        }

        //Cadastro
        [HttpPost]
        [Route("[action]")]
        public DefaultResponseModel Cadastro([FromBody] CadastroUsuariosModel Model)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(Model.Email))
            {
                string senhaCript = CriptSenha(Model.Senha);

                Model.Email = Model.Email.ToLower();

                var existente = context.Usuarios.Any(u => u.Email == Model.Email);

                if (!existente)
                {
                    var pessoa = new Pessoas
                    {
                        Nome = Model.NomeUsuario
                    };

                    context.Pessoas.Add(pessoa);

                    var usuario = new Usuarios
                    {
                        Senha = senhaCript,
                        Email = Model.Email,
                        Login = Model.NomeUsuario,
                        PessoaId = pessoa.Id
                    };

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();

                    var consulta = context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);

                    if (consulta != null)
                    {
                        // component de usuario admin
                        var defaultpermission1 = new PermissoesUsuarios
                        {
                            UsuarioId = consulta.Id,
                            Component = 6731
                        };
                        context.PermissoesUsuarios.Add(defaultpermission1);
                        context.SaveChanges();

                        return new DefaultResponseModel
                        {
                            Sucesso = true,
                            Mensagem = "Usuário cadastrado com sucesso."
                        };
                    }
                    else
                    {
                        return new DefaultResponseModel
                        {
                            Sucesso = false,
                            Mensagem = "Erro ao cadastrado."
                        };
                    }
                }
                else
                {
                    return new DefaultResponseModel
                    {
                        Sucesso = false,
                        Mensagem = "Usuário existente"
                    };
                }

            }
            else
            {
                return new DefaultResponseModel
                {
                    Sucesso = false,
                    Mensagem = "E-mail inválido."
                };
            }
        }

        // /api/acesso/Login
        [HttpPost]
        [Route("[action]")]
        public LoginResponseModel Login([FromBody] LoginRequestModel Model)
        {
            string senhaCript = CriptSenha(Model.Senha);

            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(Model.Email))
            {
                Model.Email = Model.Email.ToLower();

                var user = context.Usuarios.Include(u => u.Pessoa).FirstOrDefault(a => a.Email == Model.Email && a.Senha == senhaCript);

                if (user != null)
                {
                    var sessaousuario = new SessoesUsuarios
                    {
                        Usuario = user,
                    };

                    context.SessoesUsuarios.Add(sessaousuario);
                    context.SaveChanges();


                    return new LoginResponseModel
                    {
                        Token = sessaousuario.Token,
                        UserName = sessaousuario.Usuario.Pessoa.Nome,
                        UserNivel = sessaousuario.Usuario.Nivel,
                       
                        Mensagem = "Login Efetuado com sucesso."
                    };
                }
                else
                {
                    return new LoginResponseModel
                    {
                        Sucesso = false,
                        Mensagem = "Usuário ou senha incorreto."
                    };
                }
            }
            else
            {
                return new LoginResponseModel
                {
                    Sucesso = false,
                    Mensagem = "E-mail inválido."
                };
            }
        }

        // /api/acesso/Sessoes
        [HttpGet]
        [Route("[action]")]
        public SessoesUsuariosResponseModel Sessoes()
        {
            if (Autenticou)
            {

                var lstPermissoes = context.PermissoesUsuarios.Where(a => a.UsuarioId == SessaoUsuario.UsuarioId).ToList();

                return new SessoesUsuariosResponseModel
                {
                    Token = SessaoUsuario.Token,
                    UserName = SessaoUsuario.Usuario.Login,
                    UserId = SessaoUsuario.UsuarioId,
                    UserRID = SessaoUsuario.Usuario.RID,
                    UserNivel = SessaoUsuario.Usuario.Nivel,
                    PermissoesUser = lstPermissoes,
                    Mensagem = "Autenticado com sucesso."
                };
            }
            else
            {
                return new SessoesUsuariosResponseModel
                {
                    Sucesso = false,
                    Mensagem = "Não autenticado."
                };
            }

        }     
    }
}
