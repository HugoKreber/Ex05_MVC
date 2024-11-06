using System.ComponentModel.DataAnnotations;

namespace Ex05_MVC.Models
{
    public class ArticleVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "L'article doit avoir un nom")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'article doit avoir une description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'article doit avoir un prix")]
        public decimal Price { get; set; }
        
        [Range(0,int.MaxValue,ErrorMessage = "Le nombre d'article est invalide")]
        public int StockQuantity { get; set; }
    }
}
