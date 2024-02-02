using System.ComponentModel;

namespace C__Stock_Project
{
    // Represents a Bullish pattern recognizer
    internal class BullishRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Bullish";

        // Checks if the Bullish pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position < 0 || position >= candlesticks.Count) return false;

            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Check if the candlestick exhibits a bullish pattern
            return candle.Close > candle.Open;
        }
    }
}

