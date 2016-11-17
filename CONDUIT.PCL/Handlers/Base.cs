using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CONDUIT.PCL.Handlers
{
    public class Base
    {
        private readonly string _baseUrl;
        public string ErrorString;

        public Base(string baseURL = "http://localhost:57871/api/")
        {
            _baseUrl = baseURL;
        }

        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler();

            var client = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(20) };

            return client;
        }

        private string parseString(string urlArguments)
        {
            return urlArguments.Replace("=&", "=null&");
        }

        public T GetSync<T>(string urlArguments)
        {
            var url = String.Format(_baseUrl + "{0}", parseString(urlArguments));

            try
            {
                var client = GetHttpClient();

                var str = client.GetStringAsync(url).GetAwaiter().GetResult();

                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                ErrorString = url + "|" + ex.ToString();

                return default(T);
            }
        }
        public async Task<T> Get<T>(string urlArguments)
        {
            try
            {
                var client = GetHttpClient();

                var str = await client.GetStringAsync(String.Format(_baseUrl + "{0}", urlArguments));

                return JsonConvert.DeserializeObject<T>(str);

            }
            catch (Exception ex)
            {
                string ext = ex.ToString();

                return default(T);
            }
        }

        //public async Task<T> Post<T>(string urlArguments, MultipartFormDataContent formData)
        //{
        //    var client = GetHttpClient();

        //    var address = new Uri(String.Format(_baseUrl + "{0}", urlArguments));
        //    var response = await client.PostAsync(address, formData);

        //    var data = await response.Content.ReadAsStringAsync();

        //    return JsonConvert.DeserializeObject<T>(data);
        //}

        public async Task<K> Post<T, K>(string urlArguments, T obj)
        {
            var client = GetHttpClient();

            var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(String.Format(_baseUrl + "{0}", urlArguments), content);

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<K>(data);
        }

        //public async Task<K> Send<T, K>(string urlArguments, T obj) {
        //    var client = GetHttpClient();

        //    var address = new Uri(String.Format(_baseUrl + "{0}", urlArguments));
        //    StringContent content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        //    var response = await client.PostAsync(address, content);

        //    var data = await response.Content.ReadAsStringAsync();

        //    return JsonConvert.DeserializeObject<K>(data);
        //}

        //public async Task<K> SendAsync<T, K>(string urlArguments, T obj) {
        //    var client = GetHttpClient();

        //    var address = new Uri(String.Format(_baseUrl + "{0}", urlArguments));
        //    StringContent content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        //    var response = await client.PostAsync(address, content).ConfigureAwait(false);

        //    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //    return JsonConvert.DeserializeObject<K>(data);
        //}



        //public async Task<K> Put<T, K>(string urlArguments, T obj)
        //{
        //    var client = GetHttpClient();

        //    var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        //    var response = await client.PutAsync(String.Format(_baseUrl + "{0}", urlArguments), content);

        //    var data = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<K>(data);
        //}

        //public K SendSync<T, K>(string urlArguments, T obj)
        //{
        //    var data = String.Empty;

        //    try {
        //        var client = GetHttpClient();

        //        var address = new Uri(String.Format(_baseUrl + "{0}", urlArguments));
        //        var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        //        var response = client.PostAsync(address, content).GetAwaiter().GetResult();

        //        data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //        return JsonConvert.DeserializeObject<K>(data);
        //    } catch (Exception ex) {
        //        ErrorString = ex.ToString() + "|" + data;

        //        return default(K);
        //    }
        //}


    }
}
