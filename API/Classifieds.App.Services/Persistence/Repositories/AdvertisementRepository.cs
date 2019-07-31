using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.AzureStorageServices;
using Classifieds.App.Services.Core.Repositories;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using System;
using System.Collections.Generic;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private readonly IDatabase db;
        private readonly IConfiguration _configuration;

        public AdvertisementRepository(IDatabase db, IConfiguration configuration) : base(db)
        {
            this.db = db;
            _configuration = configuration;
        }
        public IEnumerable<AdvertisementCard> GetData()
        {
            db.Update<Advertisement>("SET Status = 'Expired' WHERE (Expiry < CURRENT_TIMESTAMP - PostedOn) and Status = 'Active';");
            return db.Fetch<AdvertisementCard>("Select * from AdvertisementCardVW ORDER BY PostedOn DESC;");
        }
        public IEnumerable<AdvertisementCard> GetDataByEmployeeId(int id)
        {
            return db.Fetch<AdvertisementCard>($";EXEC GetAdvertisementByEmployeeId @@id={id};");
        }
        public ViewAdvertisementDetails GetDetails(int id, int empId)
        {
            ViewAdvertisementDetails adDetails = new ViewAdvertisementDetails();
            var details = db.SingleOrDefault<dynamic>($";EXEC GetAdvertisementDetails @@id={id}, @@empId={empId};");
            adDetails.EmployeeId = details.EmployeeId;
            adDetails.EmployeeName = details.EmployeeName;
            adDetails.ProfilePic = details.ProfilePic;
            adDetails.Email = details.Email;
            adDetails.Location = details.Location;
            adDetails.Phone = details.Phone;
            adDetails.DisplayPhone = details.DisplayPhone;
            adDetails.OfferedByIds = db.Fetch<int>($"Select  EmployeeId from Offer where AdvertisementId = @0", id);
            adDetails.ReportedByIds = db.Fetch<int>($"Select  EmployeeId from Report where AdvertisementId = @0", id);
            List<AdvertisementDetails> advertisementDetails = db.Fetch<AdvertisementDetails>("Where AdvertisementId = @0 ", id);
            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (var item in advertisementDetails)
            {
                fields.Add(item.FieldName, item.Value);
            }
            adDetails.FieldsList = fields;
            List<string> imageList = db.Fetch<string>("Select Image From Images Where AdvertisementId = @0", id);
            adDetails.ImageList = imageList;
            List<Comments> commentList = db.Fetch<Comments>("Where AdvertisementId = @0", id);
            List<CommentsBy> comments = new List<CommentsBy>();
            CommentsBy commentsBy;
            foreach (var comment in commentList)
            {
                commentsBy = new CommentsBy();
                string employeeName = db.SingleOrDefault<string>("Select Name From Employee Where Id = @0", comment.EmployeeId);
                commentsBy.EmployeeName = employeeName;
                commentsBy.Comment = comment.Comment;
                commentsBy.Time = comment.Time;
                comments.Add(commentsBy);
            }
            adDetails.CommentList = comments;
            int viewCount = db.SingleOrDefault<int>($"Select Count(*) From Viewers Where AdvertisementId = {id}");
            adDetails.ViewCount = viewCount;
            return adDetails;
        }

        public void CreateNewAdvertisement(NewAdvertisement item)
        {
            db.BeginTransaction();
            Console.WriteLine(item.Images);
            Advertisement ad = new Advertisement();
            ad.SellingType = item.SellingType;
            ad.EmployeeId = item.EmployeeId;
            ad.CategoryId = item.CategoryId;
            ad.Expiry = item.Expiry;
            ad.PostedOn = item.PostedOn;
            ad.Status = "Active";
            ad.DisplayPhone = item.DisplayPhone;
            int id = Convert.ToInt32(db.Insert("Advertisement", "Id", true, ad));
            Images img;
            if (item.Images.Count > 0)
                foreach (var image in item.Images)
                {
                    img = new Images();
                    img.AdvertisementId = id;
                    ImageBlob imageBlob = new ImageBlob(_configuration);
                    img.Image = imageBlob.ImageUploading(image).Result;
                    db.Insert("Images", "Id", true, img);
                }
            else
            {
                img = new Images();
                img.AdvertisementId = id;
                db.Insert("Images", "Id", true, img);
            }
            AdvertisementDetails adDetails;
            foreach (var field in item.Fields)
            {
                adDetails = new AdvertisementDetails();
                adDetails.AdvertisementId = id;
                adDetails.FieldName = field.Key;
                adDetails.Value = field.Value;
                db.Insert("AdvertisementDetails", "Id", true, adDetails);
            }
            db.CompleteTransaction();
        }

        public void UpdateAdvertisement(NewAdvertisement item)
        {
            db.BeginTransaction();
            int id = item.AdvertisementId;
            Advertisement ad = new Advertisement();
            ad.Id = id;
            ad.SellingType = item.SellingType;
            ad.EmployeeId = item.EmployeeId;
            ad.CategoryId = item.CategoryId;
            ad.Expiry = item.Expiry;
            ad.PostedOn = item.PostedOn;
            ad.Status = "Active";
            ad.DisplayPhone = item.DisplayPhone;
            var update = db.Update("Advertisement", "Id", ad);
            if(update > 0)
            {
                db.Delete<Report>("WHERE AdvertisementId = @0", id);
                db.Delete<AdminMessage>("WHERE AdvertisementId = @0", id);
                Images img;
                db.Delete<Images>("WHERE AdvertisementId = @0", id);
                if (item.Images.Count > 0)
                    foreach (var image in item.Images)
                    {
                        img = new Images();
                        img.AdvertisementId = id;
                        ImageBlob imageBlob = new ImageBlob(_configuration);
                        img.Image = imageBlob.ImageUploading(image).Result;
                        db.Insert("Images", "Id", true, img);
                    }
                else
                {
                    img = new Images();
                    img.AdvertisementId = id;
                    db.Insert("Images", "Id", true, img);
                }
                foreach (var field in item.Fields)
                {
                    db.Update<AdvertisementDetails>($"SET Value = '{field.Value}' WHERE AdvertisementId = {id} and FieldName Like '%{field.Key}'");
                }
            }
            db.CompleteTransaction();
        }
        public void DeleteAdvertisement(int id)
        {
            db.Update<Report>("SET IsDeleted = 1 Where AdvertisementId = @0", id);
            db.Update<Advertisement>("SET Status = 'Removed by You', PostedOn = current_timestamp Where Id = @0", id);
        }

        public void RepostAdvertisement(int id)
        {
            db.Update<Advertisement>("SET PostedOn = current_timestamp, Status = 'Active' Where Id = @0", id);
        }

        public void MakeAnOffer(MakeOffer item)
        {
            db.Execute($";Exec MakeOffer @@empId = {item.EmployeeId}, @@adId = {item.AdvertisementId}, @@price = {item.OfferedPrice}, @@message = '{item.Message}', @@time = '{item.Time}';");
            //Offer offer = new Offer();
            //offer.EmployeeId = item.EmployeeId;
            //offer.AdvertisementId = item.AdvertisementId;
            //offer.OfferedPrice = item.OfferedPrice;
            //offer.Time = item.Time;
            //db.Insert("Offer", "Id", true, offer);
            //Inbox inbox = new Inbox();
            //inbox.AdvertisementId = item.AdvertisementId;
            //inbox.FromId = item.EmployeeId;
            //inbox.Message = item.Message;
            //inbox.Time = item.Time;
            //inbox.ToId = db.SingleOrDefault<int>("Select EmployeeId from Advertisement Where Id = @0", item.AdvertisementId);
            //db.Insert("Inbox", "Id", true, inbox);

        }
    }
}
