using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizMaker
{
    public static class QuizUI
    {
        public static Quiz CreateQuizUI()
        {
            Console.WriteLine("Enter a title for your quiz:");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) return null;

            Quiz quiz = new Quiz { Title = title };

            while (true)
            {
                Console.WriteLine("\nEnter your question (or leave blank to finish):");
                string questionText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(questionText)) break;

                Question q = new Question { Text = questionText };

                Console.WriteLine("How many answers do you want to add?");
                if (!int.TryParse(Console.ReadLine(), out int answerCount)) continue;

                for (int i = 0; i < answerCount; i++)
                {
                    Console.WriteLine($"Enter answer {i + 1}:");
                    string answerText = Console.ReadLine();

                    Console.WriteLine("Is this answer correct? (y/n)");
                    bool isCorrect = Console.ReadLine()?.Trim().ToLower() == "y";

                    q.Answers.Add(new Answer { Text = answerText, IsCorrect = isCorrect });
                }

                quiz.Questions.Add(q);
            }

            return quiz;
        }

        public static string SelectQuizUI(List<string> quizzes)
        {
            Console.WriteLine("\nAvailable quizzes:");
            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(quizzes[i])}");
            }

            Console.WriteLine("Enter the number of the quiz you want to play:");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > quizzes.Count)
            {
                Console.WriteLine("Invalid choice.");
                return null;
            }

            return quizzes[choice - 1];
        }

        public static void PlayQuizUI(Quiz quiz)
        {
            Console.WriteLine($"\nStarting quiz: {quiz.Title}\n");

            int score = 0;
            int total = quiz.Questions.Count;
            Random rng = new Random();

            foreach (var question in quiz.Questions.OrderBy(q => rng.Next()))
            {
                Console.WriteLine(question.Text);

                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i].Text}");
                }

                Console.Write("Your answer(s): ");
                string input = Console.ReadLine();
                var answers = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

                bool correct = true;
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    bool shouldBeTrue = question.Answers[i].IsCorrect;
                    bool userChose = answers.Contains((i + 1).ToString());
                    if (shouldBeTrue != userChose)
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    Console.WriteLine("✅ Correct!\n");
                    score++;
                }
                else
                {
                    Console.WriteLine("❌ Wrong!\n");
                }
            }

            Console.WriteLine($"You scored {score} out of {total}!");
        }
    }
}
