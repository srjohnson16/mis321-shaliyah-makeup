using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Database;
using api;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // GET: api/Cars
        [HttpGet]
        public List<Car> Get()
        {
            CarUtility utility = new CarUtility();
            return utility.GetAllCars();
        }

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public Car Get(int id)
        {
            CarUtility utility = new CarUtility();
            List<Car> myCars = utility.GetAllCars();
            foreach (Car car in myCars)
            {
                if (car.id == id)
                {
                    return car;
                }
            }
            return new Car();
        }

        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody] Car myCar)
        {
            
            CarUtility utility = new CarUtility();
            utility.AddCars(myCar);
            System.Console.WriteLine(myCar);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] bool isHold)
        {
            CarUtility utility = new CarUtility();
            utility.HoldCar(id, isHold);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CarUtility utility = new CarUtility();
            utility.DeleteCar(id);
        }
    }
}
