using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportStore.Test
{
	public class OrderControllerTests
	{
		[Fact]
		public void CannotCheckoutWhenCartIsEmpty()
		{
			//Arrange
			Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
			Cart cart = new Cart();

			Order order = new Order();
			OrderController target = new OrderController(mockOrderRepo.Object, cart);

			//Act
			ViewResult result = target.Checkout(order) as ViewResult;

			mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}


		[Fact]
		public void CannotCheckoutInvalidShippingData()
		{
			//Arrange
			Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
			Cart cart = new Cart();
			cart.AddItem(new Product(), 1);
			OrderController target = new OrderController(mockOrderRepo.Object, cart);
			target.ModelState.AddModelError("error", "error");
			
		    //Act
			ViewResult result = target.Checkout(new Order()) as ViewResult;

			//Assert
			mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

			Assert.True(string.IsNullOrEmpty(result.ViewName));
			Assert.False(result.ViewData.ModelState.IsValid);
		}

		[Fact]
		public void CanCheckoutAndSubmitOrder()
		{
			//Arrange
			Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
			Cart cart = new Cart();
			cart.AddItem(new Product(), 1);
			OrderController target = new OrderController(mockOrderRepo.Object, cart);

			//Act
			RedirectToPageResult result = target.Checkout(new Order()) as RedirectToPageResult;

			//Assert
			mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
			Assert.Equal("/Completed",result.PageName);
		}
	}
}
