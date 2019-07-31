namespace Classifieds.App.Models
{
    public class AdminMessage
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public string Message { get; set; }
    }
}
