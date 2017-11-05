using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPISample.Models;
using WebAPISample.DataLayer;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebAPISample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult Sample()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult PostSampleData(SampleData sampleData)
        //{
        //    DataLayer1 dl = new DataLayer1();
        //    dl.PostData(sampleData);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult PostSampleData(SampleData sampleData)
        //{
        //    DataLayer1 dl = new DataLayer1();
        //    dl.PostData(sampleData);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<ActionResult> PostSampleData(SampleData product)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string apiUrl = baseUrl + "api/SampleAPI";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(product));
                // HTTP POST
                HttpResponseMessage response = await client.PostAsync("api/SampleAPI/save", content);
                if (response.IsSuccessStatusCode)
                {
                    //string data = await response.Content.ReadAsStringAsync();
                    //product = JsonConvert.DeserializeObject<Product>(data);
                }
            }

            return RedirectToAction("Index");
        }

        //public ActionResult GetAll() {
        //    DataLayer1 dl = new DataLayer1();
        //    var model = dl.GetAll();

        //    return View(model);
        //}

        public async Task<ActionResult> GetAll()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string apiUrl = baseUrl + "api/SampleAPI";

            var table = Enumerable.Empty<SampleData>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    table = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SampleData>>(data);
                }
            }

            return View(table);
        }
    }
}