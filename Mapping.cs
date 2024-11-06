using BO;
using Elfie.Serialization;
using Ex05_MVC.BO;
using Ex05_MVC.Models;
using Exercice_5_MVC.Models;
using static NuGet.Packaging.PackagingConstants;

namespace Exercice_5_MVC
{
    public class Mapping
    {
        public static WarehouseVM ToWarehouseVM(Warehouse warehouse)
        {
            var vm = new WarehouseVM
            {
                Id = warehouse.Id,
                Address = warehouse.Address,
                Name = warehouse.Name,
                PostalCode = warehouse.PostalCode,

            };
            var listOrder = warehouse.Orders.Select(ToOrderViewModel).ToList();
            if ( listOrder.Count > 0) vm.Orders = listOrder;

            return vm;
        }

        public static Warehouse ToWarehouse(WarehouseVM wm)
        {
            var warehouse = new Warehouse {
                Id = wm.Id,
                Address = wm.Address,
                Name = wm.Name,
                PostalCode = wm.PostalCode,
                Orders = wm.Orders.Select(ToOrder).ToList()
            };
            return warehouse;
        }

        public static OrderViewModel ToOrderViewModel(Order order)
        {
            var vm = new OrderViewModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Email = order.Email,
                ShippingAddress = order.ShippingAddress,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus.ToString(), 
                WarehouseId = order.WarehouseId,
                OrderDetails = order.OrderDetails.Select(ToOrderDetailViewModel).ToList()
            };

            return vm;
        }

        public static Order ToOrder(OrderViewModel orderVM)
        {
            var order = new Order
            {
                Id = orderVM.Id,
                CustomerName = orderVM.CustomerName,
                Email = orderVM.Email,
                ShippingAddress = orderVM.ShippingAddress,
                OrderDate = orderVM.OrderDate,
                TotalAmount = orderVM.TotalAmount,
                OrderStatus = Enum.Parse<OrderStatusEnum>(orderVM.OrderStatus), 
                WarehouseId = orderVM.WarehouseId,
                OrderDetails = orderVM.OrderDetails.Select(ToOrderDetail).ToList() 
            };

            return order;
        }

        public static OrderDetail ToOrderDetail(OrderDetailVM orderDetailVM)
        {
            var orderDetail = new OrderDetail
            {
                Id = orderDetailVM.Id,
                ArticleId = orderDetailVM.ArticleId,
                Article = ToArticle(orderDetailVM.Article), 
                Quantity = orderDetailVM.Quantity,
                UnitPrice = orderDetailVM.UnitPrice
            };
            return orderDetail;
        }

        public static OrderDetailVM ToOrderDetailViewModel(OrderDetail orderDetail)
        {
            var vm = new OrderDetailVM
            {
                Id = orderDetail.Id,
                ArticleId = orderDetail.ArticleId,
                Article = ToArticleViewModel(orderDetail.Article), 
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };

            return vm;
        }

        public static ArticleVM ToArticleViewModel(Article article)
        {
            return new ArticleVM
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description,
                Price = article.Price,
                StockQuantity = article.StockQuantity
            };
        }

        public static Article ToArticle(ArticleVM articleVM)
        {
            return new Article
            {
                Id = articleVM.Id,
                Name = articleVM.Name,
                Description = articleVM.Description,
                Price = articleVM.Price,
                StockQuantity = articleVM.StockQuantity
            };
        }
    }
}
