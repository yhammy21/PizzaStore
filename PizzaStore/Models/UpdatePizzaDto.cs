namespace PizzaStore.Models
{
    public class UpdatePizzaDto
    {
        public required string PizzaName { get; set; }
        public List<string> ToppingIds { get; set; } = [];
    }
}
