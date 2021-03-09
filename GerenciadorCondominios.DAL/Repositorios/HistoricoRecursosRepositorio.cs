using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class HistoricoRecursosRepositorio : RepositorioGenerico<HistoricoRecurso>, IHistoricoRecursosRepositorio
    {
        public HistoricoRecursosRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
