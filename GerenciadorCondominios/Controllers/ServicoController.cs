using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoRepositorio _servicoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IServicoPredioRepositorio _servicoPredioRepositorio;
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;
        public ServicoController(IServicoRepositorio servicoRepositorio, IUsuarioRepositorio usuarioRepositorio, IServicoPredioRepositorio servicoPredioRepositorio, IHistoricoRecursosRepositorio historicoRecursosRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _servicoPredioRepositorio = servicoPredioRepositorio;
            _historicoRecursosRepositorio = historicoRecursosRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
            if (await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Morador"))
                return View(await _servicoRepositorio.recuperarPorUsuario(usuario.Id));

            return View(await _servicoRepositorio.ListarTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
            Servico servico = new Servico
            {
                Status = StatusServico.Pendente,
                CodigoUsuario = usuario.Id
            };

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Valor,Status,CodigoUsuario")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                await _servicoRepositorio.Inserir(servico);
                TempData["NovoRegistro"] = $"Serviço {servico.Nome} solicitado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(servico);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Servico servico = await _servicoRepositorio.RecuperarPorCodigo(id);
            if (servico == null)
                return NotFound();

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Codigo,Nome,Valor,Status,CodigoUsuario")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                await _servicoRepositorio.Alterar(servico);
                TempData["Atualizacao"] = $"Serviço {servico.Nome} alterado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(servico);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _servicoRepositorio.Excluir(id);
            TempData["Exclusao"] = "Serviço excluído com sucesso!";
            return Json("Serviço excluído");
        }

        [HttpGet]
        public async Task<IActionResult> AprovarServico(int id)
        {
            Servico servico = await _servicoRepositorio.RecuperarPorCodigo(id);

            if (servico == null)
                return NotFound();

            ServicoAprovadoViewModel viewModel = new ServicoAprovadoViewModel()
            {
                Codigo = servico.Codigo,
                Nome = servico.Nome
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprovarServico(ServicoAprovadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Servico servico = await _servicoRepositorio.RecuperarPorCodigo(viewModel.Codigo);
                servico.Status = StatusServico.Aceito;
                await _servicoRepositorio.Alterar(servico);

                ServicoPredio servicoPredio = new ServicoPredio
                {
                    CodigoServico = viewModel.Codigo,
                    DataExecucao = viewModel.Data
                };

                await _servicoPredioRepositorio.Inserir(servicoPredio);

                HistoricoRecurso historicoRecurso = new HistoricoRecurso
                {
                    Valor = servico.Valor,
                    CodigoMes = servicoPredio.DataExecucao.Month,
                    Dia = servicoPredio.DataExecucao.Day,
                    Ano = servicoPredio.DataExecucao.Year,
                    Tipo = Tipos.Saida
                };

                await _historicoRecursosRepositorio.Inserir(historicoRecurso);

                TempData["NovoRegistro"] = $"{servico.Nome} escalado com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ReprovarServico(int id)
        {
            Servico servico = await _servicoRepositorio.RecuperarPorCodigo(id);

            if (servico == null)
                return NotFound();

            servico.Status = StatusServico.Recusado;
            await _servicoRepositorio.Alterar(servico);
            TempData["Exclusao"] = $"{servico.Nome} recusado!";

            return RedirectToAction(nameof(Index));
        }
    }
}
