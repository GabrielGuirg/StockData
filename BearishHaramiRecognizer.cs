using System.ComponentModel;

namespace C__Stock_Project
{
    // Represents a Bearish Harami pattern recognizer
    public class BearishHaramiRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Bearish Harami";

        // Checks if the Bearish Harami pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position == 0 || position >= candlesticks.Count - 1) return false;

            // Retrieve the current and previous candlesticks
            var currentCandle = candlesticks[position];
            var previousCandle = candlesticks[position - 1];

            // Check if the previous candle is bullish and the current candle is bearish
            bool previousCandleIsBullish = previousCandle.Close > previousCandle.Open;
            bool currentCandleIsBearish = currentCandle.Close < currentCandle.Open;

            // Check if the conditions for the Bearish Harami pattern are met
            return previousCandleIsBullish && currentCandleIsBearish &&
                   currentCandle.Open < previousCandle.Close && currentCandle.Close > previousCandle.Open;
        }

        // Gets the range covering the three candlesticks involved in the pattern
        public override (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Return the range covering the three candlesticks involved in the pattern
            return (position - 1, position);
        }
    }
}


