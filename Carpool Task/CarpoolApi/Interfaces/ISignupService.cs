using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolApi.Interfaces
{
    public interface ISignupService
    {
        public Task<ActionResult<User>> GetUserById(int id);

        public Task<bool> VerifyExistingUser(string username);
        public Task PostUser(UserDetails details);

        public Task<string> UpdateUser(UserDetails details, int id);
    }
}
