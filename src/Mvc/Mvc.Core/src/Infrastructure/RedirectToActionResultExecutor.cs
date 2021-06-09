// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Microsoft.AspNetCore.Mvc.Infrastructure
{
    /// <summary>
    /// A <see cref="IActionResultExecutor{RedirectToActionResult}"/> for <see cref="RedirectToActionResult"/>.
    /// </summary>
    public class RedirectToActionResultExecutor : IActionResultExecutor<RedirectToActionResult>
    {
        private readonly ILogger _logger;
        private readonly IUrlHelperFactory _urlHelperFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="PhysicalFileResultExecutor"/>.
        /// </summary>
        /// <param name="loggerFactory">The factory used to create loggers.</param>
        /// <param name="urlHelperFactory">The factory used to create url helpers.</param>
        public RedirectToActionResultExecutor(ILoggerFactory loggerFactory, IUrlHelperFactory urlHelperFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (urlHelperFactory == null)
            {
                throw new ArgumentNullException(nameof(urlHelperFactory));
            }

            _logger = loggerFactory.CreateLogger<RedirectToActionResult>();
            _urlHelperFactory = urlHelperFactory;
        }

        /// <inheritdoc />
        public virtual Task ExecuteAsync(ActionContext context, RedirectToActionResult result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var urlHelper = result.UrlHelper ?? _urlHelperFactory.GetUrlHelper(context);

            var destinationUrl = urlHelper.Action(
                result.ActionName,
                result.ControllerName,
                result.RouteValues,
                protocol: null,
                host: null,
                fragment: result.Fragment);
            if (string.IsNullOrEmpty(destinationUrl))
            {
                throw new InvalidOperationException(Resources.NoRoutesMatched);
            }

            _logger.RedirectToActionResultExecuting(destinationUrl);

            if (result.PreserveMethod)
            {
                context.HttpContext.Response.StatusCode = result.Permanent ?
                    StatusCodes.Status308PermanentRedirect : StatusCodes.Status307TemporaryRedirect;
                context.HttpContext.Response.Headers.Location = destinationUrl;
            }
            else
            {
                context.HttpContext.Response.Redirect(destinationUrl, result.Permanent);
            }

            return Task.CompletedTask;
        }
    }
}