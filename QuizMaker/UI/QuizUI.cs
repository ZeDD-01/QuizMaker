using System;

namespace QuizMaker
{
    public static class QuizUI
    {
        public static void StartGame()
        {
            do
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
                        Console.WriteLine("Play Quiz – not implemented yet!");
                        break;
                    case "2":
                        CreateQuizUI();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            } while (true);
        }

        private static void CreateQuizUI()
        {
            Console.WriteLine("Enter a title for your quiz:");
            string title = Console.ReadLine();

            Quiz quiz = QuizManager.CreateEmptyQuiz(title);

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
                    bool isCorrect = Console.ReadLine()?.ToLower() == "y";

                    q.Answers.Add(new Answer { Text = answerText, IsCorrect = isCorrect });
                }

                quiz.Questions.Add(q);
            }

            QuizManager.SaveQuiz(quiz);
            Console.WriteLine($"Quiz '{quiz.Title}' saved successfully!");
        }
    }
}
