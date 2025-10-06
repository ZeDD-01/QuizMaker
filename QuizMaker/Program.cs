// See https://aka.ms/new-console-template for more information

namespace QuizMaker
{
    class Program
    {

        public List<Question> Questions;
        public List<Answer> Answers;
        public List<Quiz> Quizzes;
    
    
    static void Main(string[] args)
    {

        startGame();
    }


    public static void createQuestion()
    {
        
    }
    public static void startGame(){
        do
        {
            Console.WriteLine("Welcome to the Quiz Maker!");
            Console.WriteLine("Please choose if you want to");
            Console.WriteLine("1 - play an existing quiz ");
            Console.WriteLine("2 - create a new quiz ");
            Console.WriteLine("3 - Exit the program ");
            Console.WriteLine("Your choice: ");

            string choice = Console.ReadLine();

            Console.WriteLine($"You chose {choice}");

            if (choice == "1")
            {
                //Choose from Quiz List
                break;
            }
            else if (choice == "2")
            {
                Quiz myQuiz = new Quiz();
                break;
            }
            else if (choice == "3")
            {
                Console.WriteLine("Goodbye");
                return;
            }
            else
            {
                Console.WriteLine("Wrong input. Please try again.");
            }
        }while (true);
    }
}
}