using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Sevrice.Services.ProductService.Dtos;

namespace Store.Sevrice.Services.OrderService.Dtos
{
    public class OrderItemUrlResolver : AutoMapper.IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.ItemOrdered.PictureUrl}";

            return null;
        }
    }
}