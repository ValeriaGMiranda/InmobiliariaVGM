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
    public class PagosController : Controller
    {
        // GET: Pagos
        public ActionResult Index()
        {
            RepositorioPago rp = new RepositorioPago();
            return View(rp.ObtenerPagos());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int id)
        {
            RepositorioPago rp = new RepositorioPago();
            return View(rp.ObtenerUnPago(id));
        }

        // GET: Pagos/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
                RepositorioPago rp = new RepositorioPago();
                rp.CrearPago(pago);
                 TempData["creado"] = "Si";
                return RedirectToAction(nameof(Index));
    
        }


        // GET: Pagos/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioPago rp = new RepositorioPago();
            return View(rp.ObtenerUnPago(id));
        }

        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {

                RepositorioPago rp = new RepositorioPago();
                rp.EditarPago(pago);

                 TempData["editado"] = "Si";

                return RedirectToAction(nameof(Index));

        }

        // GET: Pagos/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioPago rp = new RepositorioPago();
            return View(rp.ObtenerUnPago(id));
        }

        // POST: Pagos/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {
                RepositorioPago rp = new RepositorioPago();
                rp.EliminarPago(id);

                 TempData["eliminado"] = "Si";

                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        // GET: Pagos/Create
        public ActionResult PagoPorContrato(int id,int id_inquilino = 0)
        {
            ViewBag.id_inquilino = id_inquilino;
            RepositorioPago rp = new RepositorioPago();
            ViewBag.Id_Contrato = id;
            ViewBag.ListaPagos = rp.PagosContratoPorInquilino(id);

            return View();
        }

        // POST: Pagos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult PagoPorContrato(int id,Pago pago)
        {
            RepositorioPago rp = new RepositorioPago();
            rp.CrearPago(pago);
            return RedirectToAction(nameof(PagoPorContrato),id);
        }
    }
}