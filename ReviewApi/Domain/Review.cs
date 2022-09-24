﻿namespace ReviewApi.Domain
{
    public class Review
    {
        public Guid  Id { get; set; }
        public Guid  HotelId { get; set; }
        public Guid  UserId { get; set; }
        public string HotelName { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Feedback{ get; set; }
        public DateTime Date { get; set; }
    }
}
