using MoodBustersLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoodBustersWebAPI.Controllers
{
    public class MoodController : ApiController
    {
        private Lazy<IRecognitionApi> recognitionApi = new Lazy<IRecognitionApi>(() => new AmazonRekognitionApi());

        [HttpPost]
        public async Task<IEnumerable<Mood>> GetMood(HttpRequestMessage message)
        {
            byte[] imageBytes = await message.Content.ReadAsByteArrayAsync();
            return await recognitionApi.Value.GetMoodsAsync(new MemoryStream(imageBytes));
        }

        [HttpGet]
        public IHttpActionResult GetHello()
        {
            DataSetTest test = new DataSetTest();
            return Ok(test.PrintTables());
        }
    }
}