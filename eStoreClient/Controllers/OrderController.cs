using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using BusinessObject;

namespace eStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client = null;
        private string OrderApiUrl = "";
        private string MemberApiUrl = "";

        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:44388/api/Orders";
            MemberApiUrl = "https://localhost:44388/api/Members";
        }


        // GET: Order
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(OrderApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            return View(listOrders);

        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/id?id={id}");

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

            var order = JsonSerializer.Deserialize<Order>(strData, options);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                HttpResponseMessage responseMem = await client.GetAsync(MemberApiUrl);
                string strMem = await responseMem.Content.ReadAsStringAsync();

                var optionsMem = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Member> listMembers = JsonSerializer.Deserialize<List<Member>>(strMem, optionsMem);
                ViewData["MemberId"] = new SelectList(listMembers, "MemberId", "Email");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderJson = JsonSerializer.Serialize(order);
                    var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(OrderApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful creation, e.g., redirect to the Order details page.
                        return RedirectToAction("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View(order);
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

            // If ModelState is not valid, return to the create order form.
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/id?id={id}");

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


            HttpResponseMessage responseMem = await client.GetAsync(MemberApiUrl);
            string strMem = await responseMem.Content.ReadAsStringAsync();

            var optionsMem = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Member> listMemers = JsonSerializer.Deserialize<List<Member>>(strMem, optionsMem);
            ViewData["MemberId"] = new SelectList(listMemers, "MemberId", "Email");

            //loaded order data
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var order = JsonSerializer.Deserialize<Order>(strData, options);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] Order order)
        {
            Order afterUpdate = new Order();
            if (ModelState.IsValid)
            {
                try
                {
                    afterUpdate.OrderId = id;
                    afterUpdate.MemberId = order.MemberId;
                    afterUpdate.OrderDate = order.OrderDate;
                    afterUpdate.RequiredDate = order.RequiredDate;
                    afterUpdate.ShippedDate = order.ShippedDate;
                    afterUpdate.Freight = order.Freight;
                    var orderJson = JsonSerializer.Serialize(afterUpdate);
                    var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{OrderApiUrl}/id?id={id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful creation, e.g., redirect to the order details page.
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

            // If ModelState is not valid, return to the create order form.
            return View(afterUpdate);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/id?id={id}");

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

            var order = JsonSerializer.Deserialize<Order>(strData, options);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{OrderApiUrl}/id?id={id}");
                }
                catch (Exception)
                {
                    // Handle other exceptions that may occur during the API call.
                    return View("Error"); // You can customize this error handling.
                }
            }

            // If ModelState is not valid, return to the create order form.
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SortOrder(DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart == DateTime.Parse("01/01/0001") || dateEnd == DateTime.Parse("01/01/0001"))
            {
                return RedirectToAction("Index");

            }
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/sortOrder?dateStart={dateStart}&dateEnd={dateEnd}");

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

            List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);

            return View("Index", listOrders);

        }
    }
}
