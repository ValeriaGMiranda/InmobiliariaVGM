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
            RepositorioPropietario rp = new RepositorioPropietario();
            RepositorioUso ru = new RepositorioUso();
            RepositorioTipo rt = new RepositorioTipo();

            ViewBag.listaPropietarios = rp.ObtenerPropietarios();
            ViewBag.listaUsos = ru.ObtenerUsos();
            ViewBag.listaTipos = rt.ObtenerTipos();

            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
                RepositorioInmueble ri = new RepositorioInmueble();
                ri.CrearInmueble(inmueble);

                TempData["creado"] = "Si";

                return RedirectToAction(nameof(Index));
        }

        // GET: Inmuebles/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            RepositorioPropietario rp = new RepositorioPropietario();
            RepositorioUso ru = new RepositorioUso();
            RepositorioTipo rt = new RepositorioTipo();

            ViewBag.listaPropietarios = rp.ObtenerPropietarios();
            ViewBag.listaUsos = ru.ObtenerUsos();
            ViewBag.listaTipos = rt.ObtenerTipos();

            return View(ri.ObtenerUnInmueble(id));
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {

                RepositorioInmueble ri = new RepositorioInmueble();
                ri.EditarInmueble(inmueble);

                 TempData["editado"] = "Si";

                return RedirectToAction(nameof(Index));
   
        }

        // GET: Inmuebles/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerUnInmueble(id));
        }

        // POST: Inmuebles/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble inmueble)
        {
                RepositorioInmueble ri = new RepositorioInmueble();
                ri.EliminarInmueble(id);

                 TempData["Eliminado"] = "Si";

                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult BuscarInmuebles()
        {
            RepositorioUso ru = new RepositorioUso();
            RepositorioTipo rt = new RepositorioTipo();

            ViewBag.listaUsos = ru.ObtenerUsos();
            ViewBag.listaTipos = rt.ObtenerTipos();
            
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerInmuebles());
        }

        [HttpPost]
        public ActionResult BuscarInmuebles(InmuebleBusqueda ib)
        {
            RepositorioUso ru = new RepositorioUso();
            RepositorioTipo rt = new RepositorioTipo();

            ViewBag.listaUsos = ru.ObtenerUsos();
            ViewBag.listaTipos = rt.ObtenerTipos(); 

            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.BuscarInmuebles(ib));
        }


        [HttpGet]
        public ActionResult InmueblesEstados()
        {
            RepositorioPropietario rp = new RepositorioPropietario();

            ViewBag.listaPropietarios = rp.ObtenerPropietarios();

            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerInmuebles());
        }

        [HttpPost]
        public ActionResult InmueblesEstados(InmuebleBusqueda ib)
        {
            RepositorioPropietario rp = new RepositorioPropietario();

            ViewBag.listaPropietarios = rp.ObtenerPropietarios();


            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.BuscarInmueblesEstado(ib));
        }

         [HttpGet]
        public ActionResult InmueblesContratos()
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.ObtenerInmuebles());
        }

        [HttpPost]
        public ActionResult InmueblesContratos(InmuebleBusqueda ib)
        {
            RepositorioInmueble ri = new RepositorioInmueble();
            return View(ri.BuscarInmuebles(ib));
        }


    }
}