using System;

namespace Classifieds.App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int IconId { get; set; }
        public string Status { get; set; }
    }
}
