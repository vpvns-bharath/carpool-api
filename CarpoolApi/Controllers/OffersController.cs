using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarpoolApi.Models;
using CarpoolApi.Services;
using System.Text;
using CarpoolApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace CarpoolApi.Controllers
{
    [Authorize]
    [Route("CarpoolApi/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IOfferService _offersServices;
        public OffersController(DatabaseContext context,IOfferService offersServices)
        {
            
            _context= context;
            _offersServices = offersServices;
        }


        // GET: api/Offers
        [HttpGet("total")]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
        {
            if (_context.TotalOffers == null)
            {
                return NotFound();
            }


            return await _offersServices.GetTotalOffers();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ActiveOffer>>> GetActiveOffers()
        {
            if (_context.ActiveOffers == null)
            {
                return NotFound();
            }


            return await _offersServices.GetActiveOffers();
        }


        [HttpPost]
        public async Task<ActionResult<Offer>> PostOffer(ActiveOffer activeOffer)
        {
          if (_context.TotalOffers == null)
          {
              return Problem("Entity set 'OffersContext.TotalOffers'  is null.");
          }

          bool isCreated = await _offersServices.PostOffer(activeOffer);
            
          if (isCreated) 
                return Ok(JsonSerializer.Serialize("Offer created"));
            
          return Content(JsonSerializer.Serialize("Not Created"));
        }
        
    }
}
