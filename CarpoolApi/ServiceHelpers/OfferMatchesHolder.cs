using CarpoolApi.Models;

namespace CarpoolApi.ServiceHelpers
{
    //used to store data temporarily to help booking an order
    public class OfferMatchesHolder
    {
        public List<Response> OfferMatches = new List<Response>();
        public Order currOrder = new Order { };
    }
}
