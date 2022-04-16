using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Admin.ViewModels
{
    public partial class SaleViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ShowDate { get; set; }
        public TimeSpan ShowTime { get; set; }
        public string Movie { get; set; }
        [Display( Name = "SeatNo" )]
        public string BookedSeatStr { get; set; }
        public int RoomNo { get; set; }
        [Column( TypeName = "money" )]
        public decimal TotalAmount { get; set; }
        [Display( Name = "PaymentDateTime" )]
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}