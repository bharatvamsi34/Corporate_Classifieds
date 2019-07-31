using System;

namespace Classifieds.App.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AdvertisementId { get; set; }
        public int OfferedPrice { get; set; }
        public DateTime Time { get; set; }
    }
}
