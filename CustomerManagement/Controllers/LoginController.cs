using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Authenticate()
        {
            User user = new User();
            return View("Login", user);
        }

        public ActionResult ValidateLogin()
        {
            //Form Authentication
            User user = new User();
            TryUpdateModel<User>(user);

            if (ModelState.IsValid)
            {
                List<User> userList = null;
                using (CustomerDBContext context = new CustomerDBContext())
                {
                    userList = (from u in context.Users
                                where u.UserName == user.UserName && u.Password == user.Password
                                select u).ToList<User>();
                }

                if (userList.Count == 1)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return RedirectToAction("Home", "Home");
                }
                else
                    ModelState.AddModelError("LoginError", "provided username or password is incorrect");
            }
            return View("Login", user);
        }
    }
}