using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevrice.Services.CashService
{
    public interface ICashService
    {
        Task SetCashResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string>GeCashResponseAsync(string key);

    }
}
