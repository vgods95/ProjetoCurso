using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EventoController(IEventoRepositorio eventoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);

            if (await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Morador"))
                return View(await _eventoRepositorio.RecuperarEventoPorUsuario(usuario.Id));
            else
                return View(await _eventoRepositorio.ListarTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
            Evento evento = new Evento
            {
                CodigoUsuario = usuario.Id
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Data,CodigoUsuario")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Inserir(evento);
                TempData["NovoRegistro"] = $"Evento {evento.Nome} inserido com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Evento evento = await _eventoRepositorio.RecuperarPorCodigo(id);
            if (evento == null)
                return NotFound();

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Data,CodigoUsuario")] Evento evento)
        {
            if (id != evento.Codigo)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Alterar(evento);
                TempData["Atualizacao"] = $"Evento {evento.Nome} alterado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventoRepositorio.Excluir(id);
            TempData["Exclusao"] = "Evento excluído com sucesso!";
            return Json("Evento excluído");
        }
    }
}
