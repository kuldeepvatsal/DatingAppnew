using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace API.Controllers;

    public class UsersController:BaseApiController
    {
        private readonly DataContext _context;

    public UsersController(DataContext context) => _context = context;
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() => await _context.AppUsers.ToListAsync();
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AppUser>> GetUser(int id) => await _context.AppUsers.FindAsync(id);
}
