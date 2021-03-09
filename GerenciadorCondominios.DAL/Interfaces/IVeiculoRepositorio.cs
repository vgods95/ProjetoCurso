using GerenciadorCondominios.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IVeiculoRepositorio : IRepositorioGenerico<Veiculo>
    {
        Task<IEnumerable<Veiculo>> recuperarVeiculoPorCodigoUsuario(string usuarioId);
    }
}
