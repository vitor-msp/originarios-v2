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
    public class PostagensController : Controller
    {
        private OriginariosEntities db = new OriginariosEntities();

        private Usuario BuscaUsuarioLogado()
        {
            string email = User.Identity.GetUserName();
            return db.Usuario.SingleOrDefault(u => u.email.Equals(email));
        }

        // rota: Meus_Produtos
        // lista os produtos do usuário logado
        public ActionResult Index(char? msg = null)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.msg = msg == 'c' ? "Produto postado com sucesso!!" 
                        : msg == 'd' ? "Produto deletado com sucesso!!"
                        : null;
            IQueryable<Postagem> postagens = db.Postagem.Where(p => p.usuario == usuarioLogado.id_usu);
            return View(postagens.ToList());
        }

        // rota: Criar_Produto
        // chama view para criação de produto
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
            [Bind(Include = "id_post,usuario,titulo,descricao,corpo,valor,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem,
            string base64_img1, string base64_img2, string base64_img3, string base64_img4
        )
        {
            if (ModelState.IsValid)
            {
                postagem.vb_img1 = string.IsNullOrEmpty(base64_img1) ? null : Convert.FromBase64String(base64_img1.Substring(22));
                postagem.vb_img2 = string.IsNullOrEmpty(base64_img2) ? null : Convert.FromBase64String(base64_img2.Substring(22));
                postagem.vb_img3 = string.IsNullOrEmpty(base64_img3) ? null : Convert.FromBase64String(base64_img3.Substring(22));
                postagem.vb_img4 = string.IsNullOrEmpty(base64_img4) ? null : Convert.FromBase64String(base64_img4.Substring(22));

                db.Postagem.Add(postagem);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg = 'c' });
            }
            return View(postagem);
        }

        // rota: Editar_Produto
        // chama view para edição do produto
        public ActionResult Edit(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario = usuarioLogado.id_usu;
            ViewBag.valor = postagem.valor.ToString().Split(',')[0];
            return View(postagem);
        }

        // POST da view Edit
        // edita produto no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit
        (
            [Bind(Include = "id_post,usuario,titulo,descricao,corpo,valor,nm_img1,vb_img1,nm_img2,vb_img2,nm_img3,vb_img3,nm_img4,vb_img4")] Postagem postagem,
            string base64_img1, string base64_img2, string base64_img3, string base64_img4
        )
        {
            if (ModelState.IsValid)
            {
                postagem.vb_img1 = string.IsNullOrEmpty(base64_img1) ? null : Convert.FromBase64String(base64_img1.Substring(22));
                postagem.vb_img2 = string.IsNullOrEmpty(base64_img2) ? null : Convert.FromBase64String(base64_img2.Substring(22));
                postagem.vb_img3 = string.IsNullOrEmpty(base64_img3) ? null : Convert.FromBase64String(base64_img3.Substring(22));
                postagem.vb_img4 = string.IsNullOrEmpty(base64_img4) ? null : Convert.FromBase64String(base64_img4.Substring(22));
                
                db.Entry(postagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = postagem.id_post, editado = true });
            }
            return View(postagem);
        }

        // rota: Ver_Produto
        // chama view para detalhes do produto
        public ActionResult Details(int? id, bool? editado = false)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }

            ViewBag.msg = editado == true ? "Produto editado com sucesso!!" : null;
            return View(postagem);
        }

        // POST do botão delete da view Details
        // remove produto no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            Usuario usuarioLogado = BuscaUsuarioLogado();
            if (id == null || usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            db.Postagem.Remove(postagem);
            db.SaveChanges();
            return RedirectToAction("Index", new { msg = 'd' });
        }

        [AllowAnonymous]
        // método que busca, renderiza e retorna imagem
        public FileContentResult ObterImgNaView(int id, int img)
        {
            Postagem postagem = db.Postagem.Find(id);
            byte[] vbImg = null;

            switch (img)
            {
                case 1:
                    vbImg = postagem.vb_img1;
                    break;
                case 2:
                    vbImg = postagem.vb_img2;
                    break;
                case 3:
                    vbImg = postagem.vb_img3;
                    break;
                case 4:
                    vbImg = postagem.vb_img4;
                    break;
                default:
                    break;
            }

            return vbImg != null ? new FileContentResult(vbImg, "image/jpeg") : null;
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
