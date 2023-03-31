using CarpoolApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CarpoolApi.ServiceHelpers
{
    public class BookingHelper
    {
        private readonly OfferMatchesHolder _offerMatches;
        private readonly DatabaseContext _context;
        public BookingHelper(OfferMatchesHolder offerMatches, DatabaseContext context)
        {
            _offerMatches = offerMatches;
            _context = context;
        }
        public async Task CheckMatches(Order order)
        {
            _offerMatches.OfferMatches.Clear();
            string route = order.From + order.To;

            var offerMatches =  _context.ActiveOffers.AsEnumerable().
                               Where(offer => IsInPath(route.ToLower(), (offer.From + offer.Stops + offer.To).ToLower()) == true
                               && order.Date.Equals(offer.Date) && order.Time.Equals(offer.Time)
                               && CheckAvailability(offer.Accomodation, $"{offer.From},{offer.Stops},{offer.To}", order) == true
                               && offer.Accomodation[GetIndex($"{offer.From},{offer.Stops},{offer.To}", order.From)] - '0' >= order.Seats
                               && offer.UserId != order.UserId
                               ).ToList() ;

            offerMatches.ForEach(offer => offer.Seats = offer.Accomodation[GetIndex($"{offer.From},{offer.Stops},{offer.To}", order.From)] - '0');

            //_offerMatches.OfferMatches.AddRange(offerMatches);

            foreach(var offer in offerMatches) 
            {
                var user =  _context.Users.Where(user=>user.UserId==offer.UserId).FirstOrDefault();
                _offerMatches.OfferMatches.Add(new Response
                {
                    Name=user.DisplayName,
                    Image=user.ImageSrc,
                    From=offer.From,
                    To = offer.To,
                    Date = offer.Date,
                    Time = offer.Time,
                    Fare = offer.Fare,
                    Seats = offer.Seats,
                    OfferId= offer.OfferId,
                    Stops= offer.Stops
                });
            }

            // return NoContent();
        }

        public bool IsInPath(string route, string path)
        {
            path = path.Replace(",", "");
            int l1 = route.Length;
            int l2 = path.Length;
            int s1 = 0, s2 = 0;

            while (s1 < l1 && s2 < l2)
            {
                if (route[s1] == path[s2])
                {
                    s1++;
                }

                s2++;
            }

            return s1 == l1;
        }


        public int GetIndex(string path, string target)
        {
            int idx = path.ToLower().Split(",").ToList().IndexOf(target.ToLower());
            return idx;
        }

        public bool CheckAvailability(string acc, string path, Order order)
        {
            bool flag = true;
            acc = acc.Substring(GetIndex(path, order.From), GetIndex(path, order.To) - GetIndex(path, order.From) + 1);

            for (int i = 0; i < acc.Length; i++)
            {
                int x = Convert.ToInt32(acc[i]) - '0';
                if (x < order.Seats)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        public void UpdateAccomodation(StringBuilder accomodation, string path)
        {
            int start = GetIndex(path, _offerMatches.currOrder.From);
            int end = GetIndex(path, _offerMatches.currOrder.To);
            for (int i = start; i < end; i++)
            {
                accomodation[i] = (char)(accomodation[i] - _offerMatches.currOrder.Seats);
            }
        }
    }
}


