using BO;
using Ex05_MVC.BO;
using Ex05_MVC.Models;
using Ex05_MVC.Service;
using Exercice_5_MVC;
using Exercice_5_MVC.Models;
using Exercice_5_MVC.Service.Exercice_4_MVC.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web.WebPages.Html;
using static NuGet.Packaging.PackagingConstants;

namespace Ex05_MVC.Controllers
{
	public class OrderController : Controller
	{
        OrderService orderService = new OrderService();
		WarehouseService warehouseService = new WarehouseService();
		ArticleService articleService = new ArticleService();

		// GET: OrderController/Create
		public ActionResult Create()
		{
            var warehouseNames = warehouseService.GetWarehouses()
				.Select(w => new 
				{
					Text = w.Name,
					Value = w.Id
                })
				.ToList();
			ViewData["WarehouseOptions"] = warehouseNames;
			var articles = articleService.GetArticles()
				.Select(w => new 
				{
					Name = w.Name,
					Price = w.Price,
					Desc = w.Description,
					Value = w.Id
                })
				.ToList();
			ViewData["ArticleOptions"] = articles;
            return View();
		}

		// POST: OrderController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(OrderViewModel orderVm)
		{
			orderVm.OrderDate = DateTime.Today;
            if (ModelState.IsValid)
            {
				Order orderToAdd = Mapping.ToOrder(orderVm);

				//incr ID
                int maxIdOrder = orderService.GetOrders().OrderByDescending(o => o.Id).FirstOrDefault().Id;
                orderToAdd.Id = maxIdOrder+1;


				//UpdateWarehouse
				Warehouse warhouseOfORder = warehouseService.GetWarehouses().Where(w => w.Id == orderToAdd.WarehouseId).FirstOrDefault();
				warhouseOfORder.Orders.Add(orderToAdd);
				warehouseService.Update(warhouseOfORder);

                orderService.Add(orderToAdd);
                return View("Index");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where( y => y.Count > 0)
                                       .ToList();
				List<string> errorsString = new List<string>();
				errors.ForEach(x =>
				{
					x.ToList().ForEach(e => errorsString.Add(e.ErrorMessage));

				});
                TempData["error"] = errorsString;
                var warehouseNames = warehouseService.GetWarehouses()
				.Select(w => new
				{
					Text = w.Name,
					Value = w.Id
				})
				.ToList();
                ViewData["WarehouseOptions"] = warehouseNames;
                var articles = articleService.GetArticles()
				.Select(w => new
				{
					Name = w.Name,
					Price = w.Price,
					Desc = w.Description,
					Value = w.Id
				})
				.ToList();
							ViewData["ArticleOptions"] = articles;
                return View(orderVm);
            }
             
        }

		// GET: OrderController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: OrderController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, OrderViewModel orderVm)
		{
			try
			{
                if (ModelState.IsValid)
                {
                    orderService.Add(Mapping.ToOrder(orderVm));
                    return RedirectToAction(nameof(Index));
                }
                else { return View(); }
			}
			catch
			{
				return View();
			}
		}
        public ActionResult Index()
        {
            var list = orderService.GetOrders().Select(x => Mapping.ToOrderViewModel(x));
            if (ModelState.IsValid)
                return View(list);
            else return View(null);
        }







    }
}
