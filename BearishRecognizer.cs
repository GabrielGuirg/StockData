using System.ComponentModel;

namespace C__Stock_Project
{
    /// <summary>
    /// Recognizes the "Bearish" candlestick pattern.
    /// </summary>
    internal class BearishRecognizer : PatternRecognizer
    {
        /// <summary>
        /// Gets the name of the recognized pattern.
        /// </summary>
        public override string PatternName => "Bearish";

        /// <summary>
        /// Determines if the Bearish pattern is present at the specified position.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to analyze.</param>
        /// <param name="position">The position in the list to check for the pattern.</param>
        /// <returns>True if the Bearish pattern is present; otherwise, false.</returns>
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Check if the position is outside the valid range
            if (position < 0 || position >= candlesticks.Count)
                return false;

            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Check if the candlestick exhibits a bearish pattern
            return candle.Open > candle.Close;
        }
    }
}
