using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpoolApi.Interfaces
{
    public interface IOrderService
    {
        public Task<ActionResult<IEnumerable<Order>>> GetOrders();

        public Task<Order> GetOrder(int id);

        public ActionResult<IEnumerable<Response>> GetMatchedOffers();

        public Task PostOrder(Order order);

        public Task BookOrder(int id);


    }
}
