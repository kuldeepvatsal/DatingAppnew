using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entites;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext  context,ITokenService tokenService)
        {
            _context = context;
            _tokenService= tokenService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>>Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.UserName)) return BadRequest("UserName already taken");
            using var hmac=  new HMACSHA512();
            var user= new  AppUser
            {
                UserName=registerDto.UserName.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key
            };
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return  new UserDTO{Username=user.UserName,
            Token=_tokenService.CreateToken(user)}  ;

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>>Login(LoginDtos  loginDtos )
        {
            var user= await _context.AppUsers.SingleOrDefaultAsync(x=>x.UserName==loginDtos.UserName.ToLower());
            if(user==null) return Unauthorized("Invalid user");
            using var hmac= new  HMACSHA512(user.PasswordSalt);
            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDtos.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return  new UserDTO{Username=user.UserName,
            Token=_tokenService.CreateToken(user)};

            

        }
        private async Task<bool>UserExists( string userName)
        {
            return await _context.AppUsers.AnyAsync(x=>x.UserName==userName.ToLower());
        }

    }
}