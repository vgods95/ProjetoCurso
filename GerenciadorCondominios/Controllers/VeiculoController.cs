using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public VeiculoController(IVeiculoRepositorio veiculoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Marca,Cor,Placa,CodigoUsuario")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
                veiculo.CodigoUsuario = usuario.Id;
                await _veiculoRepositorio.Inserir(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuario");
            }

            return View(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Veiculo veiculo = await _veiculoRepositorio.RecuperarPorCodigo(id);
            if (veiculo == null)
                return NotFound();
            else
                return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Marca,Cor,Placa,CodigoUsuario")] Veiculo veiculo)
        {
            if (id != veiculo.Codigo)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _veiculoRepositorio.Alterar(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuario");
            }

            return View(veiculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _veiculoRepositorio.Excluir(id);
            return Json("Veículo excluído com sucesso");
        }
    }
}
