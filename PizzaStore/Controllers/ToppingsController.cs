using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PizzaStore.Data;
using PizzaStore.Models;
using System;

namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaStoreDBContext dbContext;

        public ToppingsController(PizzaStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllToppings()
        {
            var allToppings = dbContext.Toppings.Select(t => new GetToppingsDto
            {
                Id = t.Id,
                ToppingName = t.ToppingName,
            }).ToList();
            if (allToppings.IsNullOrEmpty())
            {
                return NotFound("No topping data exists!");
            }
            return Ok(allToppings);
        }

        [HttpPost]
        public IActionResult AddTopping(AddToppingDto addToppingDto)
        {
            if(String.IsNullOrWhiteSpace(addToppingDto.ToppingName))
            {
                return UnprocessableEntity("Topping names must not be null or empty!");
            }
            var newTopping = new Topping()
            { 
                ToppingName = addToppingDto.ToppingName
            };
            var doesToppingExist = dbContext.Toppings.Any(t => t.ToppingName.ToLower().Replace(" ","") == addToppingDto.ToppingName.ToLower().Replace(" ",""));

            if (doesToppingExist == true)
            {
                return UnprocessableEntity(addToppingDto.ToppingName + " already exists!");
            }
            dbContext.Toppings.Add(newTopping);
            dbContext.SaveChanges();
            
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTopping(int id, UpdateToppingDto updateToppingDto)
        {         
            if (String.IsNullOrWhiteSpace(updateToppingDto.ToppingName))
            {
                return UnprocessableEntity("Topping names must not be null or empty!");
            }
            var updateTopping = dbContext.Toppings.Find(id);

            if (updateTopping != null)
            {

                var doesToppingExist = dbContext.Toppings.Any(t => t.ToppingName.ToLower().Replace(" ", "") == updateToppingDto.ToppingName.ToLower().Replace(" ", ""));

                if (doesToppingExist == true)
                {
                    return UnprocessableEntity(updateTopping.ToppingName + " is already the current name!");
                }

                updateTopping.ToppingName = updateToppingDto.ToppingName;
                dbContext.SaveChanges();
                return Ok(updateTopping);
            }

            return NotFound("Topping not found!");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTopping(int id)
        {
            var deleteTopping = dbContext.Toppings.Find(id);

            if (deleteTopping != null)
            {
                dbContext.Remove(deleteTopping);
                dbContext.SaveChanges();
                return Ok("Topping successfully deleted!");
            }

            return NotFound("Topping not found!");
        }
    }
}
