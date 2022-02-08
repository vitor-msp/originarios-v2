using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Originarios.Models;

namespace Originarios.Controllers
{
    public class ProdutosController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        // rota: Produtos
        // lista os produtos postados por todos os usuários
        public ActionResult Index(int skip = 0)
        {
            List<Postagem> allPosts = db.Postagem.ToList();
            allPosts.Reverse();
            IEnumerable<Postagem> skipedPosts = allPosts.Skip(skip);
            List<Postagem> filteredPosts = new List<Postagem>();

            for(int i = 0; i < 8; i++)
            {
                try
                {
                    filteredPosts.Add(skipedPosts.ElementAt(i));
                }catch(Exception erro){
                    continue;
                }
            }

            ViewBag.pre = skip - 8;
            ViewBag.pos = skip + 8 >= allPosts.Count ? -1 : skip + 8;
            return View(filteredPosts);
        }

        // rota: Produto/5
        // chama view para detalhes do produto
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            string tituloFormatado = removerAcentos(postagem.titulo);
            ViewBag.email = $"mailto:{postagem.Usuario1.email}?subject=Interesse%20em%20produto&body=Ola%2C%20vi%20o%20produto%20-%20{tituloFormatado}%20-%20no%20site%20Originarios%20e%20me%20interessei.%20Como%20adquiro%3F";
            ViewBag.wpp = $"https://api.whatsapp.com/send?phone=55" + $"{postagem.Usuario1.ddd}{postagem.Usuario1.whatsapp}&text=Ola%2C%20vi%20o%20produto%20-%20{tituloFormatado}%20-%20no%20site%20Originarios%20e%20me%20interessei.%20Como%20adquiro%3F";

            return View(postagem);
        }

        private static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
