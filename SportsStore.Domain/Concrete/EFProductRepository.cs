using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products =>
            context.Products;

        public Product DeleteProduct(int productId)
        {     
            Product dbEntry = context.Products.Find(productId);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
           if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntity = context.Products.Find(product.ProductID);
                if(dbEntity != null)
                {
                    dbEntity.Name = product.Name;
                    dbEntity.Description = product.Description;
                    dbEntity.Price = product.Price;
                    dbEntity.Category = product.Category;
                    dbEntity.ImageData = product.ImageData;
                    dbEntity.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
