using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class AluguelController : Controller
    {
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        private readonly IMesRepositorio _mesRepositorio;

        public AluguelController(IAluguelRepositorio aluguelRepositorio, IUsuarioRepositorio usuarioRepositorio, IPagamentoRepositorio pagamentoRepositorio, IMesRepositorio mesRepositorio)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _pagamentoRepositorio = pagamentoRepositorio;
            _mesRepositorio = mesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _aluguelRepositorio.ListarTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["codigoMes"] = new SelectList(await _mesRepositorio.ListarTodos(), "Codigo", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Valor,codigoMes,Ano")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                if (!_aluguelRepositorio.AluguelJaExiste(aluguel.codigoMes, aluguel.Ano))
                {
                    await _aluguelRepositorio.Inserir(aluguel);

                    IEnumerable<Usuario> listaUsuarios = await _usuarioRepositorio.ListarTodos();
                    Pagamento pagamento;
                    List<Pagamento> listaPagamentos = new List<Pagamento>();

                    foreach (Usuario u in listaUsuarios)
                    {
                        pagamento = new Pagamento
                        {
                            CodigoAluguel = aluguel.Codigo,
                            CodigoUsuario = u.Id,
                            DataPagamento = null,
                            Status = StatusPagamento.Pendente
                        };

                        listaPagamentos.Add(pagamento);
                    }

                    await _pagamentoRepositorio.Inserir(listaPagamentos);
                    TempData["NovoRegistro"] = $"Aluguel no valor de R$ {aluguel.Valor} de {aluguel.codigoMes}/{aluguel.Ano} adicionado!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Aluguel já existente!");
                    ViewData["codigoMes"] = new SelectList(await _mesRepositorio.ListarTodos(), "Codigo", "Nome", aluguel.codigoMes);
                    return View(aluguel);
                }
            }
            ViewData["codigoMes"] = new SelectList(await _mesRepositorio.ListarTodos(), "Codigo", "Nome", aluguel.codigoMes);
            return View(aluguel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Aluguel aluguel = await _aluguelRepositorio.RecuperarPorCodigo(id);

            if (aluguel == null)
                return NotFound();

            ViewData["codigoMes"] = new SelectList(await _mesRepositorio.ListarTodos(), "Codigo", "Nome", aluguel.codigoMes);
            return View(aluguel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Codigo,Valor,codigoMes,Ano")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                await _aluguelRepositorio.Alterar(aluguel);
                TempData["Atualizacao"] = $"Aluguel no valor de R$ {aluguel.Valor} de {aluguel.codigoMes}/{aluguel.Ano} alterado!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["codigoMes"] = new SelectList(await _mesRepositorio.ListarTodos(), "Codigo", "Nome", aluguel.codigoMes);
            return View(aluguel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _aluguelRepositorio.Excluir(id);
            TempData["Exclusao"] = "Aluguel excluído com sucesso!";
            return Json("registro excluido");
        }
    }
}
