using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class ApartamentoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApartamentoRepositorio _apartamentoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ApartamentoController(IWebHostEnvironment webHostEnvironment, IApartamentoRepositorio apartamentoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _webHostEnvironment = webHostEnvironment;
            _apartamentoRepositorio = apartamentoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
            if (await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Sindico") || await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Administrador"))
                return View(await _apartamentoRepositorio.ListarTodos());

            return View(await _apartamentoRepositorio.RecuperarPorUsuario(usuario.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MoradorId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Numero,Andar,Foto,CodigoMorador,CodigoProprietario")] Apartamento apartamento, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = string.Concat(Guid.NewGuid().ToString(), foto.FileName);

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        apartamento.Foto = string.Concat("~/Imagens/", nomeFoto);
                    }
                }

                await _apartamentoRepositorio.Inserir(apartamento);
                TempData["NovoRegistro"] = $"Apartamento número {apartamento.Numero} registrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["MoradorId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            return View(apartamento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Apartamento apartamento = await _apartamentoRepositorio.RecuperarPorCodigo(id);
            if (apartamento == null)
                return NotFound();

            TempData["Foto"] = apartamento.Foto;
            ViewData["MoradorId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            return View(apartamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Codigo,Numero,Andar,Foto,CodigoMorador,CodigoProprietario")] Apartamento apartamento, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                //O objeto foto só virá diferente de nulo caso ela seja alterada
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = string.Concat(Guid.NewGuid().ToString(), foto.FileName);

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        apartamento.Foto = string.Concat("~/Imagens/", nomeFoto);

                        //Lógica para excluir a foto anterior
                        System.IO.File.Delete(TempData["Foto"].ToString().Replace("~", "wwwroot"));
                    }
                }
                else
                    apartamento.Foto = TempData["Foto"].ToString();

                await _apartamentoRepositorio.Alterar(apartamento);
                TempData["Atualizacao"] = $"Apartamento {apartamento.Numero} alterado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["MoradorId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepositorio.ListarTodos(), "Id", "UserName");
            return View(apartamento);
        }

       [HttpPost]
       public async Task<JsonResult> Delete(int id)
        {
            Apartamento apartamento = await _apartamentoRepositorio.RecuperarPorCodigo(id);
            System.IO.File.Delete(apartamento.Foto.Replace("~", "wwwroot"));
            await _apartamentoRepositorio.Excluir(apartamento);
            TempData["Exclusao"] = $"Apartamento {apartamento.Numero} excluído com sucesso!";
            return Json("Apartamento excluído com sucesso");
        }
    }
}
