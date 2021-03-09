using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        //Usado para salvar o arquivo de foto em um diretório local
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IWebHostEnvironment webHostEnvironment, IFuncaoRepositorio funcaoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _webHostEnvironment = webHostEnvironment;
            _funcaoRepositorio = funcaoRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _usuarioRepositorio.ListarTodos());
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        //Para o objeto IFormFile foto usamos o mesmo nome que está na id do input="Foto" no arquivo .cshtml
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = string.Concat(Guid.NewGuid().ToString(), foto.FileName);

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = string.Concat("~/Imagens/", nomeFoto);
                    }
                }

                Usuario usuario = new Usuario();
                IdentityResult usuarioCriado;

                //Se não houver nenhum usuário cadastrado ainda, o primeiro será automaticamente o administrador
                //PrimeiroAcesso = false para que ele não precise redefinir a sua senha (por ser administrador)
                if (_usuarioRepositorio.VerificarExistenciaRegistro() == 0)
                {
                    usuario.UserName = model.Nome;
                    usuario.Cpf = model.Cpf;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PrimeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovada;

                    usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                    if (usuarioCriado.Succeeded)
                    {
                        await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Administrador");
                        await _usuarioRepositorio.LogarUsuario(usuario, false);
                        return RedirectToAction("Index", "Usuario");
                    }
                }

                usuario.UserName = model.Nome;
                usuario.Cpf = model.Cpf;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PrimeiroAcesso = true;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                    return View("Analise", usuario.UserName);
                else
                {
                    foreach (IdentityError erro in usuarioCriado.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            //return View(model);
        }

        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.DeslogarUsuario();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.RecuperarUsuarioPorEmail(model.Email);
                if (usuario != null)
                {
                    if (usuario.Status == StatusConta.Analisando)
                        return View("Analise", usuario.UserName);
                    else if (usuario.Status == StatusConta.Reprovada)
                        return View("Reprovado", usuario.UserName);
                    else if (usuario.PrimeiroAcesso)
                        return View("RedefinirSenha", model);
                    else
                    {
                        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
                        if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
                        {
                            await _usuarioRepositorio.LogarUsuario(usuario, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuário e/ou senha inválidos!");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário e/ou senha inválidos!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário e/ou senha inválidos!");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usuarioRepositorio.DeslogarUsuario();
            return RedirectToAction("Login");
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }

        public async Task<JsonResult> AprovarUsuario(string codigoUsuario, string nome)
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(codigoUsuario);
            usuario.Status = StatusConta.Aprovada;
            await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Morador");
            await _usuarioRepositorio.Alterar(usuario);

            return Json(true);
        }

        public async Task<JsonResult> ReprovarUsuario(string codigoUsuario)
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(codigoUsuario);
            usuario.Status = StatusConta.Reprovada;
            await _usuarioRepositorio.Alterar(usuario);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GerenciarUsuario(string Id, string nomeFuncao)
        {
            if (string.IsNullOrEmpty(Id))
                return NotFound();

            //Armazena informações no controller para mandar para a view
            //Na View eu escrevo TempData["usuarioId"] e ele substitui pelo valor
            //Ambos podem ser acessados lá na View
            TempData["usuarioId"] = Id;
            ViewBag.Nome = nomeFuncao;

            Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(Id);

            if (usuario == null)
                return NotFound();

            List<FuncaoUsuarioViewModel> viewModel = new List<FuncaoUsuarioViewModel>();

            foreach (Funcao funcao in await _funcaoRepositorio.ListarTodos())
            {
                FuncaoUsuarioViewModel model = new FuncaoUsuarioViewModel
                {
                    FuncaoId = funcao.Id,
                    Nome = funcao.Name,
                    Descricao = funcao.Descricao
                };

                if (await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, funcao.Name))
                    model.estaSelecionada = true;
                else
                    model.estaSelecionada = false;

                viewModel.Add(model);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GerenciarUsuario(List<FuncaoUsuarioViewModel> model)
        {
            string usuarioId = TempData["usuarioId"].ToString();
            Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(usuarioId);

            if (usuario == null)
                return NotFound();

            IEnumerable<string> funcoes = await _usuarioRepositorio.RecuperarFuncoesUsuario(usuario);
            IdentityResult resultado = await _usuarioRepositorio.RemoverFuncoesUsuario(usuario, funcoes);

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funçoes do usuário");
                return View("GerenciarUsuario", usuarioId);
            }

            resultado = await _usuarioRepositorio.IncluirUsuarioEmFuncoes(usuario, model.Where(x => x.estaSelecionada).Select(x => x.Nome));

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funçoes do usuário");
                return View("GerenciarUsuario", usuarioId);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MinhasInformacoes()
        {
            return View(await _usuarioRepositorio.RecuperarPorNome(User));
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(string id)
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(id);

            if (usuario == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel()
            {
                Nome = usuario.UserName,
                CodigoUsuario = usuario.Id,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Foto = usuario.Foto,
                Telefone = usuario.PhoneNumber
            };

            //Salvando no tempData para que o usuário fique com a mesma foto caso ele não a altere
            TempData["Foto"] = usuario.Foto;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel viewModel, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = string.Concat(Guid.NewGuid().ToString(), foto.FileName);

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        viewModel.Foto = string.Concat("~/Imagens/", nomeFoto);
                    }
                }
                else
                    viewModel.Foto = TempData["Foto"].ToString();

                Usuario usuario = await _usuarioRepositorio.RecuperarPorCodigo(viewModel.CodigoUsuario);
                usuario.UserName = viewModel.Nome;
                usuario.Cpf = viewModel.Cpf;
                usuario.Email = viewModel.Email;
                usuario.PhoneNumber = viewModel.Telefone;
                usuario.Foto = viewModel.Foto;

                await _usuarioRepositorio.Alterar(usuario);

                TempData["Atualizacao"] = "Registro atualizado";

                if (await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Administrador") || await _usuarioRepositorio.VerificarUsuarioFuncao(usuario, "Sindico"))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(MinhasInformacoes));
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RedefinirSenha(Usuario usuario)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = usuario.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.RecuperarUsuarioPorEmail(model.Email);
                model.Senha = _usuarioRepositorio.CodificarSenha(usuario, model.Senha);
                usuario.PasswordHash = model.Senha;
                usuario.PrimeiroAcesso = false;
                await _usuarioRepositorio.Alterar(usuario);
                await _usuarioRepositorio.LogarUsuario(usuario, false);

                return RedirectToAction(nameof(MinhasInformacoes));
            }

            return View(model);
        }
    }
}
