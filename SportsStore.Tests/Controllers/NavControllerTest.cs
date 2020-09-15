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

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class NavControllerTest
    {

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            });
            NavController navController = new NavController(mock.Object);
            var results = ((IEnumerable<string>)navController.Menu().Model).ToList();
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
             });
            NavController navController = new NavController(mock.Object);
            string categoryToSelect = "Apples";
            var result = navController.Menu(categoryToSelect).ViewBag.SelectedCategory;
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}
