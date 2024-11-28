using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotec_MVC.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();
        

        // O método está retornando a View Usuario/Index.cshtml

        public IActionResult Index()
        {
            // Pegar as informações da session que são necessárias para que apareça os detalhes do meu usuário
            int id = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            Usuario usuarioEncontrado = context.Usuario.FirstOrDefault(usuario => usuario.UsuarioID == id)!;

            if (usuarioEncontrado == null)
            {
                return NotFound();
            }

            // Procurar o curso que meu usuárioEncontrado está cadastrado!
            Curso cursoEncontrado = context.Curso.FirstOrDefault(curso => curso.CursoID == usuarioEncontrado.CursoID)!;

            if(cursoEncontrado == null)
            {
                // O usuário não possui curso cadastrado
                ViewBag.Curso = "O usuário não possui curso cadastrado";
            }else
            {
                // O usuário possui o curso XXX
                ViewBag.Curso = cursoEncontrado.Nome;
            }

            ViewBag.Nome = usuarioEncontrado.Nome;
            ViewBag.Email = usuarioEncontrado.Email;
            ViewBag.Telefone = usuarioEncontrado.Contato;
            ViewBag.DtNasc = usuarioEncontrado.DtNascimento.ToString("dd/MM/yyyy");








            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}