using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class HomeEmpresaController : Controller
    {
        // GET: HomeEmpresa
        public ActionResult Index()
        {
            return View();
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
