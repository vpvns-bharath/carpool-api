using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarpoolApi.Models;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Dynamic;
using System.Text;
using CarpoolApi.Interfaces;
using CarpoolApi.ServiceHelpers;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace CarpoolApi.Controllers
{
    [Authorize]
    [Route("CarpoolApi/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly OfferMatchesHolder _offerMatches;       
        private readonly IOrderService _ordersService;
        public OrdersController(DatabaseContext context,OfferMatchesHolder offerMatches,IOrderService ordersService)
        {
            _context = context;
            _offerMatches = offerMatches;
            _ordersService = ordersService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return await _ordersService.GetOrders();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _ordersService.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


        [HttpGet("selectRides")]
        public ActionResult<IEnumerable<Response>> GetMatchedOffers()
        {
            return _ordersService.GetMatchedOffers();
        }


        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'OrdersContext.Orders'  is null.");
            }
            await _ordersService.PostOrder(order);
            return Ok();
        }


        [HttpPost("selectRides/{id}")]
        public async Task<ActionResult<Order>> BookOrder(int id)
        {
            if (_offerMatches.OfferMatches.Any(offer => offer.OfferId == id))
            {
                await _ordersService.BookOrder(id);
                return Ok(JsonSerializer.Serialize("Order Booked"));
            }

            return Content(JsonSerializer.Serialize("Invalid OfferId, no offer exists"));
        }

    }
}       
        
