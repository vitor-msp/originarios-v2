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
                new Assunto() {id = 1, texto = "Solicitar fatura do pedido" },
                new Assunto() {id = 2, texto = "Solicitar estado do pedido" },
                new Assunto() {id = 3, texto = "Ainda não recebi reembolso" },
                new Assunto() {id = 4, texto = "Outro" }
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
