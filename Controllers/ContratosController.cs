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
        [HttpGet]
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
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {   
            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerUnContrato(id));
        }

        // POST: Contratos/Delete/5
        [Authorize(policy:"Administrador")]
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

        [HttpGet]
        public ActionResult ContratosPorInquilino(int id)
        {
            RepositorioContrato ri = new RepositorioContrato();
            return View(ri.ObtenerContratosPorInquilino(id));
        }


        [HttpGet]
        public ActionResult ContratosVigentes()
        {
            
            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerContratosVigentes());
        }

        [HttpGet]
        public ActionResult ContratosInmuebles()
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            ViewBag.listaInmuebles = ri.ObtenerInmuebles();

            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerContratos());
        }

        [HttpPost]
        public ActionResult ContratosInmuebles(ContratoBusqueda cb) 
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            ViewBag.listaInmuebles = ri.ObtenerInmuebles();

            RepositorioContrato rc = new RepositorioContrato();
            return View(rc.ObtenerContratosPorInmueble(cb));
        }

    }
}