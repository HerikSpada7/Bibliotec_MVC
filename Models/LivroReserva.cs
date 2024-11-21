using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecKure.Models
{
    public class LivroReserva
    {
        // Na tabela livro reserva temos 6 atributos
        // 1pk
        // 2fk
        
        [Key]
        public int LivroReservaID { get; set; }
        public DateOnly DtReserva { get; set; }
        public DateOnly DtDevolucao { get; set; }
        public int Status { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioID { get; set; }

        [ForeignKey("Livro")]
        public int LivroID { get; set; }
    }
}