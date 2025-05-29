using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using System.Data;

namespace PizzaStore.Data
{
    public class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options) : base(options)
        {

        }

        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
    }
}
