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

            //if (opcion == "pagina1")
            //{
            //    return RedirectToAction("Create", "Curricula");
            //}
            //else if (opcion == "pagina2")
            //{
            //    return RedirectToAction("Create", "DatosEmpresas");
            //}

            //// En caso de que no se haya seleccionado una opción válida
            
            return View();
        }


        

        public ActionResult Pagina1()
        {
            // Lógica y vista para la Pagina1
            return RedirectToAction("Create", "Curricula");
        }

        public ActionResult Pagina2()
        {
            // Lógica y vista para la Pagina2
            return RedirectToAction("Create", "DatosEmpresas");
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
                        //DatosEmpresasController datosEmpresasController = new DatosEmpresasController(user.correo);
                        //OfertaEmpleosController ofertaEmpleosController = new OfertaEmpleosController(user.correo);
                        return RedirectToAction("Index", "HomeEmpresa");
                        
                        //Session["User"] = user;
                    }
                    //Si es 2 es Futuro empleado
                    else if (user.idRol == 2)
                    {
                        //CurriculaController curricula = new CurriculaController(user.correo);
                        //HomeEmpleadoController homeEmpleadoController = new HomeEmpleadoController(user.correo);

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