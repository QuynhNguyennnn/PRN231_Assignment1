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
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private string MemberApiUrl = "";

        public MemberController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:44388/api/Members";
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(MemberApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Member> listMember = JsonSerializer.Deserialize<List<Member>>(strData, options);
            return View(listMember);

        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{MemberApiUrl}/id?id={id}");

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

            var member = JsonSerializer.Deserialize<Member>(strData, options);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Product/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //// POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberJson = JsonSerializer.Serialize(member);
                    var content = new StringContent(memberJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(MemberApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle successful creation, e.g., redirect to the product details page.
                        return RedirectToAction("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View(member);
                    }

                    // Handle other HTTP errors here.
                    return View("Error"); // You can customize this error handling.
                }
                catch (Exception)
                {
                    // Handle other exceptions that may occur during the API call.
                    return View("Err    or"); // You can customize this error handling.
                }
            }

            // If ModelState is not valid, return to the create product form.
            return View(member);
        }

        // GET: Product/Edit/5 edit product view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{MemberApiUrl}/id?id={id}");

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

            //loaded member data
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var member = JsonSerializer.Deserialize<Member>(strData, options);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password,Roles")] Member member)
        {
            Member afterUpdate = new Member();
            if (ModelState.IsValid)
            {
                try
                {
                    afterUpdate.MemberId = id;
                    afterUpdate.Email = member.Email;
                    afterUpdate.CompanyName = member.CompanyName;
                    afterUpdate.City = member.City;
                    afterUpdate.Country = member.Country;
                    afterUpdate.Password = member.Password;
                    afterUpdate.Roles = member.Roles;
                    var memberJson = JsonSerializer.Serialize(afterUpdate);
                    var content = new StringContent(memberJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{MemberApiUrl}/edit/id?id={id}", content);

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
            HttpResponseMessage response = await client.GetAsync($"{MemberApiUrl}/id?id={id}");

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

            var member = JsonSerializer.Deserialize<Member>(strData, options);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        //// POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{MemberApiUrl}/id?id={id}");
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
    }
}

