using System;
using System.Collections.Generic;

namespace Classifieds.App.Models.ViewModels
{
    public class NewAdvertisement
    {
        public int AdvertisementId { get; set; }
        public int EmployeeId { get; set; }
        public string SellingType { get; set; }
        public int CategoryId { get; set; }
        public int Expiry { get; set; }
        public DateTime PostedOn { get; set; }
        public List<string> Images { get; set; }
        public Dictionary<String, String> Fields { get; set; }
        public bool DisplayPhone { get; set; }
    }
}
