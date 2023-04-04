using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System;
using RikainGET_POST.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Web.Helpers;

namespace RikainGET_POST.Controllers
{
    public class ConsumeController : Controller
    {
        HttpClient client = new HttpClient();
        [HttpGet]
        public IActionResult Index()
        {
            /* // var movieList = JsonSerializer.Deserialize<List<Movie>>(json)
              List<Person> list = new List<Person>();
            //var list = JsonSerializer.Deserialize<List<Person>>(json);
             client.BaseAddress = new Uri("https://randomuser.me/api/");
             var response = client.GetAsync("api");
             response.Wait();
             var test=response.Result;
             if(test.IsSuccessStatusCode)
             {
                var display=test.Content.ReadAsAsync<List<Person>>();
                 display.Wait();
                 list=display.Result;
             }

             return View(list);*/
            IEnumerable<Person> persons = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://randomuser.me/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Consume");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Person>>();
                    readTask.Wait();

                    persons = readTask.Result;
                }
                else 
                {
                    

                    persons = Enumerable.Empty<Person>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(persons);
        }
    }
      
}

