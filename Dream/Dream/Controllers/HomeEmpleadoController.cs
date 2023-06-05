using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class HomeEmpleadoController : Controller
    {

        // GET: HomeEmpleado
        public ActionResult Index()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            ViewData["Mensaje"] = nombre;
            return View();
        }


        public ActionResult PaginaMercadeo()
        {

            string categoria = "Mercadeo";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }


        public ActionResult PaginaTecnologia()
        {
            string categoria = "Tecnologia";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }


        public ActionResult PaginaRecursosHumanos()
        {
            string categoria = "Recursos Humanos";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }

        public ActionResult PaginaContabilidad()
        {
            string categoria = "Contabilidad";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }

        public ActionResult PaginaSalud()
        {
            string categoria = "Salud";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }

        public ActionResult PaginaConstruccion()
        {
            string categoria = "Construccion";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index2", "OfertaEmpleos", new { categoria = categoria });
        }




























        // GET: HomeEmpleado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeEmpleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeEmpleado/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeEmpleado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeEmpleado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeEmpleado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeEmpleado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
