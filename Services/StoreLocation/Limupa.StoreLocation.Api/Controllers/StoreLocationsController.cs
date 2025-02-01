using Bogus;
using Bogus.Bson;
using Limupa.StoreLocation.Api.Context;
using Limupa.StoreLocation.Api.Dtos;
using Limupa.StoreLocation.Api.Entities;
using Limupa.StoreLocation.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Limupa.StoreLocation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreLocationsController : ControllerBase
    {
        private readonly IStoreLocationService storeLocationService;

        public StoreLocationsController(IStoreLocationService storeLocationService)
        {
            this.storeLocationService = storeLocationService;
        }
        [HttpGet]
        public IActionResult StoreLocationList()
        {            
            var values = storeLocationService.GetAllStoreLocation();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStoreLocation(City city)
        {
            await storeLocationService.CreateStoreLocationAsync(city);
            return Ok("succesfull");
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdateStoreLocation(City city)
        //{
        //    await storeLocationService.UpdateStoreLocationAsync(city);
        //    return Ok("Successful");
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStoreLocation(int id)
        //{
        //    await storeLocationService.DeleteStoreLocationAsync(id);
        //    return Ok("Successful");
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreLocationById(int id)
        {
            var values = await storeLocationService.GetByIdStoreLocationAsync(id);
            return Ok(values);
        }
    }
}
