using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44388/api/Products";
            CategoryApiUrl = "https://localhost:44388/api/Categories";
        }


        // GET: Product
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);

        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // Handle the case where there's no content for the specified ID.
                return View("NoContent"); // You can customize this view for no content scenarios.
            }

            if (!response.IsSuccessStatusCode)
            {
                // Handle other HTTP errors here.
                return View("Error"); // You can customize this error handling.
            }

            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var product = JsonSerializer.Deserialize<Product>(strData, options);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Category> listCategories = JsonSerializer.Deserialize<List<Category>>(strData, options);
                ViewData["CategoryId"] = new SelectList(listCategories, "CategoryId", "Name");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productJson = JsonSerializer.Serialize(product);
                    var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful creation, e.g., redirect to the product details page.
                        return RedirectToAction("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View(product);
                    }

                    // Handle other HTTP errors here.
                    return View("Error"); // You can customize this error handling.
                }
                catch (Exception)
                {
                    // Handle other exceptions that may occur during the API call.
                    return View("Error"); // You can customize this error handling.
                }
            }

            // If ModelState is not valid, return to the create product form.
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // Handle the case where there's no content for the specified ID.
                return View("NoContent"); // You can customize this view for no content scenarios.
            }

            if (!response.IsSuccessStatusCode)
            {
                // Handle other HTTP errors here.
                return View("Error"); // You can customize this error handling.
            }
            //loaded category data
            HttpResponseMessage responseCategory = await client.GetAsync(CategoryApiUrl);
            string strDataCategory = await responseCategory.Content.ReadAsStringAsync();

            var optionsCategory = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> listCategories = JsonSerializer.Deserialize<List<Category>>(strDataCategory, optionsCategory);
            ViewData["CategoryId"] = new SelectList(listCategories, "CategoryId", "Name");

            //loaded product data
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var product = JsonSerializer.Deserialize<Product>(strData, options);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitslnStock")] Product product)
        {
            Product afterUpdate = new Product();
            if (ModelState.IsValid)
            {
                try
                {
                    afterUpdate.ProductId = id;
                    afterUpdate.ProductName = product.ProductName;
                    afterUpdate.UnitPrice = product.UnitPrice;
                    afterUpdate.CategoryId = product.CategoryId;
                    afterUpdate.Weight = product.Weight;
                    afterUpdate.UnitslnStock = product.UnitslnStock;
                    var productJson = JsonSerializer.Serialize(afterUpdate);
                    var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{ProductApiUrl}/id?id={id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful creation, e.g., redirect to the product details page.
                        return RedirectToAction("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View(afterUpdate);
                    }

                    // Handle other HTTP errors here.
                    return View("Error"); // You can customize this error handling.
                }
                catch (Exception)
                {
                    // Handle other exceptions that may occur during the API call.
                    return View("Error"); // You can customize this error handling.
                }
            }

            // If ModelState is not valid, return to the create product form.
            return View(afterUpdate);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // Handle the case where there's no content for the specified ID.
                return View("NoContent"); // You can customize this view for no content scenarios.
            }

            if (!response.IsSuccessStatusCode)
            {
                // Handle other HTTP errors here.
                return View("Error"); // You can customize this error handling.
            }

            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var product = JsonSerializer.Deserialize<Product>(strData, options);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/id?id={id}");
                }
                catch (Exception)
                {
                    // Handle other exceptions that may occur during the API call.
                    return View("Error"); // You can customize this error handling.
                }
            }

            // If ModelState is not valid, return to the create product form.
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FindByName(string? productName)
        {
            if (productName == null)
            {
                return RedirectToAction("Index");
            }
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/keyWord?productName={productName}");

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return View("NoContent");
            }

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);

            return View("Index", listProducts);
        }
    }
}
