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
    public class PropietariosController : Controller
    {
        [Authorize]
        // GET: Propietarios
        public ActionResult Index()
        {
            RepositorioPropietario rp = new RepositorioPropietario();
            return View(rp.ObtenerPropietarios());
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            RepositorioPropietario rp = new RepositorioPropietario();
            return View(rp.ObtenerUnPropietario(id));
        }

        
        // GET: Propietarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                RepositorioPropietario rp = new RepositorioPropietario();
                rp.CrearPropietario(propietario);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietarios/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioPropietario rp = new RepositorioPropietario();
            return View(rp.ObtenerUnPropietario(id));
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Propietario propietario)
        {
            try
            {
                RepositorioPropietario rp = new RepositorioPropietario();
                rp.EditarPropietario(propietario);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietarios/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioPropietario rp = new RepositorioPropietario();
            return View(rp.ObtenerUnPropietario(id));
        }

        // POST: Propietarios/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario propietario)
        {
            try
            {
                RepositorioPropietario rp = new RepositorioPropietario();
                rp.EliminarPropietario(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}