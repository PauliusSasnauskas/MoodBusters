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

        public Mood GetMood(MemoryStream memStr)
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
                EmotionName highestConfidenceEmotion = EmotionName.UNKNOWN;
                float highestConfidence = 0f;
                DetectFacesResponse detectFacesResponse = apiClient.DetectFaces(detectFacesRequest);

                foreach (Emotion emotion in 
                    from FaceDetail face in detectFacesResponse.FaceDetails
                    from Emotion emotion in face.Emotions
                    where emotion.Confidence > highestConfidence
                    select emotion
                        )
                {
                    highestConfidence = emotion.Confidence;
                    highestConfidenceEmotion = emotion.Type;
                }

                return new Mood { Name = NormalizeEmotion(highestConfidenceEmotion), Confidence = highestConfidence };
            }
            catch (Exception e)
            {
                apiErrorHandler.ShowError(e.Message);
            }
            return new Mood { Name = MoodName.Unknown, Confidence = 0 };
        }

        private MoodName NormalizeEmotion(EmotionName emotion)
        {
            switch (emotion)
            {
                case "ANGRY":       return MoodName.Angry;
                case "CALM":        return MoodName.Calm;
                case "CONFUSED":    return MoodName.Confused;
                case "DISGUSTED":   return MoodName.Disgusted;
                case "FEAR":        return MoodName.Fear;
                case "HAPPY":       return MoodName.Happy;
                case "SAD":         return MoodName.Sad;
                case "SURPRISED":   return MoodName.Surprised;
                default:            return MoodName.Unknown;
            }
        }
    }

}
