using System;
using System.Collections.Generic;
using System.IO;

namespace QuizMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Quiz Maker!");
                Console.WriteLine("1 - Play an existing quiz");
                Console.WriteLine("2 - Create a new quiz");
                Console.WriteLine("3 - Exit");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayQuizFlow();
                        break;
                    case "2":
                        CreateQuizFlow();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            }
        }

        private static void PlayQuizFlow()
        {
            var quizzes = QuizManager.GetAvailableQuizzes();
            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes found. Please create one first.");
                return;
            }

            string selectedQuizPath = QuizUI.SelectQuizUI(quizzes);
            if (selectedQuizPath == null) return;

            var quiz = QuizManager.LoadQuiz(selectedQuizPath);
            QuizUI.PlayQuizUI(quiz);
        }

        private static void CreateQuizFlow()
        {
            var quiz = QuizUI.CreateQuizUI();
            if (quiz == null)
            {
                Console.WriteLine("Quiz creation cancelled.");
                return;
            }

            QuizManager.SaveQuiz(quiz);
            Console.WriteLine($"Quiz '{quiz.Title}' saved successfully!");
        }
    }
}