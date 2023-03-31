using CarpoolApi.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpoolApi.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        public String From { get; set; }
        public int ActualOfferId { get; set; }
        public String To { get; set; }

        public String Date { get; set; }

        public String Time { get; set; }

        public String Stops { get; set; }
        public int Seats { get; set; }

        public int Fare { get; set; }

        public int UserId { get; set; }

        public String Accomodation { get; set; }
        
    }
}
