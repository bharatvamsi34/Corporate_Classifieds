using Classifieds.App.Models;
using Classifieds.App.Models.ViewModels;
using System.Collections.Generic;

namespace Classifieds.App.Services.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        CategoryDetails GetCategoryById(int id);
        IEnumerable<CategoryDetails> GetCategoryDetails();
        void CreateNewCategory(CategoryDetails item);
        void UpdateCategoryDetails(CategoryDetails item);
        void DeleteCategory(int id);
    }
}
