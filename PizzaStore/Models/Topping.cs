namespace PizzaStore.Models
{
    public class Topping
    {
        public int Id { get; set; }        
        public required string ToppingName { get; set; }

        public List<Pizza> Pizzas { get; } = [];
    }
}
