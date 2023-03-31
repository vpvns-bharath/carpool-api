using CarpoolApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CarpoolApi.ServiceHelpers
{
    public class UpdateActiveOffers
    {
        private readonly DatabaseContext _context;

        public UpdateActiveOffers(IServiceProvider serviceProvider)
        {
            //we use different context because timer method overlaps context with other controllers and hence context closes somewhere
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
            var timer = new Timer(ModifyActiveOffers,null,0,5000);
        }

        public async void ModifyActiveOffers(object o)
        {
            //var matches = _context.ActiveOffers.AsEnumerable().Where(offer => DateTime.ParseExact(offer.Date, "dd/MM/yyyy", null) < DateTime.Now.Date).ToList();
            var activeOffers = _context.ActiveOffers.AsEnumerable();
            var matches =  activeOffers.Where(offer => DateTime.ParseExact(offer.Date, "dd/MM/yyyy", null) < DateTime.Now.Date).ToList();
            foreach (var match in matches)
            {

                _context.ActiveOffers.Remove(match);
                await _context.SaveChangesAsync();
                var compOffer = new Offer { Accomodation = match.Accomodation, Date = match.Date, Fare = match.Fare, From = match.From, Seats = match.Seats, Stops = match.Stops, Time = match.Time, To = match.To, UserId = match.UserId ,ActualOfferId=match.OfferId};

                await _context.TotalOffers.AddAsync(compOffer);
                await _context.SaveChangesAsync();

            }

        }
    }
}
