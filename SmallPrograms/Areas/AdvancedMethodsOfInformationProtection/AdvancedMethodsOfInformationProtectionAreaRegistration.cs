using System.Web.Mvc;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection
{
    public class AdvancedMethodsOfInformationProtectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdvancedMethodsOfInformationProtection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdvancedMethodsOfInformationProtection_default",
                "AdvancedMethodsOfInformationProtection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}