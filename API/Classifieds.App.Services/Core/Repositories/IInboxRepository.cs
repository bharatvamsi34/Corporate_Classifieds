using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using System.Collections.Generic;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface IInboxRepository : IRepository<Inbox>
    {
        IEnumerable<InboxDetails> GetByEmployeeId(int id);
        IEnumerable<Inbox> GetChatById(int advertisementId, int employeeId);
        void AddNewReport(Report item);
    }
}
