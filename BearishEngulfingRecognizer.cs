using System.ComponentModel;

namespace C__Stock_Project
{
    /// <summary>
    /// Recognizes the "Bearish Engulfing" candlestick pattern.
    /// </summary>
    public class BearishEngulfingRecognizer : PatternRecognizer
    {
        /// <summary>
        /// Gets the name of the recognized pattern.
        /// </summary>
        public override string PatternName => "Bearish Engulfing";

        /// <summary>
        /// Determines if the Bearish Engulfing pattern is present at the specified position.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to analyze.</param>
        /// <param name="position">The position in the list to check for the pattern.</param>
        /// <returns>True if the Bearish Engulfing pattern is present; otherwise, false.</returns>
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Check if the position is at the beginning or end of the list
            if (position == 0 || position >= candlesticks.Count - 1)
                return false;

            // Retrieve the current and previous candlesticks
            var currentCandle = candlesticks[position];
            var previousCandle = candlesticks[position - 1];

            // Check if the current candle "engulfs" the previous one in a bearish manner
            return currentCandle.Open > previousCandle.Close && currentCandle.Close < previousCandle.Open;
        }

        /// <summary>
        /// Gets the range covering the three candlesticks involved in the Bearish Engulfing pattern.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to analyze.</param>
        /// <param name="position">The position in the list representing the pattern occurrence.</param>
        /// <returns>A tuple representing the start and end positions of the pattern range.</returns>
        public override (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Return the range covering the three candlesticks involved in the pattern
            return (position - 1, position);
        }
    }
}

