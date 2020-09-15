using SportsStore.Domain;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;
        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ActionResult Index()
        {
            return View(_productRepository.Products);
        }
        public ViewResult Edit(int productId)
        {
            Product product = _productRepository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product , HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null) 
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _productRepository.SaveProduct(product);            
            TempData["message"] = string.Format("{0} has been saved ", product.Name);
            return RedirectToAction("Index"); }
             else
                return View(product);
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _productRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    } 
}