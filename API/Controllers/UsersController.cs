using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace API.Controllers;

     [ApiController]
     [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context )
        {
           _context=context;
        }
          [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
        {
          return  await _context.AppUsers.ToListAsync();
           
        }
        [HttpGet("{id}")]
          public async Task<ActionResult<AppUser>>GetUser( int id)
        {
             return await _context.AppUsers.FindAsync(id);
           
        }
    }
