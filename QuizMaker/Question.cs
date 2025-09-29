namespace QuizMaker;

public class Question
{
    public string Text;
    public List<Answer> Answers;

    public Question(string text)
    {
        Text = text;
        Answers = new List<Answer>();
    }
}