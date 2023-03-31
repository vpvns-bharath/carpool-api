using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using CarpoolApi.ServiceHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarpoolApi.Services
{
    public class OfferService:IOfferService
    {
        private readonly DatabaseContext _context;
        
        public OfferService(DatabaseContext context) 
        { 
            _context = context;   
        }
        public async Task<ActionResult<IEnumerable<Offer>>> GetTotalOffers()
        {
            
                return await _context.TotalOffers.ToListAsync();
            
        }

        public async Task<ActionResult<IEnumerable<ActiveOffer>>> GetActiveOffers()
        {

            return await _context.ActiveOffers.ToListAsync();

        }

        public async Task<bool> PostOffer(ActiveOffer activeOffer)
        {
            if (activeOffer == null) return false;

            if (activeOffer.Stops == "")
            {
                activeOffer.Accomodation = new string((char)((char)activeOffer.Seats + '0'), 2);

            }
            else
            {
                activeOffer.Accomodation = new string((char)((char)activeOffer.Seats + '0'), activeOffer.Stops.Split(",").Length + 2);
            }
            await _context.ActiveOffers.AddAsync(activeOffer);      
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
