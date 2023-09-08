using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
    public class InquilinosController : Controller
    {
        // GET: Inquilinos
        public ActionResult Index()
        {
            RepositorioInquilino ri = new RepositorioInquilino();
            return View(ri.ObtenerInquilinos());
        }

        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            RepositorioInquilino ri = new RepositorioInquilino();
            return View(ri.ObtenerUnInquilino(id));
        }

        // GET: Inquilinos/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                RepositorioInquilino ri = new RepositorioInquilino();
                ri.CrearInquilino(inquilino);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            RepositorioInquilino ri = new RepositorioInquilino();
            return View(ri.ObtenerUnInquilino(id));
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino inquilino)
        {
            try
            {
                RepositorioInquilino ri = new RepositorioInquilino();
                ri.EditarInquilino(inquilino);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            RepositorioInquilino ri = new RepositorioInquilino();
            return View(ri.ObtenerUnInquilino(id));
        }

        // POST: Inquilinos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino inquilino)
        {
            try
            {
                RepositorioInquilino ri = new RepositorioInquilino();
                ri.EliminarInquilino(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}