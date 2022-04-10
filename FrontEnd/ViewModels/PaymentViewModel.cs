using FrontEnd.Models;

namespace FrontEnd.ViewModels {
    public class PaymentViewModel {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public int RoomNo { get; set; }
        public Guid MovieId { get; set; }
        public string Movie { get; set; }
        public string MovieImage { get; set; }
        public string Date { get; set; }
        public TimeSpan Time { get; set; }
        public List<string> Seats { get; set; }
        public string BookedSeat { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
    }
}