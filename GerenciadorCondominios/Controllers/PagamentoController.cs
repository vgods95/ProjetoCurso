using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public PagamentoController(IPagamentoRepositorio pagamentoRepositorio, IAluguelRepositorio aluguelRepositorio, IHistoricoRecursosRepositorio historicoRecursosRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _pagamentoRepositorio = pagamentoRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
            _historicoRecursosRepositorio = historicoRecursosRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.RecuperarPorNome(User);
            return View(await _pagamentoRepositorio.RecuperarPorUsuario(usuario.Id));
        }

        public async Task<IActionResult> EfetuarPagamento(int id)
        {
            Pagamento pagamento = await _pagamentoRepositorio.RecuperarPorCodigo(id);
            pagamento.DataPagamento = DateTime.Today;
            pagamento.Status = StatusPagamento.Pago;
            await _pagamentoRepositorio.Alterar(pagamento);

            Aluguel aluguel = await _aluguelRepositorio.RecuperarPorCodigo(pagamento.CodigoAluguel);

            HistoricoRecurso historicoRecurso = new HistoricoRecurso
            {
                Valor = aluguel.Valor,
                CodigoMes = aluguel.codigoMes,
                Dia = DateTime.Today.Day,
                Ano = DateTime.Today.Year,
                Tipo = Tipos.Entrada
            };

            await _historicoRecursosRepositorio.Inserir(historicoRecurso);
            TempData["NovoRegistro"] = $"Pagamento no valor de {pagamento.Aluguel.Valor} realizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
