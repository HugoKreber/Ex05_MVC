using BO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ex05_MVC.Models
{
	public class OrderViewModel
	{
        [Key]
        public int Id { get; set; }


		[Required(ErrorMessage = "Le Nom du client est requis.")]
		[StringLength(100)]
		public string CustomerName { get; set; }

		[Required(ErrorMessage = "L'Email est requis.")]
		[EmailAddress(ErrorMessage = "L'adresse Email n'est pas correcte.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "L'adresse de livraison est requis.")]
		[StringLength(200,ErrorMessage = "La longueur maximum est 200 caractère.")]
		public string ShippingAddress { get; set; }

		[Required(ErrorMessage = "Une date est requise")]
		[DataType(DataType.Date,ErrorMessage = "Une date valide est requise")]
		public DateTime OrderDate { get; set; } = DateTime.Today;


        [Required(ErrorMessage = "Le montant total doit être rentrer")]
        [Range(1,double.MaxValue, ErrorMessage = "Le montant total doit être supérieur à 0")]
		public double TotalAmount { get; set; }

		[Required(ErrorMessage = "Un statut de facture est requis")]
        public string OrderStatus { get; set; }

		[MinOneElements(ErrorMessage = "Il doit y avoir au moins un Article dans la facture")]
        public List<OrderDetailVM> OrderDetails { get; set; } = new List<OrderDetailVM>();

        [Required]
        public int WarehouseId { get; set; }

		public override string ToString()
		{

        string orderDetailsStr = OrderDetails != null && OrderDetails.Count > 0
        ? string.Join(", ", OrderDetails.Select(od => od.ToString()))
        : "No Order Details";

            return $"Order ID: {Id}, " +
                   $"Customer Name: {CustomerName}, " +
                   $"Email: {Email}, " +
                   $"Shipping Address: {ShippingAddress}, " +
                   $"Order Date: {OrderDate.ToString("yyyy-MM-dd")}, " +
                   $"Total Amount: {TotalAmount:C}, " +
                   $"Order Status: {OrderStatus}, " +
                   $"Warehouse ID: {WarehouseId}, " +
                   $"Order Details: [{orderDetailsStr}]";
        }


    }
}
