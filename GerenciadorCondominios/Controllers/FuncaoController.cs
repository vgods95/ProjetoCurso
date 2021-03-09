using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class FuncaoController : Controller
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;

        public FuncaoController(IFuncaoRepositorio funcaoRepositorio)
        {
            _funcaoRepositorio = funcaoRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _funcaoRepositorio.ListarTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.AdicionarFuncao(funcao);
                TempData["NovoRegistro"] = $"Função {funcao.Name} adicionada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(funcao);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Funcao funcao = await _funcaoRepositorio.RecuperarPorCodigo(id);
            if (funcao == null)
                return NotFound();

            return View(funcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (id != funcao.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.Alterar(funcao);
                TempData["Atualizacao"] = $"Função {funcao.Name} alterada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(funcao);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            await _funcaoRepositorio.Excluir(id);
            TempData["Exclusao"] = "Função excluída com sucesso!";
            return Json("Função excluída com sucesso!");
        }
    }
}
