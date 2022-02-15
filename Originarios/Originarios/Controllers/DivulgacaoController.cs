using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Originarios.Models;

namespace Originarios.Controllers
{
    public class DivulgacaoController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        // rota: Divulgação
        // Lista as divulgações feitas pelos usuários
        public ActionResult Index(int skip = 0)
        {
            List<Publicacao> allPosts = db.Publicacao.ToList();
            allPosts.Reverse();
            IEnumerable<Publicacao> skipedPosts = allPosts.Skip(skip);
            List<Publicacao> filteredPosts = new List<Publicacao>();

            for (int i = 0; i < 8; i++)
            {
                try
                {
                    filteredPosts.Add(skipedPosts.ElementAt(i));
                }
                catch (Exception erro)
                {
                    continue;
                }
            }

            ViewBag.pre = skip - 8;
            ViewBag.pos = skip + 8 >= allPosts.Count ? -1 : skip + 8;
            return View(filteredPosts);
        }

        // rota: Publicação/5
        // chama view para ver a publicação na íntegra
        public ActionResult Integra(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacao publicacao = db.Publicacao.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }

            //string tituloFormatado = removerAcentos(publicacao.titulo);
            //ViewBag.email = $"mailto:{publicacao.Usuario1.email}?subject=Interesse%20em%20produto&body=Ola%2C%20vi%20o%20produto%20-%20{tituloFormatado}%20-%20no%20site%20Originarios%20e%20me%20interessei.%20Como%20adquiro%3F";
            //ViewBag.wpp = $"https://api.whatsapp.com/send?phone=55" + $"{publicacao.Usuario1.ddd}{publicacao.Usuario1.whatsapp}&text=Ola%2C%20vi%20o%20produto%20-%20{tituloFormatado}%20-%20no%20site%20Originarios%20e%20me%20interessei.%20Como%20adquiro%3F";

            return View(publicacao);
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
