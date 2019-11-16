using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MoodBustersLibrary;
using Newtonsoft.Json;

public class WebRequestRecognitionApi : IRecognitionApi
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

        string responseString = await response.Result.Content.ReadAsStringAsync();



        return JsonParse(responseString);
    }

    private List<Mood> JsonParse(string responseString)
    {
        List<Mood> moods = new List<Mood>();
        //thing
        return moods;
    }
}