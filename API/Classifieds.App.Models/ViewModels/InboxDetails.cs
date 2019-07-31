using System;
using System.Collections.Generic;

namespace Classifieds.App.Models.ViewModels
{
    public class InboxDetails
    {
        public int AdvertisementId { get; set; }
        public string ProfilePic { get; set; }
        public int Expiry { get; set; }
        public DateTime PostedOn { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string AdminMessage { get; set; }
        public List<OfferList> OffersList { get; set; }
    }
    public class OfferList
    {
        public int Id { get; set; }
        public String OfferedBy { get; set; }
        public string ProfilePic { get; set; }
        public String OfferedPrice { get; set; }
        public DateTime OfferedOn { get; set; }
    }
}
