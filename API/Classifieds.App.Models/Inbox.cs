using System;

namespace Classifieds.App.Models
{
    public class Inbox
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int AdvertisementId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
