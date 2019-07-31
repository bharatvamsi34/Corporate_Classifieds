using System;

namespace Classifieds.App.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string SellingType { get; set; }
        public int CategoryId { get; set; }
        public int Expiry { get; set; }
        public DateTime PostedOn { get; set; }
        public string Status { get; set; }
        public bool DisplayPhone { get; set; }
    }
}
