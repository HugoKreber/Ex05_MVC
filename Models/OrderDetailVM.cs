using BO;
using System.ComponentModel.DataAnnotations;

namespace Ex05_MVC.Models
{
    public class OrderDetailVM
    {


        [Key]
        public int Id { get; set; }
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Veuillez selectionner un nom d'article")]
        public ArticleVM Article { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Il doit y avoir au moins un Article")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Il doit y avoir unprix unitaire")]
        public decimal UnitPrice { get; set; }
    }
}
