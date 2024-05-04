using Store.Sevrice.Services.BasketService.Dtos;
using Store.Sevrice.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.PaymentSevice
{
    public  class PaymentService : IPaymentService
    {
        public Task<CustomerBasketDto> CreateOrUpdatePaymnetIntendForExistingOrder(CustomerBasketDto input)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasketDto> CreateOrUpdatePaymnetIntendForNewOrder(CustomerBasketDto input)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResultDto> UpdateOrderpaymentFailed(string paymentIntendId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResultDto> UpdateOrderpaymentSucceeded(string paymentIntendId, string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
