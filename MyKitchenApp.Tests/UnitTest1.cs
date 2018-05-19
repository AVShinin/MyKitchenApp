using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyKitchenApp.DBContexts;
using MyKitchenApp.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MyKitchenApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        string connectionString = "server=localhost;UserId=root;Password=*******;database=mykitchenapp;persistsecurityinfo=True;port=3306;SslMode=none;Character Set=utf8";

        [TestMethod]
        public async void FillDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseMySQL(connectionString)
                .Options;
            

            using (var context = new ApplicationContext(options))
            {
                context.Products.AddRange(
                    new Models.Product // 1
                    {
                        Title = "Вареная колбаса",
                        Description = "Молочная",
                        Calories = 255000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 2
                    {
                        Title = "Огурец",
                        Description = "Зеленый, свежий",
                        Calories = 15000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 3
                    {
                        Title = "Картофель",
                        Description = "Свежий",
                        Calories = 80000,
                        Count = 50000,
                        Abb = 2
                    },
                    new Models.Product // 4
                    {
                        Title = "Горошек консервированный",
                        Description = "Консервированный",
                        Calories = 55000,
                        Count = 5000,
                        Abb = 2
                    },
                    new Models.Product // 5
                    {
                        Title = "Яйцо",
                        Description = "Сырое, свежее",
                        Calories = 157000,
                        Count = 500,
                        Abb = 1
                    },
                    new Models.Product // 6
                    {
                        Title = "Лук репчатый",
                        Description = "Свежий",
                        Calories = 45000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 7
                    {
                        Title = "Морковь",
                        Description = "Свежая",
                        Calories = 32000,
                        Count = 15000,
                        Abb = 2
                    },
                    new Models.Product // 8
                    {
                        Title = "Капуста",
                        Description = "Свежая",
                        Calories = 27000,
                        Count = 20000,
                        Abb = 2
                    },
                    new Models.Product // 9
                    {
                        Title = "Свекла",
                        Description = "Свежая",
                        Calories = 42000,
                        Count = 15000,
                        Abb = 2
                    },
                    new Models.Product // 10
                    {
                        Title = "Майонез",
                        Description = "Провансаль",
                        Calories = 625000,
                        Count = 5000,
                        Abb = 2
                    },
                    new Models.Product // 11
                    {
                        Title = "Куринные грудки",
                        Description = "Охлажденные",
                        Calories = 113000,
                        Count = 50,
                        Abb = 1
                    });

                await context.Flush();

                // Салат Оливье
                var d1 = new Models.Dish
                {
                    Title = "Салат Оливье",
                    Description = "1)Помыв картофель и морковь, поставить их варить. Отдельно отварить яйца.\n" +
                    "2)Очистить картофель, морковь, яйца и нарезать маленькими кубиками. Нарезать кубиками вареную колбасу и огурцы.\n" +
                    "3)Смешать ингредиенты, добавить горошек, зелень и соль по вкусу. Заправить майонезом."
                };

                d1.Bind(context.Products.First(x => x.Equals("огурец")), 250);
                d1.Bind(context.Products.First(x => x.Equals("картофель")), 260);
                d1.Bind(context.Products.First(x => x.Equals("горошек консервированный")), 140);
                d1.Bind(context.Products.First(x => x.Equals("яйцо")), 4);
                d1.Bind(context.Products.First(x => x.Equals("вареная колбаса")), 200);
                d1.Bind(context.Products.First(x => x.Equals("майонез")), 150);

                context.Add(d1);

                // Суп Борщ
                var d2 = new Models.Dish
                {
                    Title = "Суп Легкий",
                    Description = "1)Вскипятить воду и отварить куринные грудки\n2)Порезать картофель и лук кубиками, добавить в куринный булюен.\n3)Соль перец по вкусу."
                };

                d2.Bind(context.Products.First(x => x.Equals("картофель")), 300);
                d2.Bind(context.Products.First(x => x.Equals("лук репчатый")), 1);
                d2.Bind(context.Products.First(x => x.Equals("куринные грудки")), 1);

                context.Add(d2);
            }
        }


        [TestMethod]
        public void SampleDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseMySQL(connectionString)
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Products.Load();
                context.Dishes.Load();
                var dishes = context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).ToList();
                
                foreach (var dish in dishes)
                {
                    Console.WriteLine($"\n Блюдо: {dish.Title}");
                    
                    foreach(var pd in dish.ProductDish)
                    {
                        Console.WriteLine($"{pd.Product.Title} ({pd.Count}{pd.Product.GetSfx()})");
                    }
                }


            }
        }

        [TestMethod]
        public void DeleteProductsDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseMySQL(connectionString)
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Products.Load();
                context.Dishes.Load();
                //var dishes = context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).ToList();

                var products = context.Products.ToList();
                for(int i=0;i<products.Count;++i)
                {
                    context.Remove(products[i]);
                }

            }
        }
        [TestMethod]
        public void DeleteDishesDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseMySQL(connectionString)
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Products.Load();
                context.Dishes.Load();
                //var dishes = context.Dishes.Include(c => c.ProductDish).ThenInclude(sc => sc.Product).ToList();

                var dishes = context.Dishes.ToList();
                for (int i = 0; i < dishes.Count; ++i)
                {
                    context.Remove(dishes[i]);
                }

            }
        }
    }
}
