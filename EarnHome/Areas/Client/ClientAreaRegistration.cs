using System.Web.Mvc;

namespace EarnHome.Areas.Client
{
    public class ClientAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Client";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Client_default",
                "Client/{controller}/{action}/{id}",
                new { Area = "", controller = "dashboard", action = "Index", id = UrlParameter.Optional },
                new[] { "EarnHome.Areas.Client.Controllers" }
            );
        }
    }
}