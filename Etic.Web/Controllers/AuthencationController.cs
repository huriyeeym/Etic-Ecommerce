using Etic.Business.ControllerHandler;
using Etic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Etic.Web.Controllers;

public class AuthencationController : Controller
{
    private readonly IAuthenticationControllerHandler _authencationControllerHandler;

    public AuthencationController(IAuthenticationControllerHandler authencationControllerHandler)
    {
        _authencationControllerHandler = authencationControllerHandler;
    }

    public IActionResult Index()
    {
        var r = Request;
        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // response nesnesine sadece controller içerisindeki action içinden erişilebilir.

            var loginResult = _authencationControllerHandler.UserLogin(model.Email, model.Password, Response);
            return RedirectToAction("Index", "Home");
        }
        //todo:rasim:login iş kaldı
        return
            View(model); // kullanıcı hatalı işlem yaparsa aynen girdiği datalar geri gönderilir.Yani email adresini tekrar girmesine gerek kalmaz.
    }


    public IActionResult Exit()
    {
        bool result = _authencationControllerHandler.Exit(Request);
        if (result)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Error", "Home");
        }
        return View();
    }
}