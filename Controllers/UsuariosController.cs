using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliariaVGM.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUsuarios());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            
                RepositorioUsuario ru = new RepositorioUsuario();
                ru.CrearUsuario(usuario);

                return RedirectToAction(nameof(Index));
            
        }

        // GET: Usuarios/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioUsuario  ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {

                RepositorioUsuario ru = new RepositorioUsuario();
                ru.EditarUsuario(usuario);

                return RedirectToAction(nameof(Index));

        }

        // GET: Usuarios/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario usuario)
        {
            try
            {
                RepositorioUsuario ru = new RepositorioUsuario();
                ru.EliminarUsuario(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}