using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Intefraces;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);

            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductsWithSpecifications(input);

            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);

            var countSpecs = new ProductsWithFilterAndCountSpecifications(input);

            var count = await _unitOfWork.Repository<Product, int>().CountSpecificationAsync(countSpecs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex, input.PageSize, count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? id)
        {
            if (id is null)
                throw new Exception("Id Is Null");

            var specs = new ProductsWithSpecifications(id);

            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);

            if (product is null)
                throw new Exception($"Product Not Found");

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;
        }
    }
}
