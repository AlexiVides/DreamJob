using Dream.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class HomeEmpresaController : Controller
    {
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();
        // GET: HomeEmpresa
        public ActionResult Index()
        {
            //Aca nos traemos el nombre de la empresa
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            ViewData["Mensaje"] = nombre;         

            return View();
        }







        public ActionResult PaginaMercadeo()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Mercadeo";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }


        public ActionResult PaginaTecnologia()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Tecnologia";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }


        public ActionResult PaginaRecursosHumanos()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Recursos Humanos";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }

        public ActionResult PaginaContabilidad()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Contabilidad";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }

        public ActionResult PaginaSalud()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Salud";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }

        public ActionResult PaginaConstruccion()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud


            string categoria = "Construccion";
            TempData["IdCategorio"] = categoria;
            return RedirectToAction("Index", "OfertaEmpleos", new { nombre = nombre, categoria = categoria });
        }







        // GET: HomeEmpresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeEmpresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeEmpresa/Create
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

        // GET: HomeEmpresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeEmpresa/Edit/5
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

        // GET: HomeEmpresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeEmpresa/Delete/5
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
