using System.Web.Mvc;

namespace EarnHome.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Area="", controller = "admin", action = "Index", id = UrlParameter.Optional },
                new[] { "EarnHome.Areas.Admin.Controllers" }
            );
        }
    }
}