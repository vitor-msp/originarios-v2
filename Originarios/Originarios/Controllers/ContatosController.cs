using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Originarios.Models;

namespace Originarios.Controllers
{
    public class ContatosController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        // rota: Contato
        public ActionResult Create(bool? enviado = false)
        {
            ViewBag.enviado = enviado;
            ViewBag.estaLogado = false;
            if (Request.IsAuthenticated)
            {
                ViewBag.estaLogado = true;
                string email = User.Identity.GetUserName();
                Usuario usuario = db.Usuario.SingleOrDefault(u => u.email.Equals(email));
                ViewBag.nome = usuario.nome;
                ViewBag.email = usuario.email;
                ViewBag.endereco = $"{usuario.cidade} - {usuario.estado}";
            }
            List<Assunto> assuntos = new List<Assunto>()
            {
                new Assunto() {id = 1, texto = "Contato com o vendedor" },
                new Assunto() {id = 3, texto = "Cadastro na plataforma" },
                new Assunto() {id = 4, texto = "Atualização dos meus dados" },
                new Assunto() {id = 5, texto = "Alteração da minha senha" },
                new Assunto() {id = 6, texto = "Visualização das minhas publicações" },
                new Assunto() {id = 7, texto = "Adição de uma publicação" },
                new Assunto() {id = 8, texto = "Edição de uma publicação" },
                new Assunto() {id = 9, texto = "Gostaria de contribuir com o projeto" },
                new Assunto() {id = 10, texto = "Outro" }
            };
            ViewBag.assunto = assuntos;

            return View();
        }

        // POST: Contato
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ctt,nome,email,endereco,assunto,mensagem")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contato.Add(contato);
                db.SaveChanges();
                return RedirectToAction("Create", new { enviado = true } );
            }

            return View(contato);
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
