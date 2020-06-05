using System.Web.Mvc;

namespace EarnHome.Areas.Executer
{
    public class ExecuterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Executer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Executer_default",
                "Executer/{controller}/{action}/{id}",
                new {Area="", controller="home", action = "Index", id = UrlParameter.Optional },
                new[] { "EarnHome.Areas.Executer.Controllers" }
            );
        }
    }
}