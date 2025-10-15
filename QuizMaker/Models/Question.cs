using System;
using System.Collections.Generic;

namespace QuizMaker
{
    [Serializable]
    public class Question
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}