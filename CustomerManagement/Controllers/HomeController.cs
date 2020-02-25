using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["UserIdentityName"] = User.Identity.Name;
            Session["IsUserAuthenticated"] = User.Identity.IsAuthenticated;
            Session["UserAuthenticatedType"] = User.Identity.AuthenticationType;
            Session["UsedIdentity"] = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return View("Home");
        }

        public ActionResult Home()
        {
            Session["UserIdentityName"] = User.Identity.Name;
            Session["IsUserAuthenticated"] = User.Identity.IsAuthenticated;
            Session["UserAuthenticatedType"] = User.Identity.AuthenticationType;
            Session["UsedIdentity"] = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}