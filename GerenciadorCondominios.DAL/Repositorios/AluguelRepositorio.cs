using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GerenciadorCondominios.DAL.Repositorios
{
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {
        private readonly Contexto _contexto;

        public AluguelRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool AluguelJaExiste(int codigoMes, int ano)
        {
            try
            {
                return _contexto.Alugueis.Any(a => a.codigoMes == codigoMes && a.Ano == ano);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task<IEnumerable<Aluguel>> ListarTodos()
        {
            try
            {
                return await _contexto.Alugueis.Include(a => a.Mes).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
