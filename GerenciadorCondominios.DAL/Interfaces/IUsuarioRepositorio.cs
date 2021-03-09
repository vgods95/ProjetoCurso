using GerenciadorCondominios.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        int VerificarExistenciaRegistro();
        Task LogarUsuario(Usuario usuario, bool lembrar);
        Task DeslogarUsuario();
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
        Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao);
        Task<Usuario> RecuperarUsuarioPorEmail(string email);
        Task Alterar(Usuario usuario);
        Task<bool> VerificarUsuarioFuncao(Usuario usuario, string funcao);
        Task<IEnumerable<string>> RecuperarFuncoesUsuario(Usuario usuario);
        Task<IdentityResult> RemoverFuncoesUsuario(Usuario usuario, IEnumerable<string> listaFuncoes);
        Task<IdentityResult> IncluirUsuarioEmFuncoes(Usuario usuario, IEnumerable<string> listaFuncoes);
        Task<Usuario> RecuperarPorNome(ClaimsPrincipal usuario);
        Task<Usuario> RecuperarPorCodigo(string codigoUsuario);
        string CodificarSenha(Usuario usuario, string senha);
    }
}
