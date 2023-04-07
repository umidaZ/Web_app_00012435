using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using WAD_Final_12435.Models;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WAD_Final_12435.Controllers
{
    public class CategoryController : Controller
    {
        // The Definition of Base URL
        public const string baseUrl = "http://localhost:47547/";
        Uri ClientBaseAddress = new Uri(baseUrl);
        HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public CategoryController()
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


        // GET: Category
        public async Task<ActionResult> Index()
        {
            //// Creating the list of new Category list
            List<Category> CategoryInfo = new List<Category>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Category");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                CategoryInfo = JsonConvert.DeserializeObject<List<Category>>(responseMessage);
            }
            return View(CategoryInfo);
        }




        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                string createCategoryInfo = JsonConvert.SerializeObject(category);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createCategoryInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Category/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            //Creating a Get Request to get single Category
            Category categoryDetails = new Category();

            HeaderClearing();

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Category/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;
                categoryDetails = JsonConvert.DeserializeObject<Category>(detailsInfo);
            }
            return View(categoryDetails);
        }


        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //Creating a Get Request to get single Category
            Category category = new Category();

            HeaderClearing();


            HttpResponseMessage httpResponseMessageDetails = await clnt.GetAsync(clnt.BaseAddress + "api/Category/" + id);

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(detailsInfo);
                // Create a SelectList object containing the repeat options
                SelectList repeatOptions = new SelectList(new[] { "Man", "Woman", "Kid" });

                // Pass the SelectList object and the selected value to the view
                ViewData["CategoryTypeEnum"] = new SelectList(repeatOptions.Items, category.CategoryTypeEnum);
            }
            return View(category);
        }


        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                // serializing category object into json format to send
                string editCategoryData = JsonConvert.SerializeObject(category);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(editCategoryData, Encoding.UTF8, "application/json");

                // Making a Put request
                HttpResponseMessage editHttpResponseMessage = await clnt.PutAsync(clnt.BaseAddress + "api/Category/" + id, stringContentInfo);
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //Creating a Get Request to get single Category
            Category categoryDetails = new Category();

            HeaderClearing();


            HttpResponseMessage httpResponseMessageDetails = await clnt.GetAsync(clnt.BaseAddress + "api/Category/" + id);

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                categoryDetails = JsonConvert.DeserializeObject<Category>(detailsInfo);
            }
            return View(categoryDetails);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage httpResponseMessage = await clnt.DeleteAsync(clnt.BaseAddress + "api/Category/" + id);

            //Checking the response is successful or not which is sent using HttpClient  
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
