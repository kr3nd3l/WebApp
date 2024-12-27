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
    public class TourController : ControllerBase
    {
        private readonly AppDbContext context;

        public TourController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Tour> GetTours()
        {
            return context.tours.OrderByDescending(c => c.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetTour(int id)
        {
            var tour = context.tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpPost]
        public IActionResult CreateTour(TourDto tourDto)
        {

            var otherTour = context.tours.FirstOrDefault(c => c.CountryId == tourDto.CountryId);
            if (otherTour == null)
            {
                ModelState.AddModelError("Country", "the Country is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }


            var tour = new Tour
            {

                CountryId = tourDto.CountryId,
                Title = tourDto.Title,
                Description = tourDto.Description,
                Price = tourDto.Price,
                Duration = tourDto.Duration,



            };

            context.tours.Add(tour);
            context.SaveChanges();
            return Ok(tour);
        }

        [HttpPut("{id}")]
        public IActionResult EditTour(int id, TourDto tourDto)
        {


            var otherTour = context.tours.FirstOrDefault(c => c.Id != id && c.CountryId == tourDto.CountryId);
            if (otherTour == null)
            {
                ModelState.AddModelError("Country", "The Country is alrerdy used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var tour = context.tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }



            tour.CountryId = tourDto.CountryId;
            tour.Title = tourDto.Title;
            tour.Description = tourDto.Description;
            tour.Price = tourDto.Price;
            tour.Duration = tourDto.Duration;

            context.SaveChanges();
            return Ok(tour);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTour(int id)
        {
            var tour = context.tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }
            context.tours.Remove(tour);
            context.SaveChanges();
            return Ok();
        }


    }
}
