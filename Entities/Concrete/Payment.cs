﻿using Core.Entities;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}