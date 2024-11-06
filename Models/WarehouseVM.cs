using BO;
using Ex05_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exercice_5_MVC.Models
{
    public class WarehouseVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public int PostalCode { get; set; }


        /// <summary>
        /// Cette liste contient les codes accès du warehouse en md5.
        /// </summary>
        public List<string> CodeAccesMD5 { get; set; } = new();

        [AllowNull]
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

    }
}
