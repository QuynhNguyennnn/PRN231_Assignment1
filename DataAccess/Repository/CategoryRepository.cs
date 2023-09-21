using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void DeleteCategory(Category c) => CategoryDAO.DeleteCategory(c);

        public Category GetCategoryById(int id) => CategoryDAO.GetCategoryById(id);

        public List<Category> GetCategories() => CategoryDAO.GetCategories();

        public void InsertCategory(Category c) => CategoryDAO.InsertCategory(c);

        public void UpdateCategory(Category c) => CategoryDAO.UpdateCategory(c);
    }
}
