using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Originarios.Models;
using Microsoft.AspNet.Identity;

namespace Originarios.Controllers
{
    [Authorize]
    public class PublicacoesController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        private Usuario BuscaUsuarioLogado()
        {
            string email = User.Identity.GetUserName();
            return db.Usuario.SingleOrDefault(u => u.email.Equals(email));
        }

        // rota: Minhas_publicacoes
        // lista os publicações do usuário logado
        public ActionResult Index(char? msg = null)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.msg = msg == 'c' ? "Publicação postada com sucesso!!"
                        : msg == 'd' ? "Publicação deletado com sucesso!!"
                        : null;
            IQueryable<Publicacao> publicacoes = db.Publicacao.Where(p => p.usuario == usuarioLogado.id_usu);
            return View(publicacoes.ToList());
        }

        // rota: Criar_Publicação
        // chama view para criação da publicação
        public ActionResult Create()
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.usuario = usuarioLogado.id_usu;
            return View();
        }

        // POST da view Create
        // cria produto no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create
        (
            [Bind(Include = "id_public,usuario,titulo,corpo,locali,data_public")] Publicacao publicacao
        )
        {
            if (ModelState.IsValid)
            {
                //publicacao.vb_img1 = string.IsNullOrEmpty(base64_img1) ? null : Convert.FromBase64String(base64_img1.Substring(22));
                
                //ver se vai dar certo este add depois que fazer a tabela e ligar ao bd
                db.Publicacao.Add(publicacao);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = 'c' });
            }
            return View(publicacao);
        }

        // rota: Editar_Publicação
        // chama view para edição da publicação
        public ActionResult Edit(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacao publicacao = db.Publicacao.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario = usuarioLogado.id_usu;
            return View(publicacao);
        }

        // POST da view Edit
        // edita publicacao no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit
        (
            [Bind(Include = "id_public,usuario,titulo,corpo,locali,data_public")] Publicacao publicacao
        )
        {
            if (ModelState.IsValid)
            {
              //  publicacao.vb_img1 = string.IsNullOrEmpty(base64_img1) ? null : Convert.FromBase64String(base64_img1.Substring(22));
                
                db.Entry(publicacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = publicacao.id_public, editado = true });
            }
            return View(publicacao);
        }

        // rota: Ver_Publicação
        // chama view para detalhes do publicação
        public ActionResult Details(int? id, bool? editado = false)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacao publicacao = db.Publicacao.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }

            ViewBag.msg = editado == true ? "Publicação editada com sucesso!!" : null;
            return View(publicacao);
        }

        // POST do botão delete da view Details
        // remove publicação no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacao publicacao = db.Publicacao.Find(id);
            if (publicacao == null)
            {
                return HttpNotFound();
            }
            db.Publicacao.Remove(publicacao);
            db.SaveChanges();
            return RedirectToAction("Index", new { msg = 'd' });
        }

        [AllowAnonymous]
        /*
        // método que busca, renderiza e retorna imagem
        public FileContentResult ObterImgNaView(int id, int img)
        {
            Publicacao publicacao = db.Publicacao.Find(id);
            byte[] vbImg = null;

            switch (img)
            {
                case 1:
                    vbImg = publicacao.vb_img1;
                    break;
                default:
                    break;
            }

            return vbImg != null ? new FileContentResult(vbImg, "image/jpeg") : null;
        }
        */
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
