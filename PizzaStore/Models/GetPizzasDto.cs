namespace PizzaStore.Models
{
    public class GetPizzasDto
    {
        public int Id { get; set; }
        public required string PizzaName { get; set; }

        public IEnumerable<GetToppingsDto> Toppings { get; set; } = [];
    }
}
