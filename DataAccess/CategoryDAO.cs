using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new eStoreContext())
                {
                    list = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static Category GetCategoryById(int id)
        {
            Category category = new Category();
            try
            {
                using (var context = new eStoreContext())
                {
                    category = context.Categories.FirstOrDefault(m => m.CategoryId == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return category;
        }

        public static void InsertCategory(Category category)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    var _category = context.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
                    context.Categories.Remove(_category);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
