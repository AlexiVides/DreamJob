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
    public class DatosEmpresasController : Controller
    {
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();


        int idDetalleE;

        // GET: DatosEmpresas
        public ActionResult Index()
        {
            var datosEmpresa = db.DatosEmpresa.Include(d => d.Usuario);
            return View(datosEmpresa.ToList());
        }

        // GET: DatosEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;

            idDetalleE = db.DatosEmpresa
                .Where(a => a.nombre == nombre)
                .Select(a => a.idDatosEmpresa)
                .FirstOrDefault();
            if (idDetalleE == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEmpresa datosEmpresa = db.DatosEmpresa.Find(idDetalleE);
            if (datosEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(datosEmpresa);
        }

        // GET: DatosEmpresas/Create
        public ActionResult Create()
        {
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario");
            return View();
        }

        // POST: DatosEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDatosEmpresa,nombre,direccion,telefono,licencia,descripcion,estado,idUsuario")] DatosEmpresa datosEmpresa)
        {
            if (ModelState.IsValid)
            {
                datosEmpresa.estado = "Activo";

                int idE = db.Usuario
               .OrderByDescending(p => p.idUsuario)
               .Select(p => p.idUsuario)
               .FirstOrDefault();

                datosEmpresa.idUsuario = idE;


                db.DatosEmpresa.Add(datosEmpresa);
                db.SaveChanges();

                string nombreE = db.DatosEmpresa
                .OrderByDescending(r => r.idDatosEmpresa)
                .Select(r => r.nombre)
                .FirstOrDefault();

                TempData["Nombre"] = nombreE;
                return RedirectToAction("Index", "HomeEmpresa");
            }

            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", datosEmpresa.idUsuario);
            return View(datosEmpresa);
        }

        // GET: DatosEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEmpresa datosEmpresa = db.DatosEmpresa.Find(id);
            if (datosEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", datosEmpresa.idUsuario);
            return View(datosEmpresa);
        }

        // POST: DatosEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDatosEmpresa,nombre,direccion,telefono,licencia,descripcion,estado,idUsuario")] DatosEmpresa datosEmpresa)
        {
            if (ModelState.IsValid)
            {
                string nombre = TempData["Nombre"] as string;
                TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
                ViewBag.Nombre = nombre;

                int Empresa = (from c in db.Usuario
                               join p in db.DatosEmpresa on c.idUsuario equals p.idUsuario
                               where p.nombre == nombre
                               select c.idUsuario)
                              .FirstOrDefault();

                
                datosEmpresa.estado = "Activo";
                datosEmpresa.idUsuario = Empresa;

                db.Entry(datosEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", datosEmpresa.idUsuario);
            return View(datosEmpresa);
        }

        // GET: DatosEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosEmpresa datosEmpresa = db.DatosEmpresa.Find(id);
            if (datosEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(datosEmpresa);
        }

        // POST: DatosEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosEmpresa datosEmpresa = db.DatosEmpresa.Find(id);
            db.DatosEmpresa.Remove(datosEmpresa);
            db.SaveChanges();
            return RedirectToAction("Index");
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
