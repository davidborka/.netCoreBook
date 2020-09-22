using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
	public class HomeController : Controller
	{
		private IStoreRepository repository;
		public int PageSize = 4;
		public HomeController(IStoreRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index(int productPage = 1)
		 => View(new ProductListViewModel {
			 Products = repository.Products
			 .OrderBy(p => p.ProductID)
			 .Skip((productPage - 1) * PageSize)
			 .Take(PageSize),
			 PagingInfo = new PagingInfo
			 {
				 CurrentPage = productPage,
				 ItemsPerPage = PageSize,
				 TotalItems = repository.Products.Count()
			 }
		 });
	}
}
