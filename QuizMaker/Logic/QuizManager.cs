using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizMaker
{
    public static class QuizManager
    {
        private static readonly string QuizFolder = Path.Combine(AppContext.BaseDirectory, "quizzes");

        public static void SaveQuiz(Quiz quiz)
        {
            Directory.CreateDirectory(QuizFolder);
            string safeTitle = string.Join("_", quiz.Title.Split(Path.GetInvalidFileNameChars()));
            string filePath = Path.Combine(QuizFolder, $"{safeTitle}.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, quiz);
            }
        }

        public static Quiz LoadQuiz(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (Quiz)serializer.Deserialize(fs);
            }
        }

        public static List<string> GetAvailableQuizzes()
        {
            Directory.CreateDirectory(QuizFolder);
            var files = Directory.GetFiles(QuizFolder, "*.xml");
            return new List<string>(files);
        }
    }
}