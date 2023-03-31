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
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILoginService _loginService;

        public LoginController(DatabaseContext context,ILoginService loginService)
        {
            _context = context;
            _loginService = loginService;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
          if (_context.Logins == null)
          {
              return NotFound();
          }
            return await _loginService.GetLogins();
        }

        [HttpPost]
        public async Task<ActionResult> DoLogin(Login login)
        {
            if(_context.Logins==null)
            {
                return Problem("Entity set 'LoginContext.Logins'  is null.");
            }

            var match = await _loginService.DoLogin(login);
            if(match.Item1!=null)
            {
                return Ok(JsonSerializer.Serialize(match.Item1 + match.Item2.ToString()));
            }

            return Content(JsonSerializer.Serialize("User Do not exists"));
        }

    }
}
