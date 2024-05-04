using Store.Repository.Specification.Product;
using Store.Sevrice.Helper;
using Store.Sevrice.Services.ProductService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.ProductService
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? id);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();

    }
}
