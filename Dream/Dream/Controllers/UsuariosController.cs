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
    public class UsuariosController : Controller
    {
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "rol1");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,correo,contra,estado,idRol")] Usuario usuario, int rol)
        {


            //ViewBag.Nombre = nombre;
            if (rol == 1)
            {
                if (ModelState.IsValid)
                {
                    var user = db.Usuario.Where(x => x.correo.Trim() == usuario.correo).FirstOrDefault();
                    if (user == null)
                    {
                        usuario.estado = "Activo";
                        usuario.idRol = 1;
                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        string gmail = usuario.correo;
                        TempData["Mensaje"] = gmail;
                        return RedirectToAction("Create", "DatosEmpresas");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "El usuario ya existe!";
                        return View();
                    }

                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var user = db.Usuario.Where(x => x.correo.Trim() == usuario.correo).FirstOrDefault();
                    if (user == null)
                    {
                        usuario.estado = "Activo";
                        usuario.idRol = 2;

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        string gmail = usuario.correo;
                        TempData["Mensaje"] = gmail;
                        return RedirectToAction("Create", "Curriculu");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "El usuario ya existe!";
                        return View();
                    }
                }
            }



            ViewBag.idRol = new SelectList(db.Rol, "idRol", "rol1", usuario.idRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "rol1", usuario.idRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,correo,contra,estado,idRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "rol1", usuario.idRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
