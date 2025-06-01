namespace PizzaStore.Models
{
    public class AddPizzaDto
    {
        public required string PizzaName { get; set; }
        public List<string> ToppingIds { get; set; } = [];
    }
}
