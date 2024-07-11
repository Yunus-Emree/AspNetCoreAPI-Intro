using AspNetCoreMvc_WithAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AspNetCoreMvc_WithAPI.Controllers
{
	public class CityController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CityController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var http = _httpClientFactory.CreateClient(); //HttpClient nesnesi oluşur.
			var result = await http.GetAsync("https://localhost:7081/api/Cities");
			if (result.IsSuccessStatusCode)
			{
				var jsonData = await result.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<List<CityViewModel>>(jsonData); //Json serialize işlemleri için Newtonsoft.json paketini projemize yüklüyoruz.
				return View(data);
			}
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CityViewModel model)
		{
			var http = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(model);  //view' dan gelen datayı json formata serialize ediyoruz.
			var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
			var result = await http.PostAsync("https://localhost:7081/api/Cities", content);
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var http = _httpClientFactory.CreateClient();
            //gelen id'ye sahip City bilgisi API'den çekilecek.
            var result = await http.GetAsync($"https://localhost:7081/api/Cities/{id}");
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CityViewModel>(jsonData);
                return View(data);
            }
			return View();
        }
		[HttpPost]
		public async Task<IActionResult> Edit(CityViewModel model)
		{
			var http = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, encoding: Encoding.UTF8, "application/json");
            var result = await http.PutAsync("https://localhost:7081/api/Cities", content);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
		public async Task<IActionResult> Delete(int id)
		{
			var http = _httpClientFactory.CreateClient();
            var result = await http.DeleteAsync($"https://localhost:7081/api/Cities/{id}");
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
            }
			return View();

        }
    }
}
