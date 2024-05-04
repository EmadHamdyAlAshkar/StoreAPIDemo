﻿using Store.Repository.BasketRepository.Models;
using Store.Sevrice.Services.BasketService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
