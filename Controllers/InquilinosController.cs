using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
    [Authorize]
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

                RepositorioInquilino ri = new RepositorioInquilino();
                ri.CrearInquilino(inquilino);

                TempData["creado"] = "Si";

                return RedirectToAction(nameof(Index));

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
                RepositorioInquilino ri = new RepositorioInquilino();
                ri.EditarInquilino(inquilino);

                TempData["editado"] = "Si";
    
                return RedirectToAction(nameof(Index));

        }

        // GET: Inquilinos/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioInquilino ri = new RepositorioInquilino();
            return View(ri.ObtenerUnInquilino(id));
        }

        // POST: Inquilinos/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino inquilino)
        {
                RepositorioInquilino ri = new RepositorioInquilino();
                ri.EliminarInquilino(id);

                TempData["eliminado"] = "Si";

                return RedirectToAction(nameof(Index));
        }


    }
}