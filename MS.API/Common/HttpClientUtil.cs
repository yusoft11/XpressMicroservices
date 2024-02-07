using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.API.Common
{
    public interface IHttpClientUtil
    {
        Task<T> GetJSON<T>(string path, object? queryParams = null, object? headers = null, object? cookies = null);
        Task<T> PostJSONAsync<T>(string path, object? payload = null, object? headers = null, object? cookies = null);
    }
    public class HttpClientUtil : IHttpClientUtil
    {
        private readonly int REST_REQUEST_TIMEOUT = 600000;
        private readonly ILogger<HttpClientUtil> _logger;
        public HttpClientUtil(ILogger<HttpClientUtil> logger) => _logger = logger;
        public async Task<T> GetJSON<T>(string path, object? queryParams = null, object? headers = null, object? cookies = null)
        {
            try
            {

                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.BaseAddress = new Uri(path);
                var flurlClient = new FlurlClient(httpClient);
                return await flurlClient.Request()
                .SetQueryParams(queryParams ?? new { })
                .WithCookies(cookies ?? new { })
                .WithTimeout(REST_REQUEST_TIMEOUT)
                .WithHeaders(headers ?? new { })
                .GetAsync().ReceiveJson<T>();

            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, ex.Source);
                if (ex.Call.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw ex;
                }
                var s = ex.Call.HttpResponseMessage.Content.ReadAsStringAsync();
                var response = await ex.GetResponseJsonAsync<T>();
                return response;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, ex.Source);
                return default;
            }
        }
        public async Task<T> PostJSONAsync<T>(string path, object? payload = null,
                      object? headers = null, object? cookies = null)
        {
            try
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain,
                errors) => true;
                HttpClient httpClient = new HttpClient(httpClientHandler);
                httpClient.BaseAddress = new Uri(path);
                var flurlClient = new FlurlClient(httpClient);
                return await flurlClient.Request()
                .WithCookies(cookies ?? new { })
                .WithTimeout(REST_REQUEST_TIMEOUT)
                .WithHeaders(headers ?? new { })
                .PostJsonAsync(payload ?? new object()).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, ex.Source);
                if (ex.Call.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw ex;
                }

                var response = await ex.GetResponseJsonAsync<T>();
                return response;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, ex.Source);
                return default;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, ex.Source);
                return default;
            }
        }
    }
}
