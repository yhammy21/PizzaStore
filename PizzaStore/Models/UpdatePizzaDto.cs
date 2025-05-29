namespace PizzaStore.Models
{
    public class UpdatePizzaDto
    {
        public required string PizzaName { get; set; }
        public List<int> ToppingIds { get; set; } = [];
    }
}
