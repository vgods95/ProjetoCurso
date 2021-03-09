using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        private readonly RoleManager<Funcao> _gerenciadorFuncoes;
        public FuncaoRepositorio(Contexto contexto, RoleManager<Funcao> gerenciadorFuncoes) : base(contexto)
        {
            _gerenciadorFuncoes = gerenciadorFuncoes;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                funcao.Id = Guid.NewGuid().ToString();
                await _gerenciadorFuncoes.CreateAsync(funcao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task Alterar(Funcao funcao)
        {
            try
            {
                Funcao f = await RecuperarPorCodigo(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.Name.ToUpper();
                f.Descricao = funcao.Descricao;
                await _gerenciadorFuncoes.UpdateAsync(f);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
