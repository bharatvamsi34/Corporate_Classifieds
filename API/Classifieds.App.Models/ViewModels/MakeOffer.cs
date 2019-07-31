using System;

namespace Classifieds.App.Models.ViewModels
{
    public class MakeOffer
    {
        public int AdvertisementId { get; set; }
        public int EmployeeId { get; set; }
        public int OfferedPrice { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
