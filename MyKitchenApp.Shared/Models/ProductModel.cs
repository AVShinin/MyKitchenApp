using System;
using System.Collections.Generic;

namespace MyKitchenApp.Models
{
    /// <summary>
    /// Модель продукта
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Энергетическая ценность(Калории)
        /// </summary>
        public int Calories { get; set; }
        /// <summary>
        /// В чем исчисляется количество
        /// </summary>
        public byte Abb { get; set; } // 1 - шт, 2 - гр
        /// <summary>
        /// В каких блюдах участвует
        /// </summary>
        public List<ProductDish> ProductDish { get; set; }
        /// <summary>
        /// Контруктор
        /// </summary>
        public Product()
        {
            //Объявляем массив
            ProductDish = new List<ProductDish>();
        }
        /// <summary>
        /// Функция - помощник для проверки эквивалентности
        /// </summary>
        /// <param name="key">Ключ(Название) продукта</param>
        /// <returns>True - если название равно ключу</returns>
        public bool Equals(string key)
        {
            //Проверяет игнорируя регистр
            return Title.Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// Связывает продукт с блюдом
        /// </summary>
        /// <param name="dish">Блюдо</param>
        /// <param name="count">Количество в блюде</param>
        public void Bind(Dish dish, int count)
        {
            //Добавляем связь и записываем количество
            ProductDish.Add(new ProductDish { DishId = dish.Id, ProductId = this.Id, Count = count });
        }
        /// <summary>
        /// Возвращает сокращение в чем исчисляется количество
        /// </summary>
        /// <returns>шт,гр,мл и тп.</returns>
        public string GetSfx()
        {
            if (Abb == 1) return "шт";
            if (Abb == 2) return "гр";
            if (Abb == 3) return "мл";
            return string.Empty;
        }
    }
}
