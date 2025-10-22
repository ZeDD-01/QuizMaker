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
                        PlayQuizUI();
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
        
        private static void PlayQuizUI()
    {
        var quizzes = QuizManager.GetAvailableQuizzes();
        if (quizzes.Count == 0)
        {
            Console.WriteLine("No quizzes found. Please create one first.");
            return;
        }

        Console.WriteLine("\nAvailable quizzes:");
        for (int i = 0; i < quizzes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(quizzes[i])}");
        }

        Console.WriteLine("Enter the number of the quiz you want to play:");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > quizzes.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        var selectedQuiz = QuizManager.LoadQuiz(quizzes[choice - 1]);
        Console.WriteLine($"\nStarting quiz: {selectedQuiz.Title}\n");

        int score = 0;
        int total = selectedQuiz.Questions.Count;

        Random rng = new Random();
        foreach (var question in selectedQuiz.Questions.OrderBy(q => rng.Next()))
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
