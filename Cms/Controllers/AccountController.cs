using System.Web.Mvc;
using System.Web.Security;
using DataLayer.Repositories;
using DataLayer.ViewModels;

namespace Cms.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public AccountController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                if (_loginRepository.UserExists(login.Username, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("Username", "کاربری یافت نشد");
            }

            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}