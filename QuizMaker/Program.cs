using System;

namespace QuizMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

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
                        // Here the quiz will be loaded and played
                        Console.WriteLine("Play Quiz – not implemented yet!");
                        break;
                    case "2":
                        QuizManager.CreateQuiz();
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
    }
}