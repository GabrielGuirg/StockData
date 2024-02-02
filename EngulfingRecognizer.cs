using System.ComponentModel;

namespace C__Stock_Project
{
    public class EngulfingRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Engulfing";

        // Checks if the Engulfing pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position == 0 || position >= candlesticks.Count - 1) return false;

            // Retrieve the current and previous candlesticks
            var currentCandle = candlesticks[position];
            var previousCandle = candlesticks[position - 1];

            // Check if the conditions for the Engulfing pattern are met
            return currentCandle.Open < previousCandle.Close && currentCandle.Close > previousCandle.Open;
        }

        // Gets the range covering the three candlesticks involved in the pattern
        public override (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Return the range covering the three candlesticks involved in the pattern
            return (position - 1, position);
        }
    }
}

