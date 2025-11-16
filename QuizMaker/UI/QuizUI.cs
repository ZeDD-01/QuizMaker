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

            Quiz quiz = new() { Title = title };

            while (true)
            {
                Console.WriteLine("\nEnter your question (or leave blank to finish):");
                string questionText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(questionText)) break;

                Question q = new() { Text = questionText };
                int answerCount = Program.ReadInt("How many answers? ", 2, 10);

                for (int i = 0; i < answerCount; i++)
                {
                    Console.WriteLine($"Enter answer {i + 1}:");
                    string answerText = Console.ReadLine();

                    Console.Write("Is this answer correct? (y/n): ");
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

            int choice = Program.ReadInt("Enter quiz number: ", 1, quizzes.Count);
            return quizzes[choice - 1];
        }

        public static void PlayQuizUI(Quiz quiz)
        {
            Console.WriteLine($"\nStarting quiz: {quiz.Title}\n");

            int score = 0;
            var rng = new Random();

            foreach (var question in quiz.Questions.Shuffle())
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

            Console.WriteLine($"You scored {score} out of {quiz.Questions.Count}!");
        }
    }
}
