using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolApi.Interfaces
{
    public interface ILoginService
    {
        public Task<ActionResult<IEnumerable<Login>>> GetLogins();

        public  Task<(string,int)> DoLogin(Login login);
        
    }
}
