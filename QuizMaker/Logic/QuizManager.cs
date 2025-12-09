using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizMaker
{
    public static class QuizManager
    {
        private static readonly string QuizFolder = Path.Combine(AppContext.BaseDirectory, "quizzes");

        // Wiederverwendbarer Serializer
        private static readonly XmlSerializer Serializer = new(typeof(Quiz));

        public static void SaveQuiz(Quiz quiz)
        {
            Directory.CreateDirectory(QuizFolder);

            string safeTitle = string.Join("_", quiz.Title.Split(Path.GetInvalidFileNameChars()));
            string filePath = Path.Combine(QuizFolder, $"{safeTitle}.xml");

            using FileStream fs = new(filePath, FileMode.Create);
            Serializer.Serialize(fs, quiz);
        }

        public static Quiz LoadQuiz(string filePath)
        {
            using FileStream fs = new(filePath, FileMode.Open);
            return (Quiz)Serializer.Deserialize(fs);
        }

        public static List<string> GetAvailableQuizzes()
        {
            Directory.CreateDirectory(QuizFolder);
            return new List<string>(Directory.GetFiles(QuizFolder, "*.xml"));
        }
    }
}