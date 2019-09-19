using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mood_Busters
{
    class AmazonRekognitionApi : IRecognitionApi
    {
        private AmazonRekognitionClient apiClient;
        public AmazonRekognitionApi()
        {
            var credentials = new BasicAWSCredentials("AKIA4ZMSQDX2XMY6EG64", "qFx64vQ3tWhHZfFFbm7ybhR3JzYN6DT6o9A889OZ");
            // TODO: išimti viešai matomą raktą, kad negalėtų kolegos naudotis mūsų pinigais
            apiClient = new AmazonRekognitionClient(credentials, RegionEndpoint.EUCentral1);
        }


        public Mood GetMood(string imageLocation)
        {
            Image image = new Image();
            try
            {
                using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file " + imageLocation);
                return new Mood { Name = MoodName.Error, Confidence = 0 };
            }

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
                foreach (FaceDetail face in detectFacesResponse.FaceDetails)
                {
                    foreach (Emotion emotion in face.Emotions)
                    {
                        if (emotion.Confidence > highestConfidence)
                        {
                            highestConfidence = emotion.Confidence;
                            highestConfidenceEmotion = emotion.Type;
                        }
                    }
                }

                return new Mood { Name = NormalizeEmotion(highestConfidenceEmotion), Confidence = highestConfidence };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
