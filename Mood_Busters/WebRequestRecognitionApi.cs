﻿using MoodBustersLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AndroMooda3
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
            try
            {
               var response = client.SendAsync(msg);
               var responseStream = response.Result.Content as StreamContent;
               return new List<Mood>(await response.Result.Content.ReadAsAsync<Mood[]>());
            }
            catch
            {
                MBWindow.errorHandler.ShowError(StringConst.ErrNull);
                return null;
            }
            
        }
    }
}
