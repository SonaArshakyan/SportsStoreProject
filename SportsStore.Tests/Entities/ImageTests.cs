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

namespace SportsStore.Tests.Entities
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Product product = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                product,
                new Product {ProductID = 3, Name = "P3"}
                }.AsQueryable());
            ProductController productController = new ProductController(mock.Object);
            ActionResult result = productController.GetImage(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(product.ImageMimeType,
            ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
            new Product{ProductID = 1 , Name = "P1"} , 
            new Product { ProductID = 2 , Name = "P2"}
            }.AsQueryable());
            ProductController productController = new ProductController(mock.Object);
            ActionResult result = productController.GetImage(100);
            Assert.IsNull(result);
        }
    }
}
