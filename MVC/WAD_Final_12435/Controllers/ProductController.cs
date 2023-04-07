using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using WAD_Final_12435.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace WAD_Final_12435.Controllers
{
    public class ProductController : Controller
    {
        // The Definition of Base URL
        public const string baseUrl = "http://localhost:47547/";
        Uri ClientBaseAddress = new Uri(baseUrl);
        HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public ProductController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = ClientBaseAddress;
        }

        public void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Product
        public async Task<ActionResult> Index()
        {
            // Creating the list of new Product list
            List<Product> ProductInfo = new List<Product>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Product");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                ProductInfo = JsonConvert.DeserializeObject<List<Product>>(responseMessage);
            }
            return View(ProductInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Make a GET request to the Category API endpoint to get the cat names data
            HttpResponseMessage response = await clnt.GetAsync("api/Category");
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a list of Category objects
                var categories = JsonConvert.DeserializeObject<List<Category>>(content);

                // Create a list of SelectListItem to hold the cat names
                var categoryNames = new List<SelectListItem>();

                // Add each cat name to the list
                foreach (var cat in categories)
                {
                    categoryNames.Add(new SelectListItem { Value = cat.ID.ToString(), Text = cat.Name });
                }

                // Pass the list of cat names to the view using the ViewData dictionary
                ViewData["CategoryNames"] = categoryNames;
/*---------------------------------------------------------warning----------------------------------------------------------------------*/
            }
            else
            {
                ViewData["CategoryNames"] = new List<SelectListItem>();
            }

            return View();
        }


        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
          //  product.Category = new Category { ID = product.Category.ID };
            if (ModelState.IsValid)
            {
                string createProductInfo = JsonConvert.SerializeObject(product);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createProductInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Product/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            //Creating a Get Request to get single Product
            Product productDetails = new Product();

            HeaderClearing();


            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Product/" + id).Result;

            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                productDetails = JsonConvert.DeserializeObject<Product>(detailsInfo);
            }
            return View(productDetails);
        }


        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HeaderClearing();

            HttpResponseMessage responseMessageDetails = await clnt.GetAsync($"api/Product/{id}");
            if (!responseMessageDetails.IsSuccessStatusCode)
            {
                return View(new Product());
            }

            var productDetails = JsonConvert.DeserializeObject<Product>(await responseMessageDetails.Content.ReadAsStringAsync());

            HttpResponseMessage response = await clnt.GetAsync("api/Category");
            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync());

                var CategoryNames = category.Select(h => new SelectListItem { Value = h.ID.ToString(), Text = h.Name }).ToList();

                var selectedCategory = CategoryNames.FirstOrDefault(x => x.Value == productDetails.Category.ID.ToString());
                if (selectedCategory != null)
                {
                    selectedCategory.Selected = true;
                }

                ViewData["CategoryNames"] = CategoryNames;
            }
            else
            {
                ViewData["CategoryNames"] = new List<SelectListItem>();
            }

            return View(productDetails);
        }



        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {

            if (ModelState.IsValid)
            {
                // serializing cat object into json format to send
                string editProductInfo = JsonConvert.SerializeObject(product);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(editProductInfo, Encoding.UTF8, "application/json");

                // Making a Put request
                HttpResponseMessage editHttpResponseMessage = await clnt.PutAsync(clnt.BaseAddress + "api/Product/" + id, stringContentInfo);
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }



        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Product productDetails = new Product();
            HeaderClearing();
            HttpResponseMessage response = await clnt.GetAsync(clnt.BaseAddress + "api/Product/" + id);

            if (response.IsSuccessStatusCode)
            {
                string productDetailsInfo = response.Content.ReadAsStringAsync().Result;

                productDetails = JsonConvert.DeserializeObject<Product>(productDetailsInfo);
            }
            return View(productDetails);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await clnt.DeleteAsync(clnt.BaseAddress + "api/Product/" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
