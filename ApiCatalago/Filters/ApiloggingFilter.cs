using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalago.Filters
{
    public class ApiloggingFilter : IActionFilter
    {
        private readonly ILogger<ApiloggingFilter> _logger;

        public ApiloggingFilter(ILogger<ApiloggingFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //executa antes da Action
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("~###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModalState: {context.ModelState.IsValid}");
            _logger.LogInformation("~###################################################");


        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            //executa depois da Action
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("~###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModalState: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("~###################################################");

        }

    }
}
