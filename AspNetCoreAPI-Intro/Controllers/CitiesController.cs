using AspNetCoreAPI_Intro.Entities;
using AspNetCoreAPI_Intro.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAPI_Intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _cityRepository.GetAllAsync();
            //validation ve null kontrolü yapılabilir.
            return Ok(list); //listeyle birlikte 200 success (başarılı) kodunu döndürüyor.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if(city == null)
            {
                return NotFound(id);  //200 status code döndürür.
            }
            return Ok(city); 
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            var addedCity = await _cityRepository.CreateAsync(city);
            return Created(string.Empty, addedCity);  //201 status code döndürür.
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] City city)
        {
            var entity = await _cityRepository.GetByIdAsync(city.Id);
            if(entity == null)
            {
                return NotFound(city.Id);
            }
            await _cityRepository.UpdateAsync(city);
            return NoContent();  //204 status code döndürür. (İşlem başarılı, geriye değer dönmüyor.)
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _cityRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(id);
            }
            await _cityRepository.DeleteAsync(entity);
            return NoContent();  //204 status code döndürür. (İşlem başarılı, geriye değer dönmüyor.)
        }
    }
}
