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

        public async Task<List<Mood>> GetMoodsAsync(MemoryStream memStr)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "mood");
            msg.Content = new ByteArrayContent(memStr.ToArray());
            var result = client.SendAsync(msg).Result;
            Console.WriteLine(result.ToString());
            return new List<Mood>(await result.Content.ReadAsAsync<Mood[]>());
        }
    }
}
