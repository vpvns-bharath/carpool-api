using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using CarpoolApi.ServiceHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CarpoolApi.Services
{
    public class OrderService:IOrderService
    {
        private readonly DatabaseContext _context;
        private readonly OfferMatchesHolder _offerMatches;
        private readonly BookingHelper _helper;
        public OrderService(DatabaseContext databaseContext,OfferMatchesHolder offerMatches,BookingHelper helper) 
        {
           _context = databaseContext;
           _offerMatches = offerMatches;
           _helper = helper;

        }
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            var order =await _context.Orders.FindAsync(id);
            return order;
        }

        public ActionResult<IEnumerable<Response>> GetMatchedOffers()
        {
            return  _offerMatches.OfferMatches;
        }

        public async Task PostOrder(Order order)
        {
            _offerMatches.currOrder = order;
            await _helper.CheckMatches(order);
        }

        public async Task BookOrder(int id)
        {
            var offer = _context.ActiveOffers.FirstOrDefault(offer => offer.OfferId == id);
            StringBuilder accomodation = new StringBuilder(offer.Accomodation);
            _helper.UpdateAccomodation(accomodation, $"{ offer.From},{offer.Stops},{offer.To}");
            offer.Accomodation = accomodation.ToString();
            await _context.SaveChangesAsync();
            _offerMatches.currOrder.OfferId = id;
            await _context.Orders.AddAsync(_offerMatches.currOrder);
            await _context.SaveChangesAsync();
        }
    }
}
