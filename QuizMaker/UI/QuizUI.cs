using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizMaker
{
    public static class QuizUI
    {
        // Single Random-Instance
        private static readonly Random rng = new();

        public static Quiz CreateQuizUI()
        {
            Console.WriteLine("Enter a title for your quiz:");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) return null;

            Quiz quiz = new() { Title = title };

            while (true)
            {
                Console.WriteLine("\nEnter your question (or leave blank to finish):");
                string questionText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(questionText)) break;

                Question q = new() { Text = questionText };

                int answerCount = ReadInt("How many answers? ", 2, 10);

                // outsources method
                CreateAnswers(q, answerCount);

                quiz.Questions.Add(q);
            }

            return quiz;
        }

        private static void CreateAnswers(Question q, int answerCount)
        {
            for (int i = 0; i < answerCount; i++)
            {
                Console.WriteLine($"Enter answer {i + 1}:");
                string answerText = Console.ReadLine();

                Console.Write("Is this answer correct? (y/n): ");
                bool isCorrect = Console.ReadLine()?.Trim().ToLower() == "y";

                q.Answers.Add(new Answer { Text = answerText, IsCorrect = isCorrect });
            }
        }

        public static string SelectQuizUI(List<string> quizzes)
        {
            Console.WriteLine("\nAvailable quizzes:");
            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(quizzes[i])}");
            }

            int choice = ReadInt("Enter quiz number: ", 1, quizzes.Count);
            return quizzes[choice - 1];
        }

        public static void PlayQuizUI(Quiz quiz)
        {
            Console.WriteLine($"\nStarting quiz: {quiz.Title}\n");

            int score = 0;

            foreach (var question in quiz.Questions.Shuffle(rng))
            {
                Console.WriteLine(question.Text);

                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i].Text}");
                }

                Console.Write("Your answer(s) (comma-separated): ");
                string input = Console.ReadLine() ?? "";
                var answers = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(a => a.Trim()).ToList();

                bool correct = question.Answers
                    .Select((a, i) => (a.IsCorrect, chosen: answers.Contains((i + 1).ToString())))
                    .All(x => x.IsCorrect == x.chosen);

                Console.WriteLine(correct ? "✅ Correct!\n" : "❌ Wrong!\n");

                if (correct) score++;
            }

            Console.WriteLine($"You scored {score} out of {quiz.Questions.Count}!");
        }

        // Removed from program because UI
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) &&
                    value >= min && value <= max)
                    return value;

                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }
    }
}
