using System.ComponentModel;

namespace C__Stock_Project
{
    public class PeakRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Peak";

        // Checks if the Peak pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position == 0 || position >= candlesticks.Count - 1) return false;

            // Retrieve the left, middle, and right candlesticks
            var leftCandle = candlesticks[position - 1];
            var middleCandle = candlesticks[position];
            var rightCandle = candlesticks[position + 1];

            // Determine if the conditions for the Peak pattern are met
            return middleCandle.High > leftCandle.High && middleCandle.High > rightCandle.High;
        }

        // Gets the range covering the three candlesticks involved in the pattern
        public override (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Return the range covering the three candlesticks involved in the pattern
            return (position - 1, position + 1);
        }
    }
}
