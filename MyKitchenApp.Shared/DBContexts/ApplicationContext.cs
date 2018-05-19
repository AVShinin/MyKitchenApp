using Microsoft.EntityFrameworkCore;
using MyKitchenApp.Models;
using System;
using System.Threading.Tasks;

namespace MyKitchenApp.DBContexts
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Блюда
        /// </summary>
        public DbSet<Dish> Dishes { get; set; }
        /// <summary>
        /// Продукты
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ApplicationContext()
        {
            //Создаем базу если её нет
            Database.EnsureCreated();
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="options">Пареметры контекста базы данных</param>
        public ApplicationContext(DbContextOptions options):base(options)
        {
            //Создаем базу если её нет
            Database.EnsureCreated();
        }
        /// <summary>
        /// Асинхронное сохранение изменений в базе данных
        /// </summary>
        /// <returns>True - если операция успешна</returns>
        public async Task<bool> Flush()
        {
            //Определяем, есть ли изменения
            ChangeTracker.DetectChanges();
            //Если нет, то не тратим время и выходим
            if (!ChangeTracker.HasChanges()) return false;

            try
            {
                //Иначе пытаемся сохранить
                await SaveChangesAsync(true);
            }
            catch (Exception)
            {
                //Если любая ошибка, просто выходим с отрицанием
                return false;
            }
            //Иначе все хорошо
            return true;
        }
        /// <summary>
        /// Очищаем и закрываем
        /// </summary>
        public async override void Dispose()
        {
            //Перед закрытием сохраняем изменения, если они есть
            await Flush();
            //Очищаемся
            base.Dispose();
        }
        /// <summary>
        /// Метод построения контекста данных
        /// </summary>
        /// <param name="modelBuilder">Модель билдер</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ProductModel
            modelBuilder.Entity<Product>().ToTable("Products");                                     // Переименовываем тоблицу
            modelBuilder.Entity<Product>().HasIndex(x => x.Id).IsUnique(true);                      // Задаем индекс и уникальность
            modelBuilder.Entity<Product>().HasKey(x => new { x.Id });                               // Задаем ключ
            modelBuilder.Entity<Product>().Property(x => x.Title).HasMaxLength(200).IsRequired();   // Устанавливаем длинну и ключ к обязательному заполнению
            modelBuilder.Entity<Product>().Property(x => x.Count).IsRequired();                     // Устанавливаем ключ к обязательному заполнению
            modelBuilder.Entity<Product>().Property(x => x.Calories).IsRequired();                  // Устанавливаем ключ к обязательному заполнению
            modelBuilder.Entity<Product>().Property(x => x.Abb).IsRequired();                       // Устанавливаем ключ к обязательному заполнению
            #endregion

            #region DishModel
            modelBuilder.Entity<Dish>().ToTable("Dishes");                                      // Переименовываем тоблицу
            modelBuilder.Entity<Dish>().HasIndex(x => x.Id).IsUnique(true);                     // Задаем индекс и уникальность
            modelBuilder.Entity<Dish>().HasKey(x => new { x.Id });                              // Задаем ключ
            modelBuilder.Entity<Dish>().Property(x => x.Title).HasMaxLength(200).IsRequired();  // Устанавливаем длинну и ключ к обязательному заполнению
            #endregion

            #region ProdustDish
            modelBuilder.Entity<ProductDish>().ToTable("Product_Dish");                     // Переименовываем тоблицу
            modelBuilder.Entity<ProductDish>().HasKey(x => new { x.ProductId, x.DishId });  // Задаем ключи
            modelBuilder.Entity<ProductDish>().Property(x => x.Count).IsRequired();         // Устанавливаем ключ к обязательному заполнению

            modelBuilder.Entity<ProductDish>() // Задаем привязку к продукту
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductDish)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductDish>() // Задаем привязку к блюду
                .HasOne(x => x.Dish)
                .WithMany(x => x.ProductDish)
                .HasForeignKey(x => x.DishId);
            #endregion
        }
    }
}
