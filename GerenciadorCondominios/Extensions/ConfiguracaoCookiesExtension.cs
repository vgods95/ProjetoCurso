using Microsoft.Extensions.DependencyInjection;
using System;

namespace GerenciadorCondominios.Extensions
{
    public static class ConfiguracaoCookiesExtension
    {
        public static void ConfigurarCookies(this IServiceCollection service)
        {
            service.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.Name = "IdentityCookie";
                opcoes.Cookie.HttpOnly = true; //Se o cookie será acessível via scripts do lado do cliente
                opcoes.LoginPath = "/Usuario/Login";
            });
        }
    }
}
