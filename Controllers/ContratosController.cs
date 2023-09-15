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
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioInquilino rinq = new RepositorioInquilino();


            ViewBag.listaInmuebles = ri.ObtenerInmuebles();
            ViewBag.listaInquilinos = rinq.ObtenerInquilinos();

            RepositorioContrato rc = new RepositorioContrato();

            if(ri.VerificarDisponibilidad(contrato.Id_Inmueble,contrato.Fecha_Inicio,contrato.Fecha_Fin)){
                rc.CrearContrato(contrato);

                TempData["creado"] = "Si";

                return RedirectToAction(nameof(Index));
            }
            else{
                 TempData["Otro"] = "No se pudo crear el contrato no hay disponibilidad.";

                 return RedirectToAction(nameof(Index));
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
                RepositorioInmueble ri = new RepositorioInmueble();                     
                RepositorioContrato rc = new RepositorioContrato();

           //if(ri.VerificarDisponibilidad(contrato.Id_Inmueble,contrato.Fecha_Inicio,contrato.Fecha_Fin)){
                rc.EditarContrato(contrato);

                 TempData["editado"] = "Si";

                return RedirectToAction(nameof(Index));
           // }
           // else{
                   //TempData["Otro"] = "No se pudo crear el contrato no hay disponibilidad.";
                    //return RedirectToAction(nameof(Index));
            //}
       
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
                RepositorioContrato rc = new RepositorioContrato();
                rc.EliminarContrato(id);
                TempData["eliminado"] = "Si";
                return RedirectToAction(nameof(Index));
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

        
        [HttpGet]
        public ActionResult CreateContrato(int id_inmueble = 0) 
        {
            ViewBag.id_inmueble = id_inmueble;
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioInquilino rinq = new RepositorioInquilino();

            ViewBag.listaInmuebles = ri.ObtenerInmuebles();
            ViewBag.listaInquilinos = rinq.ObtenerInquilinos();
            
            return View("Create");
        }

        [HttpGet]
        public ActionResult FinalizarContrato(int id,int id_inquilino = 0)
        {   
            ViewBag.id_inquilino = id_inquilino;
            RepositorioContrato rc = new RepositorioContrato();

            ViewBag.Monto = rc.CalcularMontoCancelacion(id);

            

            return View(rc.ObtenerUnContrato(id));
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarContrato(int id, Contrato contrato,int id_inquilino = 0)
        {
            ViewBag.id_inquilino = id_inquilino;
                RepositorioContrato rc = new RepositorioContrato();
                rc.FinalizarContrato(id);

                TempData["Otro"] = "Contrato Finalizado Correctamente.";

                return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public ActionResult RenovarContrato(int id_inmueble = 0,int id_inquilino = 0) 
        {
            ViewBag.id_inmueble = id_inmueble;
            ViewBag.id_inquilino = id_inquilino;
            
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioInquilino rinq = new RepositorioInquilino();

            ViewBag.listaInmuebles = ri.ObtenerInmuebles();
            ViewBag.listaInquilinos = rinq.ObtenerInquilinos();

            return View("Create");
        }


    }
}