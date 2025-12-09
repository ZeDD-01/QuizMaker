using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizMaker
{
    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng) =>
            source.OrderBy(_ => rng.Next());
    }
}