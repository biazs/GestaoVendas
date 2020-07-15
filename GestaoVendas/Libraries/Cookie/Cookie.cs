using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GestaoVendas.Libraries.Cookie
{
    public class Cookie
    {

        private readonly IHttpContextAccessor _context;
        public Cookie(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string key, string Valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(10);

            _context.HttpContext.Response.Cookies.Append(key, Valor, Options);
        }

        public void Atualizar(string key, string Valor)
        {
            if (Existe(key))
                Remover(key);
            Cadastrar(key, Valor);
        }

        public void Remover(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);
        }

        public string Consultar(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }
        public bool Existe(string key)
        {
            if (_context.HttpContext.Request.Cookies[key] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoverTodos()
        {
            var ListaCookie = _context.HttpContext.Request.Cookies.ToList();
            foreach (var cookie in ListaCookie)
            {
                Remover(cookie.Key);
            }
        }
    }

}
