using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Casshan.Logging;

namespace Casshan
{
    internal sealed class ExpectedFailureResponseHandler : DelegatingHandler
    {
        private readonly ILog m_Log;

        public ExpectedFailureResponseHandler(HttpMessageHandler innerHandler, ILog log)
            : base(innerHandler)
        {
            m_Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            while (response == null)
            {
                try
                {
                    response = await base.SendAsync(request, new CancellationToken(false));
                }
                catch (Exception e)
                {
                    m_Log.Log("Exception sending request: " + e, LogLevel.Error);
                    m_Log.Log("Waiting 10 seconds to try again", LogLevel.Error);
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            // HttpStatusCode enum doesn't contain a "TooManyRequests",
            // so casting to int is probably fine here.
            while (m_ExpectedFailureStatusCodes.Contains((int) response.StatusCode))
            {
                if ((int)response.StatusCode == 429)
                {
                    if (response.Headers?.RetryAfter?.Delta == null)
                    {
                        m_Log.Log("Got rate limit response, but Riot didn't tell us how long to wait.", LogLevel.Error);
                        m_Log.Log("Waiting for 1 minute and trying again...", LogLevel.Error);
                        Thread.Sleep(TimeSpan.FromMinutes(1));
                    }
                    else
                    {
                        m_Log.Log($"Hit rate limit, backing off for {response.Headers.RetryAfter.Delta.Value}", LogLevel.Warning);
                        Thread.Sleep(response.Headers.RetryAfter.Delta.Value);
                    }

                    return await SendAsync(request, new CancellationToken(false));
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.ServiceUnavailable:
                        // Sometimes this happens regardless of the status of the service
                        // In the future it would make sense to implement exponential backoff
                        m_Log.Log("Riot returned 503, waiting 5 seconds and trying again.", LogLevel.Warning);
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        break;

                    case HttpStatusCode.GatewayTimeout:
                        m_Log.Log("Gateway timeout, waiting 5 seconds and trying again", LogLevel.Warning);
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        break;

                    case HttpStatusCode.InternalServerError:
                        m_Log.Log("Riot returned server error, waiting 5 seconds and trying again", LogLevel.Warning);
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        break;
                }

                return await SendAsync(request, new CancellationToken(false));
            }

            return response;
        }

        private readonly int[] m_ExpectedFailureStatusCodes = {503, 429, 504, 500};
    }
}