using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private IBasketRepository _basketRepository;
        private IIdentityService _identityService;

        public BasketController(IBasketRepository basketRepository, IIdentityService identityService)
        {
            _basketRepository = basketRepository;
            _identityService = identityService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Servis ayakta..");
        }
        [HttpGet]
        [Route("GetBasket")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket;
        }
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserBasket>> UpdateBasketByIdAsync([FromBody] UserBasket userBasket)
        {
            var userName = User.Identity?.Name;
            userBasket.UserName = userName;
            var basket = await _basketRepository.UpdateBasketAsync(userBasket);
            return basket;
        }

        [HttpPost]
        [Route("additem")]
        [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddItemToBasket([FromBody] BasketItem basketItem)
        {
            var userId = _identityService.GetUserName();
            var basket = await _basketRepository.GetBasketAsync(userId);
            if (basket == null)
            {
                basket = new UserBasket(userId);
            }
            basket.BasketItems.Add(basketItem);
            await _basketRepository.UpdateBasketAsync(basket);
            return Ok();
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout)
        {
            var userName = basketCheckout.UserName;
            var basket = await _basketRepository.GetBasketAsync(userName);
            if (basket == null)
            {
                return BadRequest("Sepet boş");
            }
            await DeleteBasketAsync(userName);
            return Accepted();
        }

        [HttpPost]
        [Route("Delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasketAsync(string userName)
        {
            await _basketRepository.DeleteBasketAsync(userName);
            return Ok();

        }
    }
}
