using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarpoolApi.Models;
using CarpoolApi.Interfaces;
using System.Text.Json;

namespace CarpoolApi.Controllers
{
    [Route("CarpoolApi/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        
        private readonly DatabaseContext _context;
        private readonly ISignupService _signupService;

        public SignupController(DatabaseContext context,ISignupService signupService)
        {
            _context = context;
            _signupService = signupService;
        }

        // GET: api/Signup
        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>>GetUserById(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
             return await _signupService.GetUserById(id);
        }

        [HttpGet("{username}")]
        public async Task<bool> VerifyExistingUser(string username)
        {
            return await _signupService.VerifyExistingUser(username);
        }

        
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDetails details)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UsersContext.Users'  is null.");
          }
           await _signupService.PostUser(details);

           return Ok(true);
            
        }

        [HttpPut("{id}")]

        public async Task<string> UpdateUser(UserDetails details,int id)
        {
            if (_context.Users == null)
            {
                return "Entity set 'UsersContext.Users'  is null.";
            }
            await _signupService.UpdateUser(details,id);

            return JsonSerializer.Serialize("User Details Updated");

        }

    }
}
