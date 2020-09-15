using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Controllers;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Tests.Controllers
{
    [TestClass]
    public class CartControllertests
    {
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<IOrderProcessor> mock1 = new Mock<IOrderProcessor>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ ProductID = 1 , Name = "P1" , Category = "Apples"}
            });
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, mock1.Object);
            cartController.AddToCart(cart, 1, null);
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<IOrderProcessor> mock1 = new Mock<IOrderProcessor>();

            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = "Apples" } });
            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object, mock1.Object);
            RedirectToRouteResult result = cartController.AddToCart(cart, 2, "myUrl");
        }
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();
            CartController cartController = new CartController(null, null);
            CartIndexViewModel result = (CartIndexViewModel)cartController.Index(cart, "url").ViewData.Model;
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "url");
            Assert.AreEqual(result.ReturnUrl, "url");
        }
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController cartController = new CartController(null, mock.Object);
            ViewResult result = cartController.Checkout(cart, shippingDetails);
            //assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController cartController = new CartController(null, mock.Object);
            cartController.ModelState.AddModelError("error", "error");
            ViewResult viewResult = cartController.Checkout(cart, new ShippingDetails());
            //assert
            mock.Verify( m => m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never());
            Assert.AreEqual("", viewResult.ViewName);
            Assert.AreEqual(false, viewResult.ViewData.ModelState.IsValid);
        }
        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController cartController = new CartController(null, mock.Object);
            ViewResult result = cartController.Checkout(cart, new ShippingDetails());
            //assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }
    }
}



