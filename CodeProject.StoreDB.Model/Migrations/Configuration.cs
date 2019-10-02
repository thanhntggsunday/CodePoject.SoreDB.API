namespace CodeProject.StoreDB.Model.Migrations
{
    using CodeProject.StoreDB.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            CreateProductCategorySample(context);
            CreatePostCategorySample(context);
            CreatePostSample(context);
            CreateProductSample(context);

            // Creat product:
            GenerateRandomProduction(context, 1);
            GenerateRandomProduction(context, 2);
        }

        private void CreateProductCategorySample(ApplicationDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Laptop-PC",Alias="Laptop-PC",Status=true },
                    new ProductCategory() { Name="Smart phone",Alias="smart-phone",Status=true },
                    new ProductCategory() { Name="Smart TV",Alias="smart-tv",Status=true },
                    new ProductCategory() { Name="Phụ kiện Laptop",Alias="Phụ kiện Laptop",Status=true },
                    new ProductCategory() { Name="Phụ kiện Smart-phone",Alias="Phụ kiện Smart-phone",Status=true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreatePostCategorySample(ApplicationDbContext context)
        {
            if (context.PostCategories.Count() == 0)
            {
                List<PostCategory> categories = new List<PostCategory>()
                {
                    new PostCategory() { Name="Thông tin khuyến mại",Alias="Promation",Status=true },
                    new PostCategory() { Name="News Smart phone",Alias="smart-phone",Status=true },
                    new PostCategory() { Name="News Laptop",Alias="laptop",Status=true },
                    new PostCategory() { Name="News Smart TV",Alias="smart-tv",Status=true },
                    new PostCategory() { Name="News Phụ kiện Laptop",Alias="Phụ kiện Laptop",Status=true },
                    new PostCategory() { Name="News Phụ kiện Smart-phone",Alias="Phụ kiện Smart-phone",Status=true }
                };
                context.PostCategories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private void CreateProductSample(ApplicationDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                List<Product> products = new List<Product>()
                {
                    new Product() { CategoryID = 1,  Price = 12000, Name="HP Elitebook 8440p",Alias="Elitebook-8440p",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 13943, Name="HP Elitebook 44t89",Alias="Elitebook-44t89",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 44t89", Content = "Laptop HP Elitebook 44t89 OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 23000, Name="HP Elitebook 00-yyu",Alias="Elitebook-00-yyu",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 00-yyu", Content = "Laptop HP Elitebook 00-yyu OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 13943,  Name="HP Elitebook 66-ryty",Alias="Elitebook-66-ryty",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 66-ryty", Content = "Laptop HP Elitebook 66-ryty OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 14753, Name="HP Elitebook eek9901",Alias="Elitebook-eek9901",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook eek9901", Content = "Laptop HP Elitebook eek9901 OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 15000, Name="HP Elitebook 67tCG",Alias="Elitebook-67tCG",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 67tCG", Content = "Laptop HP Elitebook 67tCG OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 16867, Name="HP Elitebook hh-pp",Alias="Elitebook-hh-pp",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 67tCG", Content = "Laptop HP Elitebook 67tCG OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 13943, Name="HP Elitebook kk-uu09",Alias="Elitebook-kk-uu09",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 13943, Name="HP Elitebook oo666",Alias="Elitebook-oo666",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 19000, Name="HP Elitebook 464346",Alias="Elitebook-464346",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },new Product() { CategoryID = 1,  Price = 13943, Name="HP Elitebook 8440p",Alias="Elitebook-8440p",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },
                    new Product() { CategoryID = 1,  Price = 13943, Name="HP Elitebook 99ggk",Alias="Elitebook-99ggk",Status=true, Image = "/Content/Images/AcerE1-470-01.png", Description = "Laptop HP Elitebook 8440p", Content = "Laptop HP Elitebook 8440p OS: Windows 7 pro" },

                     new Product() { CategoryID = 2,  Price = 20000, Name="IPhone 56457",Alias="IPhone 4534",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 18000, Name="IPhone 45534",Alias="IPhone 575",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 19000, Name="IPhone tytr",Alias="IPhone 46",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                      new Product() { CategoryID = 2,  Price = 20000, Name="IPhone 3434",Alias="IPhone 34634",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 22000, Name="IPhone 5y54fgh",Alias="IPhone ryrt",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 24000, Name="IPhone 4564",Alias="IPhone reye",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                      new Product() { CategoryID = 2,  Price = 34000, Name="IPhone hhk",Alias="IPhone ggg",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 9000, Name="IPhone yyiio",Alias="IPhone 4t4",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 9500, Name="IPhone 4666hh",Alias="IPhone 5577",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                      new Product() { CategoryID = 2,  Price = 16000, Name="IPhone yy6y7",Alias="IPhone 666",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 17000, Name="IPhone 565445",Alias="IPhone 575",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" },
                     new Product() { CategoryID = 2,  Price = 19000, Name="IPhone 5547",Alias="IPhone 777",Status=true, Image = "/Content/Images/iPhone.jpg", Description = "Iphone", Content = "IPhone  OS: IOS 12" }
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private void CreatePostSample(ApplicationDbContext context)
        {
            if (context.Posts.Count() == 0)
            {
                List<Post> posts = new List<Post>()
                {
                    new Post() { CategoryID = 2, Name="Thông tin khuyến mại",Alias="Promation",Status=true, Image="/Content/Images/TV02.jpg", Content=@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur [....]",
                    Description=@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},

                    new Post() { CategoryID = 2, Name="News Laptop",Alias="Laptop",Status=true, Image="/Content/Images/TV02.jpg", Content=@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur [....]",
                    Description=@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."}
                };
                context.Posts.AddRange(posts);
                context.SaveChanges();
            }
        }

        private void GenerateRandomProduction(ApplicationDbContext context, int categoryId)
        {
            string[] arrayNames =
            {
                "ko04ii-", "66902r-", "9906dko-", "550otkkO-", "55yitklo-", "kolji990-",
                "55jo0-", "lio9974-", "88ofkl", "99rKoe-", "aloijsr-", "dao009L1-"
            };
            int[] arrayPrices =
            {
                12000, 9000, 15000, 20000, 31000, 6500, 21000, 15000
            };
            string laptopFirstName = "Laptop HP";
            string smartPhoneFirstName = "Iphone";
            string lastName = "";
            string description = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                                   sed do eiusmod tempor incididunt ut";
            // Image = "/Content/Images/AcerE1-470-01.png"
            string laptopImageUrl = @"/Content/Images/AcerE1-470-01.png";
            string smartphoneImageUrl = @"/Content/Images/iPhone.jpg";

            List<Product> products = new List<Product>();

            for (int i = 0; i < 1000; i++)
            {
                Random rnd = new Random();
                // create number of product:
                // int productNumber = rnd.Next(5000);
                int indexOfName = rnd.Next(arrayNames.Length);
                int indexOfPrice = rnd.Next(arrayPrices.Length);

                lastName = arrayNames[indexOfName] + i;
                string productName = "";
                string alias = "";
                string image = "";

                switch (categoryId)
                {
                    case 1:
                        image = laptopImageUrl;
                        productName = laptopFirstName + " " + lastName;
                        alias = laptopFirstName + "-" + lastName;
                        break;

                    case 2:
                        image = smartphoneImageUrl;
                        productName = smartPhoneFirstName + " " + lastName;
                        alias = smartPhoneFirstName + "-" + lastName;
                        break;

                    default:
                        break;
                }

                Product product = new Product()
                {
                    CategoryID = categoryId,
                    Price = arrayPrices[indexOfPrice],
                    Name = productName,
                    Alias = alias,
                    Status = true,
                    Image = image,
                    Description = description,
                    Content = description
                };

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

        }
    }
}