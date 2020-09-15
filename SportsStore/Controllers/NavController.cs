using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _productRepository;

        public NavController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public PartialViewResult Menu(string category = null )
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _productRepository.Products
                                .Select(x => x.Category)
                                .Distinct()
                                .OrderBy(x => x);
            return PartialView("FlexMenu", categories);
        }
    }
}