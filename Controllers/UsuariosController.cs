using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using inmobiliariaVGM.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace inmobiliariaVGM.Controllers
{
    
    public class UsuariosController : Controller
    {   private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;
        public UsuariosController(IConfiguration configuration, IWebHostEnvironment environment)
		{
			this.configuration = configuration;
            this.environment = environment;
		}
        // GET: Usuarios
        [Authorize(policy:"Administrador")]
        public ActionResult Index()
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUsuarios());
        }

        // GET: Usuarios/Details/5
        [Authorize(policy:"Administrador")]
        public ActionResult Details(int id)
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }


        // GET: Usuarios/Create
        [Authorize(policy:"Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            
                RepositorioUsuario ru = new RepositorioUsuario();

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: usuario.Password,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));

                usuario.Password = hashed;

                //Inicio tratamiento del Avatar
                var nom = Guid.NewGuid(); 

                if(usuario.AvatarFile != null) {
                    
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath,"Uploads");
                    
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }

                    string filename = "avatar_" + nom + Path.GetExtension(usuario.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path,filename);
                    

                    using(FileStream stream = new FileStream(pathCompleto,FileMode.Create)){
                        usuario.AvatarFile.CopyTo(stream);
                    }

                    usuario.Avatar = Path.Combine("/Uploads",filename);

                }

                

                ru.CrearUsuario(usuario);

                return RedirectToAction(nameof(Index));
            
        }

        // GET: Usuarios/Edit/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RepositorioUsuario  ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // POST: Usuarios/Edit/5
        [Authorize(policy:"Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {

            RepositorioUsuario ru = new RepositorioUsuario();

                //Inicio tratamiento del Avatar
                var nom = Guid.NewGuid(); 

                if(usuario.AvatarFile != null) {
                    
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath,"Uploads");
                    
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }

                    string filename = "avatar_" + nom + Path.GetExtension(usuario.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path,filename);
                    

                    using(FileStream stream = new FileStream(pathCompleto,FileMode.Create)){
                        usuario.AvatarFile.CopyTo(stream);
                    }

                    usuario.Avatar = Path.Combine("/Uploads",filename);

                }

                ru.EditarUsuario(usuario);

                return RedirectToAction(nameof(Index));

        }

        // GET: Usuarios/Delete/5
        [Authorize(policy:"Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // POST: Usuarios/Delete/5
        [Authorize(policy:"Administrador")]
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

        [AllowAnonymous]
        [HttpGet]       
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
            if(ModelState.IsValid)
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: login.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));

                    RepositorioUsuario ru = new RepositorioUsuario();
                        var e = ru.ObtenerPorEmail(login.Mail);
                        if(e == null || e.Password != hashed)
                        {
                            ModelState.AddModelError("", "Email o contraseña inválidos");
                            TempData["returnUrl"] = returnUrl;
                            return View();
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, e.Mail),
                            new Claim(ClaimTypes.Role, e.RolNombre),
                            new Claim("Id", e.Id_Usuario.ToString()),
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        TempData.Remove("returnUrl");
                        return Redirect(returnUrl);
            }

            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Login","Usuarios");
        }

        public ActionResult Perfil()
        {
            var id =int.Parse(User.Claims.First(x=>x.Type=="Id").Value);

            RepositorioUsuario ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }


        // GET: Usuarios/Edit/5
        [HttpGet]
        public ActionResult EditarPerfil()
        {
            var id =int.Parse(User.Claims.First(x=>x.Type=="Id").Value);
            RepositorioUsuario  ru = new RepositorioUsuario();
            return View(ru.ObtenerUnUsuario(id));
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil(int id, Usuario usuario)
        {
            id =int.Parse(User.Claims.First(x=>x.Type=="Id").Value);

            RepositorioUsuario ru = new RepositorioUsuario();

                //Inicio tratamiento del Avatar
                var nom = Guid.NewGuid(); 

                if(usuario.AvatarFile != null) {
                    
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath,"Uploads");
                    
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }

                    string filename = "avatar_" + nom + Path.GetExtension(usuario.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path,filename);
                    

                    using(FileStream stream = new FileStream(pathCompleto,FileMode.Create)){
                        usuario.AvatarFile.CopyTo(stream);
                    }

                    usuario.Avatar = Path.Combine("/Uploads",filename);

                }

                ru.EditarUsuario(usuario);

                return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public ActionResult CambiarPassword()
            {
                return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarPassword(CambiarPassword pass)
        {
                RepositorioUsuario ru = new RepositorioUsuario();
                var id =int.Parse(User.Claims.First(x=>x.Type=="Id").Value);


                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: pass.Password,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));

                if(pass.Password == pass.Confirmacion){
                    ru.CambiarPassword(id,hashed);
                }else{
                    Console.WriteLine("Error no coinciden los pass");
                }
                
                return  View();

        }

    }

}