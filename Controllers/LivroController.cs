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
            // PRIMEIRA PARTE: Cadastrar um livro na tabela Livro
            Livro novoLivro = new Livro();

            // O que meu usuário escrever no formulário será atribuido ao novoLivro

            novoLivro.Nome = form ["Nome"].ToString();
            novoLivro.Descricao = form ["Descricao"].ToString();
            novoLivro.Escritor = form ["Escritor"].ToString();
            novoLivro.Editora = form ["Editora"].ToString();
            novoLivro.Idioma = form ["Idioma"].ToString();

            // Trabalhar com imagens:
            if(form.Files.Count > 0){
                //Promeiro passo:
                    //Amazenaremos o arquivo enviado pelo usuário
                    var arquivo = form.Files[0];

                //Segundo passo:
                    var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Livros" );
                    //Validarmos se a pasta que será armazenada as imagens, existe. Caso não exista, criaremos uma nova pasta
                    if(Directory.Exists(pasta)){
                        //Criar a pasta:
                        Directory.CreateDirectory(pasta);
                    }

                // Terceiro passo: 
                    // Criar a varíavel para armazenar o caminho em que meu arquivo estara, além do nome dele
                    var caminho = Path.Combine(pasta, arquivo.FileName);
                    
                    using (var stream = new FileStream(caminho, FileMode.Create)){
                        arquivo.CopyTo(stream);
                    }

                    novoLivro.Imagem = arquivo.FileName;

                }else{
                    novoLivro.Imagem= "padrao.png";
                }

                // img

            context.Livro.Add(novoLivro);
            context.SaveChanges();

            // SEGUNDA PARTE: Adicionar dentro da LivroCategoria a categoria que pertence ao novoLivro

            // Lista a tabela LivroCategoria:
            List<LivroCategoria> ListaLivroCategorias = new List<LivroCategoria>();
            

            // Array que possui as categorias selecionas pelo usuário
            string[] categoriasSelecionadas = form ["Categoria"].ToString().Split(',');

            foreach(string categoria in categoriasSelecionadas){
                LivroCategoria livroCategoria = new LivroCategoria();
                livroCategoria.CategoriaID = int.Parse(categoria);
                livroCategoria.LivroID = novoLivro.LivroID;
                ListaLivroCategorias.Add(livroCategoria);
            }
            
                context.LivroCategoria.AddRange(ListaLivroCategorias);
                context.SaveChanges();

                return LocalRedirect("/Livro/Cadastro");
            }
            


        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
