using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.BasketRepository.Models;
using Store.Repository.Intefraces;
using Store.Repository.Specification.Order;
using Store.Sevrice.Services.BasketService;
using Store.Sevrice.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork,
                            IBasketService basketService,
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _mapper = mapper;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            //Get Basket
            var basket = await _basketService.GetBasketAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");

            //Fill OrderItems from basket Items
            var orderItems = new List<OrderItemDto>();

            foreach (var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);

                if (productItem is null)
                    throw new Exception($"Product With Id : {basketItem.ProductId} Not Exist");

                var itemOrdered = new ProductItemOrder
                {
                    ProductItemId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl,
                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ItemOrdered = itemOrdered
                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);

                orderItems.Add(mappedOrderItem);
            }
            // Get Delivery Method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception($"Delivery Method Not Provided");

            //Calculate Subtotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);

            //To Do => Check If Order Exixst


            //Create Order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.SippingAddress);

            var mappedOrderItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.Id,
                ShippingAddress = mappedShippingAddress,
                BuyerEmail = input.BuyerEmail,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal,
            };

            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
            => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemsSpecification(buyerEmail);

            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);

            var mappedOrders = _mapper.Map<List<OrderResultDto>>(orders);

            return mappedOrders;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id, string buyerEmail)
        {
            var specs = new OrderWithItemsSpecification(id, buyerEmail);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }
    }
}
