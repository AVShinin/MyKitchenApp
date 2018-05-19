namespace MyKitchenApp.Models
{
    /// <summary>
    /// Модель связи Продукт > Блюдо и Блюдо < Продукт
    /// </summary>
    public class ProductDish
    {
        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Идентификатор блюда
        /// </summary>
        public int DishId { get; set; }
        /// <summary>
        /// Блюдо
        /// </summary>
        public Dish Dish { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
    }
}
