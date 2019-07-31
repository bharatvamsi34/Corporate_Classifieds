using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;
using System;
using System.Collections.Generic;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class InboxRepository : Repository<Inbox>, IInboxRepository
    {
        private readonly IDatabase db;

        public InboxRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
        public void AddNewReport(Report item)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<InboxDetails> GetByEmployeeId(int id)
        {
            List<InboxDetails> inboxDetails = new List<InboxDetails>();
            var list = db.Fetch<dynamic>($";EXEC AllAdvertisementsOfEmployee @@id={id};");
            foreach (var listItem in list)
            {
                InboxDetails inbox = new InboxDetails();
                inbox.AdvertisementId = listItem.AdvertisementId;
                inbox.ProfilePic = db.SingleOrDefault<string>("select ProfilePic from Employee Where Id = @0", id);
                inbox.Image = listItem.Image;
                inbox.AdminMessage = db.SingleOrDefault<string>("select Message from AdminMessage Where AdvertisementId = @0", listItem.AdvertisementId);
                inbox.Status = listItem.Status;
                if (listItem.Status == "Removed by Admin")
                    inbox.PostedOn = db.SingleOrDefault<DateTime>("select Time from Report Where AdvertisementId = @0", listItem.AdvertisementId);
                else
                    inbox.PostedOn = listItem.PostedOn;
                inbox.Title = listItem.Title;
                inbox.Expiry = listItem.Expiry;
                List<OfferList> offerList = new List<OfferList>();
                var offers = db.Fetch<dynamic>($";EXEC GetOffers @@id={listItem.AdvertisementId};");
                foreach (var offer in offers)
                {
                    OfferList offerList1 = new OfferList();
                    offerList1.Id = offer.Id;
                    offerList1.OfferedBy = offer.OfferedBy;
                    offerList1.ProfilePic = offer.ProfilePic;
                    offerList1.OfferedPrice = offer.OfferedPrice.ToString();
                    offerList1.OfferedOn = offer.OfferedOn;
                    offerList.Add(offerList1);
                }
                inbox.OffersList = offerList;
                inboxDetails.Add(inbox);
            }
            return inboxDetails;
        }

        public IEnumerable<Inbox> GetChatById(int advertisementId, int employeeId)
        {
            return db.Fetch<Inbox>("Where AdvertisementId = @0 AND (FromId = @1 OR ToId = @1)", advertisementId, employeeId);
        }
    }
}
