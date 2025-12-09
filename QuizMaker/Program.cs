using System;
using System.Collections.Generic;

namespace QuizMaker
{
    class Program
    {
        private static readonly Dictionary<MenuOption, Action> MenuActions = new()
        {
            { MenuOption.Play, PlayQuizFlow },
            { MenuOption.Create, CreateQuizFlow },
            { MenuOption.Exit, () => Environment.Exit(0) }
        };

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n=== Welcome to the Quiz Maker ===");

                foreach (var option in MenuActions.Keys)
                {
                    Console.WriteLine($"{(int)option}. {option}");
                }

                // Hier geändert:
                int choice = QuizUI.ReadInt("Select an option: ", 1, MenuActions.Count);

                var selected = (MenuOption)choice;
                MenuActions[selected].Invoke();
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