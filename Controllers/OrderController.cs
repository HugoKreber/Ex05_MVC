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
				
				.ToList();
			ViewData["ArticleOptions"] = articles;
            return View();
		}

		// POST: OrderController/Create
		[HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderViewModel orderVm)
		{
			orderVm.OrderDate = DateTime.Today;
            if (ModelState.IsValid)
            {
				Order orderToAdd = Mapping.ToOrder(orderVm);



                orderService.Add(orderToAdd);
                return Ok(new { redirectUrl = Url.Action("Index", "Order") });
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

				.ToList();
				ViewData["ArticleOptions"] = articles;

                return BadRequest(new { message = string.Join(" ", errorsString) });
            }
             
        }

		// GET: OrderController/Edit/5
		public ActionResult Edit(int id)
		{
			var vm = Mapping.ToOrderViewModel(orderService.GetOrders().SingleOrDefault(o => o.Id == id));
            var warehouseNames = warehouseService.GetWarehouses()
			.Select(w => new
			{
				Text = w.Name,
				Value = w.Id
			})
			.ToList();
            ViewData["WarehouseOptions"] = warehouseNames;
            var articles = articleService.GetArticles()

                .ToList();
            ViewData["ArticleOptions"] = articles;
            return View(vm);
		}

        
        
            // POST: OrderController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] OrderViewModel orderVm)
		{
			try
			{
                if (ModelState.IsValid)
                {
                    orderService.Update(Mapping.ToOrder(orderVm));
                    return Ok(new { redirectUrl = Url.Action("Index", "Order") });
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                                           .Where(y => y.Count > 0)
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
                        Description = w.Description,
                        Id = w.Id,
                        StockQuantity = w.StockQuantity
                    })
                    .ToList();
                    ViewData["ArticleOptions"] = articles;
                    return BadRequest(new { message = string.Join(" ", errorsString) });
                }
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
            else return View();
        }







    }
}
