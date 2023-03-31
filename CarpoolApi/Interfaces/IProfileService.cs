using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolApi.Interfaces
{
    public interface IProfileService
    {
        public Task<ActionResult<IEnumerable<Response>>> GetUserOrders(int id);

        public Task<ActionResult<IEnumerable<Response>>> GetUserOffers(int id);
        public Task<ActionResult<IEnumerable<Response>>> GetUserOfferBookings(int offerId, int fare);
    }
}
