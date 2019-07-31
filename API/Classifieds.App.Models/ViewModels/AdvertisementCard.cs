using System;

namespace Classifieds.App.Models.ViewModels
{
    public class AdvertisementCard
    {
        public int AdvertisementId { get; set; }
        public string SellingType { get; set; }
        public int CategoryId { get; set; }
        public int IconId { get; set; }
        public int Expiry { get; set; }
        public DateTime PostedOn { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
    }
}
