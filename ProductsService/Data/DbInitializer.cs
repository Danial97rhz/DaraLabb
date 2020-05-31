using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Data
{
    public static class DbInitializer
    {
        public static void Initialize (ProductDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }
            var categories = new Category[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Rings" },
                new Category { Id = Guid.NewGuid(), Name = "Earrings" }
            };

            var products = new List<Product>
            {
                new Product
                {
                      Id = Guid.Empty,
                      Name = "CROWN GOLD",
                      Price = 399,
                      InStock = true,
                      Category = (from c in categories where c.Name =="Rings" select c).First(),
                      ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw2cd846f8/product/T/TR/TR2208/TR2208-414-11.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "COMPASS GOLD",
                     Price = 179,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Rings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw1419eede/product/T/TR/TR2278/TR2278-849-7.jpg?sfrm=tif"
                },
                new Product
                {
                      Id = Guid.NewGuid(),
                      Name = "BLACK DIAMOND SKULL",
                      Price = 1799,
                      InStock = true,
                      Category = (from c in categories where c.Name =="Rings" select c).First(),
                      ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw904a7798/product/J/J_TR/J_TR0015/J_TR0015-723-11.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "EAGLE",
                     Price = 489,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Rings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw207d8a2b/product/T/TR/TR2250/TR2250-976-7.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "CROWN",
                     Price = 219,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Rings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw7edfbb53/product/T/TR/TR2208/TR2208-643-11.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "FALCON",
                     Price = 399,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Rings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw48401626/product/T/TR/TR2066/TR2066-641-11.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "EAR STUDS SKULL PAVÉ",
                     Price = 399,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Earrings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dw21fbee66/product/J/J_H/J_H0012/J_H0012-714-11.jpg?sfrm=tif"
                },
                new Product
                {
                     Id = Guid.NewGuid(),
                     Name = "EARRINGS FEATHER",
                     Price = 249,
                     InStock = true,
                     Category = (from c in categories where c.Name =="Earrings" select c).First(),
                     ImgUrl = "https://www.thomassabo.com/dw/image/v2/AAQY_PRD/on/demandware.static/-/Sites-ts-master-catalog/default/dwc038123c/product/H/H/H1992/H1992-471-7.jpg?sfrm=tif"
                },
            };

            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }

            foreach (var p in products)
            {
                context.Products.Add(p);
            }


            context.SaveChanges();
        }
    }
}
