using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain;
using SportsStore.Domain.Entities;
using SportsStore;
using SportsStore.Models;
using SportsStore.HtmlHelpers;

namespace SportsStore.Tests
{
    [TestClass]
    public class ListControllerTest
    {
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
               new Product[]
               {
                   new Product { ProductID = 1 , Name = "P1"},
                   new Product { ProductID = 2 , Name = "P2"},
                   new Product { ProductID = 3 , Name = "P3"},
                   new Product { ProductID = 4 , Name = "P4"},
                   new Product { ProductID = 5 , Name = "P5"},
               }
                );

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;
            //act
            ProductsListViewModel result = (ProductsListViewModel)productController.List(null, 2).Model;
            //assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPages, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPage, 2);

        }
        [TestMethod]

        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
               new Product[]
               {
                   new Product { ProductID = 1 , Name = "P1"},
                   new Product { ProductID = 2 , Name = "P2"},
                   new Product { ProductID = 3 , Name = "P3"},
                   new Product { ProductID = 4 , Name = "P4"},
                   new Product { ProductID = 5 , Name = "P5"},
               }
                );
            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;
            //act
            ProductsListViewModel result = (ProductsListViewModel)productController.List(null, 2).Model;
            //assert
            Product[] products = result.Products.ToArray();
            Assert.AreEqual(products.Length, 2);
            Assert.AreEqual(products[0].Name, "P4");
            Assert.AreEqual(products[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });
            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2",
            1).Model)
            .Products.ToArray();
            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category ==
            "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category ==
            "Cat2");
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;
            int result1 = ((ProductsListViewModel)productController
            .List("Cat1").Model).PagingInfo.TotalItems;
            int result2 = ((ProductsListViewModel)productController
           .List("Cat2").Model).PagingInfo.TotalItems;
            int result3 = ((ProductsListViewModel)productController
           .List("Cat3").Model).PagingInfo.TotalItems;
            Assert.IsTrue(result1 == 2);
            Assert.AreEqual(result2, 2);
            Assert.AreEqual(result3,1);
        }
    }
}
