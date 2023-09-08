using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
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
            try
            {
                RepositorioPago rp = new RepositorioPago();
                rp.CrearPago(pago);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                return RedirectToAction(nameof(Index));

        }

        // GET: Pagos/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioPago rp = new RepositorioPago();
            return View(rp.ObtenerUnPago(id));
        }

        // POST: Pagos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {
            try
            {
                RepositorioPago rp = new RepositorioPago();
                rp.EliminarPago(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        // GET: Pagos/Create
        public ActionResult PagoPorContrato(int id)
        {
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