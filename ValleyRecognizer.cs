using System.ComponentModel;

namespace C__Stock_Project
{
    /// <summary>
    /// Represents a pattern recognizer for the "Valley" pattern.
    /// </summary>
    public class ValleyRecognizer : PatternRecognizer
    {
        /// <summary>
        /// Gets the name of the recognized pattern.
        /// </summary>
        public override string PatternName => "Valley";

        /// <summary>
        /// Determines whether the Valley pattern is present at a specific position in the candlestick data.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to analyze.</param>
        /// <param name="position">The position to check for the pattern.</param>
        /// <returns>True if the Valley pattern is present; otherwise, false.</returns>
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            if (position == 0 || position >= candlesticks.Count - 1) return false;

            var leftCandle = candlesticks[position - 1];
            var middleCandle = candlesticks[position];
            var rightCandle = candlesticks[position + 1];

            return middleCandle.Low < leftCandle.Low && middleCandle.Low < rightCandle.Low;
        }

        /// <summary>
        /// Gets the range covering the three candlesticks involved in the Valley pattern.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to analyze.</param>
        /// <param name="position">The position of the middle candlestick in the pattern.</param>
        /// <returns>The range covering the three candlesticks involved in the pattern.</returns>
        public override (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Return the range covering the three candlesticks involved in the pattern
            return (position - 1, position + 1);
        }
    }
}

