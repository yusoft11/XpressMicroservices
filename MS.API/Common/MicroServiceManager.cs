using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.API.Common
{
    public interface IMicroServiceManager
    {
        Task<AuthTokenResponse> ValidateAuthTokenAsync();
        Task<CategoryAPIItemResponse> GetAllCategories(string token);

    }
    public class MicroServiceManager : IMicroServiceManager
    {
        private string BaseUrlForCat;
        private string BaseUrlForProd;
        private string _userName;
        private string _Password;
        private readonly IConfiguration _config;
        private readonly IHttpClientUtil _clientUtil;
        private readonly ILogger<MicroServiceManager> _logger;
        public MicroServiceManager(IConfiguration config, IHttpClientUtil clientUtil, ILogger<MicroServiceManager> logger)
        {
            _config = config;
            _clientUtil = clientUtil;
            _logger = logger;
            _userName = _config["API_PARAM:USERNAME"];
            _Password = _config["API_PARAM:PASSWORD"];
            BaseUrlForCat = _config["API_PARAM:BASE_URL_CAT"];
            if (!BaseUrlForCat.EndsWith("/"))
                BaseUrlForCat = $"{BaseUrlForCat}/";
            BaseUrlForProd = _config["API_PARAM:BASE_URL_PROD"];
            if (!BaseUrlForProd.EndsWith("/"))
                BaseUrlForProd = $"{BaseUrlForProd}/";

        }

        internal async Task<TResponse> GetResponseAuthTokenAsync<TRequest, TResponse>(TRequest request, string method)
        {
            TResponse response = (TResponse)Activator.CreateInstance(typeof(TResponse));
            try
            {
                _logger.LogInformation($"POST- {method} method called with request");
                string url = $"{BaseUrlForCat}{method}";
                var utcdate = DateTime.UtcNow;
                //var x_token = GetXTokenHeader(utcdate, _clientId, _Password);
                var headers = new List<KeyValuePair<string, string>>();
                //headers.Add(new KeyValuePair<string, string>("x-token", x_token));
                //headers.Add(new KeyValuePair<string, string>("UTCTimestamp", utcdate.ToString("yyyy-MM-ddTHH:mm:ss.fff")));
                //headers.Add(new KeyValuePair<string, string>("Client_ID", _clientId));
                //headers.Add(new KeyValuePair<string, string>("Content-Type", "application/json"));
               // headers.Add(new KeyValuePair<string, string>("Ocp-Apim-Subscription-Key", _subkey));
                response = await _clientUtil.PostJSONAsync<TResponse>(path: url, headers: headers, payload: request);
                _logger.LogInformation($"response returned from {method}: {Newtonsoft.Json.JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, method);
            }
            return response;
        }

        internal async Task<TResponse> DoGetCategoriesAsync<TResponse>(List<KeyValuePair<string, object>> _queryParams,string accesstoken, string method)
        {
            //TResponse response = (TResponse)Activator.CreateInstance(typeof(TResponse));
            TResponse response = default;
            try
            {
                //Log(_queryParams, method);
                _logger.LogInformation($"GET-{method} method called");
                string url = $"{BaseUrlForCat}{method}";
                string _token = accesstoken ;
                var headers = new List<KeyValuePair<string, string>>();
                headers.Add(new KeyValuePair<string, string>("Authorization", _token));
                //headers.Add(new KeyValuePair<string, string>("UTCTimestamp", utcdate.ToString("yyyy-MM-ddTHH:mm:ss.fff")));
                //headers.Add(new KeyValuePair<string, string>("Client_ID", _clientId));
                //headers.Add(new KeyValuePair<string, string>("Ocp-Apim-Subscription-Key", _subkey));
                response = await _clientUtil.GetJSON<TResponse>(path: url, headers: headers, queryParams: _queryParams);
                _logger.LogInformation($"response returned from {method}: {Newtonsoft.Json.JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, method);
            }
            return response;
        }
        public async Task<AuthTokenResponse> ValidateAuthTokenAsync()
        {
            AuthRequest request = new AuthRequest { userName = _userName, password = _Password };
            return await GetResponseAuthTokenAsync<AuthRequest, AuthTokenResponse>(request, "api/authcategory/login");
        }

        public async Task<CategoryAPIItemResponse> GetAllCategories(string token)
        {
            var _queryParams = new List<KeyValuePair<string, object>>();
            return await DoGetCategoriesAsync<CategoryAPIItemResponse>(_queryParams, token,  $"api/categoryRequests");
        }
    }
}
