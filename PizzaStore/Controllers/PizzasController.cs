using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaStore.Data;
using PizzaStore.Models;


namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaStoreDBContext dbContext;

        public PizzasController(PizzaStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllPizzas()
        {
            var allPizzas = dbContext.Pizzas.Select(p => new GetPizzasDto {
                Id = p.Id,
                PizzaName = p.PizzaName,
                Toppings = p.Toppings.Select(t => new GetToppingsDto {
                    Id = t.Id,
                    ToppingName = t.ToppingName,
                })
            }).ToList();           

            if (allPizzas.IsNullOrEmpty())
            {
                return NotFound("No pizza data exists!");
            }
            
            return Ok(allPizzas);
        }

        [HttpPost]
        public IActionResult AddPizza(AddPizzaDto addPizzaDto)
        {
            var existingToppings = dbContext.Toppings.Where(t => addPizzaDto.ToppingIds.Contains(t.Id)).ToList();
            var newPizza = new Pizza()
            {
                PizzaName = addPizzaDto.PizzaName,
                Toppings = existingToppings
            };
            var doesPizzaExist = dbContext.Pizzas.Any(p => p.PizzaName.ToLower().Replace(" ", "") == addPizzaDto.PizzaName.ToLower().Replace(" ", ""));

            if (doesPizzaExist == true)
            {
                return UnprocessableEntity(addPizzaDto.PizzaName + " already exists!");
            }

            foreach (var topping in addPizzaDto.ToppingIds)
            {
                var doesToppingExist = dbContext.Toppings.Find(topping);
                if (doesToppingExist == null)
                {
                    return NotFound("Topping Id:" + topping + " does not exist!");
                }
            }

            dbContext.Pizzas.Add(newPizza);
            dbContext.SaveChanges();


            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdatePizza(int id, UpdatePizzaDto updatePizzaDto)
        {
            var existingToppings = dbContext.Toppings.Where(t => updatePizzaDto.ToppingIds.Contains(t.Id)).ToList();
            var updatePizza = dbContext.Pizzas.Find(id);

            if (updatePizza != null)
            {
                var doesPizzaExist = dbContext.Pizzas.Any(p => p.PizzaName.ToLower().Replace(" ", "") == updatePizzaDto.PizzaName.ToLower().Replace(" ", ""));

                if (doesPizzaExist == true && updatePizza.Id != id)
                {
                    return UnprocessableEntity(updatePizzaDto.PizzaName + " already exists!");
                }
                foreach (var topping in updatePizzaDto.ToppingIds)
                {
                    var doesToppingExist = dbContext.Toppings.Find(topping);
                    if (doesToppingExist == null)
                    {
                        return NotFound("Topping Id:" + topping + " does not exist!");
                    }
                }

                dbContext.Pizzas.Include(p => p.Toppings).Single(p => p.Id == id).Toppings.Clear();
                dbContext.SaveChanges();
                updatePizza.PizzaName = updatePizzaDto.PizzaName;
                updatePizza.Toppings = existingToppings;
                dbContext.SaveChanges();                

                var updatedPizza = dbContext.Pizzas.Where(p => p.Id == id).Select(p => new GetPizzasDto
                {
                    Id = p.Id,
                    PizzaName = p.PizzaName,
                    Toppings = p.Toppings.Select(t => new GetToppingsDto
                    {
                        Id = t.Id,
                        ToppingName = t.ToppingName,
                    })
                }).ToList();;
                return Ok(updatedPizza);
            }

            return NotFound("Pizza not found!");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePizza(int id)
        {
            var deletePizza = dbContext.Pizzas.Find(id);

            if (deletePizza != null)
            {
                dbContext.Remove(deletePizza);
                dbContext.SaveChanges();
                return Ok("Pizza successfully deleted!");
            }

            return NotFound("Pizza not found!");
        }
    }
}
