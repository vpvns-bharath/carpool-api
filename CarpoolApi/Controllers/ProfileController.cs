using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarpoolApi.Controllers
{
    [Authorize]
    [Route("CarpoolApi/[controller]")]
    [ApiController]
    public class ProfileController
    {
       
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService) 
        {
            _profileService = profileService;
        }

        [HttpGet("{id}/orders")]

        public async Task<ActionResult<IEnumerable<Response>>> GetUserOrders(int id)
        {
            return await _profileService.GetUserOrders(id);
        }

        [HttpGet("{id}/offers")]

        public async Task<ActionResult<IEnumerable<Response>>> GetUserOffers(int id)
        {
           return await _profileService.GetUserOffers(id);
        }

        [HttpGet("booking/{offerId}/{fare}")]
        public async Task<ActionResult<IEnumerable<Response>>> GetUserOfferBookings(int offerId, int fare)
        {
            return await _profileService.GetUserOfferBookings(offerId, fare);
        }
    }
}
