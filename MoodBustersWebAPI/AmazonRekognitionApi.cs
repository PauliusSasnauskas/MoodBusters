using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;
using MoodBustersLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace MoodBustersWebAPI
{
    class AmazonRekognitionApi : IRecognitionApi
    {
        private AmazonRekognitionClient apiClient;
        //TODO: fix errorhandler
		//private IErrorHandler errorHandler = MBWindow.errorHandler;
        public AmazonRekognitionApi()
        {
            try
            {
                var credentials = new BasicAWSCredentials(
                    WebConfigurationManager.AppSettings["Key0"],
                    WebConfigurationManager.AppSettings["Key1"]
                );
                apiClient = new AmazonRekognitionClient(credentials, RegionEndpoint.EUCentral1);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Whoops!");
                System.Diagnostics.Debug.WriteLine(e.Message);
                //TODO: fix errorhandler
                //errorHandler.HandleAndExit(StringConst.ErrLicenceNotFound, StringConst.ErrLicense);
            }
        }

        /// <summary>
        /// Returns a list of all moods discovered in MemoryStream format image.
        /// </summary>
        /// <param name="memStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Mood>> GetMoodsAsync(MemoryStream memStr)
        {
            Image image = new Image();
            image.Bytes = memStr;

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = image,
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                //DetectFacesResponse detectFacesResponse = apiClient.DetectFaces(detectFacesRequest);
                DetectFacesResponse detectFacesResponse = await apiClient.DetectFacesAsync(detectFacesRequest);

                // List<Mood> es = detectFacesResponse.FaceDetails.Select((face) =>
                //     NormalizeEmotion(face.Emotions.OrderByDescending(em => em.Confidence).First(), face.BoundingBox)
                // ).ToList();

                List<Mood> moods = (from faces in detectFacesResponse.FaceDetails
                                    from emotions in faces.Emotions
                                    orderby emotions.Confidence descending
                                    group emotions by faces into facemotions
                                    select NormalizeEmotion(facemotions.First(), facemotions.Key.BoundingBox)).ToList();

                return moods;
            }
            catch (Exception e)
            {
                //TODO: fix errorhandler
                //errorHandler.ShowError(e.Message);
            }
            return null;
        }

        private Mood NormalizeEmotion(Emotion emotion, BoundingBox box)
        {
            Mood mood = new Mood();
            mood.Confidence = emotion.Confidence;
            mood.Top = box.Top;
            mood.Left = box.Left;
            mood.Width = box.Width;
            mood.Height = box.Height;

            switch (emotion.Type.ToString())
            {
                case "ANGRY":       mood.Name = MoodName.Angry; break;
                case "CALM":        mood.Name = MoodName.Calm; break;
                case "CONFUSED":    mood.Name = MoodName.Confused; break;
                case "DISGUSTED":   mood.Name = MoodName.Disgusted; break;
                case "FEAR":        mood.Name = MoodName.Fear; break;
                case "HAPPY":       mood.Name = MoodName.Happy; break;
                case "SAD":         mood.Name = MoodName.Sad; break;
                case "SURPRISED":   mood.Name = MoodName.Surprised; break;
                default:            mood.Name = MoodName.Unknown; break;
            }
            return mood;
        }
    }

}
