using E_commerce.Models;

namespace E_commerce.ViewModel
{
    public class AuctionsViewModel
    {
        public Auction auction { get; set; }
        public AuctionsUser auctionUser { get; set; }
        public User user { get; set; }

    }
}
