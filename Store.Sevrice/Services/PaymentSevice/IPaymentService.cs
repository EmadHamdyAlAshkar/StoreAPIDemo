using Store.Sevrice.Services.BasketService.Dtos;
using Store.Sevrice.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.PaymentSevice
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymnetIntendForExistingOrder(CustomerBasketDto input);
        Task<CustomerBasketDto> CreateOrUpdatePaymnetIntendForNewOrder(CustomerBasketDto input);
        Task<OrderResultDto> UpdateOrderpaymentSucceeded(string paymentIntendId, string basketId);
        Task<OrderResultDto> UpdateOrderpaymentFailed(string paymentIntendId);
    }
}
