﻿using System;
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
    public class OfertaEmpleosController : Controller
    {
        private BdDreamJobEntities db = new BdDreamJobEntities();
        private string correo;

        public OfertaEmpleosController(string correo)
        {
            this.correo = correo;
        }

        // GET: OfertaEmpleos
        public ActionResult Index()
        {
            var ofertaEmpleo = db.OfertaEmpleo.Include(o => o.Cargo).Include(o => o.Categoria).Include(o => o.DatosEmpresa).Where(o => o.estado=="Activo" /*&& o.DatosEmpresa.Usuario.correo == correo*/) ;
            return View(ofertaEmpleo.ToList());
        }
        // GET: OfertaEmpleos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
            if (ofertaEmpleo == null)
            {
                return HttpNotFound();
            }
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Create
        public ActionResult Create()
        {
            ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "nombre");
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre");
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre");
            return View();
        }

        // POST: OfertaEmpleos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOfertaEmpleo,nombre,nVacantes,descripcion,requerimientos,funciones,Salario,prestaciones,estado,idCategoria,idCargo,idDatosEmpresa")] OfertaEmpleo ofertaEmpleo)
        {
            if (ModelState.IsValid)
            {
                db.OfertaEmpleo.Add(ofertaEmpleo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "nombre", ofertaEmpleo.idCargo);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre", ofertaEmpleo.idCategoria);
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre", ofertaEmpleo.idDatosEmpresa);
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
            if (ofertaEmpleo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "nombre", ofertaEmpleo.idCargo);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre", ofertaEmpleo.idCategoria);
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre", ofertaEmpleo.idDatosEmpresa);
            return View(ofertaEmpleo);
        }

        // POST: OfertaEmpleos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOfertaEmpleo,nombre,nVacantes,descripcion,requerimientos,funciones,Salario,prestaciones,estado,idCategoria,idCargo,idDatosEmpresa")] OfertaEmpleo ofertaEmpleo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ofertaEmpleo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCargo = new SelectList(db.Cargo, "idCargo", "nombre", ofertaEmpleo.idCargo);
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre", ofertaEmpleo.idCategoria);
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre", ofertaEmpleo.idDatosEmpresa);
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
            if (ofertaEmpleo == null)
            {
                return HttpNotFound();
            }
            return View(ofertaEmpleo);
        }

        // POST: OfertaEmpleos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
            db.OfertaEmpleo.Remove(ofertaEmpleo);
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
