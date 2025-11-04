using System;

namespace QuizMaker
{
    [Serializable]
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}