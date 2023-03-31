using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarpoolApi.Services
{
    public class ProfileService:IProfileService
    {
        private readonly DatabaseContext _databaseContext;
        public ProfileService(DatabaseContext databaseContext) 
        {
            _databaseContext= databaseContext;
        }
        public async Task<ActionResult<IEnumerable<Response>>> GetUserOrders(int id)
        {
            List<Response> profileOrders = new List<Response>();
            var userOrders = await _databaseContext.Orders.Where(order=> order.UserId == id).ToListAsync();
            foreach (var order in userOrders) 
            {
                var existingOffer = await _databaseContext.ActiveOffers.Where(offer => offer.OfferId == order.OfferId).FirstOrDefaultAsync();
                var pastOffer = await _databaseContext.TotalOffers.Where(offer => offer.ActualOfferId == order.OfferId).FirstOrDefaultAsync();
                User createdUser;
                int price=0;
                if(existingOffer != null) 
                {
                    createdUser = await _databaseContext.Users.Where(user=> user.UserId==existingOffer.UserId).FirstOrDefaultAsync();
                    price = existingOffer.Fare;
                }
                else
                {
                    createdUser = await _databaseContext.Users.Where(user => user.UserId == pastOffer.UserId).FirstOrDefaultAsync();
                    price = pastOffer.Fare;
                }

                 var profileOrder = new Response
                {
                    Name = createdUser.DisplayName,
                    Image = createdUser.ImageSrc,
                    From = order.From,
                    To = order.To,
                    Date = order.Date,
                    Time = order.Time,
                    Fare = price,
                    Seats = order.Seats
                };
                profileOrders.Add(profileOrder);
            }

            return profileOrders;
        }

        public async Task<ActionResult<IEnumerable<Response>>> GetUserOffers(int id)
        {
            var userOffers = await _databaseContext.TotalOffers.Where(offer => offer.UserId == id).ToListAsync();
            var userActiveOffers = await _databaseContext.ActiveOffers.Where(offer => offer.UserId == id).ToListAsync();

            List<Response> profileOffers = new List<Response>();
            foreach(var offer in userOffers)
            {
                profileOffers.Add(new Response
                {
                    Name = "Click To See Passengers",
                    Image = "../../../assets/images/user-profile-icon-free-vector.jpg",
                    From = offer.From,
                    To = offer.To,
                    Date = offer.Date,
                    Time = offer.Time,
                    Fare = offer.Fare,
                    Seats = offer.Seats,
                    OfferId=offer.ActualOfferId,
                });
            }
            foreach (var offer in userActiveOffers)
            {
                profileOffers.Add(new Response
                {
                    Name = "Click To See Passengers",
                    Image = "../../../assets/images/user-profile-icon-free-vector.jpg",
                    From = offer.From,
                    To = offer.To,
                    Date = offer.Date,
                    Time = offer.Time,
                    Fare = offer.Fare,
                    Seats = offer.Seats,
                    OfferId=offer.OfferId
                });
            }
            return profileOffers;
        }

        public async Task<ActionResult<IEnumerable<Response>>> GetUserOfferBookings(int offerId,int fare)
        {
            var userOfferBookings = new List<Response>();
            var orderedBy = await _databaseContext.Orders.Where(order=>order.OfferId== offerId).ToListAsync();
            foreach (var order in orderedBy) 
            {
                if(order!=null)
                {
                    var user = await _databaseContext.Users.Where(user=>user.UserId==order.UserId).FirstOrDefaultAsync();
                    var booking = new Response
                    {
                        Name = user.DisplayName,
                        Image = user.ImageSrc,
                        From = order.From,
                        To = order.To,
                        Date = order.Date,
                        Time = order.Time,
                        Fare = fare,
                        Seats = order.Seats
                    };
                    userOfferBookings.Add(booking);
                }
            }
            return userOfferBookings;
        }
    }
}
