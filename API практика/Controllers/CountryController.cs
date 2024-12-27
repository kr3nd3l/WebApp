using API.Models;
using API_практика.Models;
using API_практика.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace API_практика.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext context;

        public CountryController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Country> GetCountries()
        {
            return context.countries.OrderByDescending(c => c.Id).ToList();
        }


        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var country = context.countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }


        [HttpPost]
        public IActionResult CreateCountry(CountryDto countryDto)
        {

            var otherCountry = context.countries.FirstOrDefault(c => c.Name == countryDto.Name);
            if (otherCountry != null)
            {
                ModelState.AddModelError("Name", "the Name is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }


            var country = new Country
            {

                Name = countryDto.Name,
                Code = countryDto.Code,
                

            };

            context.countries.Add(country);
            context.SaveChanges();
            return Ok(country);
        }

        [HttpPut("{id}")]
        public IActionResult EditCountry(int id, CountryDto countryDto)
        {


            var otherCountry = context.countries.FirstOrDefault(c => c.Id != id && c.Name == countryDto.Name);
            if (otherCountry != null)
            {
                ModelState.AddModelError("Name", "The Name is alrerdy used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var country = context.countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }


           
            country.Name = countryDto.Name;
            country.Code = countryDto.Code;

            context.SaveChanges();
            return Ok(country);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var country = context.countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            context.countries.Remove(country);
            context.SaveChanges();
            return Ok();
        }


    }
}