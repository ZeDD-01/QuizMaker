using System;
using System.IO;
using System.Xml.Serialization;

namespace QuizMaker
{
    public static class QuizManager
    {
        private static readonly string QuizFolder = Path.Combine(AppContext.BaseDirectory, "quizzes");

        public static Quiz CreateEmptyQuiz(string title)
        {
            Directory.CreateDirectory(QuizFolder);
            return new Quiz { Title = title };
        }

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

       
    }
}