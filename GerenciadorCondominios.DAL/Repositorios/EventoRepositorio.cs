using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repositorios
{
    public class EventoRepositorio : RepositorioGenerico<Evento>, IEventoRepositorio
    {
        private readonly Contexto _contexto;
        public EventoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Evento>> RecuperarEventoPorUsuario(string codigoUsuario)
        {
            try
            {
                return await _contexto.Eventos.Where(x => x.CodigoUsuario == codigoUsuario).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
