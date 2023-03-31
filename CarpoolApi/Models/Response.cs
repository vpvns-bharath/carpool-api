﻿namespace CarpoolApi.Models
{
    public class Response
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public int Fare { get; set; }
        public int Seats { get; set;}
        public int OfferId { get; set; }
        public String Stops { get; set; }
    }
}
