using BaseProject.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseProject.Filters
{
    public class BaseExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<DefaultService> _logger;
        private readonly IHostEnvironment _hostEnvironment;


        public BaseExceptionFilter(ILogger<DefaultService> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        
        public void OnException(ExceptionContext context)
        {
            if (!_hostEnvironment.IsDevelopment())
            {
                // Don't display exception details unless running in Development.
                return;
            }

            //TODO add logging for exception check.
            context.Result = new ContentResult
            {
            
            Content = context.Exception.ToString()
            };

            _logger.Log(LogLevel.Error, context.Exception.ToString());
        }
    }
}
