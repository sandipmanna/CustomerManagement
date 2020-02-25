using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerManagement.Controllers
{
    public class RegistrationBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase context = controllerContext.HttpContext;
            string _userName = context.Request.Form["UserName"].ToString();
            string _Password = context.Request.Form["Password"].ToString();
            string _email = context.Request.Form["email"].ToString();
            Register details = new Register()
            {
                UserName = _userName,
                Password = _Password,
                email = _email
            };
            return details;
        }
    }
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Register(int id = 0)
        {
            Register user = new Register();
            return View(user);
        }

        [HttpPost]
        public ActionResult Register([ModelBinder(typeof(RegistrationBinder))] Register details)
        {
            if (ModelState.IsValid)
            {
                Membership.CreateUser(details.UserName, details.Password, details.email);
            }
            return View("Register");
        }
    }
}