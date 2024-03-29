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
    public class CurriculuController : Controller
    {
        int IdCurriculo;
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();

        // GET: Curriculu
        public ActionResult Index()
        {
            var curriculum = db.Curriculum.Include(c => c.Usuario);           
            return View(curriculum.ToList());
        }

        //Esta sera un tabla de todos los usuarios los cuales pueden buscar su curriculu
        //public ActionResult Index2()
        //{
        //    var curriculum = db.Curriculum.Include(c => c.Usuario);

        //    return View(curriculum.ToList());
        //}

        public ActionResult Index2(string buscar)
        {
            var listaRegistros = db.Curriculum.ToList(); 

            if (!string.IsNullOrEmpty(buscar))
            {
                listaRegistros = listaRegistros.Where(r => r.nombre.Contains(buscar)).ToList();
                // Reemplaza "Nombre" con el nombre de la columna en la tabla que deseas utilizar para la búsqueda
            }
            else
            {
                var curriculum = db.Curriculum.Include(c => c.Usuario);

                return View(curriculum.ToList());
            }

            return View(listaRegistros);
        }







        // GET: Curriculu/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Curriculum curriculum = db.Curriculum.Find(id);
            //if (curriculum == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(curriculum);
            var curriculum = db.Curriculum.Include(c => c.Usuario).Where(c => c.idCurriculum == id);
            return View(curriculum.ToList());
        }

        public ActionResult DetailsEmpleado(int? id)
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            ViewData["Mensaje"] = nombre;

            IdCurriculo = db.Curriculum
                .Where(a => a.nombre == nombre)
                .Select(a => a.idCurriculum)
                .FirstOrDefault();

            if (IdCurriculo == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(IdCurriculo);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            return View(curriculum);

        }

        // GET: Curriculu/Create
        public ActionResult Create()
        {
            string gmail = TempData["Mensaje"] as string;
            TempData.Keep("Mensaje"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = gmail;
            ViewData["Mensaje"] = gmail;
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo");
           
            return View();
        }

        // POST: Curriculu/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCurriculum,nombre,apellido,edad,genero,direccion,telefono,dui,licencia,nivelAcademico,historialAcademico,referenciaPers,experienciaLab,descripcion,correoOpc,segundoIdioma,imagen,estado,idUsuario")] Curriculum curriculum)
        {
            if (ModelState.IsValid)
            {
                curriculum.estado = "Activo";
                int idU = db.Usuario
               .OrderByDescending(p => p.idUsuario)
               .Select(p => p.idUsuario)
               .FirstOrDefault();

                curriculum.idUsuario = idU;
                db.Curriculum.Add(curriculum);
                db.SaveChanges();

                string nombre = db.Curriculum
                .OrderByDescending(r => r.idCurriculum)
                .Select(r => r.nombre)
                .FirstOrDefault();

                TempData["Nombre"] = nombre;
                return RedirectToAction("Index", "HomeEmpleado");
            }

            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", curriculum.idUsuario);
            return View(curriculum);
        }

        // GET: Curriculu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(id);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", curriculum.idUsuario);
            return View(curriculum);
        }

        // POST: Curriculu/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCurriculum,nombre,apellido,edad,genero,direccion,telefono,dui,licencia,nivelAcademico,historialAcademico,referenciaPers,experienciaLab,descripcion,correoOpc,segundoIdioma,imagen,estado,idUsuario")] Curriculum curriculum)
        {
            if (ModelState.IsValid)
            {
                string nombre = TempData["Nombre"] as string;
                TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
                ViewBag.Nombre = nombre;

                int Empleado = (from c in db.Usuario
                               join p in db.Curriculum on c.idUsuario equals p.idUsuario
                               where p.nombre == nombre
                               select c.idUsuario)
                               .FirstOrDefault();

                curriculum.estado = "Activo";
                curriculum.idUsuario = Empleado;

                db.Entry(curriculum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsEmpleado");
            }
            ViewBag.idUsuario = new SelectList(db.Usuario, "idUsuario", "correo", curriculum.idUsuario);
            return View(curriculum);
        }

        // GET: Curriculu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(id);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            return View(curriculum);
        }

        // POST: Curriculu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curriculum curriculum = db.Curriculum.Find(id);
            db.Curriculum.Remove(curriculum);
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
