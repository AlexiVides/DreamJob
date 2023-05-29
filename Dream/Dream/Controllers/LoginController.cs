using Dream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario log)
        {
            try
            {
                using (BdDreamJobEntities db = new BdDreamJobEntities())
                {
                    var user = db.Usuario.Where(x => x.correo.Trim() == log.correo && x.contra == log.contra.Trim()).FirstOrDefault();

                    if (user == null)
                    {
                        ViewData["Mensaje"] = "Incorrecto";
                        return View();

                    }
                    //Si es 1 es Empresa
                    if (user.idRol == 1)
                    {
                        return RedirectToAction("Index", "HomeEmpresa");
                        //Session["User"] = user;
                    }
                    //Si es 2 es Futuro empleado
                    else if (user.idRol == 2)
                    {
                        return RedirectToAction("Index", "HomeEmpleado");
                        //Session["User"] = user;
                    }

                   

                }
                return View();
                //return RedirectToAction("Index", "Home");

            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}