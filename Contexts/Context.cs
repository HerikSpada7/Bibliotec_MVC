using BibliotecKure.Models;
using Microsoft.EntityFrameworkCore;

namespace Bibliotec.Contexts
{
    // ! Classe que terá as informações do banco de dados !
    public class Context : DbContext
    {
        // ! Criar um método construtor !
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // ! OnConfiguring -> Possui a configuracao da conexao com o banco de dados

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Colocar as informacoes do banco 
            // As configurações existem?

            if (!optionsBuilder.IsConfigured)
            // A string de conexão de banco de dados 
            // Data Source -> Nome do servidor do banco de dados
            // Initial Catalog -> Nome do banco de dados
            // User ID e Password -> Informações de acesso ao servidor do banco de dados
            // Ryan Kure:
            {
                optionsBuilder.UseSqlServer(@"Data Source=NOTE14-S28\\SQLEXPRESS; 
                    Initial Catalog = Bibliotec_MVC;
                    User Id=sa;
                    Password=123; 
                    Integrated Security=true; 
                    TrustServerCertificate = true");
            }
        }
        
        // As referencias das nossas tabelas no banco de dados:
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<LivroCategoria> LivroCategoria { get; set; }
        public DbSet<LivroReserva> LivroReserva { get; set; }
        
        
        
        
        
        
        
        
        
    } 
}