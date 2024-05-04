using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Sevrice.Services.BasketService;
using Store.Sevrice.Services.BasketService.Dtos;

namespace Store.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketByID (string id)
            => await _basketService.GetBasketAsync(id);

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok(await _basketService.UpdateBasketAsync(basket));

        [HttpDelete]
        public async Task<ActionResult> DeleteBasketAsync(string id)
            => Ok(await _basketService.DeleteBasketAsync(id));


    }
}
