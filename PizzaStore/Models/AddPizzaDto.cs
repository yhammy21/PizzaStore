namespace PizzaStore.Models
{
    public class AddPizzaDto
    {
        public required string PizzaName { get; set; }
        public List<int> ToppingIds { get; set; } = [];
    }
}
