using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class HomeEmpleadoController : Controller
    {
        private string correo;

        public HomeEmpleadoController(string correo)
        {
            this.correo = correo;
        }

        // GET: HomeEmpleado
        public ActionResult Index()
        {
            return View();
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
