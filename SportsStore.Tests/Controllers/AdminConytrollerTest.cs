using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class AdminConytrollerTest
    {

        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
                });

            AdminController adminController = new AdminController(mock.Object);

            ViewResult result = (ViewResult)adminController.Index();
            Assert.IsTrue(result.Model != null);
            var model = ((IEnumerable<Product>)result.Model).ToArray();
            Assert.AreEqual("P1", model[0].Name);
            Assert.AreEqual("P2", model[1].Name);
            Assert.AreEqual("P3", model[2].Name);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController adminController = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            ActionResult result = adminController.Edit(product);
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController adminController = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            adminController.ModelState.AddModelError("error", "error");
            ActionResult result = adminController.Edit(product);
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result , typeof(ViewResult));
        }

        [TestMethod]

        public void Can_Delete_Valid_Products()
        {
            Product product = new Product
            {
                ProductID = 2,
                Name = "Test"
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 3, Name = "P3"},
            });
            AdminController adminController = new AdminController(mock.Object);
             adminController.Delete(product.ProductID);            mock.Verify(m => m.DeleteProduct
            (product.ProductID));        
        }

    }
}
