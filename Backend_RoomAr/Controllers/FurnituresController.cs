using Backend_RoomAr.ApplicationData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_RoomAr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnituresController : Controller
    {
        public static RoomArDbContext context = new RoomArDbContext();
        private readonly IConfiguration _configuration;

        public FurnituresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Furniture>> Get(int id)
        {
            try
            {
                return context.Furnitures.Where(x => x.CategoryId == id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}