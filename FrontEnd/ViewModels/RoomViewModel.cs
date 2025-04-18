﻿using FrontEnd.Models;

namespace FrontEnd.ViewModels {
    public class RoomViewModel {
        public Guid Id { get; set; }
        public int RoomNo { get; set; }
        public Guid MovieId { get; set; }
        public string Movie { get; set; }
        public string Date { get; set; }
        public TimeSpan Time { get; set; }
        public Guid ReleasedDateTimeId { get; set; }
        public PositionPlan[] Seats { get; set; }
        public decimal? Price { get; set; }

        public class PositionPlan {
            public string SeatNo { get; set; }
            public bool IsOccupied { get; set; }

        }


    }
}