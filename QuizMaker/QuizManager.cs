using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizMaker
{
    public static class QuizManager
    {
        private static string quizFolder = @"C:\tmp\quizzes\"; // Store location for quizzes

        public static void CreateQuiz()
        {
            Directory.CreateDirectory(quizFolder);

            Console.WriteLine("Enter a title for your quiz:");
            string title = Console.ReadLine();

            Quiz quiz = new Quiz { Title = title };

            while (true)
            {
                Question q = new Question();
                Console.WriteLine("\nEnter your question (or leave blank to finish):");
                string questionText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(questionText)) break;

                q.Text = questionText;

                Console.WriteLine("How many answers do you want to add?");
                int answerCount = int.Parse(Console.ReadLine());

                for (int i = 0; i < answerCount; i++)
                {
                    Console.WriteLine($"Enter answer {i + 1}:");
                    string answerText = Console.ReadLine();

                    Console.WriteLine("Is this answer correct? (y/n)");
                    bool isCorrect = Console.ReadLine()?.ToLower() == "y";

                    q.Answers.Add(new Answer { Text = answerText, IsCorrect = isCorrect });
                }

                quiz.Questions.Add(q);
            }

            SaveQuiz(quiz);
            Console.WriteLine($"Quiz '{quiz.Title}' saved successfully!");
        }

        private static void SaveQuiz(Quiz quiz)
        {
            string filePath = Path.Combine(quizFolder, $"{quiz.Title}.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(Quiz));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, quiz);
            }
        }
    }
}
