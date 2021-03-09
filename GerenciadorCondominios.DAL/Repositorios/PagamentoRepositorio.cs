using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class PagamentoRepositorio : RepositorioGenerico<Pagamento>, IPagamentoRepositorio
    {
        private readonly Contexto _contexto;
        public PagamentoRepositorio(Contexto contexto) : base (contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Pagamento>> RecuperarPorUsuario(string codigoUsuario)
        {
            try
            {
                IEnumerable<Pagamento> pagamentos = await _contexto.Pagamentos.Include(p => p.Aluguel).ThenInclude(p => p.Mes)
                    .Where(p => p.CodigoUsuario.Equals(codigoUsuario)).ToListAsync();

                return pagamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
