using Microsoft.AspNetCore.Mvc;
using ProjektMitAPI.Models;
using System.Security.Claims;

namespace ProjektMitAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly MovieAppContext _context; 
        public MovieController(MovieAppContext context)
        {
            _context = context;
        }
    




        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(_context.Movies);
        }
    }
}
