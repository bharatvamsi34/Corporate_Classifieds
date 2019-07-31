using System;
using System.Collections.Generic;

namespace Classifieds.App.Models.ViewModels
{
    public class ViewAdvertisementDetails
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public bool DisplayPhone { get; set; }
        public Dictionary<string, string> FieldsList { get; set; }
        public List<int> OfferedByIds { get; set; }
        public List<int> ReportedByIds { get; set; }
        public List<CommentsBy> CommentList { get; set; }
        public List<string> ImageList { get; set; }
        public int ViewCount { get; set; }
    }
    public class CommentsBy
    {
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
    }
}
