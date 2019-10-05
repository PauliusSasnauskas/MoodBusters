using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mood_Busters
{
    class AmazonRekognitionApi : IRecognitionApi
    {
        private AmazonRekognitionClient apiClient;
		private IErrorHandler apiErrorHandler;
        public AmazonRekognitionApi()
        {
            var credentials = new BasicAWSCredentials(Key.GetKeys[0], Key.GetKeys[1]);
            apiClient = new AmazonRekognitionClient(credentials, RegionEndpoint.EUCentral1);
			apiErrorHandler = new ErrorHandlerWindows();
        }

        public List<Mood> GetMoods(MemoryStream memStr)
        {
            Console.WriteLine("What");
            Image image = new Image();
            image.Bytes = memStr;

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = image,
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                DetectFacesResponse detectFacesResponse = apiClient.DetectFaces(detectFacesRequest);

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
                apiErrorHandler.ShowError(e.Message);
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
