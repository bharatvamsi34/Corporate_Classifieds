using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using Classifieds.App.Services.Core.Repositories;
using PetaPoco;
using System;
using System.Collections.Generic;

namespace Classifieds.App.Services.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IDatabase db;

        public CategoryRepository(IDatabase db) : base(db)
        {
            this.db = db;
        }
        public CategoryDetails GetCategoryById(int id)
        {
            CategoryDetails categoryDetail = new CategoryDetails();
            Category category = db.SingleOrDefault<Category>("Where Id = @0", id);
            if (category == null)
                return null;
            categoryDetail.CategoryId = category.Id;
            categoryDetail.Name = category.Name;
            categoryDetail.Description = category.Description;
            categoryDetail.CreatedOn = category.CreatedOn;
            categoryDetail.IconId = category.IconId;
            List<Fields> fields = db.Fetch<Fields>("Where CategoryId = @0 ORDER BY Position", id);
            categoryDetail.FieldList = fields;
            return categoryDetail;
        }

        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
            List<Category> categories = db.Fetch<Category>("Where Status = 'Active'");
            List<CategoryDetails> categoryDetails = new List<CategoryDetails>();
            CategoryDetails categoryDetail;
            foreach (var category in categories)
            {
                categoryDetail = new CategoryDetails();
                categoryDetail.CategoryId = category.Id;
                categoryDetail.CreatedOn = category.CreatedOn;
                categoryDetail.Description = category.Description;
                categoryDetail.Name = category.Name;
                categoryDetail.IconId = category.IconId;
                List<Fields> fields = db.Fetch<Fields>("Where CategoryId = @0 ORDER BY Position", category.Id);
                categoryDetail.FieldList = fields;
                categoryDetails.Add(categoryDetail);
            }
            return categoryDetails;
        }
        public void CreateNewCategory(CategoryDetails item)
        {
            try
            {
                db.BeginTransaction();
                Category category = new Category();
                category.Name = item.Name;
                category.Description = item.Description;
                category.IconId = item.IconId;
                category.CreatedOn = item.CreatedOn;
                category.Status = "Active";
                int id = Convert.ToInt32(db.Insert("Category", "Id", true, category));
                Fields fields;
                foreach (var field in item.FieldList)
                {
                    fields = new Fields();
                    fields.CategoryId = id;
                    fields.Name = field.Name;
                    fields.DataType = field.DataType;
                    fields.Mandatory = field.Mandatory;
                    fields.Position = field.Position;
                    db.Insert("Fields", "Id", true, fields);
                }
                db.CompleteTransaction();
            }
            catch (Exception)
            {
                db.AbortTransaction();
            }
        }

        public void UpdateCategoryDetails(CategoryDetails item)
        {
            try
            {
                db.BeginTransaction();
                Category category = new Category();
                Console.WriteLine(item.CategoryId);
                category.Id = item.CategoryId;
                category.Name = item.Name;
                category.Description = item.Description;
                category.IconId = item.IconId;
                category.CreatedOn = item.CreatedOn;
                db.Update("Category", "Id", category);
                Fields fields;
                foreach (var field in item.FieldList)
                {
                    fields = new Fields();
                    fields.Id = field.Id;
                    fields.CategoryId = field.CategoryId;
                    fields.Name = field.Name;
                    fields.DataType = field.DataType;
                    fields.Mandatory = field.Mandatory;
                    fields.Position = field.Position;
                    if (field.Id == 0)
                        db.Insert("Fields", "Id", true, fields);
                    else
                        db.Update("Fields", "Id", fields);
                }
                db.CompleteTransaction();
            }
            catch (Exception)
            {
                db.AbortTransaction();
            }
        }
        public void DeleteCategory(int id)
        {
            db.Update<Advertisement>("SET Status = 'Removed by Admin' Where CategoryId = @0", id);
            db.Update<Category>("SET Status = 'Removed' Where Id = @0", id);
        }
    }
}
