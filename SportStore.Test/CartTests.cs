﻿using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Test
{
	public class CartTests
	{
		[Fact]
		public void CanAddNewLines()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };

			//Arrange
			Cart target = new Cart();

			//Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			CartLine[] results = target.Lines.ToArray();

			//Assert
			Assert.Equal(2, results.Length);
			Assert.Equal(p1, results[0].Product);
			Assert.Equal(p2, results[1].Product);

		}

		[Fact]
		public void CanAddQuantityForExistingLines()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };

			//Arrange
			Cart target = new Cart();

			//Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 10);

			CartLine[] results = target.Lines.OrderBy(c=> c.Product.ProductID).ToArray();


			//Assert
			Assert.Equal(2, results.Length);
			Assert.Equal(11, results[0].Quantity);
			Assert.Equal(1, results[1].Quantity);
		}

		[Fact]
		public void CanRemoveLine()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1" };
			Product p2 = new Product { ProductID = 2, Name = "P2" };
			Product p3 = new Product { ProductID = 3, Name = "P3" };

			//Arrange
			Cart target = new Cart();

			//Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 10);
			target.AddItem(p3, 5);

			//Act
			target.RemoveLine(p2);

			//Assert
			Assert.Empty(target.Lines.Where(c => c.Product == p2));
			Assert.Equal(2, target.Lines.Count());
		}

		[Fact]
		public void CalculateCartTotal()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
			Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
			
			//Arrange
			Cart target = new Cart();

			//Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 3);
			decimal result = target.ComputeTotalValue();

			//Assert
			Assert.Equal(450M, result);

		}

		[Fact]
		public void CanClearContents()
		{
			//Arange
			Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
			Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

			//Arrange
			Cart target = new Cart();

			//Act
			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 3);

			target.Clear();
			//Assert
			Assert.Empty(target.Lines);
		}
	}
}
