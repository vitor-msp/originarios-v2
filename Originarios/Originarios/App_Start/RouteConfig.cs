using System.Web.Mvc;
using System.Web.Routing;

namespace Originarios
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Contato",
                url: "Contato",
                defaults: new { controller = "Contatos", action = "Create" }
            );

            routes.MapRoute(
                name: "Produtos",
                url: "Produtos",
                defaults: new { controller = "Produtos", action = "Index" }
            );

            routes.MapRoute(
                name: "Produto",
                url: "Produto",
                defaults: new { controller = "Produtos", action = "Details" }
            );

            routes.MapRoute(
                name: "Criar_Conta",
                url: "Criar_Conta",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Logon",
                url: "Logon",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Minha_Conta",
                url: "Minha_Conta",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Editar_Conta",
                url: "Editar_Conta",
                defaults: new { controller = "Manage", action = "Edit" }
            );

            routes.MapRoute(
                name: "Alterar_Senha",
                url: "Alterar_Senha",
                defaults: new { controller = "Manage", action = "ChangePassword" }
            );

            routes.MapRoute(
                name: "Meus_Produtos",
                url: "Meus_Produtos",
                defaults: new { controller = "Postagens", action = "Index" }
            );

            routes.MapRoute(
                name: "Criar_Produto",
                url: "Criar_Produto",
                defaults: new { controller = "Postagens", action = "Create" }
            );

            routes.MapRoute(
                name: "Editar_Produto",
                url: "Editar_Produto",
                defaults: new { controller = "Postagens", action = "Edit" }
            );

            routes.MapRoute(
                name: "Ver_Produto",
                url: "Ver_Produto",
                defaults: new { controller = "Postagens", action = "Details" }
            );
            //aqui começa o que eu e Andriele editamos neste arquivo

            routes.MapRoute(
                name: "DivulgacaoIntegra",
                url: "Divulgacao",
                defaults: new { controller = "Divulgacao", action = "Integra" }
            );

            routes.MapRoute(
                name: "DivulgacaoIndex",
                url: "Divulgacoes",
                defaults: new { controller = "Divulgacao", action = "Index" }
            );

            routes.MapRoute(
                name: "Criar_Publicacao",
                url: "Criar_Publicacao",
                defaults: new { controller = "Publicacoes", action = "Create" }
            );

            routes.MapRoute(
                name: "Editar_Publicacao",
                url: "Editar_Publicacao",
                defaults: new { controller = "Publicacoes", action = "Edit" }
            );

            routes.MapRoute(
                name: "Ver_Publicacao",
                url: "Ver_Publicacao",
                defaults: new { controller = "Publicacoes", action = "Details" }
            );
            //aqui termina o que editamos 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
