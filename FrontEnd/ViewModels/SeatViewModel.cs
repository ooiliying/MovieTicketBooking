using FrontEnd.Models;

namespace FrontEnd.ViewModels {
    public class SeatViewModel {
        public Guid Id { get; set; }
        public int RoomNo { get; set; }
        public List<string> Seats { get; set; }
    }
}