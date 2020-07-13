using System.Collections.Generic;
using System.Linq;
using GestaoVendas.Data;

namespace GestaoVendas.Models.Dao
{
    public class DaoAcessoTipoUsuarios
    {
        public List<AcessoTipoUsuario> ListarTodosAcessos(GestaoVendasContext _context)
        {
            var listaAcessos = (from a in _context.AcessoTipoUsuario
                                join tu in _context.TipoUsuario on a.IdTipoUsuario equals tu.Id
                                join fu in _context.Funcionalidade on a.IdFuncionalidade equals fu.Id
                                orderby tu.NomeTipoUsuario, fu.NomeFuncionalidade
                                select new
                                {
                                    a.Id,
                                    a.IdTipoUsuario,
                                    a.IdFuncionalidade,
                                    fu.NomeFuncionalidade,
                                    tu.NomeTipoUsuario
                                });
            List<AcessoTipoUsuario> lista = new List<AcessoTipoUsuario>();
            AcessoTipoUsuario item;

            foreach (var ls in listaAcessos)
            {
                item = new AcessoTipoUsuario
                {
                    Id = ls.Id,
                    IdTipoUsuario = ls.IdTipoUsuario,
                    IdFuncionalidade = ls.IdFuncionalidade,
                    NomeFuncionalidade = ls.NomeFuncionalidade,
                    NomeTipoUsuario = ls.NomeTipoUsuario
                };
                lista.Add(item);

            }

            return lista;

        }
    }
}
