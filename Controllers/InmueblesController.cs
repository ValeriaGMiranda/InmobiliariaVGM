using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
    public class InmueblesController : Controller
    {
        // GET: Inmuebles
        public ActionResult Index()
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerInmuebles());
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerUnInmueble(id));
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                RepositorioInmueble ri = new RepositorioInmueble();
                ri.CrearInmueble(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmuebles/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioInmueble ri = new RepositorioInmueble();

            return View(ri.ObtenerUnInmueble(id));
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {

                RepositorioInmueble ri = new RepositorioInmueble();
                ri.EditarInmueble(inmueble);

                return RedirectToAction(nameof(Index));
   
        }

        // GET: Inmuebles/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerUnInmueble(id));
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble inmueble)
        {
            try
            {
                RepositorioInmueble ri = new RepositorioInmueble();
                ri.EliminarInmueble(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}