using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class ApartamentoRepositorio : RepositorioGenerico<Apartamento>, IApartamentoRepositorio
    {
        private readonly Contexto _contexto;
        public ApartamentoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Apartamento>> RecuperarPorUsuario(string codigoUsuario)
        {
            try
            {
                return await _contexto.Apartamentos
                    .Include(x => x.Morador).Include(x => x.Proprietario)
                    .Where(x => x.CodigoMorador == codigoUsuario || x.CodigoProprietario == codigoUsuario).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task<IEnumerable<Apartamento>> ListarTodos()
        {
            try
            {
                return await _contexto.Apartamentos
                    .Include(x => x.Morador).Include(x => x.Proprietario).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
