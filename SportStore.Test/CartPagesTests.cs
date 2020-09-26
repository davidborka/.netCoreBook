using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStore.Models;
using SportStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SportStore.Test
{
	public class CartPagesTests
	{
		[Fact]
		public void CanLoadCart()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] { p1, p2 }).AsQueryable());

			Cart testCart = new Cart();
			testCart.AddItem(p1, 1);
			testCart.AddItem(p2, 1);

			//-create a mock page context and session
			Mock<ISession> sessionMock = new Mock<ISession>();
			byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
			sessionMock.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));
			Mock<HttpContext> mockContext = new Mock<HttpContext>();
			mockContext.Setup(ctx => ctx.Session).Returns(sessionMock.Object);

			//Action
			CartModel cartModel = new CartModel(mock.Object, testCart)
			{
				PageContext = new PageContext(new Microsoft.AspNetCore.Mvc.ActionContext
				{
					HttpContext = mockContext.Object,
					RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
					ActionDescriptor = new PageActionDescriptor()
				})
			};
			cartModel.OnGet("myUrl");

			Assert.Equal(2, cartModel.Cart.Lines.Count);
			Assert.Equal("myUrl", cartModel.ReturnUrl);
			
		}

		[Fact]
		public void CanUpdateCart()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] { p1}).AsQueryable());

			Cart testCart = new Cart();
			Mock<ISession> sessionMock = new Mock<ISession>();
			sessionMock.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
				.Callback<string, byte[]>((key, val) => {
					testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val));
				});

			Mock<HttpContext> mockContext = new Mock<HttpContext>();
			mockContext.Setup(ctx => ctx.Session).Returns(sessionMock.Object);
			
			//Action
			CartModel cartModel = new CartModel(mock.Object, testCart)
			{
				PageContext = new PageContext(new Microsoft.AspNetCore.Mvc.ActionContext
				{
					HttpContext = mockContext.Object,
					RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
					ActionDescriptor = new PageActionDescriptor()
				})
			};

			cartModel.OnPost(1, "myUrl");

			Assert.Single(testCart.Lines);
			Assert.Equal("P1", testCart.Lines.First().Product.Name);
			Assert.Equal(1, testCart.Lines.First().Quantity);
		}
	}
}
