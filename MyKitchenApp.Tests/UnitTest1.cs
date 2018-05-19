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
                        Title = "������� �������",
                        Description = "��������",
                        Calories = 255000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 2
                    {
                        Title = "������",
                        Description = "�������, ������",
                        Calories = 15000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 3
                    {
                        Title = "���������",
                        Description = "������",
                        Calories = 80000,
                        Count = 50000,
                        Abb = 2
                    },
                    new Models.Product // 4
                    {
                        Title = "������� ����������������",
                        Description = "����������������",
                        Calories = 55000,
                        Count = 5000,
                        Abb = 2
                    },
                    new Models.Product // 5
                    {
                        Title = "����",
                        Description = "�����, ������",
                        Calories = 157000,
                        Count = 500,
                        Abb = 1
                    },
                    new Models.Product // 6
                    {
                        Title = "��� ��������",
                        Description = "������",
                        Calories = 45000,
                        Count = 10000,
                        Abb = 2
                    },
                    new Models.Product // 7
                    {
                        Title = "�������",
                        Description = "������",
                        Calories = 32000,
                        Count = 15000,
                        Abb = 2
                    },
                    new Models.Product // 8
                    {
                        Title = "�������",
                        Description = "������",
                        Calories = 27000,
                        Count = 20000,
                        Abb = 2
                    },
                    new Models.Product // 9
                    {
                        Title = "������",
                        Description = "������",
                        Calories = 42000,
                        Count = 15000,
                        Abb = 2
                    },
                    new Models.Product // 10
                    {
                        Title = "�������",
                        Description = "����������",
                        Calories = 625000,
                        Count = 5000,
                        Abb = 2
                    },
                    new Models.Product // 11
                    {
                        Title = "�������� ������",
                        Description = "�����������",
                        Calories = 113000,
                        Count = 50,
                        Abb = 1
                    });

                await context.Flush();

                // ����� ������
                var d1 = new Models.Dish
                {
                    Title = "����� ������",
                    Description = "1)����� ��������� � �������, ��������� �� ������. �������� �������� ����.\n" +
                    "2)�������� ���������, �������, ���� � �������� ���������� ��������. �������� �������� ������� ������� � ������.\n" +
                    "3)������� �����������, �������� �������, ������ � ���� �� �����. ��������� ���������."
                };

                d1.Bind(context.Products.First(x => x.Equals("������")), 250);
                d1.Bind(context.Products.First(x => x.Equals("���������")), 260);
                d1.Bind(context.Products.First(x => x.Equals("������� ����������������")), 140);
                d1.Bind(context.Products.First(x => x.Equals("����")), 4);
                d1.Bind(context.Products.First(x => x.Equals("������� �������")), 200);
                d1.Bind(context.Products.First(x => x.Equals("�������")), 150);

                context.Add(d1);

                // ��� ����
                var d2 = new Models.Dish
                {
                    Title = "��� ������",
                    Description = "1)���������� ���� � �������� �������� ������\n2)�������� ��������� � ��� ��������, �������� � �������� ������.\n3)���� ����� �� �����."
                };

                d2.Bind(context.Products.First(x => x.Equals("���������")), 300);
                d2.Bind(context.Products.First(x => x.Equals("��� ��������")), 1);
                d2.Bind(context.Products.First(x => x.Equals("�������� ������")), 1);

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
                    Console.WriteLine($"\n �����: {dish.Title}");
                    
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
