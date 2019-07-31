using System;

namespace Classifieds.App.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AdvertisementId { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
    }
}
