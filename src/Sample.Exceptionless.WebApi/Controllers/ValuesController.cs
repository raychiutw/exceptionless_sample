using System;
using System.Collections.Generic;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Sample.Exceptionless.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController
    {
        private readonly ExceptionlessClient _exceptionlessClient;
        private readonly ILogger _logger;

        public ValuesController(ExceptionlessClient exceptionlessClient, ILogger<ValuesController> logger)
        {
            // ExceptionlessClient instance from DI that was registered with the AddExceptionless
            // call in Startup.ConfigureServices
            this._exceptionlessClient = exceptionlessClient;
            this._logger = logger;
        }

        // GET values
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            // Submit a feature usage event directly using the client instance that is injected from
            // the DI container.
            this._exceptionlessClient.SubmitFeatureUsage("ValuesController_Get");

            // This log message will get sent to Exceptionless since Exceptionless has be added to
            // the logging system in Program.cs.
            this._logger.LogWarning("Test warning message");

            this._logger.LogInformation("log info");
            this._logger.LogTrace("log trace");

            this._logger.LogWarning("log warning");

            this._logger.LogError("log error");

            try
            {
                throw new Exception($"Handled Exception: {Guid.NewGuid()}");
            }
            catch (Exception handledException)
            {
                // Use the ToExceptionless extension method to submit this handled exception to
                // Exceptionless using the client instance from DI.
                handledException.ToExceptionless(this._exceptionlessClient).Submit();
            }

            try
            {
                throw new Exception($"Handled Exception (Default Client): {Guid.NewGuid()}");
            }
            catch (Exception handledException)
            {
                // Use the ToExceptionless extension method to submit this handled exception to
                // Exceptionless using the default client instance (ExceptionlessClient.Default).
                // This works and is convenient, but its generally not recommended to use static
                // singleton instances because it makes testing and other things harder.
                handledException.ToExceptionless().Submit();
            }

            // Unhandled exceptions will get reported since called UseExceptionless in the
            // Startup.cs which registers a listener for unhandled exceptions.
            throw new Exception($"Unhandled Exception: {Guid.NewGuid()}");
        }
    }
}