namespace PizzaStore.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public required string PizzaName { get; set; }

        public List<Topping> Toppings { get; set; } = [];
    }
}
