using System.Collections.Generic;

namespace MyKitchenApp.Models
{
    /// <summary>
    /// Модель блюда
    /// </summary>
    public class Dish
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
        /// Из каких продуктов состоит
        /// </summary>
        public List<ProductDish> ProductDish { get; set; }
        /// <summary>
        /// Контруктор
        /// </summary>
        public Dish()
        {
            //Объявляем массив
            ProductDish = new List<ProductDish>();
        }
        /// <summary>
        /// Связывает продукт и блюдо
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <param name="count">Количество в блюде</param>
        public void Bind(Product product, int count)
        {
            //Добавляем связь и записываем количество
            ProductDish.Add(new ProductDish { DishId = this.Id, ProductId = product.Id, Count= count});
        }
    }
}
