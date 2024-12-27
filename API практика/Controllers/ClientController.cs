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
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext context;

        public ClientController(AppDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public List<Client> GetClients()
        {
            return context.clients.OrderByDescending(c=> c.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = context.clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }


        [HttpPost]
        public IActionResult CreateClient(ClientDto clientDto)
        {

            var otherClient = context.clients.FirstOrDefault(c=>c.Email == clientDto.Email);
            if (otherClient != null)
            {
                ModelState.AddModelError("Email", "the Email address is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }


            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                
            };

            context.clients.Add(client);
            context.SaveChanges();
            return Ok(client);
        }

        [HttpPut("{id}")]
        public IActionResult EditClient(int id, ClientDto clientDto)
        {


            var otherClient = context.clients.FirstOrDefault(c=>c.Id!=id&&c.Email==clientDto.Email);
            if (otherClient != null)
            {
                ModelState.AddModelError("Email", "The Email address is alrerdy used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = context.clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }


            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.Email = clientDto.Email;
            client.Phone = clientDto.Phone;

            context.SaveChanges();
            return Ok(client);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = context.clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            context.clients.Remove(client);
            context.SaveChanges();
            return Ok();
        }


    }
}
