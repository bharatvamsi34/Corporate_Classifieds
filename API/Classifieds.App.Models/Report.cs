using System;

namespace Classifieds.App.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AdvertisementId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime Time { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
