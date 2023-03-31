using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarpoolApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        public String From { get; set; }

        public String To { get; set; }

        public String Date { get; set; }
        public String Time { get; set; }

        public int Seats { get; set; }

        public int OfferId { get; set; }

        public int UserId { get; set; }


    }
}
