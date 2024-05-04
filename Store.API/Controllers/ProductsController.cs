using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Helper;
using Store.Repository.Specification.Product;
using Store.Sevrice.HandleResponses;
using Store.Sevrice.Helper;
using Store.Sevrice.Services.ProductService;
using Store.Sevrice.Services.ProductService.Dtos;

namespace Store.API.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Cashe(90)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());

        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<ProductDetailsDto>>> GetAllproducts([FromQuery] ProductSpecification input)
            => Ok(await _productService.GetAllProductsAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int? id)
        {
            if (id is null)
                return BadRequest(new Response(400, "Id is Null"));

            var product = await _productService.GetProductByIdAsync(id);

            if(product is null)
                return NotFound(new Response(404));

            return Ok(product);
        }
            
    }
}
