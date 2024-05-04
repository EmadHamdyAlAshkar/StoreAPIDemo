using Store.Data.Entities;
using Store.Sevrice.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateOrderAsync(OrderDto input);
        Task <IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string buyerEmail);
        Task <OrderResultDto> GetOrderByIdAsync(Guid id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
    }
}
