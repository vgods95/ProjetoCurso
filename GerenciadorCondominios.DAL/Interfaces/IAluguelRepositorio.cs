using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IAluguelRepositorio : IRepositorioGenerico<Aluguel>
    {
        bool AluguelJaExiste(int codigoMes, int ano);

        new Task<IEnumerable<Aluguel>> ListarTodos();
    }
}
