using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        void InsertCategory(Category c);
        Category GetCategoryById(int id);
        void DeleteCategory(Category c);
        void UpdateCategory(Category c);
        List<Category> GetCategories();
    }
}
