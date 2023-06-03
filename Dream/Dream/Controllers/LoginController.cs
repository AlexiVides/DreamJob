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

        public ActionResult EleccionRol(string opcion)
        {

          
            return View();
        }


        

        public ActionResult Pagina1()
        {
            // Lógica y vista para la Pagina1


            int rol = 2;
            TempData["IdRol"] = rol;
            //return RedirectToAction("Create", "Curricula" ,new { dato = rol }) ;
            return RedirectToAction("Create", "Usuarios" ,new { rol = rol }) ;


        }

        public ActionResult Pagina2()
        {
            // Lógica y vista para la Pagina2
            int rol = 1;
            TempData["IdRol"] = rol;
            return RedirectToAction("Create", "Usuarios",  new { rol = rol });
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
                        string nombreEmpresa = db.DatosEmpresa
                     .Where(p => p.idUsuario == user.idUsuario)
                     .Select(p => p.nombre)
                     .FirstOrDefault();

                        string nombre = nombreEmpresa;
                        TempData["Nombre"] = nombre;

                        return RedirectToAction("Index", "HomeEmpresa");
                        
                       
                    }
                    //Si es 2 es Futuro empleado
                    else if (user.idRol == 2)
                    {
                       
                       string nombreUsuario = db.Curriculum
                      .Where(p => p.idUsuario == user.idUsuario)
                      .Select(p => p.nombre)
                      .FirstOrDefault();


                        string nombre = nombreUsuario;
                        TempData["Nombre"] = nombre;

                        return RedirectToAction("Index", "HomeEmpleado" );
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