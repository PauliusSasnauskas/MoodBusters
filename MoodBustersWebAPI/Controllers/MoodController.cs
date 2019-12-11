using MoodBustersLibrary;
using MoodBustersWebAPI.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace MoodBustersWebAPI.Controllers
{
    public class MoodController : ApiController
    {
        private Lazy<IRecognitionApi> recognitionApi = new Lazy<IRecognitionApi>(() => new AmazonRekognitionApi());
        private readonly UserService userService = new UserService();
        private readonly LogRecordService logRecordService = new LogRecordService();

        [HttpPost]
        public async Task<IEnumerable<Mood>> GetMood(HttpRequestMessage message)
        {
            byte[] imageBytes = await message.Content.ReadAsByteArrayAsync();
            User u = userService.GetUserByIP(HttpContext.Current.Request.UserHostAddress);
            logRecordService.Add(new LogRecord { UserId = u.Id, ByteCount = (ulong)imageBytes.Length });
            return await recognitionApi.Value.GetMoodsAsync(new MemoryStream(imageBytes));
        }

        [HttpGet]
        public HttpResponseMessage GetHello()
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            DataSetTest test = new DataSetTest();
            message.Content = new StringContent(test.PrintTables());
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return message;
        }
    }
}