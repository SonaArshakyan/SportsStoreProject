using SportsStore.Domain;
using SportsStore.Domain.Entities;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productsRepository;
        public int PageSize = 1;
        public ProductController(IProductRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public ViewResult List(string category,  int page = 1 )
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _productsRepository.Products
                        .Where(p => p.Category == category || category == null)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPages = PageSize,
                    TotalItems = category == null ? _productsRepository.Products.Count() :
                    _productsRepository.Products.Where(e => e.Category == category).Count()

                },
                CurrentCategory = category
            };
            return View(model);
        }
        public FileContentResult GetImage(int productId)
        {
            Product product = _productsRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;         
            }
        }
    }
}