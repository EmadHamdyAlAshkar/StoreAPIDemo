using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Store.Sevrice.Services.CashService;
using System.Text;

namespace Store.API.Helper
{
    public class CasheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CasheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _casheService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var casheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var casheResponse = await _casheService.GeCashResponseAsync(casheKey);

            if (!string.IsNullOrEmpty(casheResponse))
            {
                var cotentResult = new ContentResult
                {
                    Content = casheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = cotentResult;

                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response)
                await _casheService.SetCashResponseAsync(casheKey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            StringBuilder casheKey = new StringBuilder();

            casheKey.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(x=> x.Key))
                casheKey.Append($"|{key}-{value}");

            return casheKey.ToString();
        }
    }
}
