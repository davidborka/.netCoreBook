using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Test
{
	public class HomeControllerTest
	{
		[Fact]
		public void CanUseRepository()
		{
			//Arange
			Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
			Product[] testProduct = new Product[]
			{
				new Product {ProductID = 1, Name = "P1"},
				new Product {ProductID = 2, Name = "P2"}
			};

			mockRepo.Setup(m => m.Products).Returns(testProduct.AsQueryable<Product>());
			HomeController controller = new HomeController(mockRepo.Object);


			//Act
			ProductListViewModel result = controller.Index(null).ViewData.Model as ProductListViewModel;

			//Assert
			Product[] prodArray = result.Products.ToArray();
			Assert.Equal(testProduct, prodArray);
		}

		[Fact]
		public void Can_Paginate()
		{
			// Arrange
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] {
						new Product {ProductID = 1, Name = "P1"},
                        new Product {ProductID = 2, Name = "P2"},
						new Product {ProductID = 3, Name = "P3"},
						new Product {ProductID = 4, Name = "P4"},
						new Product {ProductID = 5, Name = "P5"}
						}).AsQueryable<Product>());
			HomeController controller = new HomeController(mock.Object);
			controller.PageSize = 3;
			// Act
			ProductListViewModel result = controller.Index(null, 2).ViewData.Model as ProductListViewModel;
			// Assert
			Product[] prodArray = result.Products.ToArray();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P4", prodArray[0].Name);
			Assert.Equal("P5", prodArray[1].Name);
		}

		[Fact]
		public void CanSendPaginationViewModel()
		{
			//Arange
			Mock<IStoreRepository> repo = new Mock<IStoreRepository>();
			repo.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID = 1, Name = "P1"},
				new Product {ProductID = 2, Name ="P2"},
				new Product {ProductID = 3, Name ="P3"},
				new Product {ProductID = 4, Name = "P4"},
				new Product {ProductID = 5, Name = "P5"},

			}).AsQueryable());

			//Arange
			HomeController controller = new HomeController(repo.Object) { PageSize = 3 };

			//Act
			ProductListViewModel result = controller.Index(null, 2).ViewData.Model as ProductListViewModel;

			//Assert
			PagingInfo pagingInfo = result.PagingInfo;
			Assert.Equal(2, pagingInfo.CurrentPage);
			Assert.Equal(3, pagingInfo.ItemsPerPage);
			Assert.Equal(5, pagingInfo.TotalItems);
			Assert.Equal(2, pagingInfo.TotalPages);		
		}

		[Fact]
		public void CanUseCategoryFilter()
		{
			Mock<IStoreRepository> repo = new Mock<IStoreRepository>();
			repo.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductID = 1, Name = "P1", Category="test"},
				new Product {ProductID = 2, Name ="P2", Category="test"},
				new Product {ProductID = 3, Name ="P3", Category="noValid"},
				new Product {ProductID = 4, Name = "P4", Category="noValid"},
				new Product {ProductID = 5, Name = "P5", Category="noValid"},

			}).AsQueryable());
			
			//Arange
			HomeController controller =new HomeController(repo.Object) { PageSize = 3};

			//Act
			ProductListViewModel result = controller.Index("test", 1).ViewData.Model as ProductListViewModel;

			//Assert
			Product[] prodArray = result.Products.ToArray();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P1", prodArray[0].Name);
			Assert.Equal("test", prodArray[0].Category);

			Assert.Equal("P2", prodArray[1].Name);
			Assert.Equal("test", prodArray[1].Category);


		}


	}
}
