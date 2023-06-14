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
    public class OfertaEmpleosController : Controller
    {
        private BdDreamJobEntities1 db = new BdDreamJobEntities1();


        // GET: OfertaEmpleos

        //Este muestra todas las ofertas que a creado la empresa en la que se inicio sesion
        public ActionResult TodasLasOfertas()
        {
            string nombre = TempData["Nombre"] as string;
            TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            ViewBag.Nombre = nombre;
            var ofertaEmpleo = db.OfertaEmpleo.Include(o => o.Categoria).Include(o => o.DatosEmpresa).Where(o => o.estado == "Activo" && o.DatosEmpresa.nombre == nombre);
            return View(ofertaEmpleo.ToList());
        }

        //aca muestra las ofertas de empleo de la empresa que se inicio sesion en la categoria que se le dio clic
        public ActionResult Index(string nombre, string categoria)
        {
            var ofertaEmpleo = db.OfertaEmpleo.Include(o => o.Categoria).Include(o => o.DatosEmpresa).Where(o => o.estado == "Activo" && o.DatosEmpresa.nombre == nombre && o.Categoria.nombre == categoria);
            return View(ofertaEmpleo.ToList());
        }


        //Este Index2 es el que se les mostrara a los empleados cuando le den a alguna categoria
        public ActionResult Index2(string categoria)
        {
            var ofertaEmpleo = db.OfertaEmpleo.Include(o => o.Categoria).Include(o => o.DatosEmpresa).Where(o => o.estado == "Activo" && o.Categoria.nombre == categoria);
            return View(ofertaEmpleo.ToList());
        }

        //Este TodasLasOfertasEmpleados es que se le mostrara todas la ofertas existentes al empleado o futuro empleado
        public ActionResult TodasLasOfertasEmpleados()
        {
            var ofertaEmpleo = db.OfertaEmpleo.Include(o => o.Categoria).Include(o => o.DatosEmpresa).Where(o => o.estado == "Activo");
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


        public ActionResult AplicarOferta([Bind(Include = "idAplicacion,idCurriculum,idOfertaEmpleo")] Aplicacion aplicacion, int? id)
        {
            if (ModelState.IsValid)
            {
                //Nombre del usuario
                string nombre = TempData["Nombre"] as string;
                TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
                ViewBag.Nombre = nombre;
                ViewData["Mensaje"] = nombre;

                int IdUsuario = db.Curriculum
                         .Where(p => p.nombre == nombre)
                         .Select(p => p.idCurriculum)
                         .FirstOrDefault();

                var Aplica = db.Aplicacion.Where(x => x.OfertaEmpleo.idOfertaEmpleo == id && x.Curriculum.idCurriculum == IdUsuario).FirstOrDefault();
                if (Aplica == null)
                {
                    //Id de la oferta de empleo
                    aplicacion.idOfertaEmpleo = id;
                    aplicacion.idCurriculum = IdUsuario;
                    db.Aplicacion.Add(aplicacion);
                    db.SaveChanges();
                    return RedirectToAction("TodasLasOfertasEmpleados", "OfertaEmpleos");
                }
                else
                {
                    ViewData["Mensaje"] = "El usuario ya existe!";
                }
            }

            ViewBag.idCurriculum = new SelectList(db.Curriculum, "idCurriculum", "nombre", aplicacion.idCurriculum);
            ViewBag.idOfertaEmpleo = new SelectList(db.OfertaEmpleo, "idOfertaEmpleo", "nombre", aplicacion.idOfertaEmpleo);
            return RedirectToAction("TodasLasOfertasEmpleados", "OfertaEmpleos");
        }


        public ActionResult Details2(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
            //if (ofertaEmpleo == null)
            //{
            //    return HttpNotFound();
            //}
            //TempData["Nombre"] = id;
            var ofertaEmpleo = db.OfertaEmpleo.Include(c => c.DatosEmpresa).Where(c => c.idOfertaEmpleo == id);
            return View(ofertaEmpleo);
        }








        // GET: OfertaEmpleos/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre");
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre");
            return View();
        }

        // POST: OfertaEmpleos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOfertaEmpleo,nombre,nVacantes,descripcion,requerimientos,funciones,Salario,prestaciones,cargo,estado,idCategoria,idDatosEmpresa")] OfertaEmpleo ofertaEmpleo)
        {
            if (ModelState.IsValid)
            {
                string nombre = TempData["Nombre"] as string;
                TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
                ViewBag.Nombre = nombre;

                int Empresa = db.DatosEmpresa
                        .Where(p => p.nombre == nombre)
                        .Select(p => p.idDatosEmpresa)
                        .FirstOrDefault();

                ofertaEmpleo.estado = "Activo";
                ofertaEmpleo.idDatosEmpresa = Empresa;
                db.OfertaEmpleo.Add(ofertaEmpleo);
                db.SaveChanges();
                return RedirectToAction("TodasLasOfertas");
            }


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
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre", ofertaEmpleo.idCategoria);
            //ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre", ofertaEmpleo.idDatosEmpresa);
            return View(ofertaEmpleo);
        }

        // POST: OfertaEmpleos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOfertaEmpleo,nombre,nVacantes,descripcion,requerimientos,funciones,Salario,prestaciones,cargo,estado,idCategoria,idDatosEmpresa")] OfertaEmpleo ofertaEmpleo)
        {
            if (ModelState.IsValid)
            {
                string nombre = TempData["Nombre"] as string;
                TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
                ViewBag.Nombre = nombre;

                int Empresa = db.DatosEmpresa
                        .Where(p => p.nombre == nombre)
                        .Select(p => p.idDatosEmpresa)
                        .FirstOrDefault();

                ofertaEmpleo.estado = "Activo";

                ofertaEmpleo.idDatosEmpresa = Empresa;
                db.Entry(ofertaEmpleo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TodasLasOfertas");
            }
            ViewBag.idCategoria = new SelectList(db.Categoria, "idCategoria", "nombre", ofertaEmpleo.idCategoria);
            ViewBag.idDatosEmpresa = new SelectList(db.DatosEmpresa, "idDatosEmpresa", "nombre", ofertaEmpleo.idDatosEmpresa);
            return View(ofertaEmpleo);
        }

        // GET: OfertaEmpleos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OfertaEmpleo ofertaEmpleo = db.OfertaEmpleo.Find(id);
        //    if (ofertaEmpleo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ofertaEmpleo);
        //}

        // POST: OfertaEmpleos/Delete/5
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed([Bind(Include = "idOfertaEmpleo,idCategoria,idDatosEmpresa, estado")] OfertaEmpleo ofertaEmpleo, int? id)
        //{
        //    string nombre = TempData["Nombre"] as string;
        //    TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
        //    ViewBag.Nombre = nombre;

        //    int Empresa = db.DatosEmpresa
        //            .Where(p => p.nombre == nombre)
        //            .Select(p => p.idDatosEmpresa)
        //            .FirstOrDefault();

        //    ofertaEmpleo.estado = "Inactivo";

        //    //ofertaEmpleo.idDatosEmpresa = Empresa;
        //    db.Entry(ofertaEmpleo).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("TodasLasOfertas");
        //}

        public ActionResult desactivar(int? id)
        {
            //string nombre = TempData["Nombre"] as string;
            //    TempData.Keep("Nombre"); // Mantener los datos de TempData para la próxima solicitud
            //    ViewBag.Nombre = nombre;

            //    int Empresa = db.DatosEmpresa
            //            .Where(p => p.nombre == nombre)
            //            .Select(p => p.idDatosEmpresa)
            //            .FirstOrDefault();

            OfertaEmpleo of = db.OfertaEmpleo.Find(id);


                of.estado = "Desactivo";
                db.Entry(of).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TodasLasOfertas");
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

