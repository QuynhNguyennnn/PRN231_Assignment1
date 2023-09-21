using eStoreClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using BusinessObject;
using System.Net;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";
        private string AuthApiUrl = "";

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44388/api/Products";
            CategoryApiUrl = "https://localhost:44388/api/Categories";
            AuthApiUrl = "https://localhost:44388/api/Auth";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            Member member = new Member();
            if (ModelState.IsValid)
            {
                try
                {
                    member.Email = email;
                    member.Password = password;
                    var memberJson = JsonSerializer.Serialize(member);
                    var content = new StringContent(memberJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{AuthApiUrl}/login", content);
                    string memberResponse = await response.Content.ReadAsStringAsync();

                    var optionsMember = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Member mem = JsonSerializer.Deserialize<Member>(memberResponse, optionsMember);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MemberId", mem.MemberId);
                        HttpContext.Session.SetString("Email", mem.Email);
                        HttpContext.Session.SetString("Roles", mem.Roles);
                        return View("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View("Index");
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
            return View("Index");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var memberJson = JsonSerializer.Serialize(member);
                    var content = new StringContent(memberJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{AuthApiUrl}/register", content);
                    string memberResponse = await response.Content.ReadAsStringAsync();

                    var optionsMember = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Member mem = JsonSerializer.Deserialize<Member>(memberResponse, optionsMember);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("MemberId", mem.MemberId);
                        HttpContext.Session.SetString("Email", mem.Email);
                        HttpContext.Session.SetString("Roles", mem.Roles);
                        return View("Index");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle validation errors from the API.
                        ModelState.AddModelError(string.Empty, "Invalid input data.");
                        return View("Index");
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
            return View("Index");
        }
    }
}
