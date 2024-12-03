using System.Net.Mime;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotec_MVC.Controllers
{
    [Route("[controller]")]
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();

        public IActionResult Index()
        {
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;

            // Criar uma lista de livros
            List<Livro> listaLivros = context.Livro.ToList();

            // Verificar se o livro tem reserva ou não
            var livrosReservados = context.LivroReserva.ToDictionary(Livro => Livro.LivroID, livror => livror.DtReserva);

            ViewBag.Livros = listaLivros;
            ViewBag.LivrosComReserva = livrosReservados;
            
            return View();
        }



        // Método que retorna a tela de cadastro: 
    	[Route("Cadastro")]
        public IActionResult Cadastro()
        {
            ViewBag.Admin = HttpContext.Session.GetString("Admin")!;
            ViewBag.Categorias = context.Categoria.ToList();
            // Retorna a View de cadastro:
            return View();
        }


        // Método para cadastrar um livro:
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Livro novoLivro = new Livro();

            // O que meu usuário escrever no formulário será atribuido ao novoLivro

            novoLivro.Nome = form ["Nome"].ToString();
            novoLivro.Descricao = form ["Descricao"].ToString();
            novoLivro.Escritor = form ["Escritor"].ToString();
            novoLivro.Editora = form ["Editora"].ToString();
            novoLivro.Idioma = form ["Idioma"].ToString();

            context.Livro.Add(novoLivro);
            context.SaveChanges();

        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}