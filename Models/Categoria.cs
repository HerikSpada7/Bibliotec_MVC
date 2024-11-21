using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BibliotecKure.Models
{
    public class Categoria
    {
    
    [Key]
        public int CategoriaID { get; set; }
        public string ? Nome { get; set; }
    }
}