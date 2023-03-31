using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolApi.Interfaces
{
    public interface IOfferService
    {
        public Task<ActionResult<IEnumerable<Offer>>> GetTotalOffers();

        public Task<ActionResult<IEnumerable<ActiveOffer>>> GetActiveOffers();
        
        public Task<bool> PostOffer(ActiveOffer activeOffer);
    }
}
