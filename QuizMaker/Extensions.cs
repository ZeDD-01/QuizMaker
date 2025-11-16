using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizMaker
{
    public static class Extensions
    {
        private static readonly Random rng = new();
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) =>
            source.OrderBy(_ => rng.Next());
    }
}