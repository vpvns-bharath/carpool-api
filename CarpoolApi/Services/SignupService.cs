using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarpoolApi.Services
{
    public class SignupService : ISignupService
    {
        private readonly DatabaseContext _context;
        public SignupService(DatabaseContext context) 
        {
            _context = context;
        }
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            return await _context.Users.Where(user=>user.UserId == id).FirstAsync();
        }

        public async Task<bool> VerifyExistingUser(string username)
        {
            return await _context.Users.AnyAsync(user => user.Email == username);
        }

        public async Task PostUser(UserDetails details)
        {
            _context.Users.Add(new User
            {
                UserId = details.UserId,
                Email = details.Email,
                DisplayName = details.DisplayName,
                ImageSrc = details.ImageSrc,
                FirstName = details.FirstName,
                LastName = details.LastName,
                Dob= details.Dob,
                Mobile= details.Mobile,
                Gender=details.Gender,
            });
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(details.Password);
            _context.Logins.Add(new Login
            {
                UserId = details.UserId,
                Email = details.Email,
                Password = Convert.ToBase64String(b)
            });

            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateUser(UserDetails details, int id)
        {

            var user = new User
            {
                UserId=id,
                Email = details.Email,
                DisplayName = details.DisplayName,
                ImageSrc = details.ImageSrc,
                FirstName = details.FirstName,
                LastName = details.LastName,
                Dob= details.Dob,
                Mobile= details.Mobile,
                Gender = details.Gender
            };

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return "";
        }

    }
}
