using System;
using System.Collections.Generic;

namespace Classifieds.App.Models.ViewModels
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int IconId { get; set; }
        public List<Fields> FieldList { get; set; }
    }
}
