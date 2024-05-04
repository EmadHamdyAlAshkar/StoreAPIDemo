using Store.Data.Entities.OrderEntities;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.OrderService.Dtos
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntendId { get; set; }

    }
}
