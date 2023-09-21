using Microsoft.AspNetCore.Mvc;
using System;

namespace eStoreAPI.DTOs
{
    public class AddOrderDtos 
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
    }
}
