using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products
        {
            get;
        }
        void SaveProduct(Product product);
        Product DeleteProduct(int prodductId);
    }
}
