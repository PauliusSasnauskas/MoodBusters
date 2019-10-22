using MoodBustersLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mood_Busters
{
    class WebRequestRecognitionApi : IRecognitionApi
    {
        public string BaseUrl { get; set; }
        public WebRequestRecognitionApi(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<IEnumerable<Mood>> GetMoodsAsync(MemoryStream memStr)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "mood");
            msg.Content = new ByteArrayContent(memStr.ToArray());
            var response = client.SendAsync(msg);
            if (response == null)
            {
                Console.WriteLine("Error?");
                return null;
            }
            var responseStream = response.Result.Content as StreamContent;
            return new List<Mood>(await response.Result.Content.ReadAsAsync<Mood[]>());
        }
    }
}
