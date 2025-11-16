using System;
using System.Collections.Generic;

namespace QuizMaker
{
    [Serializable]
    public class Quiz
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}