using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dream.Models;

namespace Dream.Controllers
{
    public class AplicacionesController : Controller
    {
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();

        // GET: Aplicaciones
        public ActionResult Index()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            ViewData["Mensaje"] = nombre;
            var aplicacion = db.Aplicacion.Include(a => a.Curriculum).Include(a => a.OfertaEmpleo).Include(a => a.OfertaEmpleo.DatosEmpresa).Where(a => a.Curriculum.nombre == nombre);
            return View(aplicacion.ToList());

            //var aplicacion = (from c in db.Aplicacion
            //                  join p in db.OfertaEmpleo on c.idOfertaEmpleo equals p.idOfertaEmpleo
            //                  where p.idDatosEmpresa == p.idDatosEmpresa
            //                  select c.idAplicacion);
            //return View(aplicacion.ToList());
        }

        public ActionResult IndexEmpresa()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            ViewData["Mensaje"] = nombre;
            var aplicacion = db.Aplicacion.Include(a => a.Curriculum).Include(a => a.OfertaEmpleo).Include(a => a.OfertaEmpleo.DatosEmpresa).Where(a => a.OfertaEmpleo.DatosEmpresa.nombre == nombre);
            return View(aplicacion.ToList());
        }

        // GET: Aplicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion aplicacion = db.Aplicacion.Find(id);
            if (aplicacion == null)
            {
                return HttpNotFound();
            }
            return View(aplicacion);
        }

        // GET: Aplicaciones/Create
        public ActionResult Create()
        {
            ViewBag.idCurriculum = new SelectList(db.Curriculum, "idCurriculum", "nombre");
            ViewBag.idOfertaEmpleo = new SelectList(db.OfertaEmpleo, "idOfertaEmpleo", "nombre");
            return View();
        }

        // POST: Aplicaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAplicacion,idCurriculum,idOfertaEmpleo")] Aplicacion aplicacion)
        {
            if (ModelState.IsValid)
            {
                db.Aplicacion.Add(aplicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCurriculum = new SelectList(db.Curriculum, "idCurriculum", "nombre", aplicacion.idCurriculum);
            ViewBag.idOfertaEmpleo = new SelectList(db.OfertaEmpleo, "idOfertaEmpleo", "nombre", aplicacion.idOfertaEmpleo);
            return View(aplicacion);
        }

        // GET: Aplicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion aplicacion = db.Aplicacion.Find(id);
            if (aplicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCurriculum = new SelectList(db.Curriculum, "idCurriculum", "nombre", aplicacion.idCurriculum);
            ViewBag.idOfertaEmpleo = new SelectList(db.OfertaEmpleo, "idOfertaEmpleo", "nombre", aplicacion.idOfertaEmpleo);
            return View(aplicacion);
        }

        // POST: Aplicaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAplicacion,idCurriculum,idOfertaEmpleo")] Aplicacion aplicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aplicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCurriculum = new SelectList(db.Curriculum, "idCurriculum", "nombre", aplicacion.idCurriculum);
            ViewBag.idOfertaEmpleo = new SelectList(db.OfertaEmpleo, "idOfertaEmpleo", "nombre", aplicacion.idOfertaEmpleo);
            return View(aplicacion);
        }

        // GET: Aplicaciones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Aplicacion aplicacion = db.Aplicacion.Find(id);
        //    if (aplicacion == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aplicacion);
        //}

        // POST: Aplicaciones/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Eliminar(int? id)
        {
            Aplicacion aplicacion = db.Aplicacion.Find(id);
            db.Aplicacion.Remove(aplicacion);
            db.SaveChanges();
            return RedirectToAction("IndexEmpresa", "Aplicaciones");
        }
        public ActionResult EliminarEmple(int? id)
        {
            Aplicacion aplicacion = db.Aplicacion.Find(id);
            db.Aplicacion.Remove(aplicacion);
            db.SaveChanges();
            return RedirectToAction("Index", "Aplicaciones");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
