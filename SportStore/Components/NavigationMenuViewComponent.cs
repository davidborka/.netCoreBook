using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SportStore.Components
{
	public class NavigationMenuViewComponent:ViewComponent
	{
		private IStoreRepository repository;

		public NavigationMenuViewComponent(IStoreRepository repo)
		{
			repository = repo;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"]; 
			return View(repository.Products
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x));
		}
	}
}
