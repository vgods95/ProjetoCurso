using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class VeiculoRepositorio : RepositorioGenerico<Veiculo>, IVeiculoRepositorio
    {
        private readonly Contexto _contexto;

        public VeiculoRepositorio(Contexto contexto) : base (contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Veiculo>> recuperarVeiculoPorCodigoUsuario(string usuarioId)
        {
            try
            {
                return await _contexto.Veiculos.Where(v => v.CodigoUsuario == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
