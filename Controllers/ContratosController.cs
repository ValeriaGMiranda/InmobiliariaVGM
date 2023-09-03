using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
    public class ContratosController : Controller
    {
        // GET: Contratos
        public ActionResult Index()
        {
            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerContratos());
        }

        // GET: Contratos/Details/5
        public ActionResult Details(int id)
        {
            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerUnContrato(id));
        }

        // GET: Contratos/Create
        public ActionResult Create()
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioInquilino rinq = new RepositorioInquilino();

            ViewBag.listaInmuebles = ri.ObtenerInmuebles();
            ViewBag.listaInquilinos = rinq.ObtenerInquilinos();
            
            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            try
            {
                RepositorioContrato rc = new RepositorioContrato();
                rc.CrearContrato(contrato);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contratos/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioContrato rc = new RepositorioContrato();
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioInquilino rinq = new RepositorioInquilino();

            ViewBag.listaInmuebles = ri.ObtenerInmuebles();
            ViewBag.listaInquilinos = rinq.ObtenerInquilinos();
        

            return View(rc.ObtenerUnContrato(id));
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato contrato)
        {

                RepositorioContrato rc = new RepositorioContrato();
                rc.EditarContrato(contrato);

                return RedirectToAction(nameof(Index));
       
        }

        // GET: Contratos/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {   
            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerUnContrato(id));
        }

        // POST: Contratos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato contrato)
        {
            try
            {
                RepositorioContrato rc = new RepositorioContrato();
                rc.EliminarContrato(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}