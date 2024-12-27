using API_2.Models;
using API_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ToursController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public List<Tour> GetTours()
        {
            return context.Tours.OrderByDescending(c => c.Id).ToList();
        }


        [HttpGet("{id}")]
        public IActionResult GetTour(int id)
        {
            var tour = context.Tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }


        [HttpPost]
        public IActionResult CreateTour(TourDto tourDto)
        {
            var otherTour = context.Tours.FirstOrDefault(c => c.CountryId == tourDto.CountryId);
            if (otherTour != null)
            {
                ModelState.AddModelError("Country", "The Country is already used");
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

            context.Tours.Add(tour);
            context.SaveChanges();
            return Ok(tour);
        }


        [HttpPut("{id}")]
        public IActionResult EditTour(int id, TourDto tourDto)
        {

            var otherTour = context.Tours.FirstOrDefault(c => c.Id != id && c.CountryId == tourDto.CountryId);
            if (otherTour != null)
            {
                ModelState.AddModelError("Country", "The Country is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }


            var tour = context.Tours.Find(id);
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
            var tour = context.Tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }
            context.Tours.Remove(tour);
            context.SaveChanges();

            return Ok();
        }

    }
}
